using Day12;


using var file = new StreamReader("input.txt");

List<Cave> caves = new();

while (file.ReadLine() is string line)
{
    var tokens = line.Split('-');

    var cave1 = caves.SingleOrDefault(c => c.Name == tokens[0]);
    if (cave1 is null)
    {
        cave1 = new Cave(tokens[0]);
        caves.Add(cave1);
    }

    var cave2 = caves.SingleOrDefault(c => c.Name == tokens[1]);
    if (cave2 is null)
    {
        cave2 = new Cave(tokens[1]);
        caves.Add(cave2);
    }

    cave1.ConnectedCaves.Add(cave2);
    cave2.ConnectedCaves.Add(cave1);
}



var resultOne = TraverseCaves(caves.Single(c => c.Name == "start"), new());
Console.WriteLine(resultOne);



var resultTwo = TraverseCaves2(caves.Single(c => c.Name == "start"), new());
Console.WriteLine(resultTwo);



static int TraverseCaves(Cave cave, List<Cave> path)
{
    if (path.Contains(cave) && cave.IsSmall)
    {
        return 0;
    }

    if (cave.Name == "end")
    {
        return 1;
    }

    path.Add(cave);
    return cave.ConnectedCaves.Where(c => c.Name != "start").Sum(c => TraverseCaves(c, path!.ToList()));
}


static int TraverseCaves2(Cave cave, List<Cave> path, bool hasVisitedSmallCave = false)
{
    if (path.Contains(cave) && cave.IsSmall && hasVisitedSmallCave)
    {
        return 0;
    }

    if (cave.Name == "end")
    {
        return 1;
    }

    hasVisitedSmallCave = hasVisitedSmallCave || path.Contains(cave) && cave.IsSmall;
    path.Add(cave);

    return cave.ConnectedCaves.Where(c => c.Name != "start").Sum(c => TraverseCaves2(c, path!.ToList(), hasVisitedSmallCave));
}