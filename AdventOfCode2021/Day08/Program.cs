using var file = new StreamReader("input.txt");

List<(string[] Debug, string[] Output)> lines = new();

while (file.ReadLine() is string line)
{
    var parts = line.Split('|');
    lines.Add((parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries), parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)));
}



var validLengths = new int[] { 2, 4, 3, 7 };
var resultOne = lines.Sum(l => l.Output.Count(o => validLengths.Contains(o.Length)));
Console.WriteLine(resultOne);



ulong resultTwo = 0;

foreach (var (debug, output) in lines)
{
    Dictionary<int, string> numberMap = new()
    {
        { 1, debug.First(d => d.Length == 2) },
        { 4, debug.First(d => d.Length == 4) },
        { 7, debug.First(d => d.Length == 3) },
        { 8, debug.First(d => d.Length == 7) }
    };

    var similarities = debug
        .Where(d => !numberMap.ContainsValue(d))
        .Select(d => (Number: d, Similarity(numberMap[1], d), Similarity(numberMap[4], d), Similarity(numberMap[7], d), Similarity(numberMap[8], d)));

    foreach (var item in similarities)
    {
        var number = item switch
        {
            (_, 2, 3, 3, 6) => 0,
            (_, 1, 2, 2, 5) => 2,
            (_, 2, 3, 3, 5) => 3,
            (_, 1, 3, 2, 5) => 5,
            (_, 1, 3, 2, 6) => 6,
            (_, 2, 4, 3, 6) => 9
        };

        numberMap.Add(number, item.Number);
    }

    uint partialSum = 0;
    for (int i = 0; i < 4; i++)
    {
        var number = numberMap.First(n => n.Value.Length == output[i].Length && n.Value.Intersect(output[i]).Count() == output[i].Length).Key;
        partialSum += (uint) number * (uint) Math.Pow(10, 3 - i);
    }
    resultTwo += partialSum;
}

Console.WriteLine(resultTwo);


static int Similarity(string a, string b)
    => a.Intersect(b).Count();
