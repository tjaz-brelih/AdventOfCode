using Common;


var lines = Helpers.ReadInputFile();

var (width, height) = (lines[0].Length, lines.Count);

List<(int X, int Y)> startingPositions = [];

for (int i = 0; i < height; i++)
{
    for (var j = 0; j < width; j++)
    {
        if (lines[i][j] == '0') { startingPositions.Add((j, i)); }   
    }
}


var resultOne = startingPositions.Sum(x => CountTrailheads(lines, x, []));
Console.WriteLine(resultOne);

var resultTwo = startingPositions.Sum(x => CountTrailheads(lines, x));
Console.WriteLine(resultTwo);


static int CountTrailheads(List<string> map, (int X, int Y) position, HashSet<(int, int)>? nines = null)
{
    var height = char.GetNumericValue(map[position.Y][position.X]);
    if (height == 9) { return (nines?.Add(position) ?? true) ? 1 : 0; }

    (int X, int Y)[] moves = [(position.X + 1, position.Y), (position.X - 1, position.Y), (position.X, position.Y + 1), (position.X, position.Y - 1)];

    return moves
        .Where(x => x.X >= 0 && x.X < map[position.Y].Length && x.Y >= 0 && x.Y < map.Count)
        .Where(x => char.GetNumericValue(map[x.Y][x.X]) == height + 1)
        .Sum(x => CountTrailheads(map, x, nines));
}