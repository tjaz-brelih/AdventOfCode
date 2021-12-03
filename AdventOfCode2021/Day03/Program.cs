using var file = new StreamReader("input.txt");

var width = 0;
List<uint> data = new();

while (file.ReadLine() is string line)
{
    width = line.Length;
    data.Add(line.Aggregate(0u, (a, c) => (a << 1) | (c == '1' ? 1u : 0u)));
}



var gamma = 0u;
var epsilon = 0u;

for (int i = 0; i < width; i++)
{
    var (ones, zeros) = CountBit(data, i);

    gamma |= (ones > zeros ? 1u : 0u) << i;
    epsilon |= (zeros > ones ? 1u : 0u) << i;
}

var resultOne = gamma * epsilon;
Console.WriteLine(resultOne);



var oxygen = CommonBit(data, width, (ones, zeros) => ones >= zeros ? 1 : 0);
var co2 = CommonBit(data, width, (ones, zeros) => ones < zeros ? 1 : 0);

var resultTwo = oxygen * co2;
Console.WriteLine(resultTwo);




static (int, int) CountBit(IEnumerable<uint> input, int position)
{
    var ones = input.Where(x => ((x >> position) & 1) == 1).Count();
    return (ones, input.Count() - ones);
}


static uint CommonBit(IEnumerable<uint> input, int width, Func<int, int, int> compare)
{
    var position = width - 1;
    var validNumbers = input.ToHashSet();

    while (validNumbers.Count != 1)
    {
        var (ones, zeros) = CountBit(validNumbers, position);
        var r = compare(ones, zeros);

        validNumbers.RemoveWhere(n => ((n >> position) & 1) != r);

        position--;
    }

    return validNumbers.Single();
}
