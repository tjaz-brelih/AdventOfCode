using Day11;

using var file = new StreamReader("input.txt");

List<Monkey> monkeys = new();

while (file.ReadLine() is string line)
{
    var currentMonkey = new Monkey();
    monkeys.Add(currentMonkey);

    var span = file.ReadLine()!.AsSpan()[18..];

    while (true)
    {
        var index = span.IndexOf(' ');
        if (index < 0)
        {
            currentMonkey.Items.Add(ulong.Parse(span));
            break;
        }

        currentMonkey.Items.Add(ulong.Parse(span[..(index - 1)]));
        span = span[(index + 1)..];
    }


    span = file.ReadLine()!.AsSpan()[23..];

    Func<ulong, ulong, ulong> op = span[0] switch
    {
        '+' => (x, y) => x + y,
        '*' => (x, y) => x * y,
        _ => throw new Exception()
    };

    var val = span[2..];
    currentMonkey.Operation = ulong.TryParse(val, out var valInt)
        ? x => op(x, valInt)
        : x => op(x, x);


    span = file.ReadLine()!.AsSpan()[21..];
    currentMonkey.Test = ulong.Parse(span);

    span = file.ReadLine()!.AsSpan()[29..];
    currentMonkey.TrueThrow = int.Parse(span);

    span = file.ReadLine()!.AsSpan()[30..];
    currentMonkey.FalseThrow = int.Parse(span);

    file.ReadLine();
}


// Part 1
var monkeysOne = monkeys.Select(x => x.Clone()).ToList();

for (int round = 0; round < 20; round++)
{
    foreach (var monkey in monkeysOne)
    {
        var items = monkey.Items.ToArray();

        for (int i = 0; i < items.Length; i++)
        {
            var worry = items[i];
            var newWorry = monkey.Operation(worry) / 3ul;
            var newMonkey = monkeysOne[monkey.ThrowToMonkey(newWorry)];

            newMonkey.Items.Add(newWorry);
            monkey.Items.Remove(worry);
        }

        monkey.CountInspected += (ulong) items.Length;
    }
}

var resultOne = monkeysOne
    .Select(x => x.CountInspected)
    .OrderDescending()
    .Take(2)
    .Aggregate(1ul, (acc, x) => acc * x);

Console.WriteLine(resultOne);


// Part 2
var monkeysTwo = monkeys.Select(x => x.Clone()).ToList();
var modulo = monkeysTwo.Aggregate(1ul, (acc, x) => acc * x.Test);

for (int round = 0; round < 10000; round++)
{
    foreach (var monkey in monkeysTwo)
    {
        var items = monkey.Items.ToArray();

        for (int i = 0; i < items.Length; i++)
        {
            var worry = items[i];
            var newWorry = monkey.Operation(worry) % modulo;
            var newMonkey = monkeysTwo[monkey.ThrowToMonkey(newWorry)];

            newMonkey.Items.Add(newWorry);
            monkey.Items.Remove(worry);
        }

        monkey.CountInspected += (ulong) items.Length;
    }
}

var resultTwo = monkeysTwo
    .Select(x => x.CountInspected)
    .OrderDescending()
    .Take(2)
    .Aggregate(1ul, (acc, x) => acc * x);

Console.WriteLine(resultTwo);
