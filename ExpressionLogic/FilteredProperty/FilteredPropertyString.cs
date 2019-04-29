using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionLogic.FilteredProperty
{
    public class FilteredPropertyString : FilteredPropertyBase<string>
    {
        public FilteredPropertyString(string fieldName):base(fieldName)
        {
            AvailableActions.Add(CompareAction.Equal);
            AvailableActions.Add(CompareAction.EqualIgnoreCase);
        }
    }
}

