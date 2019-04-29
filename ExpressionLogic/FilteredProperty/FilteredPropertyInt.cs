using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionLogic.FilteredProperty
{
    public class FilteredPropertyInt : FilteredPropertyBase<int>
    {
        public FilteredPropertyInt(string fieldName):base(fieldName)
        {
            AvailableActions.Add(CompareAction.Equal);
            AvailableActions.Add(CompareAction.Less);
            AvailableActions.Add(CompareAction.More);
        }
    }
}
