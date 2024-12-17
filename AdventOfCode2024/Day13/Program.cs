using Pair = (long X, long Y);


using var file = new StreamReader("input.txt");


List<(Pair A, Pair B, Pair Target)> machines = [];
Span<Range> split = stackalloc Range[2];

while (file.ReadLine() is string line)
{
    var span = line.AsSpan();
    Pair a = (long.Parse(span.Slice(12, 2)), long.Parse(span.Slice(18, 2)));

    span = file.ReadLine()!.AsSpan();
    Pair b = (long.Parse(span.Slice(12, 2)), long.Parse(span.Slice(18, 2)));

    span = file.ReadLine()!.AsSpan();
    span.Split(split, ',');
    Pair target = (long.Parse(span[split[0]][9..]), long.Parse(span[split[1]][3..]));

    file.ReadLine();

    machines.Add((a, b, target));
}



var resultOne = 0L;

foreach (var (a, b, target) in machines)
{
    var (bxay, byax) = (b.X * a.Y, b.Y * a.X);
    var (xay, yax) = (target.X * a.Y, target.Y * a.X);

    var second = (xay - yax) / (bxay - byax);
    var first = (target.X - b.X * second) / a.X;

    if (a.X * first + b.X * second == target.X && a.Y * first + b.Y * second == target.Y) { resultOne += 3 * first + second; }
}

Console.WriteLine(resultOne);


var resultTwo = 0L;
var shift = 10_000_000_000_000;

foreach (var machine in machines)
{
    var (a, b, target) = machine;
    target = (target.X + shift, target.Y + shift);

    var (bxay, byax) = (b.X * a.Y, b.Y * a.X);
    var (xay, yax) = (target.X * a.Y, target.Y * a.X);

    var second = (xay - yax) / (bxay - byax);
    var first = (target.X - b.X * second) / a.X;

    if (a.X * first + b.X * second == target.X && a.Y * first + b.Y * second == target.Y) { resultTwo += 3 * first + second; }
}

Console.WriteLine(resultTwo);