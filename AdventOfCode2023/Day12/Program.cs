using Common;

var lines = Helpers.ReadInputFile();


foreach (var line in lines)
{
    var index = line.IndexOf(' ');

    var springs = line.AsSpan()[..index].ToArray();
    var groups = line[(index + 1)..].Split(',').Select(int.Parse).ToArray();


}
