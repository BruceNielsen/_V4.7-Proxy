using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reversible
{
    /// <summary>
    /// Represents a static method that sets a given property of a given instance to a given value. 
    /// </summary>
    /// <typeparam name="TOwner">Type that owns the property to set.</typeparam>
    /// <typeparam name="TProperty">Type of property to set</typeparam>
    /// <param name="instance">Instance to set property for.</param>
    /// <param name="newValue">Value to set property to</param>
    public delegate void ReversiblePropertySetter<TOwner,TProperty>(TOwner instance, TProperty newValue) where TOwner : class;
    
    /// <summary>
    /// Represents a instance method that sets a given instance property to a given value. 
    /// </summary>
    /// <typeparam name="TProperty">Type of property to set</typeparam>
    /// <param name="newValue">Value to set property to.</param>
    public delegate void ReversibleInstancePropertySetter<TProperty>(TProperty newValue);
    
    /*
    public class ReversibleProperty<TOwner,TProperty>  
    {
        readonly string propName;
        readonly ReversiblePropertySetter<TOwner, TProperty> setter;

        public ReversibleProperty(string propertyName, ReversiblePropertySetter<TOwner,TProperty> setter)
        {
            this.propName = propertyName;
            this.setter = setter;
        }

        public void SetValue(TOwner instance, TProperty newValue)
        {
            setter(instance, newValue);
        }
    }
    */

    public class ReversiblePropertyEdit<TOwner, TProperty> : Edit where TOwner : class 
    {
        ReversiblePropertySetter<TOwner, TProperty> setter;
        TOwner instance;
        TProperty oldValue;
        TProperty newValue;

        public ReversiblePropertyEdit(ReversiblePropertySetter<TOwner, TProperty> setter, TOwner instance, TProperty oldValue, TProperty newValue)
        {
            this.setter = setter;
            this.instance = instance;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public override Edit Reverse()
        {
            setter(instance, oldValue);
            Switch(ref oldValue, ref newValue);
            return this;
        }
    }

    public class ReversibleInstancePropertyEdit<TProperty> : Edit
    {
        ReversibleInstancePropertySetter<TProperty> setter;
        TProperty oldValue;
        TProperty newValue;

        public ReversibleInstancePropertyEdit(ReversibleInstancePropertySetter<TProperty> setter, TProperty oldValue, TProperty newValue)
        {
            this.setter = setter;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public override Edit Reverse()
        {
            setter(oldValue);
            Switch(ref oldValue, ref newValue);
            return this;
        }
    }
}
