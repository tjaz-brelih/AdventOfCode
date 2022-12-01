using var file = new StreamReader("input.txt");

List<List<int>> elves = new();

List<int> currentList = new();

elves.Add(currentList);

while (file.ReadLine() is string line)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        currentList = new();
        elves.Add(currentList);

        continue;
    }

    currentList.Add(int.Parse(line));
}


// Part 1
var resultOne = elves.Max(x => x.Sum());
Console.WriteLine(resultOne);

// Part 2
var resultTwo = elves.Select(x => x.Sum()).OrderByDescending(x => x).Take(3).Sum();
Console.WriteLine(resultTwo);