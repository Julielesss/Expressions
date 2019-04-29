using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionLogic.FilteredProperty
{
    public class FilteredPropertyDateTime : FilteredPropertyBase<DateTime>
    {
        public FilteredPropertyDateTime(string fieldName):base(fieldName)
        {
            AvailableActions.Add(CompareAction.Holiday);
            AvailableActions.Add(CompareAction.Workday);
        }
    }
}
