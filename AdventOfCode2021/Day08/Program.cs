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
        { 1, debug.Single(d => d.Length == 2) },
        { 4, debug.Single(d => d.Length == 4) },
        { 7, debug.Single(d => d.Length == 3) },
        { 8, debug.Single(d => d.Length == 7) }
    };

    numberMap.Add(3, debug.First(d => d.Length == 5 && d.Intersect(numberMap[1]).Count() == 2));
    numberMap.Add(6, debug.First(d => d.Length == 6 && d.Intersect(numberMap[1]).Count() == 1));
    numberMap.Add(9, debug.First(d => d.Length == 6 && d.Intersect(numberMap[3]).Count() == numberMap[3].Length));
    numberMap.Add(5, debug.First(d => d.Length == 5 && numberMap[9].Except(d).Count() == 1 && d != numberMap[3]));
    numberMap.Add(2, debug.First(d => d.Length == 5 && d != numberMap[3] && d != numberMap[5]));
    numberMap.Add(0, debug.First(d => !numberMap.ContainsValue(d)));

    uint partialSum = 0;
    for (int i = 0; i < 4; i++)
    {
        var number = numberMap.Single(n => n.Value.Length == output[i].Length && n.Value.Intersect(output[i]).Count() == output[i].Length).Key;
        partialSum += (uint) number * (uint) Math.Pow(10, 3 - i);
    }
    resultTwo += partialSum;
}

Console.WriteLine(resultTwo);