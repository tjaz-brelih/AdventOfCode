using Common;


var lines = Helpers.ReadInputFile();
List<List<char>> grid = [.. lines.Select(line => line.ToList())];


var resultOne = 0;

for (int y = 0; y < grid.Count; y++)
{
    for (int x = 0; x < grid[y].Count; x++)
    {
        if (grid[y][x] != '@') { continue; }

        var neighbors = GetNeighbors(x, y)
            .Where(c => c.X >= 0 && c.X < grid[y].Count && c.Y >= 0 && c.Y < grid.Count)
            .Count(c => grid[c.Y][c.X] == '@');

        if (neighbors < 4) { resultOne++; }
    }
}

Console.WriteLine(resultOne);



var resultTwo = 0;

while (true)
{
    var removed = 0;

    for (int y = 0; y < grid.Count; y++)
    {
        for (int x = 0; x < grid[y].Count; x++)
        {
            if (grid[y][x] != '@') { continue; }

            var neighbors = GetNeighbors(x, y)
                .Where(c => c.X >= 0 && c.X < grid[y].Count && c.Y >= 0 && c.Y < grid.Count)
                .Count(c => grid[c.Y][c.X] == '@');

            if (neighbors < 4)
            {
                grid[y][x] = 'x';
                removed++;
            }
        }
    }

    if (removed == 0) { break; }
    resultTwo += removed;
}

Console.WriteLine(resultTwo);




static IEnumerable<(int X, int Y)> GetNeighbors(int x, int y)
{
    var directions = new (int dx, int dy)[]
    {
        (-1, -1), (0, -1), (1, -1),
        (-1,  0),          (1,  0),
        (-1,  1), (0,  1), (1,  1)
    };

    foreach (var (dx, dy) in directions)
    {
        yield return (x + dx, y + dy);
    }
}