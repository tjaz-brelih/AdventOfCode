using var file = new StreamReader("input.txt");

var input = file.ReadLine().AsSpan()[15..];

var tokenIndex = input.IndexOf('.');
var x1 = int.Parse(input[..tokenIndex]);
input = input[(tokenIndex + 2)..];

tokenIndex = input.IndexOf(',');
var x2 = int.Parse(input[..tokenIndex]);
input = input[(tokenIndex + 4)..];

tokenIndex = input.IndexOf('.');
var y1 = int.Parse(input[..tokenIndex]);
input = input[(tokenIndex + 2)..];

var y2 = int.Parse(input);


var targetX = (Min: x1, Max: x2);
var targetY = (Min: y1, Max: y2);



var resultOne = (targetY.Min + 1) * targetY.Min / 2;
Console.WriteLine(resultOne);



var minStartX = (int) Math.Ceiling((Math.Sqrt((8 * targetX.Min) + 1) - 1) / 2);
var maxStartX = targetX.Max;
var minStartY = targetY.Min;
var maxStartY = -(targetY.Min + 1);

var resultTwo = 0;

for (int x = minStartX; x <= maxStartX; x++)
{
    for (int y = minStartY; y <= maxStartY; y++)
    {
        resultTwo += SimulateShot(x, y, targetX, targetY) switch { true => 1, _ => 0 };
    }
}

Console.WriteLine(resultTwo);



static bool SimulateShot(int velocityX, int velocityY, (int Min, int Max) targetX, (int Min, int Max) targetY)
{
    var (X, Y) = (0, 0);

    while (X <= targetX.Max && Y >= targetY.Min)
    {
        X += velocityX;
        Y += velocityY;

        if (X >= targetX.Min && X <= targetX.Max && Y >= targetY.Min && Y <= targetY.Max)
        {
            return true;
        }

        velocityX += velocityX switch
        {
            var v when v > 0 => -1,
            var v when v < 0 => 1,
            _ => 0
        };

        velocityY -= 1;
    }

    return false;
}