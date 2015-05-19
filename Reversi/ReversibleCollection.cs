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
using Reversible.Extensions;

namespace Reversible
{
    /// <summary>
    /// Represents a generic reversible collection.
    /// </summary>
    /// <typeparam name="T">Type of items in the collection</typeparam>
    public class ReversibleCollection<T> : ICollection<T>
    {
        ICollection<T> collection;

        /// <summary>
        /// Creates a new empty reversible collection
        /// </summary>
        public ReversibleCollection()
        {
            collection = new List<T>();
        }

        /// <summary>
        /// Creates a reversible collection that wraps a non-reversible collection.
        /// </summary>
        /// <param name="collectionToWrap"></param>
        public ReversibleCollection(ICollection<T> collectionToWrap)
        {
            this.collection = collectionToWrap;
        }

        #region ICollection<T> Members

        public void Add(T item)
        {
            collection.Add_Reversible(item);
        }

        public void Clear()
        {
            collection.Clear_Reversible();
        }

        public bool Contains(T item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return collection.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return collection.Remove_Reversible(item);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        #endregion
    }

}
