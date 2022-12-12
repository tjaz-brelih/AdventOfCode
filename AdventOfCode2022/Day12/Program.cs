using var file = new StreamReader("input.txt");

List<string> input = new();

while (file.ReadLine() is string line)
{
    input.Add(line);
}

var (height, width) = (input.Count, input[0].Length);

var start = (0, 0);
var end = (0, 0);

int[,] heights = new int[width, height];

for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        (heights[j, i], start, end) = input[i][j] switch
        {
            'S' => (0, (j, i), end),
            'E' => (25, start, (j, i)),
            var x => (x - 97, start, end)
        };
    }
}


// Part 1
Dictionary<(int, int), List<(int, int)>> possibleMoves = new();

for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        var elevation = heights[j, i];
        var moves = new List<(int, int)>();

        if (i - 1 >= 0 && heights[j, i - 1] < elevation + 1)
        {
            moves.Add((j, i - 1));
        }

        if (i + 1 < height && heights[j, i + 1] < elevation + 1)
        {
            moves.Add((j, i + 1));
        }

        if (j - 1 >= 0 && heights[j - 1, i] < elevation + 1)
        {
            moves.Add((j - 1, i));
        }

        if (j + 1 < width && heights[j + 1, i] < elevation + 1)
        {
            moves.Add((j + 1, i));
        }

        possibleMoves[(j, i)] = moves;
    }
}

var resultOne = FindShortestPath(possibleMoves, start, end);
Console.WriteLine(resultOne);


// Part 2
var resultTwo = int.MaxValue;

for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        if (heights[j, i] == 0)
        {
            var path = FindShortestPath(possibleMoves, (j, i), end);
            if (path > -1 && path < resultTwo)
            {
                resultTwo = path;
            }
        }
    }
}

Console.WriteLine(resultTwo);



static int FindShortestPath(Dictionary<(int, int), List<(int, int)>> possibleMoves, (int x, int y) start, (int, int) end)
{
    Queue<(int x, int y, int path)> queue = new();
    HashSet<(int, int)> visited = new()
    {
        start
    };

    queue.Enqueue((start.x, start.y, 0));

    while (queue.Count > 0)
    {
        var (x, y, path) = queue.Dequeue();

        if ((x, y) == end)
        {
            return path;
        }

        foreach (var newPosition in possibleMoves[(x, y)])
        {
            if (!visited.Contains(newPosition))
            {
                visited.Add(newPosition);
                queue.Enqueue((newPosition.Item1, newPosition.Item2, path + 1));
            }
        }
    }

    return -1;
}
