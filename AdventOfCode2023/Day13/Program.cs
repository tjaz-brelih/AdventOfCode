using Common;
using Coord = (int X, int Y);

var lines = Helpers.ReadInputFile();


var index = 0;
List<List<Coord>> patterns = [];

while (index < lines.Count)
{
    List<Coord> coordinates = [];
    var y = 0;

    while (index < lines.Count && !string.IsNullOrEmpty(lines[index]))
    {
        var line = lines[index];

        for (int x = 0; x < line.Length; x++)
        {
            if (line[x] == '#') { coordinates.Add((x, y)); }
        }

        y++;
        index++;
    }

    patterns.Add(coordinates);
    index++;
}


var resultOne = 0L;

foreach (var coordinates in patterns)
{
    var (maxX, maxY) = MaxXY(coordinates);
    var max = Math.Max(maxX, maxY);

    for (int i = 0; i < max; i++)
    {
        if (i < maxX)
        {
            var (left, right) = Mirrors(coordinates, i, maxX, (x) => x.X, (v, c) => (v, c.Y));
            if (left.SetEquals(right))
            {
                resultOne += i + 1;
                break;
            }
        }

        if (i < maxY)
        {
            var (up, down) = Mirrors(coordinates, i, maxY, (x) => x.Y, (v, c) => (c.X, v));
            if (up.SetEquals(down))
            {
                resultOne += (i + 1) * 100;
                break;
            }
        }
    }
}

Console.WriteLine(resultOne);



var resultTwo = 0L;

foreach (var coordinates in patterns)
{
    var (maxX, maxY) = MaxXY(coordinates);
    var max = Math.Max(maxX, maxY);

    for (int i = 0; i < max; i++)
    {
        if (i < maxX)
        {
            var (left, right) = Mirrors(coordinates, i, maxX, (x) => x.X, (v, c) => (v, c.Y));
            if (left.Count != right.Count)
            {
                var (superset, subset) = left.Count > right.Count ? (left, right) : (right, left);

                superset.RemoveWhere(subset.Contains);
                if (superset.Count == 1)
                {
                    resultTwo += i + 1;
                    break;
                }
            }
        }

        if (i < maxY)
        {
            var (up, down) = Mirrors(coordinates, i, maxY, (x) => x.Y, (v, c) => (c.X, v));
            if (up.Count != down.Count)
            {
                var (superset, subset) = up.Count > down.Count ? (up, down) : (down, up);

                superset.RemoveWhere(subset.Contains);
                if (superset.Count == 1)
                {
                    resultTwo += (i + 1) * 100;
                    break;
                }
            }
        }
    }
}

Console.WriteLine(resultTwo);


static (int, int) MaxXY(IEnumerable<Coord> coordinates)
{
    int maxX = 0, maxY = 0;
    foreach (var (x, y) in coordinates)
    {
        if (x > maxX) maxX = x;
        if (y > maxY) maxY = y;
    }
    return (maxX, maxY);
}

static (HashSet<Coord>, HashSet<Coord>) Mirrors(IEnumerable<Coord> coordinates, int axis, int max, Func<Coord, int> itemSelector, Func<int, Coord, Coord> coordinateFactory)
{
    HashSet<Coord> first = [], second = [];

    foreach (var coordinate in coordinates)
    {
        var axisValue = itemSelector(coordinate);

        if (axisValue <= axis)
        {
            var newValue = axis + axis - axisValue + 1;
            if (newValue > max) { continue; }

            first.Add(coordinateFactory(newValue, coordinate));
        }
        else
        {
            if (axisValue > axis + axis + 1) { continue; }
            second.Add(coordinate);
        }
    }

    return (first, second);
}