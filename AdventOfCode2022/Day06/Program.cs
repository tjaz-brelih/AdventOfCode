using var file = new StreamReader("input.txt");

List<string> input = new();

while (file.ReadLine() is string line)
{
    input.Add(line);
}


// Part 1
var resultOne = 0;
var length = 4;
var set = new HashSet<char>();

foreach (var line in input)
{
    var span = line.AsSpan();

    for (int i = 0; i < span.Length - length; i++)
    {
        set.Clear();
        var subSpan = span[i..(i + length)];
        FillSet(set, subSpan);

        if (set.Count == subSpan.Length)
        {
            resultOne = i + length;
            break;
        }
    }

    Console.WriteLine(resultOne);
}


// Part 2
var resultTwo = 0;
length = 14;

foreach (var line in input)
{
    var span = line.AsSpan();

    for (int i = 0; i < span.Length - length; i++)
    {
        set.Clear();
        var subSpan = span[i..(i + length)];
        FillSet(set, subSpan);

        if (set.Count == subSpan.Length)
        {
            resultTwo = i + length;
            break;
        }
    }

    Console.WriteLine(resultTwo);
}




static void FillSet<T>(HashSet<T> set, ReadOnlySpan<T> input)
{
    foreach (var c in input)
    {
        set.Add(c);
    }
}