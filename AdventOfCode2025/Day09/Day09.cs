using Common;
using Coord = (long X, long Y);

var lines = Helpers.ReadInputFile();

var tiles = lines
    .Select(x => x.Split(','))
    .Select(x => (Coord) (long.Parse(x[0]), long.Parse(x[1])))
    .ToList();


var resultOne = 0L;

for (var i = 0; i < tiles.Count - 1; i++)
{
    for (var j = i + 1; j < tiles.Count; j++)
    {
        var area = (Math.Abs(tiles[i].X - tiles[j].X) + 1) * (Math.Abs(tiles[i].Y - tiles[j].Y) + 1);
        if (area > resultOne) { resultOne = area; }
    }
}

Console.WriteLine(resultOne);