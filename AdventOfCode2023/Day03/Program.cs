using Common;


var lines = Helpers.ReadInputFile();

List<Number> numbers = [];

for (int y = 0; y < lines.Count; y++)
{
    var line = lines[y];

    var index = 0;

    while (true)
    {
        var (start, end) = FindNextNumber(line, index);
        if (start == -1)
        {
            break;
        }

        var number = int.Parse(line[start..end]);

        numbers.Add(new(start, end - 1, y, number));

        index = end;
    }
}


var (minX, maxX, minY, maxY) = (0, lines[0].Length - 1, 0, lines.Count - 1);

var resultOne = 0L;

foreach (var number in numbers)
{
    var startX = Math.Max(number.StartX - 1, minX);
    var endX = Math.Min(number.EndX + 1, maxX);
    var startY = Math.Max(number.Y - 1, minY);
    var endY = Math.Min(number.Y + 1, maxY);

    for (var y = startY; y <= endY; y++)
    {
        for (var x = startX; x <= endX; x++)
        {
            var character = lines[y][x];

            if (!char.IsAsciiDigit(character) && character != '.')
            {
                resultOne += number.Value;
            }
        }
    }
}

Console.WriteLine(resultOne);



List<(int X, int Y)> gears = [];

for (int y = 0; y < lines.Count; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        if (lines[y][x] == '*')
        {
            gears.Add((x, y));
        }
    }
}


var resultTwo = 0L;

foreach (var (X, Y) in gears)
{
    HashSet<Number> adjacentNumbers = [];

    for (var y = Y - 1; y <= Y + 1; y++)
    {
        for (var x = X - 1; x <= X + 1; x++)
        {
            foreach (var number in numbers)
            {
                if (number.Y == y && number.StartX <= x && number.EndX >= x)
                {
                    adjacentNumbers.Add(number);
                    break;
                }
            }
        }
    }


    if (adjacentNumbers.Count == 2)
    {
        var multiply = 1L;

        foreach (var number in adjacentNumbers)
        {
            multiply *= number.Value;
        }

        resultTwo += multiply;
    }
}

Console.WriteLine(resultTwo);




static (int Start, int End) FindNextNumber(string input, int start)
{
    var x = IndexOfDigit(input, start);
    if (x == -1) { return (-1, -1); }

    var end = x;

    while (end < input.Length && char.IsAsciiDigit(input[end])) end++;

    return (x, end);
}

static int IndexOfDigit(string input, int start = 0)
{
    for (int i = start; i < input.Length; i++)
    {
        if (char.IsAsciiDigit(input[i]))
        {
            return i;
        }
    }

    return -1;
}

record Number(int StartX, int EndX, int Y, int Value);