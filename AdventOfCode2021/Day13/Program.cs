using var file = new StreamReader("input.txt");

List<(int, int)> dots = new();

while (file.ReadLine() is string line && !string.IsNullOrEmpty(line))
{
    var coordinates = line.Split(',');
    dots.Add((int.Parse(coordinates[0]), int.Parse(coordinates[1])));
}

List<(char Axis, int Line)> folds = new();

while(file.ReadLine() is string line)
{
    var axis = line[11];
    var coordinate = int.Parse(line.AsSpan()[13..]);
    
    folds.Add((axis, coordinate));
}



var (foldAxis, foldLine) = folds.First();
HashSet<(int, int)> visibleDots = new();

foreach (var (x, y) in dots)
{
    var newPoint = foldAxis switch
    {
        'x' => (Fold(x, foldLine), y),
        _ => (x, Fold(y, foldLine))
    };

    visibleDots.Add(newPoint);
}

var resultOne = visibleDots.Count;
Console.WriteLine(resultOne);



visibleDots = dots.ToHashSet();
List<(int, int)> previousDots = new();

foreach (var (axis, line) in folds)
{
    previousDots.Clear();
    previousDots.AddRange(visibleDots);

    visibleDots.Clear();

    foreach (var (x, y) in previousDots)
    {
        var newPoint = axis switch
        {
            'x' => (Fold(x, line), y),
            _ => (x, Fold(y, line))
        };

        visibleDots.Add(newPoint);
    }
}

var maxX = visibleDots.Max(t => t.Item1);
var maxY = visibleDots.Max(t => t.Item2);

for (int i = 0; i <= maxY; i++)
{
    for (int j = 0; j <= maxX; j++)
    {
        Console.Write(visibleDots.Contains((j, i)) ? '#' : '.');
    }
    Console.WriteLine();
}



static int Fold(int point, int fold)
    => point < fold ? point : 2 * fold - point;