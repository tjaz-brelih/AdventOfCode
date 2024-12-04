using Common;


var lines = Helpers.ReadInputFile();


var width = lines[0].Length;
var height = lines.Count;

var target = "XMAS";
var current = new char[target.Length];

var targetSpaceLength = target.Length - 1;

var resultOne = 0;

for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        if (lines[i][j] != 'X') continue;

        var (isRightSpace, isLeftSpace, isDownSpace, isUpSpace) = (j + targetSpaceLength < width, j - targetSpaceLength >= 0, i + targetSpaceLength < height, i - targetSpaceLength >= 0);

        if (isRightSpace) { resultOne += CheckEquality(lines, current, target, k => (i, j + k)); }
        if (isLeftSpace) { resultOne += CheckEquality(lines, current, target, k => (i, j - k)); }
        if (isDownSpace) { resultOne += CheckEquality(lines, current, target, k => (i + k, j)); }
        if (isUpSpace) { resultOne += CheckEquality(lines, current, target, k => (i - k, j)); }
        if (isRightSpace && isDownSpace) { resultOne += CheckEquality(lines, current, target, k => (i + k, j + k)); }
        if (isRightSpace && isUpSpace) { resultOne += CheckEquality(lines, current, target, k => (i - k, j + k)); }
        if (isLeftSpace && isDownSpace) { resultOne += CheckEquality(lines, current, target, k => (i + k, j - k)); }
        if (isLeftSpace && isUpSpace) { resultOne += CheckEquality(lines, current, target, k => (i - k, j - k)); }
    }
}

Console.WriteLine(resultOne);



target = "MAS";
targetSpaceLength = target.Length - 1;

current = new char[target.Length];

var resultTwo = 0;

for (int i = 1; i < height - 1; i++)
{
    for (int j = 1; j < width - 1; j++)
    {
        if (lines[i][j] != 'A') continue;

        var targetCount = 0;

        targetCount += CheckEquality(lines, current, target, k => (i + k - 1, j + k - 1)); // Down-right
        targetCount += CheckEquality(lines, current, target, k => (i - k + 1, j + k - 1)); // Up-right
        targetCount += CheckEquality(lines, current, target, k => (i + k - 1, j - k + 1)); // Down-left
        targetCount += CheckEquality(lines, current, target, k => (i - k + 1, j - k + 1)); // Up-left

        if (targetCount == 2) resultTwo++;
    }
}

Console.WriteLine(resultTwo);



static int CheckEquality(List<string> source, Span<char> destination, ReadOnlySpan<char> target, Func<int, (int, int)> indexFunc)
{
    for (int i = 0; i < target.Length; i++) { var (x, y) = indexFunc(i); destination[i] = source[x][y]; }
    return (destination.Length == target.Length && destination.StartsWith(target)) ? 1 : 0;
};
