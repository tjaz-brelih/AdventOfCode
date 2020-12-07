using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Day07;


using var file = new StreamReader("input.txt");

List<string> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(line);
}


HashSet<Node> nodes = new();

foreach (var line in lines)
{
    var bagTokens = line.Split(' ');
    nodes.Add(new Node(bagTokens[0] + " " + bagTokens[1]));
}


foreach (var line in lines)
{
    var bags = line.Split(',');
    var bagTokens = bags[0].Split(' ');

    if (bagTokens[4] == "no" && bagTokens[5] == "other")
    {
        continue;
    }

    var topLevelNode = nodes.SingleOrDefault(b => b.Name == bagTokens[0] + " " + bagTokens[1]);

    var containedTopLevelNode = nodes.Single(b => b.Name == bagTokens[5] + " " + bagTokens[6]);
    topLevelNode.AddRelation(containedTopLevelNode, int.Parse(bagTokens[4]));

    foreach (var bag in bags.Skip(1))
    {
        bagTokens = bag.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        containedTopLevelNode = nodes.Single(b => b.Name == bagTokens[1] + " " + bagTokens[2]);

        topLevelNode.AddRelation(containedTopLevelNode, int.Parse(bagTokens[0]));
    }
}


var resultOne = nodes.Single(n => n.Name == "shiny gold").SourceNodes().Count();
Console.WriteLine(resultOne);

var resultTwo = nodes.Single(n => n.Name == "shiny gold").DestinationsCount();
Console.WriteLine(resultTwo);
