using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


using var file = new StreamReader("input.txt");

List<int> seatIds = new();

while (file.ReadLine() is string line)
{
    var frontBack = line[0..7];
    var leftRight = line[7..];

    var row = FromBinary(frontBack, 'F', 'B');
    var column = FromBinary(leftRight, 'L', 'R');

    seatIds.Add(row * 8 + column);
}


var resultOne = seatIds.Max();
Console.WriteLine(resultOne);


Console.WriteLine(FindMissing(seatIds));



static int FromBinary(string input, char low, char high)
{
    var result = 0;
    var currentMultiplier = 1;

    var inputSpan = input.AsSpan();

    for (int i = input.Length - 1; i >= 0; i--)
    {
        if (inputSpan[i] == high)
        {
            result += currentMultiplier;
        }

        currentMultiplier *= 2;
    }

    return result;
}


static int FindMissing(IEnumerable<int> input)
{
    var orderedInput = input.OrderByDescending(i => i).ToList();

    for (int i = 0; i < orderedInput.Count - 1; i++)
    {
        if (orderedInput[i] - orderedInput[i + 1] == 2)
        {
            return orderedInput[i] - 1;
        }
    }

    throw new Exception();
}
