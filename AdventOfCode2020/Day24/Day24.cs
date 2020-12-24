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



HashSet<(int, int)> blackTiles = new();

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
            ('e', _) => (1, 0),
            ('w', _) => (-1, 0),

            ('s', 'e') => (1, -1),
            ('s', 'w') => (0, -1),

            ('n', 'e') => (0, 1),
            ('n', 'w') => (-1, 1),

            _ => throw new InvalidDataException()
        };

        coordinates.X += dX;
        coordinates.Y += dY;
    }

    _ = blackTiles.Contains(coordinates) ? blackTiles.Remove(coordinates) : blackTiles.Add(coordinates);
}

var resultOne = blackTiles.Count;
Console.WriteLine(resultOne);



Dictionary<(int X, int Y), bool> floor = new( blackTiles.Select(t => new KeyValuePair<(int, int), bool>(t, true) ));
Dictionary<(int X, int Y), bool> floorPrev = new();

for (int eh = 0; eh < 100; eh++)
{
    CloneDictionary(floor, floorPrev);

    (int minX, int minY) = (int.MaxValue, int.MaxValue);
    (int maxX, int maxY) = (int.MinValue, int.MinValue);

    foreach (var state in floor)
    {
        (int X, int Y) = state.Key;

        if (X < minX)
            minX = X;
        if (X > maxX)
            maxX = X;

        if (Y < minY)
            minY = Y;
        if (Y > maxY)
            maxY = Y;
    }

    for (int i = minX - 1; i <= maxX + 1; i++)
    {
        for (int j = minY - 1; j <= maxY + 1; j++)
        {
            var currentState = floorPrev.TryGetValue((i, j), out var state) && state;
            var activeStates = GenerateNeighbours(i, j).Count(n => floorPrev.GetValueOrDefault((n.Item1, n.Item2), false));

            floor[(i, j)] = currentState switch
            {
                true => activeStates == 1 || activeStates == 2,
                false => activeStates == 2
            };
        }
    }
}

var resultTwo = floor.Count(kv => kv.Value);
Console.WriteLine(resultTwo);




static IEnumerable<(int, int)> GenerateNeighbours(int X, int Y)
{
    yield return (X + 1, Y);
    yield return (X - 1, Y);

    yield return (X + 1, Y - 1);
    yield return (X, Y - 1);

    yield return (X, Y + 1);
    yield return (X - 1, Y + 1);
}


static void CloneDictionary<Tk, Tv>(Dictionary<Tk, Tv> input, Dictionary<Tk, Tv> output)
{
    output.Clear();

    foreach (var kv in input)
    {
        output.Add(kv.Key, kv.Value);
    }
}