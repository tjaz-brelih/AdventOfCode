using Common;


var lines = Helpers.ReadInputFile();


var (width, height) = (lines[0].Length, lines.Count);

HashSet<Coordinates> obstacles = [];
Coordinates startingGuardLocation = new(0, 0);

for (int i = 0; i < height; i++)
{
    for (var j = 0; j < width; j++)
    {
        if (lines[i][j] == '#') obstacles.Add(new Coordinates(i, j));
        if (lines[i][j] == '^') startingGuardLocation = new Coordinates(i, j);
    }
}



HashSet<Coordinates> visited = [];
Coordinates direction = new(-1, 0); // Up
var guardLocation = startingGuardLocation;

while (true)
{
    visited.Add(guardLocation);

    var nextGuardLocation = guardLocation + direction;
    var (x, y) = (nextGuardLocation.X, nextGuardLocation.Y);

    if (x >= height || x < 0 || y >= width || y < 0) { break; }

    if (!obstacles.Contains(nextGuardLocation)) { guardLocation = nextGuardLocation; continue; }

    direction = Turn(direction);
}

var resultOne = visited.Count;
Console.WriteLine(resultOne);



var resultTwo = 0;
visited.Remove(startingGuardLocation);

foreach (var obstacle in visited)
{
    HashSet<(Coordinates Visited, Coordinates Direction)> visitedDirection = [];
    direction = new(-1, 0);
    guardLocation = startingGuardLocation;

    while (true)
    {
        if (visitedDirection.Contains((guardLocation, direction))) { resultTwo++; break; } // Deja Vu, I've just been in this place before

        visitedDirection.Add((guardLocation, direction));

        var nextGuardLocation = guardLocation + direction;
        var (x, y) = (nextGuardLocation.X, nextGuardLocation.Y);

        if (x >= height || x < 0 || y >= width || y < 0) { break; } // Normal exit

        if (!(obstacles.Contains(nextGuardLocation) || nextGuardLocation == obstacle)) { guardLocation = nextGuardLocation; continue; }

        direction = Turn(direction);
    }
}

Console.WriteLine(resultTwo);



static Coordinates Turn(Coordinates direction) => direction switch
{
    (-1, 0) => new(0, 1),
    (0, 1) => new(1, 0),
    (1, 0) => new(0, -1),
    _ => new(-1, 0)
};

record Coordinates(int X, int Y)
{
    public static Coordinates operator +(Coordinates a, Coordinates b) => new (a.X + b.X, a.Y + b.Y);
}