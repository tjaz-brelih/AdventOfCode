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



Dictionary<(int X, int Y, int Z, int W), bool> cubeStates = new();
Dictionary<(int X, int Y, int Z, int W), bool> cubeStatesPrev = new();

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        cubeStates[(j, i, 0, 0)] = lines[i][j] switch
        {
            '#' => true,
            _ => false
        };
    }
}


for (int eh = 0; eh < 6; eh++)
{
    CloneDictionary(cubeStates, cubeStatesPrev);

    (int minX, int minY, int minZ) = (int.MaxValue, int.MaxValue, int.MaxValue);
    (int maxX, int maxY, int maxZ) = (int.MinValue, int.MinValue, int.MinValue);

    foreach (var state in cubeStates)
    {
        (int X, int Y, int Z, _) = state.Key;

        if (X < minX)
            minX = X;
        if (X > maxX)
            maxX = X;

        if (Y < minY)
            minY = Y;
        if (Y > maxY)
            maxY = Y;

        if (Z < minZ)
            minZ = Z;
        if (Z > maxZ)
            maxZ = Z;
    }


    for (int i = minX - 1; i <= maxX + 1; i++)
    {
        for (int j = minY - 1; j <= maxY + 1; j++)
        {
            for (int k = minZ - 1; k <= maxZ + 1; k++)
            {
                var currentState = cubeStatesPrev.TryGetValue((i, j, k, 0), out var state) && state;
                var activeStates = GenerateNeighbours(i, j, k, 1).Count(n => cubeStatesPrev.GetValueOrDefault((n.Item1, n.Item2, n.Item3, 0), false));

                cubeStates[(i, j, k, 0)] = currentState switch
                {
                    true => activeStates == 2 || activeStates == 3,
                    false => activeStates == 3
                };
            }
        }
    }

}

var resultOne = cubeStates.Count(kv => kv.Value);
Console.WriteLine(resultOne);



cubeStates.Clear();
cubeStatesPrev.Clear();

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        cubeStates[(j, i, 0, 0)] = lines[i][j] switch
        {
            '#' => true,
            _ => false
        };
    }
}


for (int eh = 0; eh < 6; eh++)
{
    CloneDictionary(cubeStates, cubeStatesPrev);

    (int minX, int minY, int minZ, int minW) = (int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
    (int maxX, int maxY, int maxZ, int maxW) = (int.MinValue, int.MinValue, int.MinValue, int.MinValue);

    foreach (var state in cubeStates)
    {
        (int X, int Y, int Z, int W) = state.Key;

        if (X < minX)
            minX = X;
        if (X > maxX)
            maxX = X;

        if (Y < minY)
            minY = Y;
        if (Y > maxY)
            maxY = Y;

        if (Z < minZ)
            minZ = Z;
        if (Z > maxZ)
            maxZ = Z;
        
        if (W < minW)
            minW = W;
        if (W > maxW)
            maxW = W;
    }


    for (int i = minX - 1; i <= maxX + 1; i++)
    {
        for (int j = minY - 1; j <= maxY + 1; j++)
        {
            for (int k = minZ - 1; k <= maxZ + 1; k++)
            {
                for (int m = minW - 1; m <= maxW + 1; m++)
                {
                    var currentState = cubeStatesPrev.TryGetValue((i, j, k, m), out var state) && state;
                    var activeStates = GenerateNeighbours4D(i, j, k, m, 1).Count(n => cubeStatesPrev.GetValueOrDefault(n, false));

                    cubeStates[(i, j, k, m)] = currentState switch
                    {
                        true => activeStates == 2 || activeStates == 3,
                        false => activeStates == 3
                    };
                }
            }
        }
    }

}

var resultTwo = cubeStates.Count(kv => kv.Value);
Console.WriteLine(resultTwo);




static IEnumerable<(int, int, int)> GenerateNeighbours(int X, int Y, int Z, int distance)
{
    for (int i = -distance; i <= distance; i++)
    {
        for (int j = -distance; j <= distance; j++)
        {
            for (int k = -distance; k <= distance; k++)
            {
                if (i == 0 && j == 0 && k == 0)
                    continue;

                yield return (X + i, Y + j, Z + k);
            }
        }
    }
}


static IEnumerable<(int, int, int, int)> GenerateNeighbours4D(int X, int Y, int Z, int W, int distance)
{
    for (int i = -distance; i <= distance; i++)
    {
        for (int j = -distance; j <= distance; j++)
        {
            for (int k = -distance; k <= distance; k++)
            {
                for (int m = -distance; m <= distance; m++)
                {
                    if (i == 0 && j == 0 && k == 0 && m == 0)
                        continue;

                    yield return (X + i, Y + j, Z + k, W + m);
                }
            }
        }
    }
}


static void CloneDictionary<Tk, Tv>(Dictionary<Tk, Tv> input, Dictionary<Tk, Tv> output)
{
    output.Clear();

    foreach (var kv in input)
    {
        output.Add(kv.Key, kv.Value);
    }
}