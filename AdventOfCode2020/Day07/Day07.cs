using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Day07;


using var file = new StreamReader("input.txt");

List<string> lines = new();
HashSet<Bag> topLevelBags = new();

while (file.ReadLine() is string line)
{
    lines.Add(line);
}

foreach (var line in lines)
{
    var bagTokens = line.Split(' ');

    Bag currentBag = new(bagTokens[0] + " " + bagTokens[1]);
    topLevelBags.Add(currentBag);
}


foreach (var line in lines)
{
    var bags = line.Split(',');
    var bagTokens = bags[0].Split(' ');

    if (bagTokens[4] == "no" && bagTokens[5] == "other")
    {
        continue;
    }

    var topLevelBag = topLevelBags.SingleOrDefault(b => b.Name == bagTokens[0] + " " + bagTokens[1]);
    
    var containedTopLevelBag = topLevelBags.Single(b => b.Name == bagTokens[5] + " " + bagTokens[6]);
    topLevelBag.ContainedBags.Add(containedTopLevelBag);

    foreach (var bag in bags.Skip(1))
    {
        bagTokens = bag.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        containedTopLevelBag = topLevelBags.Single(b => b.Name == bagTokens[1] + " " + bagTokens[2]);

        topLevelBag.ContainedBags.Add(containedTopLevelBag);
    }
}


List<Bag> goldBags = new();
foreach (var bag in topLevelBags)
{
    bag.Contains("shiny gold", goldBags);
}

Console.WriteLine(goldBags.Where(b => b.Name != "shiny gold").Select(b => b.Name).Distinct().Count());
