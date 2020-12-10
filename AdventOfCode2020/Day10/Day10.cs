using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


using var file = new StreamReader("input.txt");

SortedDictionary<int, ulong> adapters = new();

while (file.ReadLine() is string line)
{
    adapters.Add(int.Parse(line), 0);
}



Dictionary<int, int> differencesCount = new()
{
    { 1, 1 },
    { 2, 1 },
    { 3, 1 }
};

for (int i = 0; i < adapters.Count - 1; i++)
{
    var currentAdapter = adapters.ElementAt(i);
    var nextAdapter = adapters.ElementAt(i + 1);
    var difference = nextAdapter.Key - currentAdapter.Key;

    differencesCount[difference]++;
}

var resultOne = differencesCount[1] * differencesCount[3];
Console.WriteLine(resultOne);



adapters[0] = 1;

for (int i = 1; i < adapters.Count; i++)
{
    var currentKey = adapters.ElementAt(i).Key;

    for (int j = 1; j < 4; j++)
    {
        if(i - j < 0)
        {
            break;
        }

        var prev = adapters.ElementAt(i - j);
        if (currentKey - prev.Key > 3)
        {
            break;
        }

        adapters[currentKey] += prev.Value;
    }
}


var resultTwo = adapters.Last().Value;
Console.WriteLine(resultTwo);
