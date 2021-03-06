﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


using var file = new StreamReader("input.txt");

List<List<string>> groups = new();
List<string> currentGroup = new();

while (true)
{
    var line = file.ReadLine();

    if (line == null || line.Length == 0)
    {
        groups.Add(currentGroup);

        if (line == null)
        {
            break;
        }

        currentGroup = new();
        continue;
    }


    currentGroup.Add(line);
}


var resultOne = groups.Sum(g => g.SelectMany(g => g.ToList()).Distinct().Count());
Console.WriteLine(resultOne);

var resultTwo = groups.Sum(group => group.SelectMany(g => g.ToList()).GroupBy(c => c).Select(g => g.Count()).Where(c => c == group.Count).Count());
Console.WriteLine(resultTwo);