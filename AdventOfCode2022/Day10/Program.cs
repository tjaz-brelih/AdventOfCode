using var file = new StreamReader("input.txt");

List<string> input = new();

while (file.ReadLine() is string line)
{
    input.Add(line);
}


// Part 1
var x = 1;
var cycle = 1;
var resultOne = 0;

var cycles = new int[] { 20, 60, 100, 140, 180, 220 };

foreach (var line in input)
{
    var span = line.AsSpan();

    resultOne = UpdateSignalStrength(cycles, cycle++, x, resultOne);

    if (span[0] == 'a')
    {
        resultOne = UpdateSignalStrength(cycles, cycle++, x, resultOne);
        x += int.Parse(span[5..]);
    }
}

Console.WriteLine(resultOne);


// Part 2
x = 1;
cycle = 1;

HashSet<(int, int)> pixels = new();

foreach (var line in input)
{
    var span = line.AsSpan();

    DrawPixel(pixels, x, cycle++);

    if (span[0] == 'a')
    {
        DrawPixel(pixels, x, cycle++);
        x += int.Parse(span[5..]);
    }
}

for (int i = 0; i < 40; i++)
{
    for (int j = 0; j < 6; j++)
    {
        Console.Write(pixels.Contains((j, i)) ? '#' : '.');
    }

    Console.WriteLine();
}




static int UpdateSignalStrength(int[] cycles, int cycle, int x, int strength)
    => strength + (cycles.Contains(cycle) ? cycle * x : 0);

static void DrawPixel(HashSet<(int, int)> pixels, int x, int cycle)
{
    var posX = (cycle - 1) % 40;

    if (Enumerable.Range(x - 1, 3).Contains(posX))
    {
        pixels.Add((posX, (cycle - 1) / 40));
    }
}
