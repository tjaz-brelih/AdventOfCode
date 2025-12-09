using Common;

var lines = Helpers.ReadInputFile();

var position = 50;
var resultOne = 0;

foreach (var line in lines)
{
    var span = line.AsSpan();
    var diff = int.Parse(span[1..]);

    var r = diff % 100;

    position += span[0] == 'R' ? r : -r;

    if (position < 0) { position += 100; }
    else if (position > 99) { position -= 100; }

    if (position == 0) { resultOne++; }
}

Console.WriteLine(resultOne);



position = 50;
var resultTwo = 0;

foreach (var line in lines)
{
    var span = line.AsSpan();
    var diff = int.Parse(span[1..]);

    var (q, r) = Math.DivRem(diff, 100);
    resultTwo += q;

    for (int i = 0; i < r; i++)
    {
        if (span[0] == 'L') { position--; }
        else { position++; }

        if (position < 0) { position = 99; }
        else if (position == 100) { position = 0; }

        if (position == 0) { resultTwo++; }
    }
}

Console.WriteLine(resultTwo);