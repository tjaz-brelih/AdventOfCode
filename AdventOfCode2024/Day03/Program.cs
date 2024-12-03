using Common;
using System.Text.RegularExpressions;


var lines = Helpers.ReadInputFile();


var patternOne = @"mul\((\d+),(\d+)\)";

var resultOne = lines.Sum(line => Regex.Matches(line, patternOne).Sum(x =>
{
    var groups = x.Groups.Values.ToArray();

    return long.Parse(groups[1].ValueSpan) * long.Parse(groups[2].ValueSpan);
}));

Console.WriteLine(resultOne);



var patternTwo = @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)";
var isEnabled = true;

var resultTwo = lines.Sum(line => Regex.Matches(line, patternTwo).Sum(x =>
{
    var groups = x.Groups.Values.ToArray();

    (isEnabled, var sum) = (isEnabled, x.Value) switch
    {
        (_, "do()") => (true, 0),
        (_, "don't()") => (false, 0),
        (true, var span) => (isEnabled, long.Parse(groups[1].ValueSpan) * long.Parse(groups[2].ValueSpan)),
        (false, _) => (isEnabled, 0)
    };

    return sum;
}));

Console.WriteLine(resultTwo);