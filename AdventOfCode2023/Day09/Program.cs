using Common;

var lines = Helpers.ReadInputFile();
//var lines = Helpers.ReadInputFile("test.txt");


var resultOne = 0L;

foreach (var line in lines)
{
    List<List<long>> things = [line.Split(' ').Select(long.Parse).ToList()];

    while (!things[^1].All(x => x == 0))
    {
        things.Add(CalculateDifferences(things[^1]));
    }


    var value = 0L;
    for (int i = things.Count - 1; i >= 0; i--)
    {
        things[i].Add(things[i][^1] + value);
        value = things[i][^1];
    }

    resultOne += things[0][^1];
}

Console.WriteLine(resultOne);



var resultTwo = 0L;

foreach (var line in lines)
{
    List<List<long>> things = [line.Split(' ').Select(long.Parse).ToList()];

    while (!things[^1].All(x => x == 0))
    {
        things.Add(CalculateDifferences(things[^1]));
    }


    var value = 0L;
    for (int i = things.Count - 1; i >= 0; i--)
    {
        things[i].Add(things[i][0] - value);
        value = things[i][^1];
    }

    resultTwo += things[0][^1];
}

Console.WriteLine(resultTwo);




static List<long> CalculateDifferences(List<long> input)
{
    List<long> differences = [];

    for (int i = 0; i < input.Count - 1; i++)
    {
        differences.Add(input[i + 1] - input[i]);
    }

    return differences;
}