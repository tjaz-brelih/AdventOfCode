using var file = new StreamReader("input.txt");

var levels = new int[10, 10];
var row = 0;

while (file.ReadLine() is string line)
{
    for (int i = 0; i < line.Length; i++)
    {
        levels[row, i] = (int) char.GetNumericValue(line[i]);
    }

    row++;
}



var levelsOne = (int[,]) levels.Clone();
var hasFlashed = new bool[10, 10];
var resultOne = 0;

for (int step = 0; step < 100; step++)
{
    for (int x = 0; x < 10; x++)
    {
        for (int y = 0; y < 10; y++)
        {
            resultOne += CheckLevel(levelsOne, hasFlashed, x, y);
        }
    }


    for (int x = 0; x < 10; x++)
    {
        for (int y = 0; y < 10; y++)
        {
            if (hasFlashed[x, y])
            {
                levelsOne[x, y] = 0;
                hasFlashed[x, y] = false;
            }
        }
    }
}

Console.WriteLine(resultOne);



var levelsTwo = (int[,]) levels.Clone();
var allFlashed = false;
var resultTwo = 0;

while (!allFlashed)
{
    for (int x = 0; x < 10; x++)
    {
        for (int y = 0; y < 10; y++)
        {
            CheckLevel(levelsTwo, hasFlashed, x, y);
        }
    }

    allFlashed = true;

    for (int x = 0; x < 10; x++)
    {
        for (int y = 0; y < 10; y++)
        {
            allFlashed = allFlashed && hasFlashed[x, y];

            if (hasFlashed[x, y])
            {
                levelsTwo[x, y] = 0;
                hasFlashed[x, y] = false;
            }
        }
    }

    resultTwo++;
}

Console.WriteLine(resultTwo);



static int CheckLevel(int[,] levels, bool[,] hasFlashed, int x, int y)
{
    if (levels[x, y] < 9 || hasFlashed[x, y])
    {
        levels[x, y]++;
        return 0;
    }

    hasFlashed[x, y] = true;

    return 1 + Neighbours(x, y).Sum(n => CheckLevel(levels, hasFlashed, n.X, n.Y));
}

static IEnumerable<(int X, int Y)> Neighbours(int x, int y)
    => Combinations(Range(x), Range(y)).Where(c => c != (0, 0)).Select(c => (x + c.X, y + c.Y));

static IEnumerable<int> Range(int position, int limit = 9)
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