using Common;
using Coord = (int X, int Y, int Z);

var lines = Helpers.ReadInputFile();

var coordinates = lines
    .Select(x => x.Split(','))
    .Select(x => (int.Parse(x[0]), int.Parse(x[1]), int.Parse(x[2])))
    .ToList();


List<(Coord, Coord)> pairs = [];

for (int i = 0; i < coordinates.Count - 1; i++)
{
    for (int j = i + 1; j < coordinates.Count; j++)
    {
        pairs.Add((coordinates[i], coordinates[j]));
    }
}



List<HashSet<Coord>> circuits = [];

foreach (var (first, second) in pairs.OrderBy(x => Distance(x.Item1, x.Item2)).Take(1000))
{
    var firstCircuit = circuits.FirstOrDefault(x => x.Contains(first));
    var secondCircuit = circuits.FirstOrDefault(x => x.Contains(second));

    if (firstCircuit is not null && secondCircuit is not null && firstCircuit != secondCircuit)
    {
        firstCircuit.UnionWith(secondCircuit);
        circuits.Remove(secondCircuit);
    }
    else if (firstCircuit is null && secondCircuit is null)
    {
        circuits.Add([first, second]);
    }
    else
    {
        firstCircuit?.Add(second);
        secondCircuit?.Add(first);
    }
}

var resultOne = circuits
    .Select(x => x.Count)
    .OrderDescending()
    .Take(3)
    .Aggregate(1, (acc, x) => acc * x);

Console.WriteLine(resultOne);



var resultTwo = 0;
circuits = [.. coordinates.Select(x => new HashSet<Coord> { x })];

foreach (var (first, second) in pairs.OrderBy(x => Distance(x.Item1, x.Item2)))
{
    var firstCircuit = circuits.FirstOrDefault(x => x.Contains(first));
    var secondCircuit = circuits.FirstOrDefault(x => x.Contains(second));

    if (firstCircuit is not null && secondCircuit is not null && firstCircuit != secondCircuit)
    {
        firstCircuit.UnionWith(secondCircuit);
        circuits.Remove(secondCircuit);

        if (circuits.Count == 1)
        {
            resultTwo = first.X * second.X;
            break;
        }
    }
    else if (firstCircuit is null && secondCircuit is null)
    {
        circuits.Add([first, second]);
    }
    else
    {
        firstCircuit?.Add(second);
        secondCircuit?.Add(first);
    }
}

Console.WriteLine(resultTwo);



static double Distance(Coord first, Coord second) =>
    Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2) + Math.Pow(first.Z - second.Z, 2);