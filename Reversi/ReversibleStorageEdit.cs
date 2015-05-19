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
    sealed class ReversibleStorageEdit<T> : Edit 
    {
        private ReversibleStorage<T> storage;
        private T backupValue;

        public ReversibleStorageEdit(ReversibleStorage<T> storage, T backup)
        {
            this.storage = storage;
            backupValue = backup;
        }

        public override Edit Reverse()
        {
            T lastValue = storage.CurrentValue;
            storage.CurrentValue = backupValue;
            backupValue = lastValue;
            return this;
        }
    }
}
