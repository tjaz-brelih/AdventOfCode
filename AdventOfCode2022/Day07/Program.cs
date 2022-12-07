using Day07;

using var file = new StreamReader("input.txt");

List<string> input = new();

while (file.ReadLine() is string line)
{
    input.Add(line);
}


// Part 1
DirectoryAoc topDirectory = new("/");
DirectoryAoc currentDirectory = topDirectory;

foreach (var line in input)
{
    var span = line.AsSpan();

    if (span.StartsWith("$ cd"))
    {
        currentDirectory = span[5] switch
        {
            '/' => topDirectory,
            '.' => currentDirectory.Parent ?? currentDirectory,
            _ => currentDirectory.AddSubdirectory(span[5..])
        };
    }
    // File output
    else if (char.IsAsciiDigit(span[0]))
    {
        var spaceIndex = span.IndexOf(' ');

        var size = span[..spaceIndex];
        var name = span[(spaceIndex + 1)..];

        currentDirectory.AddFile(name, ulong.Parse(size));
    }
}

var directoryList = new List<DirectoryAoc>();
FilterDirectories(directoryList, topDirectory, 100000);

var resultOne = directoryList.Aggregate(0ul, (acc, x) => acc + x.Size);

Console.WriteLine(resultOne);


// Part 2
var deleteSize = 30000000ul - (70000000ul - topDirectory.Size);
directoryList = new();
FilterDirectories(directoryList, topDirectory, ulong.MaxValue);

var resultTwo = directoryList
    .Where(x => x.Size >= deleteSize)
    .Min(x => x.Size);

Console.WriteLine(resultTwo);




static void FilterDirectories(List<DirectoryAoc> directories, DirectoryAoc current, ulong maxSize)
{
    if (current.Size <= maxSize)
    {
        directories.Add(current);
    }

    foreach (var directory in current.Directories)
    {
        FilterDirectories(directories, directory, maxSize);
    }
}
