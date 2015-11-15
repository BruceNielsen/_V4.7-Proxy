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

using System.Collections.Generic;

namespace Reversible.Extensions
{
    /// <summary>
    /// Contains reversible extension methods for type-safe ICollections
    /// </summary>
    public static class ICollectionReversibleExtension
    {
        /// <summary>
        /// Adds an item reversible
        /// </summary>
        /// <typeparam name="T">Type of item to add</typeparam>
        /// <param name="collection">Collection to add item to</param>
        /// <param name="itemToAdd">Item to add</param>
        public static void Add_Reversible<T>(this ICollection<T> collection, T itemToAdd)
        {
            collection.Add(itemToAdd);
            Transaction txn = Transaction.Current;
            if (txn != null)
            {
                txn.AddEdit(new ICollectionEdit<T>(ICollectionEditAction.AddItem, collection, itemToAdd));
            }
        }

        /// <summary>
        /// Removes an item reversibly.
        /// </summary>
        /// <typeparam name="T">Type of item to add</typeparam>
        /// <param name="collection">Collection to remove from</param>
        /// <param name="itemToRemove">Item to remove</param>
        public static bool Remove_Reversible<T>(this ICollection<T> collection, T itemToRemove)
        {
            bool retValue = collection.Remove(itemToRemove);
            Transaction txn = Transaction.Current;
            if (txn != null)
            {
                txn.AddEdit(new ICollectionEdit<T>(ICollectionEditAction.RemoveItem, collection, itemToRemove));
            }
            return retValue;
        }

        /// <summary>
        /// Clears an items reversibly.
        /// </summary>
        /// <typeparam name="T">Type of ICollection</typeparam>
        /// <param name="collection">Collection to clear</param>
        public static void Clear_Reversible<T>(this ICollection<T> collection)
        {
            Transaction txn = Transaction.Current;
            if (txn != null)
            {
                txn.AddEdit(new ICollectionClearEdit<T>(collection));
            }
            collection.Clear();
        }

        /// <summary>
        /// Creates a reversible version of a given collection.
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="collection">collection to wrap</param>
        /// <returns></returns>
        public static ReversibleCollection<T> AsReversible<T>(this ICollection<T> collection)
        {
            return new ReversibleCollection<T>(collection);
        }

        private enum ICollectionEditAction
        {
            AddItem,
            RemoveItem
        }

        private class ICollectionEdit<T> : Edit
        {
            private ICollectionEditAction action;
            private ICollection<T> collection;
            private T item;

            public ICollectionEdit(ICollectionEditAction action, ICollection<T> collection, T item)
            {
                this.action = action;
                this.collection = collection;
                this.item = item;
            }

            public override Edit Reverse()
            {
                switch (action)
                {
                    case ICollectionEditAction.AddItem:
                        collection.Remove(item);
                        action = ICollectionEditAction.RemoveItem;
                        break;

                    case ICollectionEditAction.RemoveItem:
                        collection.Add(item);
                        action = ICollectionEditAction.AddItem;
                        break;
                }
                return this;
            }
        }

        private class ICollectionClearEdit<T> : Edit
        {
            private ICollection<T> collection;
            private List<T> backupItems;

            public ICollectionClearEdit(ICollection<T> collection)
            {
                this.collection = collection;
                backupItems = new List<T>(collection);
            }

            public override Edit Reverse()
            {
                if (collection.Count > 0)
                {
                    collection.Clear();
                }
                else
                {
                    // 080601: Fixed bug: Changed collection to backupItems below (reported by schnarky).
                    foreach (T item in backupItems)
                    {
                        collection.Add(item);
                    }
                }
                return this;
            }
        }
    }
}