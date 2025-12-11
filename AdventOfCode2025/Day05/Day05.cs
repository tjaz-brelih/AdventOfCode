using Common;

using Range = (ulong Min, ulong Max);


var lines = Helpers.ReadInputFile();

List<Range> ranges = [];
List<ulong> ids = [];

var readingRanges = true;

foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        readingRanges = false;
        continue;
    }

    if (readingRanges)
    {
        var span = line.AsSpan();
        var index = span.IndexOf('-');

        ranges.Add((ulong.Parse(span[..index]), ulong.Parse(span[(index + 1)..])));
    }
    else
    {
        ids.Add(ulong.Parse(line));
    }
}



var resultOne = 0;

foreach (var id in ids)
{
    if (ranges.Any(x => id >= x.Min && id <= x.Max)) { resultOne++; }
}

Console.WriteLine(resultOne);



while (true)
{
    var merged = false;

    for (var i = 0; i < ranges.Count - 1; i++)
    {
        for (var j = i + 1; j < ranges.Count; j++)
        {
            var (r1, r2) = (ranges[i], ranges[j]);

            if (r1.Min <= r2.Max && r2.Min <= r1.Max)
            {
                // Ranges overlap
                var m = (Math.Min(r1.Min, r2.Min), Math.Max(r1.Max, r2.Max));

                ranges.Remove(r1);
                ranges.Remove(r2);
                ranges.Add(m);

                merged = true;
            }
        }
    }

    if (!merged) { break; }
}

var resultTwo = ranges.Aggregate(0ul, (acc, x) => acc + x.Max - x.Min + 1);

Console.WriteLine(resultTwo);
