using var file = new StreamReader("inputTest.txt");

List<(string, string)> packetPairs = new();

while (file.ReadLine() is string firstLine)
{
    var secondLine = file.ReadLine()!;

    packetPairs.Add((firstLine, secondLine));

    file.ReadLine();
}


// Part 1
var resultOne = 0;
var pair = 1;

foreach (var (first, second) in packetPairs)
{
    resultOne += CompareLists(first.AsSpan()[1..^1], second.AsSpan()[1..^1]) switch
    {
        true => pair,
        false => 0,
        _ => throw new Exception()
    };

    Console.WriteLine($"Pair {pair}");
    Console.WriteLine(resultOne);

    pair++;
}

Console.WriteLine(resultOne);




static bool? CompareLists(ReadOnlySpan<char> first, ReadOnlySpan<char> second)
{
    if (first.IsEmpty && second.IsEmpty)
    {
        return null;
    }
    else if (first.IsEmpty)
    {
        return true;
    }
    else if (second.IsEmpty)
    {
        return false;
    }

    var isFirstDigit = char.IsAsciiDigit(first[0]);
    var firstNextValueRange = NextValue(first);
    var firstValue = first[firstNextValueRange.Item1..firstNextValueRange.Item2];

    var isSecondDigit = char.IsAsciiDigit(second[0]);
    var secondNextValueRange = NextValue(second);
    var secondValue = second[secondNextValueRange.Item1..secondNextValueRange.Item2];

    if (isFirstDigit && isSecondDigit)
    {
        var firstDigit = int.Parse(firstValue);
        var secondDigit = int.Parse(secondValue);

        if (firstDigit < secondDigit)
        {
            return true;
        }
        else if (firstDigit > secondDigit)
        {
            return false;
        }
    }
    else
    {
        var ret = CompareLists(firstValue, secondValue);
        if (ret is not null)
        {
            return ret;
        }

        first = first[firstNextValueRange.Item2..];
        second = second[secondNextValueRange.Item2..];
    }


    var firstNext = first.IndexOf(',');
    var secondNext = second.IndexOf(',');

    if (firstNext == -1 && secondNext == -1)
    {
        return null;
    }
    else if (firstNext == -1)
    {
        return true;
    }
    else if (secondNext == -1)
    {
        return false;
    }

    return CompareLists(first[(firstNext + 1)..], second[(secondNext + 1)..]);
}

static (int, int) NextValue(ReadOnlySpan<char> span)
{
    return span[0] == '['
        ? GetNextListRange(span)
        : (0, NextTokenStart(span));
}

static int NextTokenStart(ReadOnlySpan<char> span)
{
    int[] indices = new int[]
    {
        span.IndexOf(','),
        span.IndexOf('['),
    };

    return indices
        .Where(x => x != -1)
        .DefaultIfEmpty(span.Length)
        .Min();
}

static (int Start, int End) GetNextListRange(ReadOnlySpan<char> span)
{
    var startIndex = span.IndexOf('[');
    if (startIndex < 0)
    {
        return (0, span.Length);
    }

    var listLevel = 0;
    var endIndex = span.Length;

    for (int i = startIndex; i < span.Length; i++)
    {
        listLevel += span[i] switch
        {
            '[' => 1,
            ']' => -1,
            _ => 0
        };

        if (listLevel == 0)
        {
            endIndex = i;
            break;
        }
    }

    return (startIndex + 1, endIndex);
}
