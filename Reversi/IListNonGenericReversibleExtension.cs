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
using System.Linq;
using System.Text;
using Reversible;
using System.Collections;

namespace Reversible.Extensions.NonGeneric
{

    public static class IListNonGenericReversibleExtension
    {
        /// <summary>
        /// Sets a list item reversibly.
        /// </summary>
        /// <param name="list">List to set item in</param>
        /// <param name="index">Index of item to set</param>
        /// <param name="newValue">New value to assign.</param>
        public static void Item_SetReversible(this IList list, int index, object newValue)
        {
            Transaction txn = Transaction.Current;
            if (txn != null)
            {
                object oldValue = list[index];
                list[index] = newValue;
                txn.AddEdit(new IListEdit(ListEditAction.ChangeItem, list, oldValue, index));
            }
            else
            {
                list[index] = newValue;
            }
        }

        /// <summary>
        /// Adds an item to the end of a list (reversibly).
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item">Item to add</param>
        /// <returns>Index of newly added item</returns>
        public static int Add_Reversible(this IList list, object item)
        {
            Insert_Reversible(list, list.Count, item);
            return list.Count-1;
        }

        /// <summary>
        /// Inserts an item reversibly.
        /// </summary>
        /// <param name="list">List to insert in</param>
        /// <param name="index">Index to insert at</param>
        /// <param name="itemToInsert">Item to insert</param>
        public static void Insert_Reversible(this IList list, int index, object itemToInsert)
        {
            Transaction txn = Transaction.Current;
            if (txn != null)
            {
                list.Insert(index, itemToInsert);
                txn.AddEdit(new IListEdit(ListEditAction.AddItem, list, itemToInsert, index));
            }
            else
            {
                list.Insert(index, itemToInsert);
            }
        }

        /// <summary>
        /// Remove the first occurence of an item in a list (reversibly).
        /// </summary>
        /// <param name="list">List to remove from</param>
        /// <param name="item">Item to remove</param>
        /// <returns>True if item found and removed.</returns>
        public static bool Remove_Reversible(this IList list, object item)
        {
            int index = list.IndexOf(item);
            if (index >= 0)
            {
                RemoveAt_Reversible(list, index);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes an item reversibly.
        /// </summary>
        /// <param name="list">List to insert in</param>
        /// <param name="index">Index to insert at</param>
        public static void RemoveAt_Reversible(this IList list, int index)
        {
            Transaction txn = Transaction.Current;
            if (txn != null)
            {
                object itemToRemove = list[index];
                list.RemoveAt(index);
                txn.AddEdit(new IListEdit(ListEditAction.RemoveItem, list, itemToRemove, index));
            }
            else
            {
                list.RemoveAt(index);
            }
        }

        /// <summary>
        /// Clears all items reversibly.
        /// </summary>
        /// <param name="collection">Collection to clear</param>
        public static void Clear_Reversible(this IList list)
        {
            Transaction txn = Transaction.Current;
            if (txn != null)
            {
                txn.AddEdit(new IListClearEdit(list));
            }
            list.Clear();
        }

        /// <summary>
        /// Creates a reversible version of a given non-reversible list.
        /// </summary>
        /// <param name="list">List to make reversible</param>
        /// <returns>A reversible wrapper for the given list.</returns>
        public static IList AsReversible(this IList list)
        {
            return new ReversibleIListWrapper(list);
        }

        private class IListClearEdit : Edit
        {
            private IList list;
            private ArrayList backupItems;

            public IListClearEdit(IList list)
            {
                
                this.list = list;
                backupItems = new ArrayList(list);
            }

            public override Edit Reverse()
            {
                if (list.Count > 0)
                {
                    list.Clear();
                }
                else
                {
                    // 080601: Fixed bug: Changed collection to backupItems below (reported by schnarky).
                    foreach (Object item in backupItems)
                    {
                        list.Add(item);
                    }
                }
                return this;
            }
        }

    }

     
    /// <summary>
    /// Represents a reversible change in one of an IList's item.
    /// </summary>
    public class IListEdit : Edit
    {
        private ListEditAction action;
        private IList list;
        private object item;
        private int index;

        public IListEdit(ListEditAction action, IList list, object item, int index)
        {
            this.action = action;
            this.list = list;
            this.item = item;
            this.index = index;
        }

        public override Edit Reverse()
        {
            switch (action)
            {
                case ListEditAction.ChangeItem:
                    object backup = list[index];
                    list[index] = item;
                    item = backup;
                    break;
                case ListEditAction.AddItem:
                    list.RemoveAt(index);
                    action = ListEditAction.RemoveItem;
                    break;
                case ListEditAction.RemoveItem:
                    list.Insert(index, item);
                    action = ListEditAction.AddItem;
                    break;
            }
            return this;
        }
    }

    internal class ReversibleIListWrapper : IList
    {
        IList list;

        public ReversibleIListWrapper(IList listToWrap)
        {
            list = listToWrap;
        }



        #region IList Members

        public int IndexOf(object item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, object item)
        {
            list.Insert_Reversible(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt_Reversible(index);
        }

        public object this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list.Item_SetReversible(index, value);
            }
        }

        #endregion

        #region ICollection Members

        public int Add(object item)
        {
            return list.Add_Reversible(item);
        }

        public void Clear()
        {
            list.Clear_Reversible();
        }

        public bool Contains(object item)
        {
            return list.Contains(item);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return list.IsReadOnly; }
        }

        public void Remove(object item)
        {
            list.Remove_Reversible(item);
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IList Members


        public bool IsFixedSize
        {
            get { return list.IsFixedSize; }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            list.CopyTo(array, index);
        }

        public bool IsSynchronized
        {
            get { return list.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return list.SyncRoot; }
        }

        #endregion
    }

}
