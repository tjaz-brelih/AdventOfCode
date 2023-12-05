using Common;

var lines = Helpers.ReadInputFile();


List<Card> cards = [];

foreach (var line in lines)
{
    var index = line.IndexOf(':');
    var indexLine = line.IndexOf('|');

    HashSet<int> numbers = new(line[(index + 1)..indexLine].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
    HashSet<int> myNumbers = new(line[(indexLine + 1)..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

    var intersectCount = numbers.Intersect(myNumbers).Count();

    cards.Add(new(intersectCount));
}


var resultOne = cards.Sum(x => x.WinCount switch
    {
        < 2 => x.WinCount,
        _ => 2 << (x.WinCount - 2)
    });

Console.WriteLine(resultOne);



for (int i = 0; i < cards.Count; i++)
{
    CardCounting(cards, i);
}

var resultTwo = cards.Sum(x => x.Count);
Console.WriteLine(resultTwo);




static void CardCounting(List<Card> cards, int index)
{
    var card = cards[index];

    card.Count++;

    for (int i = 1; i <= card.WinCount; i++)
    {
        CardCounting(cards, index + i);
    }
}

record class Card(int WinCount)
{
    public int Count { get; set; }
}