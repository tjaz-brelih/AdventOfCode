using Common;
using Map = (long Destination, long SourceStart, long SourceEnd);

var lines = Helpers.ReadInputFile();


List<List<Map>> maps = [[], [], [], [], [], [], []];

var seeds = lines[0][7..].Split(' ').Select(long.Parse).ToArray();

var lineIndex = 1;

for (int i = 0; i < maps.Count; i++)
{
    lineIndex += 2;

    while (lineIndex < lines.Count && !string.IsNullOrEmpty(lines[lineIndex]))
    {
        var line = lines[lineIndex];

        var map = line.Split(' ').Select(long.Parse).ToArray();

        maps[i].Add((map[0], map[1], map[1] + map[2]));

        lineIndex++;
    }

    maps[i].Sort(CompareMapsBySourceStart);
}


var resultOne = long.MaxValue;

foreach (var seed in seeds)
{
    var value = seed;

    foreach (var map in maps)
    {
        value = Map(map, value);
    }

    if (value < resultOne)
    {
        resultOne = value;
    }
}

Console.WriteLine(resultOne);



var resultTwo = long.MaxValue;
var o = new object();

for (int i = 0; i < seeds.Length; i += 2)
{
    var seedStart = seeds[i];
    var seedRange = seeds[i + 1];

    // More powerful bruteforce, still takes a handful of minutes to complete
    Parallel.For(seedStart, seedStart + seedRange, (input) =>
    {
        var value = input;

        foreach (var map in maps)
        {
            value = Map(map, value);
        }

        lock (o)
        {
            if (value < resultTwo)
            {
                resultTwo = value;
            }
        }
    });
}

Console.WriteLine(resultTwo);




static long Map(List<Map> maps, long input)
{
    foreach (var (destination, sourceStart, sourceEnd) in maps)
    {
        if (sourceStart > input) break;

        if (sourceStart <= input && sourceEnd > input)
        {
            return (input - sourceStart) + destination;
        }
    }

    return input;
}

static int CompareMapsBySourceStart(Map x, Map y) => x.SourceStart.CompareTo(y.SourceStart);