using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicExpressions.Model
{
    public class Round
    {
        public int Id { get; set; }
        public int Views  { get; set; }
        public int Steps { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Name { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Views: {Views}, Steps: {Steps}, Start: {Start.DayOfWeek}, End: {End.DayOfWeek}, Name: {Name}";
        }

    }
}
