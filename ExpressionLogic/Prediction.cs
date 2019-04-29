using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionLogic
{
    public class Prediction
    {
        public string PropertyName { get; set; }

        public LogicalOperators Operator { get; set; }
        public CompareAction CompareAction { get; set; }
        public object RightValue { get; set; }

        public bool NeedRight() => CompareAction == CompareAction.Less
            || CompareAction == CompareAction.More
            || CompareAction == CompareAction.Equal
            || CompareAction == CompareAction.EqualIgnoreCase;
    }
}
