using var file = new StreamReader("input.txt");

List<int> crabs = null!;

while (file.ReadLine() is string line)
{
    var tokens = line.Split(',');
    crabs = tokens.Select(t => int.Parse(t)).ToList();
}



crabs.Sort();
var medianIndex = crabs.Count / 2;
var median = crabs[medianIndex];
var resultOne = crabs.Sum(c => Math.Abs(c - median));
Console.WriteLine(resultOne);



var average = (int) Math.Round(crabs.Average());
var resultTwo = new int[] { average - 1, average, average + 1 }.Min(x => FuelNeeded(crabs, x));
Console.WriteLine(resultTwo);


static int FuelNeeded(IEnumerable<int> input, int position)
    => input.Sum(c => (Math.Abs(c - position) * (Math.Abs(c - position) + 1)) / 2);
