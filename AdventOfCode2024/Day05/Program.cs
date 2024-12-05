using Common;


var lines = Helpers.ReadInputFile();


var space = lines.IndexOf("");

var rules = new Dictionary<int, HashSet<int>>();

foreach (var line in lines.Take(space))
{
    var (first, second) = (int.Parse(line[..2]), int.Parse(line[3..]));

    if (rules.TryGetValue(first, out var value)) { value.Add(second); }
    else { rules[first] = [second]; }
}

var updates = lines.Skip(space + 1).Select(x => x.Split(',').Select(int.Parse).ToArray()).ToArray();



var resultOne = 0;

foreach (var update in updates)
{
    var middlePage = update[update.Length / 2];

    for (var i = update.Length - 1; i >= 1; i--)
    {
        if (!rules.TryGetValue(update[i], out var pageRules)) { continue; }
        if (update.Take(i).Any(pageRules.Contains)) { middlePage = 0; break; }
    }

    resultOne += middlePage;
}

Console.WriteLine(resultOne);



var resultTwo = 0;
var comparer = new PageComparer(rules);

foreach (var update in updates)
{
    for (var i = update.Length - 1; i >= 1; i--)
    {
        if (!rules.TryGetValue(update[i], out var pageRules)) { continue; }

        if (update.Take(i).Any(pageRules.Contains))
        {
            var orderedUpdate = update.Order(comparer);
            resultTwo += orderedUpdate.Skip(update.Length / 2).First();

            break;
        }
    }
}

Console.WriteLine(resultTwo);


class PageComparer(Dictionary<int, HashSet<int>> rules) : IComparer<int>
{
    public int Compare(int x, int y) => rules.TryGetValue(x, out var pageRules) && !pageRules.Contains(y) ? 1 : -1;
}