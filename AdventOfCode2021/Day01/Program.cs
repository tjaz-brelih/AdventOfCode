using var file = new StreamReader("input.txt");

List<uint> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(uint.Parse(line));
}



var zip = lines.Append(uint.MaxValue).Zip(lines.Skip(1));

Console.WriteLine(zip.Count(t => t.Second > t.First));



uint prevSum = 0;
int largerCount = -1;

for (int i = 0; i < lines.Count - 2; i++)
{
    var curSum = lines[i] + lines[i + 1] + lines[i + 2];

    if (curSum > prevSum)
    {
        largerCount++;
    }
    
    prevSum = curSum;
}

Console.WriteLine(largerCount);