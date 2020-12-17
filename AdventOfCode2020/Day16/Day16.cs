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



var validTickets = nearbyTickets.Where(t => t.All(f => rules.Any(r => r.InAnyRange(f))));

var avaliableRules = rules.ToHashSet();
Dictionary<Rule, int> ruleColumn = new();

while (avaliableRules.Count > 0)
{
    for (int i = 0; i < validTickets.First().Count; i++)
    {
        var validRules = avaliableRules.Where(r => validTickets.All(t => r.InAnyRange(t[i])));

        if (validRules.Count() == 1)
        {
            var rule = validRules.Single();

            ruleColumn[rule] = i;
            avaliableRules.Remove(rule);
        }
    }
}

var directionRules = ruleColumn.Where(kv => kv.Key.Name.StartsWith("departure"));
var resultTwo = directionRules.Aggregate(1UL, (acc, kv) => acc * (ulong) myTicket[kv.Value]);
Console.WriteLine(resultTwo);
