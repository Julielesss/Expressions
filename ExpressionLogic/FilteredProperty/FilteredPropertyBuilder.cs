using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionLogic.FilteredProperty
{
    public class FilteredPropertyBuilder
    {
        public Type BaseType { get; set; }

        public FilteredPropertyBuilder(Type type)
        {
            BaseType = type;
        }

        public IFilteredProperty Build(string propertyName)
        {
            var property = BaseType.GetProperty(propertyName);
            var propertyType = property.PropertyType;

            switch (Type.GetTypeCode(propertyType))
            {
                case TypeCode.Int32:
                    return new FilteredPropertyInt(propertyName);

                case TypeCode.DateTime:
                    return new FilteredPropertyDateTime(propertyName);
                case TypeCode.String:
                    return new FilteredPropertyString(propertyName);
            }
            return null;
        }
    }
}
