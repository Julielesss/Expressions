using DynamicExpressions.Model;
using ExpressionLogic;
using ExpressionLogic.FilteredProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = GetRandomList();
            foreach(var round in list)
                Console.WriteLine(round);
            Console.WriteLine("***********");

            //var filtered = list.Where(round => round.Steps > 5 && round.Views < 6);

            var builder = new FilteredPropertyBuilder(typeof(Round));
            
            var filteredFields = new List<IFilteredProperty>();


            var properties = typeof(Round).GetProperties();
            foreach (var property in properties)
            {
                filteredFields.Add(builder.Build(property.Name));
            }

            var dynamicFilter = new DynamicFilter<Round>();
            var count = 0;

            while (true)
            {
                var prediction = new Prediction();

                if (count != 0)
                {
                    var logicalOperators = Enum.GetValues(typeof(LogicalOperators));
                    for(int i = 0; i<logicalOperators.Length; ++i)
                    {
                        Console.WriteLine($"{i}: {logicalOperators.GetValue(i)}");
                    }
                    Console.Write("Choose field index or -1 to exit: ");
                    var indexOfFieldOperator = ReadObject<int>();
                    if (indexOfFieldOperator == -1)
                        break;
                    prediction.Operator = (LogicalOperators)logicalOperators.GetValue(indexOfFieldOperator);
                }

                Console.WriteLine();
                for (int i = 0; i < filteredFields.Count; i++)
                {
                    Console.WriteLine($"{i}: {filteredFields[i].FieldName}");
                }
                Console.Write("Choose field index: ");

                var indexOfField = ReadObject<int>();

                var filteredField = filteredFields[indexOfField];
                prediction.PropertyName = filteredField.FieldName;

                Console.WriteLine();
                for (int i = 0; i < filteredField.AvailableActions.Count; i++)
                {
                    Console.WriteLine($"{i}: {filteredField.AvailableActions[i]}");
                }
                Console.Write("Choose action index: ");

                var indexOfAction = ReadObject<int>();
                prediction.CompareAction = filteredField.AvailableActions[indexOfAction];

                Console.WriteLine();
                if (prediction.NeedRight())
                {
                    Console.Write("Set right part: ");
                    var rightPart = ReadObject(filteredField.FieldType);
                    prediction.RightValue = rightPart;
                }
                dynamicFilter.AddPredict(prediction);
                ++count;
            }

            var filtered = dynamicFilter.Filter(list);

            Console.WriteLine("Lambda: ");
            Console.WriteLine(dynamicFilter.GetLambda());

            foreach (var round in filtered)
                Console.WriteLine(round);
        }

        private static T ReadObject<T>()
        {
            return (T)ReadObject(typeof(T));
        }

        private static object ReadObject(Type type)
        {
            var line = Console.ReadLine();
            object obj = null;

           
            while (obj == null)
            {
                try
                {
                    obj = Convert.ChangeType(line, type);
                }
                catch (Exception)
                {
                    Console.WriteLine($"'{line}' it is not a {type.Name}. Please enter {type.Name}");
                    line = Console.ReadLine();
                }
            }

            return obj;
        }

        public static List<Round> GetRandomList()
        {
            var list = new List<Round>();
            var rand = new Random();

            for (int i = 0; i < 10; ++i)
            {
                var round = new Round() {
                    Id = i,
                    Steps = rand.Next(10),
                    Views = rand.Next(10),
                    Start = DateTime.Now.AddDays(rand.Next(8)),
                    End = DateTime.Now.AddDays(rand.Next(8)),
                    Name = "Name" + i
                };
                list.Add(round);
            }
            return list;
        }

    }
}
