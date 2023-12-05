namespace Common;

public static class Helpers
{
    public static List<string> ReadInputFile(string filePath = "input.txt")
    {
        using var file = new StreamReader(filePath);

        List<string> lines = [];

        while (file.ReadLine() is string line)
        {
            lines.Add(line);
        }

        return lines;
    }
}