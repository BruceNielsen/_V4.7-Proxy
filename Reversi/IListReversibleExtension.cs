﻿/*
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

using System.Collections.Generic;

namespace Reversible.Extensions
{
    /// <summary>
    /// Enumerates possible actions in an ListEdit and IListEdit.
    /// </summary>
    public enum ListEditAction
    {
        ChangeItem = 1,
        AddItem,
        RemoveItem
    }

    public static class IListReversibleExtension
    {
        /// <summary>
        /// Sets a list item reversibly.
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="list">List to set item in</param>
        /// <param name="index">Index of item to set</param>
        /// <param name="newValue">New value to assign.</param>
        public static void Item_SetReversible<T>(this IList<T> list, int index, T newValue)
        {
            Transaction txn = Transaction.Current;
            if (txn != null)
            {
                T oldValue = list[index];
                list[index] = newValue;
                txn.AddEdit(new IListEdit<T>(ListEditAction.ChangeItem, list, oldValue, index));
            }
            else
            {
                list[index] = newValue;
            }
        }

        /// <summary>
        /// Adds an item to the end of a list (reversibly).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void Add_Reversible<T>(this IList<T> list, T item)
        {
            Insert_Reversible<T>(list, list.Count, item);
        }

        /// <summary>
        /// Inserts an item reversibly.
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="list">List to insert in</param>
        /// <param name="index">Index to insert at</param>
        /// <param name="itemToInsert">Item to insert</param>
        public static void Insert_Reversible<T>(this IList<T> list, int index, T itemToInsert)
        {
            Transaction txn = Transaction.Current;
            if (txn != null)
            {
                list.Insert(index, itemToInsert);
                txn.AddEdit(new IListEdit<T>(ListEditAction.AddItem, list, itemToInsert, index));
            }
            else
            {
                list.Insert(index, itemToInsert);
            }
        }

        /// <summary>
        /// Remove the first occurence of an item in a list (reversibly).
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="list">List to remove from</param>
        /// <param name="item">Item to remove</param>
        /// <returns>True if item found and removed.</returns>
        public static bool Remove_Reversible<T>(this IList<T> list, T item)
        {
            int index = list.IndexOf(item);
            if (index >= 0)
            {
                RemoveAt_Reversible<T>(list, index);
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
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="list">List to insert in</param>
        /// <param name="index">Index to insert at</param>
        public static void RemoveAt_Reversible<T>(this IList<T> list, int index)
        {
            Transaction txn = Transaction.Current;
            if (txn != null)
            {
                T itemToRemove = list[index];
                list.RemoveAt(index);
                txn.AddEdit(new IListEdit<T>(ListEditAction.RemoveItem, list, itemToRemove, index));
            }
            else
            {
                list.RemoveAt(index);
            }
        }

        /// <summary>
        /// Creates a reversible version of a given non-reversible list.
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="list">List to make reversible</param>
        /// <returns>A reversible wrapper for the given list.</returns>
        public static IList<T> AsReversible<T>(this IList<T> list)
        {
            return new ReversibleIListWrapper<T>(list);
        }
    }

    /// <summary>
    /// Represents a reversible change in one of an IList's item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IListEdit<T> : Edit
    {
        private ListEditAction action;
        private IList<T> list;
        private T item;
        private int index;

        public IListEdit(ListEditAction action, IList<T> list, T item, int index)
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
                    T backup = list[index];
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

    internal class ReversibleIListWrapper<T> : IList<T>
    {
        private IList<T> list;

        public ReversibleIListWrapper(IList<T> listToWrap)
        {
            list = listToWrap;
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            list.Insert_Reversible(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt_Reversible(index);
        }

        public T this[int index]
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

        #endregion IList<T> Members

        #region ICollection<T> Members

        public void Add(T item)
        {
            list.Add_Reversible(item);
        }

        public void Clear()
        {
            list.Clear_Reversible();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return list.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return list.Remove_Reversible(item);
        }

        #endregion ICollection<T> Members

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion IEnumerable<T> Members

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion IEnumerable Members
    }
}