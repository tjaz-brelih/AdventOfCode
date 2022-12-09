using var file = new StreamReader("input.txt");

List<(char, int)> moves = new();

while (file.ReadLine() is string line)
{
    var span = line.AsSpan();

    moves.Add((span[0], int.Parse(span[2..])));
}


// Part 1
var head = (0, 0);
var tail = (0, 0);

HashSet<(int, int)> positions = new();

foreach (var (direction, distance) in moves)
{
    var moveHead = DirectionFunc(direction);

    for (int i = 0; i < distance; i++)
    {
        head = moveHead(head.Item1, head.Item2);
        tail = MoveTail(head, tail);

        positions.Add(tail);
    }
}

var resultOne = positions.Count;
Console.WriteLine(resultOne);


// Part 2
List<(int, int)> knots = Enumerable.Range(0, 10).Select(_ => (0, 0)).ToList();

positions.Clear();

foreach (var (direction, distance) in moves)
{
    var moveHead = DirectionFunc(direction);

    for (int i = 0; i < distance; i++)
    {
        knots[0] = moveHead(knots[0].Item1, knots[0].Item2);

        for (int j = 0; j < knots.Count - 1; j++)
        {
            head = knots[j];
            tail = knots[j + 1];

            tail = MoveTail(head, tail);

            knots[j + 1] = tail;
        }

        positions.Add(knots.Last());
    }
}

var resultTwo = positions.Count;
Console.WriteLine(resultTwo);




static (int, int) MoveTail((int, int) head, (int, int) tail)
{
    var (headX, headY) = head;
    var (tailX, tailY) = tail;

    var distance = Math.Abs(headX - tailX) + Math.Abs(headY - tailY);

    var (difX, difY) = (headX - tailX, headY - tailY);

    if (distance >= 3)
    {
        difX += difX > 0 ? 1 : -1;
        difY += difY > 0 ? 1 : -1;
    }

    tailX += difX / 2;
    tailY += difY / 2;

    return (tailX, tailY);
}

static Func<int, int, (int, int)> DirectionFunc(char direction)
    => direction switch
    {
        'U' => (x, y) => (x, y + 1),
        'D' => (x, y) => (x, y - 1),
        'L' => (x, y) => (x - 1, y),
        'R' => (x, y) => (x + 1, y),
        _ => throw new Exception()
    };
