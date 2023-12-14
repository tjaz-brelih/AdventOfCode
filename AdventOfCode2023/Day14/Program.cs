using Common;

var lines = Helpers.ReadInputFile();
//var lines = Helpers.ReadInputFile("test.txt");


HashSet<Loc> rocksRounded = [];
HashSet<Loc> rocksSquared = [];

for (int y = 0; y < lines.Count; y++)
{
    var line = lines[y];
    if (string.IsNullOrEmpty(line)) { continue; }

    for (int x = 0; x < line.Length; x++)
    {
        _ = line[x] switch
        {
            'O' => rocksRounded.Add(new() { X = x, Y = y }),
            '#' => rocksSquared.Add(new() { X = x, Y = y }),
            _ => false
        };
    }
}



var maxLoad = lines.Count;
var resultOne = 0L;

for (int i = 0; i < lines[0].Length; i++)
{
    var rocks = rocksSquared.Where(x => x.X == i).ToArray();
    var y = -1;

    while (y < maxLoad)
    {
        var nextRock = rocks.Where(x => x.Y > y).MinBy(x => x.Y)?.Y ?? maxLoad;
        var rocksRoundedCount = rocksRounded.Count(x => x.X == i && y < x.Y && x.Y < nextRock);

        for (int j = 0; j < rocksRoundedCount; j++) { resultOne += maxLoad - (y + 1) - j; }

        y = nextRock;
    }
}

Console.WriteLine(resultOne);




class Loc
{
    public required int X { get; set; }
    public required int Y { get; set; }
}