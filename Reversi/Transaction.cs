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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reversible
{
    
    
    /// <summary>
    /// Represents a transaction enabling rollbacks, undoing and redoing of operations in an application. 
    /// </summary>
    public class Transaction : Edit, IDisposable
    {

        protected static Transaction current;

        /// <summary>
        /// Gets the current transaction
        /// </summary>
        public static Transaction Current
        {
            get { return current; }
        }

        /// <summary>
        /// Begins a new transaction (nested in current)
        /// </summary>
        /// <returns></returns>
        public static Transaction Begin()
        {
            return Begin(null);
        }

        /// <summary>
        /// Begins a new named transaction (nested in current)
        /// </summary>
        /// <param name="transactionName">Name of transaction/operation</param>
        /// <returns></returns>
        public static Transaction Begin(string transactionName)
        {
            Transaction newTransaction = new Transaction(current, transactionName);
            if (current != null)
            {
                current.currentChild = newTransaction;
            }
            current = newTransaction;
            return newTransaction;
        }

        /// <summary>
        /// The parent transaction that this transaction will be added to when commited. 
        /// </summary>
        private readonly Transaction parent;
        
        /// <summary>
        /// The list of edits added to the transaction
        /// </summary>
        protected readonly List<Edit> edits = new List<Edit>();
        
        /// <summary>
        /// The index in edits list to add next edit to.
        /// </summary>
        protected int currentIndex = 0;

        /// <summary>
        /// The currently active child transaction.
        /// </summary>
        protected Transaction currentChild = null;

        /*
        /// <summary>
        /// A list of post operations to be performed after undoing or redoing.
        /// </summary>
        protected List<PostOperation> postOperations = null;
        */

        /// <summary>
        /// Fired when some or all operations in this transaction are reversed (undone, redone or rollbacked).
        /// </summary>
        public event EventHandler Reversed;

        /// <summary>
        /// Creates a new transaction instance without making it active.
        /// </summary>
        /// <param name="parentTrans">Parent transaction (or null if it is a root transaction)</param>
        /// <param name="transName">Name of transaction</param>
        protected Transaction(Transaction parentTrans, string transName)
        {
            parent = parentTrans;
            this.transName = transName;
        }

        private readonly string transName;

        /// <summary>
        /// Gets the name of the transaction
        /// </summary>
        public override string Name { get { return transName; } }

        /// <summary>
        /// Gets a value indicating whether this transaction has been finished (committed or rollbacked).
        /// </summary>
        public bool IsFinished { get; private set; }

        #region IDisposable Members

        /// <summary>
        /// Disposes this transaction. If it has not been commited it will be rollbacked.
        /// </summary>
        void IDisposable.Dispose()
        {
            if (!IsFinished)
            {
                Rollback();
            }
        }

        #endregion

        /// <summary>
        /// Adds a reversible operation to this transaction
        /// </summary>
        /// <param name="edit">An edit representing the reversible operation to be added.</param>
        /// <exception cref="InvalidOperationException">If this transaction has been finished (committed or rollbacked).</exception>
        public virtual void AddEdit(Edit edit)
        {
            if (IsFinished)
            {
                throw new InvalidOperationException("Cannot add edits to a finished transaction");
            }

            // Remove all from currentIndex, i.e. redone edits 
            edits.RemoveRange(currentIndex, edits.Count - currentIndex); 
            
            edits.Add(edit);
            currentIndex++;
        }

        /// <summary>
        /// Called by child transaction when they are commited or rollback.
        /// </summary>
        /// <param name="childFinished">Child transaction finished.</param>
        /// <remarks>
        /// This default implementation clears the current child reference and sets this parent transaction as the active one
        /// (if and only if childFinished is the current child, i.e. the last started.)
        /// Override to alter this behaviour.
        /// </remarks>
        protected virtual void OnChildFinished(Transaction childFinished)
        {
            if (childFinished == currentChild)
            {
                currentChild = null;
                
                // Makes this (the parent) transaction the active one.
                current = this;
            }
        }


        /// <summary>
        /// Reverses all reversible operations performed during this transaction and ends this transaction (making any parent
        /// transaction current again).
        /// </summary>
        /// <exception cref="InvalidOperationException">If this transaction already has been finished (committed or rollbacked).</exception>
        public void Rollback()
        {
            if (IsFinished)
            {
                throw new InvalidOperationException("Rollback cannot be performed on an finished transaction. Ensure that Rollback is only called once and not after a Commit has been made.");
            }

            IsFinished = true;

            // Clears the current transaction during rollback.
            current = null;

            // Rollback any active child first.
            if (currentChild != null)
            {
                currentChild.Rollback();
            }

            // Reverses all operations in this transaction.
            if (currentIndex > 0)
            {
                Reverse();
            }

            // Clears parents current child.
            if (parent != null)
            {

                parent.OnChildFinished(this);
            }
            else
            {
                
                // Makes none transaction active.
                current = null;
            }
            
        }

        /// <summary>
        /// Mark this transaction as successfully completed. This transaction will be added as a reversible operation in the parent 
        /// transaction (if any) which also will be the new current transaction.
        /// </summary>
        /// <remarks>
        /// Empty transactions (i.e. with no edits) will be discarded directly and not added to parent transaction.
        /// </remarks>
        /// <exception cref="InvalidOperationException">If this transaction already has been finished (committed or rollbacked).</exception>
        public void Commit()
        {
            if (IsFinished)
            {
                throw new InvalidOperationException("Commit cannot be performed on an finished transaction. Ensure that Commit is only called once and not after a Rollback has been made.");
            }

            IsFinished = true;

            // Commit any unfinished child transaction.
            if (currentChild != null)
            {
                currentChild.Commit();
            }

            if (parent != null)
            {

                if (edits.Count > 0)
                {
                    parent.AddEdit(this);
                }

                parent.OnChildFinished(this);
            }
            else
            {
                // Make the no transaction active
                current = null;
            }
        }

        /// <summary>
        /// Makes this transaction the current one (or its current child transaction).
        /// </summary>
        public void Enter()
        {
            Transaction leafTransaction = this;
            while (leafTransaction.currentChild != null)
            {
                leafTransaction = leafTransaction.currentChild;
            }
            current = leafTransaction;
        }

        /// <summary>
        /// Leaves this transaction making none transaction active if this transaction (or one of its children) is currently active.
        /// </summary>
        /// <remarks>
        /// Call Enter() to switch back to the transaction.
        /// </remarks>
        public void Exit()
        {
            Transaction txn = current;
            while (txn != null)
            {
                if (txn == this)
                {
                    current = null;
                    return;
                }
                txn = txn.parent;
            }
        }

        /// <summary>
        /// Reverses all operation in this transaction (either undos or redos all operation depending on state). 
        /// </summary>
        /// <returns>Edit that represents the reverse operation</returns>
        public override Edit Reverse()
        {
            if (currentIndex > 0)
            {
                for (int i = currentIndex - 1; i >= 0; i--)
                {
                   
                    edits[i] = edits[i].Reverse();
                }
                currentIndex = 0;
            }
            else
            {
                for (int i = 0; i < edits.Count; i++)
                {
                    edits[i] = edits[i].Reverse();
                }
                currentIndex = edits.Count;
            }
            OnReversed();
            return this;
        }

        /// <summary>
        /// Called when this transaction is wholy or partly reversed. Raises the Reversed event.
        /// </summary>
        protected virtual void OnReversed()
        {
            EventHandler reversed = Reversed;
            if (reversed != null)
            {
                reversed(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Adds a reversible property change to the current transaction (if any).
        /// </summary>
        /// <typeparam name="TOwner">Type that owns the property</typeparam>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="setter">A method that sets the property</param>
        /// <param name="instance">The instance whose property is changed</param>
        /// <param name="oldValue">The present value of the property</param>
        /// <param name="newValue">The new value to be assigned to the property</param>
        public static void AddPropertyChange<TOwner,TProperty>(ReversiblePropertySetter<TOwner, TProperty> setter, TOwner instance, TProperty oldValue, TProperty newValue) where TOwner : class
        {
            Transaction txn = current;
            if (txn != null)
            {
                txn.AddEdit(new ReversiblePropertyEdit<TOwner, TProperty>(setter, instance, oldValue, newValue));
            }
        }

        /// <summary>
        /// Adds a reversible property change to the current transaction (if any).
        /// </summary>
        /// <typeparam name="TOwner">Type that owns the property</typeparam>
        /// <param name="setter">A instance method (delegate) that sets the property of to a given value</param>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="oldValue">The present value of the property</param>
        /// <param name="newValue">The new value to be assigned to the property</param>
        public static void AddPropertyChange<TProperty>(ReversibleInstancePropertySetter<TProperty> setter, TProperty oldValue, TProperty newValue)
        {
            Transaction txn = current;
            if (txn != null)
            {
                txn.AddEdit(new ReversibleInstancePropertyEdit<TProperty>(setter, oldValue, newValue));
            }
        }
    }
}
