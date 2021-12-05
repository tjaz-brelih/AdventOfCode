using var file = new StreamReader("input.txt");

List<(int X1, int Y1, int X2, int Y2)> lines = new();

while (file.ReadLine() is string line)
{
    var tokens = line.Split(',');
    var tokens2 = tokens[1].Split(' ');

    var x1 = int.Parse(tokens[0]);
    var y2 = int.Parse(tokens[2]);

    var y1 = int.Parse(tokens2[0]);
    var x2 = int.Parse(tokens2[2]);

    lines.Add((x1, y1, x2, y2));
}



Dictionary<(int X, int Y), int> map = new();

foreach (var (x1, y1, x2, y2) in lines)
{
    if (x1 == x2)
    {
        var (min, max) = y1 > y2 ? (y2, y1) : (y1, y2);

        for (int i = min; i <= max; i++)
        {
            var location = (x1, i);
            map[location] = map.TryGetValue(location, out var value) ? ++value : 1;
        }
    }
    else if (y1 == y2)
    {
        var (min, max) = x1 > x2 ? (x2, x1) : (x1, x2);

        for (int i = min; i <= max; i++)
        {
            var location = (i, y1);
            map[location] = map.TryGetValue(location, out var value) ? ++value : 1;
        }
    }
}

var resultOne = map.Values.Count(v => v >= 2);
Console.WriteLine(resultOne);



map.Clear();

foreach (var (x1, y1, x2, y2) in lines)
{
    if (x1 == x2)
    {
        var (min, max) = y1 > y2 ? (y2, y1) : (y1, y2);

        for (int i = min; i <= max; i++)
        {
            var location = (x1, i);
            map[location] = map.TryGetValue(location, out var value) ? ++value : 1;
        }
    }
    else if (y1 == y2)
    {
        var (min, max) = x1 > x2 ? (x2, x1) : (x1, x2);

        for (int i = min; i <= max; i++)
        {
            var location = (i, y1);
            map[location] = map.TryGetValue(location, out var value) ? ++value : 1;
        }
    }
    else
    {
        var (minX, maxX) = x1 > x2 ? (x2, x1) : (x1, x2);
        var (minY, maxY) = y1 > y2 ? (y2, y1) : (y1, y2);

        for (int i = 0; i <= maxX - minX; i++)
        {
            var x = x1 > x2 ? x1 - i : x1 + i;
            var y = y1 > y2 ? y1 - i : y1 + i;
            var location = (x, y);
            map[location] = map.TryGetValue(location, out var value) ? ++value : 1;
        }
    }
}

var resultTwo = map.Values.Count(v => v >= 2);
Console.WriteLine(resultTwo);
