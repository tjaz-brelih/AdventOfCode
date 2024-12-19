using Common;

var lines = Helpers.ReadInputFile();

var towels = lines[0].Split(", ");
var designs = lines[2..];


var resultOne = designs.Count(x => FindMatches(x.AsMemory(), towels, []) > 0);
Console.WriteLine(resultOne);

var resultTwo = designs.Sum(x => FindMatches(x.AsMemory(), towels, []));
Console.WriteLine(resultTwo);



static long FindMatches(ReadOnlyMemory<char> design, IList<string> towels, Dictionary<ReadOnlyMemory<char>, long> cache)
{
    if (design.Length == 0) { return 1; }
    if (cache.TryGetValue(design, out var matches)) { return matches; }

    var count = towels.Sum(x => design.Span.StartsWith(x) ? FindMatches(design[x.Length..], towels, cache) : 0);
    cache[design] = count;

    return count;
}