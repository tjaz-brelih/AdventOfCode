using Common;

var lines = Helpers.ReadInputFile();


List<(int X, int Y)> galaxies = [];

for (int y = 0; y < lines.Count; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        if (lines[y][x] == '#') galaxies.Add((x, y));
    }
}

var emptyRows = Enumerable.Range(0, lines.Count).ToHashSet();
emptyRows.ExceptWith(galaxies.Select(x => x.Y).Distinct());

var emptyColumns = Enumerable.Range(0, lines[0].Length).ToHashSet();
emptyColumns.ExceptWith(galaxies.Select(x => x.X).Distinct());

var resultOne = 0L;

for (int i = 0; i < galaxies.Count - 1; i++)
{
    for (int j = i + 1; j < galaxies.Count; j++)
    {
        var (startX, endX) = MinMax(galaxies[i].X, galaxies[j].X);
        var (startY, endY) = MinMax(galaxies[i].Y, galaxies[j].Y);

        var distance = endX - startX + endY - startY 
            + emptyColumns.Count(x => startX < x && endX > x)
            + emptyRows.Count(y => startY < y && endY > y);

        resultOne += distance;
    }
}

Console.WriteLine(resultOne);



var resultTwo = 0L;

for (int i = 0; i < galaxies.Count - 1; i++)
{
    for (int j = i + 1; j < galaxies.Count; j++)
    {
        var (startX, endX) = MinMax(galaxies[i].X, galaxies[j].X);
        var (startY, endY) = MinMax(galaxies[i].Y, galaxies[j].Y);

        var distance = endX - startX + endY - startY
            + (emptyColumns.Count(x => startX < x && endX > x) * 999_999L)
            + (emptyRows.Count(y => startY < y && endY > y) * 999_999L);

        resultTwo += distance;
    }
}

Console.WriteLine(resultTwo);




static (int, int) MinMax(int a, int b) => a < b ? (a, b) : (b, a);