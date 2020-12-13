using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



using var file = new StreamReader("input2.txt");

var earliest = int.Parse(file.ReadLine());
var busIds = file.ReadLine().Split(',').AsEnumerable();



var resultOne = busIds
    .Where(b => b != "x")
    .Select(b => new
    {
        Id = int.Parse(b),
        Arrival = ((decimal) earliest / int.Parse(b))
    })
    .OrderByDescending(b => b.Arrival % 1)
    .First();

Console.WriteLine(resultOne.Id * ((resultOne.Id * Math.Ceiling(resultOne.Arrival)) - earliest));



var arrivals = busIds
    .Select((b, i) => (ParseIntOrDefault(b, 0), i))
    .Where(b => b.Item1 != 0);





static ulong CalculateFirstSequence(IEnumerable<(int Id, int Offset)> input, int inputIndex = 0, ulong current = 1, ulong increment = 1)
{
    if (inputIndex == input.Count() - 1)
    {
        return current;
    }

    var currentElement = input.ElementAt(inputIndex);
    var nextElement = input.ElementAt(inputIndex + 1);

    return 0UL;
}


static int ParseIntOrDefault(string input, int def = 0)
{
    if (int.TryParse(input, out var result))
    {
        return result;
    }

    return def;
}