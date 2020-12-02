using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace Day02
{
    public class Day02
    {
        public static void Main()
        {
            var file = new StreamReader("input.txt");
            string line;

            List<Policy> policies = new();

            while ((line = file.ReadLine()) is not null)
            {
                var span = line.AsSpan();

                var separatorIndex = span.IndexOf('-');
                var spaceIndex = span.IndexOf(' ');

                var min = span[0..separatorIndex].ToString();
                var max = span[(separatorIndex + 1)..spaceIndex].ToString();

                var letter = span[spaceIndex + 1];

                var password = span[(spaceIndex + 4)..];

                policies.Add(new(int.Parse(min), int.Parse(max), letter, password.ToString()));
            }



            var resultCount = policies.Count(p => p.CheckPasswordAgainstCountPolicy());
            Console.WriteLine(resultCount);


            var resultPosition = policies.Count(p => p.CheckPasswordAgainstPositionPolicy());
            Console.WriteLine(resultPosition);
        }
    }
}
