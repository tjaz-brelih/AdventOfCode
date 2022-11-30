using var file = new StreamReader("input.txt");

List<int> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(int.Parse(line));
}
