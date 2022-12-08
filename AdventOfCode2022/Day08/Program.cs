using var file = new StreamReader("input.txt");

List<string> input = new();

while (file.ReadLine() is string line)
{
    input.Add(line);
}

var dimensions = (Height: input.Count, Width: input[0].Length);

int[,] trees = new int[dimensions.Width, dimensions.Height];

for (int i = 0; i < dimensions.Height; i++)
{
    var line = input[i];
    for (int j = 0; j < dimensions.Width; j++)
    {
        trees[j, i] = line[j];
    }
}


// Part 1
var resultOne = dimensions.Height * 2 + dimensions.Width * 2 - 4;

for (int i = 1; i < dimensions.Height - 1; i++)
{
    for (int j = 1; j < dimensions.Width - 1; j++)
    {
        if (IsTreeVisible(dimensions, (j, i), trees))
        {
            resultOne++;
        }
    }
}

Console.WriteLine(resultOne);


// Part 2
var resultTwo = 0;

for (int i = 1; i < dimensions.Height - 1; i++)
{
    for (int j = 1; j < dimensions.Width - 1; j++)
    {
        resultTwo = Math.Max(resultTwo, ScenicScore(dimensions, (j, i), trees));
    }
}

Console.WriteLine(resultTwo);




static bool IsTreeVisible((int, int) dimensions, (int, int) coords, int[,] trees)
    => CalculateStuff(dimensions, coords, trees, 0).IsTreeVisible
       ||
       CalculateStuff(dimensions, coords, trees, 1).IsTreeVisible
       ||
       CalculateStuff(dimensions, coords, trees, 2).IsTreeVisible
       ||
       CalculateStuff(dimensions, coords, trees, 3).IsTreeVisible;

static int ScenicScore((int, int) dimensions, (int, int) coords, int[,] trees)
    => Enumerable.Range(0, 4).Aggregate(1, (acc, x) => acc * CalculateStuff(dimensions, coords, trees, x).ViewingDistance);

static (int ViewingDistance, bool IsTreeVisible) CalculateStuff((int, int) dimensions, (int, int) coords, int[,] trees, int direction)
{
    var (height, width) = dimensions;
    var (x, y) = coords;

    var directionFunc = DirectionFunc(direction);
    var treeHeight = trees[x, y];

    var viewingDistance = 1;

    while (true)
    {
        (x, y) = directionFunc(x, y);

        if (trees[x, y] >= treeHeight)
        {
            return (viewingDistance, false);
        }

        if (x <= 0 || x >= width - 1 || y <= 0 || y >= height - 1)
        {
            return (viewingDistance, true);
        }

        viewingDistance++;
    }
}

static Func<int, int, (int, int)> DirectionFunc(int direction)
    => direction switch
    {
        0 => (x, y) => (x, y - 1),
        1 => (x, y) => (x + 1, y),
        2 => (x, y) => (x, y + 1),
        3 => (x, y) => (x - 1, y),
        _ => throw new Exception()
    };
