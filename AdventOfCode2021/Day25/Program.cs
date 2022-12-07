using var file = new StreamReader("input.txt");

HashSet<(int, int)> east = new();
HashSet<(int, int)> south = new();

var y = 0;
var maxX = 0;
var maxY = 0;

while (file.ReadLine() is string line)
{
    maxX = line.Length - 1;

    for (int i = 0; i < line.Length; i++)
    {
        _ = line[i] switch
        {
            '>' => east.Add((i, y)),
            'v' => south.Add((i, y)),
            '.' => true,
            _ => throw new Exception()
        };
    }

    y++;
}

maxY = y - 1;

// Part 1
var steps = 1;

HashSet<(int, int)> newEast = new();
HashSet<(int, int)> newSouth = new();

while (true)
{
    var movements = 0;

    foreach (var entity in east)
    {
        var posX = (entity.Item1 + 1 > maxX) ? 0 : (entity.Item1 + 1);
        var pos = (posX, entity.Item2);

        if (!east.Contains(pos) && !south.Contains(pos))
        {
            newEast.Add(pos);
            movements++;
        }
        else
        {
            newEast.Add(entity);
        }
    }

    foreach (var entity in south)
    {
        var posY = (entity.Item2 + 1 > maxY) ? 0 : (entity.Item2 + 1);
        var pos = (entity.Item1, posY);

        if (!newEast.Contains(pos) && !south.Contains(pos))
        {
            newSouth.Add(pos);
            movements++;
        }
        else
        {
            newSouth.Add(entity);
        }
    }

    east = newEast.ToHashSet();
    south = newSouth.ToHashSet();

    newEast.Clear(); newSouth.Clear();

    if (movements == 0)
    {
        break;
    }

    steps++;
}

Console.WriteLine(steps);


static void Print(int maxX, int maxY, IEnumerable<(int x, int y)> east, IEnumerable<(int x, int y)> south)
{
    for (int i = 0; i < maxY + 1; i++)
    {
        for (int j = 0; j < maxX + 1; j++)
        {
            if (east.Contains((j, i)))
            {
                Console.Write('>');
            }
            else if (south.Contains((j, i)))
            {
                Console.Write('v');
            }
            else
            {
                Console.Write('.');
            }
        }

        Console.WriteLine();
    }
}