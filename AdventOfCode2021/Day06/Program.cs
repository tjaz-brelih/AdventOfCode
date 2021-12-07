using var file = new StreamReader("input.txt");

var fishes = file.ReadLine()!.Split(',').Select(t => int.Parse(t)).ToList();



var resultOne = SimulateGrowth(80, fishes);
Console.WriteLine(resultOne);



var resultTwo = SimulateGrowth(256, fishes);
Console.WriteLine(resultTwo);



static ulong SimulateGrowth(int days, IEnumerable<int> input)
{
    var fishCycle = new ulong[7];
    var newFishCycle = new ulong[7];

    foreach (var fish in input)
    {
        fishCycle[fish]++;
    }

    for (int i = 0; i < days; i++)
    {
        newFishCycle[(i + 2) % 7] += fishCycle[i % 7];
        fishCycle[i % 7] += newFishCycle[i % 7];
        newFishCycle[i % 7] = 0;
    }

    ulong sum = 0;

    for (int i = 0; i < 7; i++)
    {
        sum += fishCycle[i] + newFishCycle[i];
    }

    return sum;
}