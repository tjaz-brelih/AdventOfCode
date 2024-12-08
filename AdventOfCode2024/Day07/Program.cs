using Common;


var lines = Helpers.ReadInputFile();


var resultOne = 0L;

foreach (var line in lines)
{
    var index = line.IndexOf(':');

    var target = long.Parse(line[..index]);
    var values = line[(index + 2)..].Split(' ').Select(long.Parse).ToArray();

    foreach (var operators in Enumerable.Range(0, (int) Math.Pow(2, values.Length - 1)))
    {
        if (Calculate(values, operators) == target) { resultOne += target; break; }
    }
}

Console.WriteLine(resultOne);



static long Calculate(long[] values, int operators)
{
    var result = values[0];

    for (var i = 1; i < values.Length; i++)
    {
        result = (operators & (1 << (values.Length - i - 1))) switch
        {
            0 => result + values[i],
            _ => result * values[i]
        };
    }

    return result;
}