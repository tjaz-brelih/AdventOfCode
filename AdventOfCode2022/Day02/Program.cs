using var file = new StreamReader("input.txt");

List<(char, char)> input = new();

while (file.ReadLine() is string line)
{
    input.Add((line[0], line[2]));
}


// Part 1
var resultOne = 0;

foreach (var (opShape, me) in input)
{
    var meShape = GetShape(me);

    resultOne += ShapeScore(meShape) + GameScore(meShape, opShape);
}

Console.WriteLine(resultOne);


// Part 2
var resultTwo = 0;

foreach (var (opShape, strategy) in input)
{
    var meShape = PredictShape(strategy, opShape);

    resultTwo += ShapeScore(meShape) + GameScore(meShape, opShape);
}

Console.WriteLine(resultTwo);




static char GetShape(char shape)
    => shape switch
    {
        'X' => 'A',
        'Y' => 'B',
        'Z' => 'C',
        _ => throw new InvalidOperationException()
    };

static int ShapeScore(char shape)
    => shape switch
    {
        'A' => 1,
        'B' => 2,
        'C' => 3,
        _ => throw new InvalidOperationException()
    };

static int GameScore(char p1, char p2)
{
    if (p1 == p2)
    {
        return 3;
    }

    if ((p1 is 'A' && p2 is 'C') || (p1 is 'B' && p2 is 'A') || (p1 is 'C' && p2 is 'B'))
    {
        return 6;
    }

    return 0;
}

static char PredictShape(char strategy, char opShape)
{
    if (strategy is 'Y')
    {
        return opShape;
    }

    if (strategy is 'X')
    {
        return opShape switch
        {
            'A' => 'C',
            'B' => 'A',
            'C' => 'B',
        _ => throw new InvalidOperationException()
        };
    }

    if (strategy is 'Z')
    {
        return opShape switch
        {
            'A' => 'B',
            'B' => 'C',
            'C' => 'A',
            _ => throw new InvalidOperationException()
        };
    }

    throw new InvalidOperationException();
}