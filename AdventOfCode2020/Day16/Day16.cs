using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Day16;



using var file = new StreamReader("input.txt");

List<Rule> rules = new();

while (file.ReadLine() is string line)
{
    if (line.Trim().Length == 0)
    {
        break;
    }

    rules.Add(Rule.FromString(line));
}


file.ReadLine();
var myTicket = file.ReadLine().Split(',').Select(s => int.Parse(s)).ToList();

file.ReadLine();
file.ReadLine();


List<List<int>> nearbyTickets = new();

while (file.ReadLine() is string line)
{
    nearbyTickets.Add(line.Split(',').Select(s => int.Parse(s)).ToList());
}



var resultOne = nearbyTickets
    .SelectMany(t => t.Select(f => f))
    .Where(f => !rules.Any(r => r.InAnyRange(f)))
    .Sum();

Console.WriteLine(resultOne);



var validTickets = nearbyTickets
    .Where(t => t.All(f => rules.Any(r => r.InAnyRange(f))));

Dictionary<Rule, int> ruleColumn = new();

while (true)
{
    var unknownRules = rules.Where(r => !ruleColumn.ContainsKey(r));

    if (!unknownRules.Any())
    {
        break;
    }

    foreach (var rule in unknownRules)
    {
        var ruleConforms = false;

        for (int i = 0; i < validTickets.First().Count; i++)
        {
            ruleConforms = validTickets.Select(t => t[i]).All(f => rule.InAnyRange(f));

            if (ruleConforms)
            {
                ruleColumn[rule] = i;
                break;
            }
        }

        if (ruleConforms)
        {
            break;
        }
    }
}

var resultTwo = ruleColumn
    .Where(kv => kv.Key.Name.StartsWith("direction"))
    .Aggregate(1UL, (acc, kv) => acc * (ulong) myTicket[kv.Value]);

Console.WriteLine(resultTwo);
