using var file = new StreamReader("input.txt");

List<List<int>> heights = new();

while (file.ReadLine() is string line)
{
    heights.Add(line.Select(c => (int) char.GetNumericValue(c)).ToList());
}

var (a, b) = (heights.Count, heights[0].Count);



List<(int X, int Y, int Height)> lowpoints = new();

for (int i = 0; i < a; i++)
{
    for (int j = 0; j < b; j++)
    {
        var neighbours = Neighbours(i, j, a, b);
        if (neighbours.All(t => heights[i][j] < heights[t.X][t.Y]))
        {
            lowpoints.Add((i, j, heights[i][j]));
        }
    }
}

var resultOne = lowpoints.Sum(p => p.Height + 1);
Console.WriteLine(resultOne);



// Key - lowpoint (basin), Value - point
Dictionary<(int, int), HashSet<(int, int)>> basins = lowpoints.ToDictionary(t => (t.X, t.Y), t => new HashSet<(int, int)>() { (t.X, t.Y) });

for (int i = 0; i < a; i++)
{
    for (int j = 0; j < b; j++)
    {
        if (heights[i][j] == 9)
        {
            continue;
        }

        _ = FlowToBasin(basins, heights, i, j, a, b);
    }
}

var resultTwo = basins.Select(b => b.Value.Count).OrderByDescending(x => x).Take(3).Aggregate(1, (a, c) => a * c);
Console.WriteLine(resultTwo);



static (int X, int Y) FlowToBasin(Dictionary<(int, int), HashSet<(int, int)>> basins, List<List<int>> heights, int x, int y, int a, int b)
{
    var basin = basins.Where(kv => kv.Value.Contains((x, y))).Select(kv => kv.Key).FirstOrDefault((-1, -1));
    if (basin != (-1, -1))
    {
        return basin;
    }

    var basinCoordinates = Neighbours(x, y, a, b)
        .Where(n => heights[n.X][n.Y] < heights[x][y])
        .Select(n => FlowToBasin(basins, heights, n.X, n.Y, a, b))
        .First();

    basins[basinCoordinates].Add((x, y));

    return basinCoordinates;
}

static IEnumerable<(int X, int Y)> Neighbours(int x, int y, int a, int b)
    => Combinations(Range(x, a - 1), Range(y, b - 1)).Where(c => Math.Abs(c.X + c.Y) == 1).Select(c => (x + c.X, y + c.Y));

static IEnumerable<int> Range(int position, int limit)
{
    var (start, count) = position switch
    {
        0 => (0, 2),
        var x when x == limit => (-1, 2),
        _ => (-1, 3)
    };

    return Enumerable.Range(start, count);
}

static IEnumerable<(int X, int Y)> Combinations(IEnumerable<int> first, IEnumerable<int> second)
{
    foreach (var f in first)
    {
        foreach (var s in second)
        {
            yield return (f, s);
        }
    }
}
