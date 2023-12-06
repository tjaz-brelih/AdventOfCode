using Common;

var lines = Helpers.ReadInputFile();


var times = lines[0][5..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
var recordDistances = lines[1][9..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

List<long> ways = [];

for (int i = 0; i < times.Length; i++)
{
    var t = times[i];
    var recordDistance = recordDistances[i];

    var currentWays = 0;

    for (int j = 1; j < t; j++)
    {
        var d = j * (t - j);

        if (d > recordDistance)
        {
            currentWays++;
        }
    }

    ways.Add(currentWays);
}

var resultOne = ways.Aggregate(1L, (acc, x) => acc * x);
Console.WriteLine(resultOne);



var time = long.Parse(string.Concat(lines[0][5..].Split(' ', StringSplitOptions.RemoveEmptyEntries)));
var distance = long.Parse(string.Concat(lines[1][9..].Split(' ', StringSplitOptions.RemoveEmptyEntries)));

var resultTwo = 0L;

for (int i = 1; i < time; i++)
{
    var d = i * (time - i);

    if (d > distance)
    {
        resultTwo++;
    }
}

Console.WriteLine(resultTwo);