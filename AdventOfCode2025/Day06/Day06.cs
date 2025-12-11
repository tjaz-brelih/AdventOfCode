using Common;

var lines = Helpers.ReadInputFile();

List<List<ulong>> operands = [];
var operators = lines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);

for (var i = 0; i < lines.Count - 1; i++)
{
    var numbers = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    for (var j = 0; j < numbers.Length; j++)
    {
        var list = operands.ElementAtOrDefault(j);
        if (list is null) { list = []; operands.Add(list); }

        list.Add(ulong.Parse(numbers[j]));
    }
}


var resultOne = operands
    .Zip(operators)
    .Select(x => (x.Second == "+" ? (0ul, Sum()) : (1ul, Multiply()), x.First))
    .Aggregate(0ul, (acc, x) => acc + x.First.Aggregate(x.Item1.Item1, x.Item1.Item2));

Console.WriteLine(resultOne);



var resultTwo = 0ul;
List<ulong> values = [];
var operatorIndex = 0;

for (var x = 0; x < lines[0].Length; x++)
{
    var value = 0ul;

    for (var y = 0; y < lines.Count - 1; y++)
    {
        if (lines[y][x] != ' ')
        {
            value *= 10;
            value += (ulong) char.GetNumericValue(lines[y][x]);
        }
    }

    values.Add(value);

    if (value == 0 || x == lines[0].Length - 1)
    {
        if (x < lines[0].Length) { values.Remove(0); }

        var (initial, operation) = operators[operatorIndex++] == "+" ? (0ul, Sum()) : (1ul, Multiply());
        resultTwo += values.Aggregate(initial, operation);

        values.Clear();
    }
}

Console.WriteLine(resultTwo);



static Func<ulong, ulong, ulong> Sum() => (a, b) => a + b;
static Func<ulong, ulong, ulong> Multiply() => (a, b) => a * b;