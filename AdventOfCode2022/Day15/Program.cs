using var file = new StreamReader("input.txt");

List<(int, int, int)> sensors = new();

while (file.ReadLine() is string line)
{
    var span = line.AsSpan();

    var indexOne = span.IndexOf(',');
    var sensorX = int.Parse(span[12..indexOne]);

    var indexTwo = span.IndexOf(':');
    var sensorY = int.Parse(span[(indexOne + 4)..indexTwo]);

    span = span[indexTwo..];

    indexOne = span.IndexOf(',');
    var beaconX = int.Parse(span[25..indexOne]);
    var beaconY = int.Parse(span[(indexOne + 4)..]);

    var distance = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);

    sensors.Add((sensorX, sensorY, distance));
}


// Part 1
var targetY = 2000000;
List<(int, int)> ranges = new();

foreach (var (sensorX, sensorY, distanceBeacon) in sensors)
{
    var distanceY = Math.Abs(sensorY - targetY);

    if (distanceBeacon >= distanceY)
    {
        var width = distanceBeacon - distanceY;
        ranges.Add((sensorX - width, sensorX + width));
    }
}

var resultOne = MergeRanges(ranges).Sum(x => Math.Abs(x.Item2 - x.Item1));
Console.WriteLine(resultOne);


// Part 2
var (min, max) = (0, 4000000);
var (distressX, distressY) = (0, -1);

for (int currentY = min; currentY <= max; currentY++)
{
    ranges.Clear();

    foreach (var (sensorX, sensorY, distanceBeacon) in sensors)
    {
        var distanceY = Math.Abs(sensorY - currentY);

        if (distanceBeacon >= distanceY)
        {
            var width = distanceBeacon - distanceY;
            var (start, end) = (sensorX - width, sensorX + width);

            if ((start >= min && start <= max) || (end >= min && end <= max))
            {
                ranges.Add((Math.Max(start, min), Math.Min(end, max)));
            }
        }
    }

    var enumerator = MergeRanges(ranges).GetEnumerator();

    enumerator.MoveNext();
    var (x, y) = enumerator.Current;

    if (x == min + 1)
    {
        (distressX, distressY) = (min, currentY);
        break;
    }

    if (y == max - 1)
    {
        (distressX, distressY) = (max, currentY);
        break;
    }

    if (enumerator.MoveNext())
    {
        (distressX, distressY) = (y + 1, currentY);
        break;
    }
}

var resultTwo = (ulong) distressX * 4000000ul + (ulong) distressY;
Console.WriteLine(resultTwo);




static IEnumerable<(int X, int Y)> MergeRanges(List<(int, int)> ranges)
{
    if (ranges.Count == 0)
    {
        yield break;
    }

    ranges.Sort(Compare);
    var enumerator = ranges.GetEnumerator();

    enumerator.MoveNext();
    var (prevStart, prevEnd) = enumerator.Current;
    var currentStart = prevStart;

    while (enumerator.MoveNext())
    {
        var (start, end) = enumerator.Current;

        if (start > prevEnd + 1)
        {
            yield return (currentStart, prevEnd);
            currentStart = start;
        }

        prevStart = start;
        prevEnd = Math.Max(end, prevEnd);
    }

    yield return (currentStart, prevEnd);
}

static int Compare((int, int) x, (int, int) y) => x.Item1.CompareTo(y.Item1);