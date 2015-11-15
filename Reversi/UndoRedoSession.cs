/*
 * Copyright (C) Henrik Jonsson 2007
 *
 * THIS WORK IS PROVIDED UNDER THE TERMS OF THIS CODE PROJECT OPEN LICENSE ("LICENSE").
 * THE WORK IS PROTECTED BY COPYRIGHT AND/OR OTHER APPLICABLE LAW. ANY USE OF THE WORK
 * OTHER THAN AS AUTHORIZED UNDER THIS LICENSE OR COPYRIGHT LAW IS PROHIBITED.
 *
 * See licence.txt or http://thecodeproject.com/info/eula.aspx for full licence description.
 *
 * This copyright notice must not be removed.
*/

using System;

namespace Reversible
{
    /// <summary>
    /// Represents a session that keeps track of reversible operations making it possible to undo and redo them.
    /// </summary>
    public class UndoRedoSession : Transaction
    {
        private Transaction previous;

        /// <summary>
        /// Creates a new undo redo session without making it active.
        /// </summary>
        public UndoRedoSession()
            : base(null, null)
        {
        }

        /// <summary>
        /// Creates a new undo redo session without making it active.
        /// </summary>
        /// <param name="sessionName">A name to assign to the session</param>
        public UndoRedoSession(string sessionName)
            : base(null, sessionName)
        {
        }

        /// <summary>
        /// Starts a new transaction in this undo-redo-session.
        /// </summary>
        /// <param name="operationName">Name of new Transaction.</param>
        /// <returns>A new Transaction</returns>
        /// <remarks>In first version (1.0.0) this UndoSession remained active transaction when the returned transaction was
        /// commited or rollbacked. This was changed in version (1.0.1) to avoid problems like the one reported by Fraysse.
        /// Now, instead, the Transaction active when calling this function becomes the active one when the new transaction ends.
        /// </remarks>
        public new Transaction Begin(string operationName)
        {
            if (currentChild != null)
            {
                throw new InvalidOperationException("Cannot start a new transaction in and undo-redo session as long as the last started transaction is not finished. " +
                                                    "Ensure that all transactions started by Begin are finished properly using Commit or Rollback. " +
                                                    "It is higly recommended to always use the using statement to ensure that transactions are ended even in case of exceptions.");
            }
            previous = Transaction.Current;
            current = this;

            return Transaction.Begin(operationName);
        }

        /// <summary>
        /// Makes this UndoRedoSession active and initiates a new transaction in it
        /// </summary>
        /// <returns>A new Transaction</returns>
        public new Transaction Begin()
        {
            return this.Begin(null);
        }

        /// <summary>
        /// Called when a child transaction is committed or rollbacked.
        /// </summary>
        /// <param name="childFinished">Child Transaction finished.</param>
        /// <remarks>This is overriden to restore the current transaction to the activated before UndoRedoSession.Begin(...) was called.</remarks>
        protected override void OnChildFinished(Transaction childFinished)
        {
            if (childFinished == currentChild)
            {
                base.OnChildFinished(childFinished);

                current = previous;
                previous = null;
            }
            else
            {
                base.OnChildFinished(childFinished);
            }
        }

        /// <summary>
        /// Add an edit operation to this transaction
        /// </summary>
        /// <param name="edit">Edit to add</param>
        public override void AddEdit(Edit edit)
        {
            base.AddEdit(edit);

            RaiseHistoryChanged();
        }

        /// <summary>
        /// Raised whenever the undo history changes.
        /// </summary>
        public event EventHandler HistoryChanged;

        private void RaiseHistoryChanged()
        {
            EventHandler eh = HistoryChanged;
            if (eh != null)
            {
                eh(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when this transaction is wholy or partly reversed. Raises the Reversed and the HistoryChanged events.
        /// </summary>
        protected override void OnReversed()
        {
            base.OnReversed();

            RaiseHistoryChanged();
        }

        /// <summary>
        /// Reverses the last (not previously undone) operation.
        /// </summary>
        public void Undo()
        {
            Undo(1);
        }

        /// <summary>
        /// Undos a given number of operations
        /// </summary>
        /// <param name="count">Number of operations to undo.</param>
        public void Undo(int count)
        {
            if (currentIndex > 0)
            {
                // Clears the current transaction during undo operation
                Transaction currentTransaction = Current;
                current = null;

                while (count > 0 && currentIndex > 0)
                {
                    currentIndex--;
                    int i = currentIndex;
                    edits[i] = edits[i].Reverse();
                    count--;
                }

                OnReversed();

                // Restores the current transaction.
                current = currentTransaction;
            }
        }

        /// <summary>
        /// Returns true if there is anything to undo.
        /// </summary>
        /// <returns></returns>
        public bool CanUndo()
        {
            return (currentIndex > 0);
        }

        /// <summary>
        /// Redos the last undone operation.
        /// </summary>
        public void Redo()
        {
            Redo(1);
        }

        /// <summary>
        /// Redos a given number of operations.
        /// </summary>
        /// <param name="count">Number of operations to redo.</param>
        public void Redo(int count)
        {
            if (currentIndex < edits.Count)
            {
                // Clears the current transaction during redo operation.
                Transaction currentTransaction = Current;
                current = null;

                while (count > 0 && currentIndex < edits.Count)
                {
                    int i = currentIndex;
                    edits[i] = edits[i].Reverse();

                    currentIndex++;
                    count--;
                }

                OnReversed();

                // Restores the current operation.
                current = currentTransaction;
            }
        }

        /// <summary>
        /// Returns true if there is anything that can be redone.
        /// </summary>
        /// <returns></returns>
        public bool CanRedo()
        {
            return currentIndex < edits.Count;
        }

        /// <summary>
        /// Gets the name of the last performed operation
        /// </summary>
        /// <returns>Name of last operation performed (not undone), or null if no operation performed</returns>
        public string GetUndoText()
        {
            if (currentIndex > 0)
            {
                return this.edits[currentIndex - 1].Name;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the name of the last undone operation
        /// </summary>
        /// <returns>Name of last operation undone), or null if no operation is undone</returns>
        public string GetRedoText()
        {
            if (currentIndex < edits.Count)
            {
                return this.edits[currentIndex].Name;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the names for all operations that can be undone starting with the most recently added.
        /// </summary>
        /// <returns>An array of operation names currently in the undo list.</returns>
        public string[] GetUndoTextList()
        {
            string[] texts = new string[currentIndex];
            for (int i = 0; i < currentIndex; i++)
            {
                texts[i] = edits[currentIndex - i - 1].Name;
            }
            return texts;
        }

        /// <summary>
        /// Gets the names for all operations that can be redone starting with the most recently undone.
        /// </summary>
        /// <returns>An array of operation names currently in the redo list.</returns>
        public string[] GetRedoTextList()
        {
            string[] texts = new string[edits.Count - currentIndex];
            for (int i = 0; i < edits.Count - currentIndex; i++)
            {
                texts[i] = edits[currentIndex + i].Name;
            }
            return texts;
        }
    }
}