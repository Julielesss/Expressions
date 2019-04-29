using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;


namespace ExpressionLogic
{
    public class DynamicFilter<T>
    {
        private ParameterExpression parameterExpression { get; set; }

        private List<KeyValuePair<Expression, LogicalOperators>> Predicts { get; set; } = new List<KeyValuePair<Expression, LogicalOperators>>();
        //private List<Expression> Predicts { get; set; } = new List<Expression>();
        public DynamicFilter()
        {
            parameterExpression = Expression.Parameter(typeof(T), "round"); // round =>
        }

        public Expression<Func<T, bool>> GetLambda()
        {
            var finalPredict = GetDummy();
            foreach (var predict in Predicts)
            {
                switch (predict.Value)
                {
                    case LogicalOperators.And:
                        finalPredict = Expression.And(finalPredict, predict.Key);
                        break;
                    case LogicalOperators.Or:
                        finalPredict = Expression.Or(finalPredict, predict.Key);
                        break;
                    case LogicalOperators.NotEqual:
                        finalPredict = Expression.NotEqual(finalPredict, predict.Key);
                        break;
                }                
            }

            return Expression.Lambda<Func<T, bool>>(finalPredict, new ParameterExpression[] { parameterExpression });
        }

        public void AddPredict(Prediction prediction)
        {
            var left = Expression.Property(parameterExpression, prediction.PropertyName); // от параметра (round) взять свойство - Steps, round.Steps, 
                                                                                     // nameof - имя steps может измениться
            var right = Expression.Constant(prediction.RightValue);
            Expression expression = null;

            switch(prediction.CompareAction)
            {
                case CompareAction.Less:
                    expression = Expression.LessThan(left, right);
                    break;
                case CompareAction.More:
                    expression = Expression.GreaterThan(left, right);
                    break;
                case CompareAction.Equal:
                    expression = Expression.Equal(left, right);
                    break;
                case CompareAction.Workday:
                    expression = Expression.IsFalse(HolidayExpression(left));
                    break;
                case CompareAction.Holiday:
                    expression = Expression.IsTrue(HolidayExpression(left));
                    break;
                case CompareAction.EqualIgnoreCase:
                    var lowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                    var leftLower = Expression.Call(left, lowerMethod);
                    var rightLower = Expression.Call(right, lowerMethod);
                    expression = Expression.Equal(leftLower, rightLower);
                    break;
            }
            Predicts.Add(new KeyValuePair<Expression, LogicalOperators>(expression, prediction.Operator));
        }


        public IQueryable<T> Filter (List<T> list)
        {
            var lambda = GetLambda();
            return list.AsQueryable().Where(lambda);
        }

        private Expression GetDummy()
        {
            var constOne = Expression.Constant(1);
            return Expression.Equal(constOne, constOne);
        }
        private Expression HolidayExpression(Expression left)
        {
            var dayOfWeek = Expression.Property(left, nameof(DateTime.Now.DayOfWeek));
            var holidayDays = new List<DayOfWeek>() { DayOfWeek.Saturday, DayOfWeek.Sunday };
            var holidayDaysExpression = Expression.Constant(holidayDays);
            return Expression.Call(holidayDaysExpression, holidayDays.GetType().GetMethod("Contains"), dayOfWeek);
        }
    }
}
