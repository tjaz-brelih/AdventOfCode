using var file = new StreamReader("input.txt");

List<(Range, Range)> input = new();

while (file.ReadLine() is string line)
{
    var span = line.AsSpan();
    var comma = span.IndexOf(',');

    input.Add((GetRange(span[..comma]), GetRange(span[(comma + 1)..])));
}


// Part 1
var resultOne = 0;

foreach (var (first, second) in input)
{
    if (IsContained(first, second) || IsContained(second, first))
    {
        resultOne++;
    }
}

Console.WriteLine(resultOne);


// Part 2
var resultTwo = 0;

foreach (var (first, second) in input)
{
    if (IsOverlapped(first, second) || IsOverlapped(second, first))
    {
        resultTwo++;
    }
}

Console.WriteLine(resultTwo);




static Range GetRange(ReadOnlySpan<char> input)
{
    var index = input.IndexOf('-');

    return new Range(int.Parse(input[..index]), int.Parse(input[(index + 1)..]));
}

static bool IsContained(Range first, Range second)
    => first.Start.Value <= second.Start.Value && first.End.Value >= second.End.Value;

static bool IsOverlapped(Range first, Range second)
    => second.Start.Value >= first.Start.Value && second.Start.Value <= first.End.Value;