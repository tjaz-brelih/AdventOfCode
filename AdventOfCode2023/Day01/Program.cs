using Common;

var lines = Helpers.ReadInputFile();

List<long> values = [];

foreach (var line in lines)
{
    List<long> numbers = [];

    foreach (var value in line)
    {
        if (char.IsDigit(value))
        {
            numbers.Add(value - 48);
        }
    }

    values.Add(10 * numbers[0] + numbers[^1]);
}


Console.WriteLine(values.Sum());



string[] numbersLookup = ["one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

values.Clear();

foreach (var line in lines)
{
    List<long> numbers = [];

    for (int i = 0; i < line.Length; i++)
    {
        char value = line[i];

        if (char.IsDigit(value))
        {
            numbers.Add(value - 48);
            continue;
        }

        for (int j = 0; j < numbersLookup.Length; j++)
        {
            if (line[i..].StartsWith(numbersLookup[j]))
            {
                numbers.Add(j + 1);
                break;
            }
        }
    }

    values.Add(10 * numbers[0] + numbers[^1]);
}


Console.WriteLine(values.Sum());