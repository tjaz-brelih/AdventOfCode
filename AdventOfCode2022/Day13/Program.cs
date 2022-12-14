using Day13;

using var file = new StreamReader("input.txt");

List<(Item, Item)> packetPairs = new();

while (file.ReadLine() is string firstLine)
{
    var secondLine = file.ReadLine()!;

    packetPairs.Add((ParseItem(firstLine), ParseItem(secondLine)));

    file.ReadLine();
}


// Part 1
var pair = 1;
var resultOne = 0;
ItemComparer comparer = new();

foreach (var (first, second) in packetPairs)
{
    resultOne += comparer.Compare(first, second) switch
    {
        < 0 => pair,
        > 0 => 0,
        _ => throw new Exception()
    };

    pair++;
}

Console.WriteLine(resultOne);


// Part 2
var dividers = new[] { ParseItem("[[2]]"), ParseItem("[[6]]") };

var order = 1;
var resultTwo = 1;

foreach (var packet in packetPairs.SelectMany(x => new[] { x.Item1, x.Item2 }).Concat(dividers).Order(comparer))
{
    if (dividers.Contains(packet))
    {
        resultTwo *= order;
    }

    order++;
}

Console.WriteLine(resultTwo);




static Item ParseItem(ReadOnlySpan<char> span)
{
    if (char.IsAsciiDigit(span[0]))
    {
        return new Item(int.Parse(span));
    }

    var endIndex = GetArrayIndexEnd(span);

    List<Item> items = new();

    for (int i = 1; i < endIndex; i++)
    {
        if (char.IsAsciiDigit(span[i]))
        {
            var index = span[i..endIndex].IndexOfAny(",]");

            if (index == -1)
            {
                items.Add(ParseItem(span[i..endIndex]));
                break;
            }

            items.Add(ParseItem(span[i..(index + i)]));
            i += index;
        }
        else if (span[i] == '[')
        {
            items.Add(ParseItem(span[i..endIndex]));
        }
    }

    return new Item(items);
}

static int GetArrayIndexEnd(ReadOnlySpan<char> span)
{
    var listLevel = 0;
    var endIndex = span.Length;

    for (int i = 0; i < span.Length; i++)
    {
        listLevel += span[i] switch
        {
            '[' => 1,
            ']' => -1,
            _ => 0
        };

        if (listLevel == 0)
        {
            endIndex = i;
            break;
        }
    }

    return endIndex;
}

class ItemComparer : IComparer<Item>
{
    public int Compare(Item? x, Item? y)
    {
        if (x is null || y is null) return 0;

        return this.CompareItems(x, y) switch
        {
            true => -1,
            false => 1,
            _ => throw new Exception()
        };
    }

    private bool? CompareItems(Item first, Item second)
    {
        if (first.IsInt && second.IsInt)
        {
            if (first.Value < second.Value) { return true; }
            if (first.Value > second.Value) { return false; }

            return null;
        }

        if (first.IsList && second.IsList)
        {
            for (int i = 0; i < Math.Min(first.Items!.Count, second.Items!.Count); i++)
            {
                var ret = CompareItems(first.Items[i], second.Items[i]);
                if (ret is not null) { return ret; }
            }

            if (first.Items.Count < second.Items.Count) { return true; }
            if (first.Items.Count > second.Items.Count) { return false; }

            return null;
        }

        if (first.IsInt && second.IsList)
        {
            List<Item> list = new() { new Item(first.Value!.Value) };
            return CompareItems(new(list), second);
        }

        if (first.IsList && second.IsInt)
        {
            List<Item> list = new() { new Item(second.Value!.Value) };
            return CompareItems(first, new(list));
        }

        return null;
    }
}
