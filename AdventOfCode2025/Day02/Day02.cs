using Common;

var lines = Helpers.ReadInputFile();

List<(ulong Min, ulong Max)> ranges = [.. lines
    .SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))
    .Select(x =>
    {
        var span = x.AsSpan();
        var index = span.IndexOf('-');
        return (ulong.Parse(span[..index]), ulong.Parse(span[(index + 1)..]));
    })];



var resultOne = 0UL;

foreach (var (min, max) in ranges)
{
    for (var i = min; i <= max; i++)
    {
        var span = i.ToString().AsSpan();
        if (span.Length % 2 != 0) { continue; }

        var middle = span.Length / 2;

        var first = span[..middle];
        var second = span[middle..];

        if (first.SequenceEqual(second)) { resultOne += i; }
    }
}

Console.WriteLine(resultOne);



var resultTwo = 0UL;

foreach (var (min, max) in ranges)
{
    for (var i = min; i <= max; i++)
    {
        var span = i.ToString().AsSpan();

        foreach (var divisor in Helpers.GetDivisors(span.Length))
        {
            if (divisor == span.Length) { continue; }

            var parts = span.Length / divisor;
            var match = true;

            for (var j = 0; j < parts - 1; j++)
            {
                var start = j * divisor;
                var middle = (j + 1) * divisor;
                var end = (j + 2) * divisor;

                var first = span[start..middle];
                var second = span[middle..end];

                if (!first.SequenceEqual(second)) { match = false; break; }
            }

            if (match) { resultTwo += i; break; }
        }
    }
}

Console.WriteLine(resultTwo);