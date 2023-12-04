using Common;

var lines = Helpers.ReadInputFile();


char[] numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

for (int y = 0; y < lines.Count; y++)
{
    var line = lines[y];

    var start = 0;

    start = line.IndexOfAny(numbers, start);
    var end = start;
}