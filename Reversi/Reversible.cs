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
    /// Wraps a field to make it reversible.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Reversible<T>
    {
        private T localValue;
        private ReversibleStorage<T> storage;

        public Reversible(T initialValue)
        {
            localValue = initialValue;
            storage = null;
        }

        public T Value
        {
            get
            {
                if (storage == null)
                {
                    return localValue;
                }
                else
                {
                    return storage.CurrentValue;
                }
            }
            set
            {
                Transaction txn = Transaction.Current;
                if (storage == null)
                {
                    if (txn != null)
                    {
                        storage = new ReversibleStorage<T>(value, localValue);
                        txn.AddEdit(storage);
                    }
                    else
                    {
                        localValue = value;
                    }
                }
                else
                {
                    if (txn != null)
                    {
                        ReversibleStorageEdit<T> storageEdit = new ReversibleStorageEdit<T>(storage, storage.CurrentValue);
                        storage.CurrentValue = value;
                        txn.AddEdit(storageEdit);
                    }
                    else
                    {
                        storage.CurrentValue = value;
                    }
                }
            }
        }

        public override int GetHashCode()
        {
            object valueObj = (object)Value;
            if (valueObj == null)
            {
                return 0;
            }
            else
            {
                return valueObj.GetHashCode();
            }
        }

        public override string ToString()
        {
            object valueObj = (object)Value;
            if (valueObj == null)
            {
                return null;
            }
            else
            {
                return valueObj.ToString();
            }
        }

        public override bool Equals(object obj)
        {
            object valueObj = (object)Value;
            if (valueObj == null)
            {
                return false;
            }
            else
            {
                return valueObj.Equals(obj);
            }
        }

    }
}
