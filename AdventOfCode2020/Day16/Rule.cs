using System.Linq;
using System.Collections.Generic;


namespace Day16
{
    public class Rule
    {
        public string Name { get; }

        public List<Range> Ranges { get; } = new();



        public Rule(string name)
        {
            this.Name = name;
        }


        public static Rule FromString(string input)
        {
            var colonIndex = input.IndexOf(':');

            var name = input[0..colonIndex];
            var rule = new Rule(name);

            var tokens = input[(colonIndex + 2)..].Split(' ');

            rule.Ranges.Add(Range.FromString(tokens[0]));
            rule.Ranges.Add(Range.FromString(tokens[2]));

            return rule;
        }



        public bool InAnyRange(int value) => this.Ranges.Any(r => r.IsInRange(value));
    }


    public class Range
    {
        public int Start { get; set; }
        public int End { get; set; }



        public Range(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }


        public static Range FromString(string input)
        {
            var hyphenIndex = input.IndexOf('-');

            return new Range(int.Parse(input[0..hyphenIndex]), int.Parse(input[(hyphenIndex + 1)..]));
        }



        public bool IsInRange(int value) => value >= this.Start && value <= this.End;
    }
}
