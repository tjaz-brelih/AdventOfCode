using var file = new StreamReader("input.txt");

List<List<(int, int)>> formations = new();

while (file.ReadLine() is string line)
{
    var span = line.AsSpan();

    var rocks = new List<(int, int)>();
    formations.Add(rocks);

    while (true)
    {
        var index = span.IndexOf(',');

        var x = int.Parse(span[..index]);

        span = span[(index + 1)..];

        index = span.IndexOf('-');

        var y = 0;
        if (index == -1)
        {
            y = int.Parse(span);
            rocks.Add((x, y));

            break;
        }

        y = int.Parse(span[..(index - 1)]);
        rocks.Add((x, y));

        span = span[(index + 3)..];
    }
}


// Part 1
var map = GenerateMap(formations);
var maxY = map.Max(x => x.Item2);
var sand = (500, 0);
var isFull = false;

var resultOne = 0;

while (!isFull)
{
    var (x, y) = sand;

    while (true)
    {
        if (y >= maxY)
        {
            isFull = true;
            break;
        }

        if (!map.Contains((x, y + 1))) { y++; }
        else if (!map.Contains((x - 1, y + 1))) { x--; y++; }
        else if (!map.Contains((x + 1, y + 1))) { x++; y++; }
        else
        {
            map.Add((x, y));
            resultOne++;

            break;
        }
    }
}

Console.WriteLine(resultOne);


// Part 2
map.Clear();
map = GenerateMap(formations);

var resultTwo = 0;

while (true)
{
    var (x, y) = sand;
    if (map.Contains((x, y)))
    {
        break;
    }

    while (true)
    {
        if (y == maxY + 1)
        {
            map.Add((x, y));
            resultTwo++;

            break;
        }

        if (!map.Contains((x, y + 1))) { y++; }
        else if (!map.Contains((x - 1, y + 1))) { x--; y++; }
        else if (!map.Contains((x + 1, y + 1))) { x++; y++; }
        else
        {
            map.Add((x, y));
            resultTwo++;

            break;
        }
    }
}

Console.WriteLine(resultTwo);




static HashSet<(int, int)> GenerateMap(List<List<(int X, int Y)>> formations)
{
    HashSet<(int, int)> map = new();

    foreach (var rocks in formations)
    {
        for (int i = 0; i < rocks.Count - 1; i++)
        {
            var (first, second) = (rocks[i], rocks[i + 1]);

            if (first.X == second.X)
            {
                var (start, end) = (Math.Min(first.Y, second.Y), Math.Max(first.Y, second.Y));
                for (int j = start; j <= end; j++)
                {
                    map.Add((first.X, j));
                }
            }

            if (first.Y == second.Y)
            {
                var (start, end) = (Math.Min(first.X, second.X), Math.Max(first.X, second.X));
                for (int j = start; j <= end; j++)
                {
                    map.Add((j, first.Y));
                }
            }
        }
    }

    return map;
}
