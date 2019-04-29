using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionLogic.FilteredProperty
{
    public interface IFilteredProperty
    {
        string FieldName { get; set; }
        List<CompareAction> AvailableActions { get; set; }

        Type FieldType { get; set; }
    }
}