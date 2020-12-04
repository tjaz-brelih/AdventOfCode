using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


using var file = new StreamReader("input.txt");

List<string> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(line);
}



var direction = (3, 1);
var resultOne = CheckTreesCount(lines, direction);

Console.WriteLine(resultOne);



var directions = new (int, int)[]
{
    (1, 1),
    (3, 1),
    (5, 1),
    (7, 1),
    (1, 2)
};

var resultTwo = directions
    .Select(d => CheckTreesCount(lines, d))
    .Aggregate(1L, (acc, r) => acc * r);

Console.WriteLine(resultTwo);




static long CheckTreesCount(IList<string> input, (int x, int y) direction)
{
    var columnIndex = 0;
    var rowIndex = 0;

    var maxColumnIndex = input.First().Length;

    var treesCount = 0;

    while (rowIndex < input.Count)
    {
        var currentFeature = input[rowIndex][columnIndex];

        treesCount += currentFeature switch
        {
            '#' => 1,
            _ => 0
        };

        rowIndex += direction.y;
        columnIndex += direction.x;

        columnIndex %= maxColumnIndex;
    }

    return treesCount;
}