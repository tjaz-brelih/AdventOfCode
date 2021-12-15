using var file = new StreamReader("input.txt");

List<List<int>> risks = new();

while (file.ReadLine() is string line)
{
    risks.Add(line.Select(c => (int) char.GetNumericValue(c)).ToList());
}

var (a, b) = (risks.Count, risks[0].Count);



int[,] graphOne = new int[a, b];

for (int i = 0; i < a; i++)
{
    for (int j = 0; j < b; j++)
    {
        graphOne[i, j] = risks[i][j];
    }
}

var resultOne = ShortestPath(graphOne, a, b);
Console.WriteLine(resultOne);



var (newA, newB) = (a * 5, b * 5);

int[,] graphTwo = new int[newA, newB];

for (int i = 0; i < newA; i++)
{
    for (int j = 0; j < newB; j++)
    {
        var val = risks[i % a][j % b] + i / a + j / b;
        graphTwo[i, j] = val <= 9 ? val : val - 9;
    }
}

var resultTwo = ShortestPath(graphTwo, newA, newB);
Console.WriteLine(resultTwo);



static ulong ShortestPath(int[,] graph, int a, int b)
{
    var start = (0, 0);
    var end = (a - 1, b - 1);

    Dictionary<(int X, int Y), ulong> distances = new();
    PriorityQueue<(int X, int Y), ulong> queue = new();

    for (int i = 0; i < a; i++)
    {
        for (int j = 0; j < b; j++)
        {
            if (i != 0 || j != 0)
            {
                distances[(i, j)] = int.MaxValue;
            }
        }
    }

    distances[start] = 0;
    queue.Enqueue(start, 0);

    while (queue.Count > 0)
    {
        _ = queue.TryDequeue(out var u, out var priority);

        if (u == end) { return priority; }
        if (priority > distances[u]) { continue; }

        foreach (var v in Neighbours(u.X, u.Y, a, b))
        {
            var alt = distances[u] + (uint) graph[v.X, v.Y];

            if (alt < distances[v])
            {
                queue.Enqueue(v, alt);
                distances[v] = alt;
            }
        }
    }

    return 0;
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