using var file = new StreamReader("input.txt");

List<string> input = new();

while (file.ReadLine() is string line)
{
    input.Add(line);
}


// Part 1
var resultOne = 0;

foreach (var line in input)
{
    var half = line.Length / 2;

    var first = line[..half].ToHashSet();
    var second = line[half..].ToHashSet();

    first.IntersectWith(second);

    resultOne += GetRanking(first.Single());
}

Console.WriteLine(resultOne);


// Part 2
var resultTwo = 0;

for (int i = 0; i < input.Count; i += 3)
{
    var first = input[i].ToHashSet();
    var second = input[i + 1].ToHashSet();
    var third = input[i + 2].ToHashSet();

    first.IntersectWith(second);
    first.IntersectWith(third);

    resultTwo += GetRanking(first.Single());
}

Console.WriteLine(resultTwo);



static int GetRanking(char c) => c - (char.IsAsciiLetterLower(c) ? 96 : 38);