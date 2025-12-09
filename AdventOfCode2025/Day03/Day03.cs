using Common;

var lines = Helpers.ReadInputFile();

var resultOne = 0;

foreach (var line in lines)
{
    var span = line.AsSpan();

    var maxIndex = 0;
    var max = span[0];

    for (var i = 1; i < span.Length - 1; i++)
    {
        if (span[i] > max) { max = span[i]; maxIndex = i; }
    }

    max = span[maxIndex + 1];

    for (var i = maxIndex + 2; i < span.Length; i++)
    {
        if (span[i] > max) { max = span[i]; }
    }

    resultOne += (span[maxIndex] - '0') * 10 + (max - '0');
}

Console.WriteLine(resultOne);



var resultTwo = 0L;

foreach (var line in lines)
{
    var span = line.AsSpan();

    var indices = new int[12];
    var startIndex = 0;

    for (var i = 0; i < indices.Length; i++)
    {
        indices[i] = startIndex;

        for (var x = startIndex; x <= span.Length - indices.Length + i; x++)
        {
            if (span[x] > span[indices[i]]) { indices[i] = x; }
        }

        startIndex = indices[i] + 1;
    }

    var val = indices.Select((x, i) => (long) Math.Pow(10, indices.Length - i - 1) * (long) char.GetNumericValue(line[x])).Sum();

    resultTwo += val;
}

Console.WriteLine(resultTwo);