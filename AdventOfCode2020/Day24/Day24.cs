using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;



using var file = new StreamReader("input.txt");

List<string> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(line);
}



Dictionary<(int, int), bool> floor = new();

foreach (var line in lines)
{
    var index = 0;
    (int X, int Y) coordinates = (0, 0);

    while (index < line.Length)
    {
        char first  = ' ';
        char second = ' ';

        if (line[index] == 'w' || line[index] == 'e')
        {
            first = line[index];
            index++;
        }
        else
        {
            first = line[index];
            second = line[index + 1];

            index += 2;
        }

        (var dX, var dY) = (first, second) switch
        {
            ('e', _) => (2, 0),
            ('w', _) => (-2, 0),

            ('s', 'e') => (1, -1),
            ('s', 'w') => (-1, -1),

            ('n', 'e') => (1, 1),
            ('n', 'w') => (-1, 1),

            _ => throw new InvalidDataException()
        };

        coordinates.X += dX;
        coordinates.Y += dY;
    }


    floor[coordinates] = floor.TryGetValue(coordinates, out var value) ? !value : true;
}

var resultOne = floor.Where(kv => kv.Value).Count();
Console.WriteLine(resultOne);


