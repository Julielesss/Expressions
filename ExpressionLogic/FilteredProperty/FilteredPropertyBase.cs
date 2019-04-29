using ExpressionLogic.FilteredProperty;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionLogic.FilteredProperty
{
    public abstract class FilteredPropertyBase<T>:IFilteredProperty
    {
        public string FieldName { get; set; }
        public List<CompareAction> AvailableActions { get; set; } = new List<CompareAction>();
        public Type FieldType { get; set; }
        public FilteredPropertyBase(string fieldName)
        {
            FieldName = fieldName;
            FieldType = typeof(T);
        }
    }
}
