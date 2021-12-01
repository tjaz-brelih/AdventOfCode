using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Day20;



using var file = new StreamReader("input2.txt");

List<Tile> tiles = new();

while (file.ReadLine() is string line)
{
    if (line == string.Empty)
    {
        continue;
    }

    var colonIndex = line.IndexOf(':');

    var tileNumber = int.Parse(line.AsSpan()[5..colonIndex]);

    char[,] tileData = new char[10,10];

    for (int i = 0; i < 10; i++)
    {
        var data = file.ReadLine();
        for (int j = 0; j < 10; j++)
        {
            tileData[i, j] = data[j];
        }
    }

    tiles.Add(new Tile(tileNumber, tileData));
}



//(int X, int Y) coordinates = (0, 0);
//Dictionary<(int, int), Tile> image = new()
//{
//    { coordinates, tiles.First() }
//};

//var tilesLeft = tiles.ToHashSet();
//tilesLeft.Remove(tiles.First());

//foreach (var tile in tilesLeft)
//{

//}

var resultOne = tiles
    .Where(t => t.NeighboursCount == 2)
    .Aggregate(1UL, (acc, t) => acc * (ulong)t.Name);
Console.WriteLine(resultOne);



static bool EqualsAny(int value, (int A, int B, int C, int D) tuple)
    => value == tuple.A || value == tuple.B || value == tuple.C || value == tuple.D;
