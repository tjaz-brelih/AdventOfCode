using var file = new StreamReader("input.txt");

List<uint> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(uint.Parse(line));
}



var resultOne = lines.Append(uint.MaxValue)
    .Zip(lines.Skip(1))
    .Count(t => t.Second > t.First);
Console.WriteLine(resultOne);



var sums = lines.Append(uint.MaxValue).Append(uint.MaxValue)
    .Zip(
        lines.Skip(1).Append(uint.MaxValue),
        lines.Skip(2)
    )
    .Select(t => t.First + t.Second + t.Third);

var resultTwo = sums.Append(uint.MaxValue)
    .Zip(sums.Skip(1))
    .Count(t => t.Second > t.First);

Console.WriteLine(resultTwo);