using Common;

var lines = Helpers.ReadInputFile();


var resultOne = 0;

foreach (var line in lines)
{
    var index = line.IndexOf(':');
    var gameId = int.Parse(line[5..index]);

    var possible = true;

    var tokens = line[(index + 2)..].Split((char[]) [',', ';'], StringSplitOptions.TrimEntries);


    foreach (var token in tokens)
    {
        index = token.IndexOf(' ');
        var count = int.Parse(token[..index]);
        var color = token[(index + 1)..];

        var compare = color switch
        {
            "red" => 12,
            "green" => 13,
            "blue" => 14,
            _ => throw new Exception()
        };

        if (count > compare)
        {
            possible = false;
            break;
        }
    }

    if (possible)
    {
        resultOne += gameId;
    }
}


Console.WriteLine(resultOne);



var resultTwo = 0L;

foreach (var line in lines)
{
    var (redMin, greenMin, blueMin) = (0, 0, 0);

    var index = line.IndexOf(':');
    var gameId = int.Parse(line[5..index]);

    var tokens = line[(index + 2)..].Split((char[]) [',', ';'], StringSplitOptions.TrimEntries);

    foreach (var token in tokens)
    {
        index = token.IndexOf(' ');
        var count = int.Parse(token[..index]);
        var color = token[(index + 1)..];

        var compare = color switch
        {
            "red" => redMin = Math.Max(count, redMin),
            "green" => greenMin = Math.Max(count, greenMin),
            "blue" => blueMin = Math.Max(count, blueMin),
            _ => throw new Exception()
        };
    }

    resultTwo += redMin * greenMin * blueMin;
}

Console.WriteLine(resultTwo);