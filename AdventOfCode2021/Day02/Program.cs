using var file = new StreamReader("input.txt");

List<(string, int)> lines = new();

while (file.ReadLine() is string line)
{
    var tokens = line.Split(' ');
    lines.Add((tokens[0], int.Parse(tokens[1])));
}


var (X1, Y1) = lines.Aggregate((X: 0, Y: 0), (a, t) => t.Item1 switch
{
    "forward" => (a.X + t.Item2, a.Y),
    "up" => (a.X, a.Y - t.Item2),
    "down" => (a.X, a.Y + t.Item2),
    _ => a
});

Console.WriteLine(X1 * Y1);



var (X2, Y2, _) = lines.Aggregate((X: 0, Y: 0, A: 0), (a, t) => t.Item1 switch
{
    "forward" => (a.X + t.Item2, a.Y + a.A * t.Item2, a.A),
    "up" => (a.X, a.Y, a.A - t.Item2),
    "down" => (a.X, a.Y, a.A + t.Item2),
    _ => a
});

Console.WriteLine(X2 * Y2);