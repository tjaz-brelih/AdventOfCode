using Common;

var lines = Helpers.ReadInputFile();


List<Thing> things = [];

foreach (var line in lines)
{
    var hand = line[0..5];
    var bid = int.Parse(line[6..]);

    var groups = hand.GroupBy(x => x).Select(x => (Card: x.First(), Count: x.Count())).ToList();

    var type = groups.Count switch
    {
        1 => 0, // Five of a kind
        2 => groups.Max(x => x.Count) == 4 ? 1 : 2, // Four of a kind or full house
        3 => groups.Max(x => x.Count) == 3 ? 3 : 4, // Three of a kind or two pairs
        4 => 5, // One pair
        _ => 6  // High card
    };

    things.Add(new(hand, bid, type));
}

things.Sort(CompareThingsOne);

var resultOne = 0L;
for (int i = 0; i < things.Count; i++)
{
    resultOne += things[i].Bid * (i + 1);
}
Console.WriteLine(resultOne);



var thingsTwo = things
    .Select(x => x with
    {
        Type = Math.Max(0, (x.Type, x.Hand.Count(y => y == 'J')) switch
        {
            (_, 0) => x.Type,
            (5, _) or (3, _) or (0, _) => x.Type - 2,
            (4, var j) => 4 - j - 1,
            (_, var j) => x.Type - j
        })
    })
    .ToList();

thingsTwo.Sort(CompareThingsTwo);

var resultTwo = 0L;
for (int i = 0; i < thingsTwo.Count; i++)
{
    resultTwo += thingsTwo[i].Bid * (i + 1);
}
Console.WriteLine(resultTwo);




static int CompareThingsOne(Thing x, Thing y)
{
    var typeComparison = y.Type.CompareTo(x.Type);
    if (typeComparison != 0) return typeComparison;

    var ranking = "AKQJT98765432";

    for (int i = 0; i < x.Hand.Length; i++)
    {
        var xCard = ranking.IndexOf(x.Hand[i]);
        var yCard = ranking.IndexOf(y.Hand[i]);

        var compare = yCard.CompareTo(xCard);
        if (compare != 0) return compare;
    }

    return 0;
}

static int CompareThingsTwo(Thing x, Thing y)
{
    var typeComparison = y.Type.CompareTo(x.Type);
    if (typeComparison != 0) return typeComparison;

    var ranking = "AKQT98765432J";

    for (int i = 0; i < x.Hand.Length; i++)
    {
        var xCard = ranking.IndexOf(x.Hand[i]);
        var yCard = ranking.IndexOf(y.Hand[i]);

        var compare = yCard.CompareTo(xCard);
        if (compare != 0) return compare;
    }

    return 0;
}

record Thing(string Hand, int Bid, int Type);