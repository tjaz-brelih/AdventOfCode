using var file = new StreamReader("input.txt");

Dictionary<int, Stack<char>> stacksOriginal = new();
List<(int, int, int)> moves = new();

while (file.ReadLine() is string line)
{
    int index = line.IndexOf('[');
    if (index == -1)
    {
        break;
    }

    do
    {
        index++;
        var ch = line[index];

        var stackIndex = (index - 1) / 4;

        if (!stacksOriginal.ContainsKey(stackIndex))
        {
            stacksOriginal[stackIndex] = new();
        }

        stacksOriginal[stackIndex].Push(ch);
    } while ((index = line.IndexOf('[', index)) != -1);
}

file.ReadLine();

while (file.ReadLine() is string line)
{
    var span = line.AsSpan()[5..];

    var index = span.IndexOf(' ');
    var count = int.Parse(span[..index]);
    span = span[(index + 1)..];

    var startStack = int.Parse(span[5..6]);
    var endStack = int.Parse(span[10..11]);

    moves.Add((count, startStack, endStack));
}


// Part 1
var stacks = CloneDictionary(stacksOriginal);

foreach (var (count, start, end) in moves)
{
    var startStack = stacks[start - 1];
    var endStack = stacks[end - 1];

    for (int i = 0; i < count; i++)
    {
        endStack.Push(startStack.Pop());
    }
}

char[] resultOne = new char[stacks.Count];

foreach (var (key, stack) in stacks)
{
    resultOne[key] = stack.Peek();
}

Console.WriteLine(resultOne);


// Part 2
stacks = CloneDictionary(stacksOriginal);

foreach (var (count, start, end) in moves)
{
    var tempStack = new Stack<char>();
    var startStack = stacks[start - 1];
    var endStack = stacks[end - 1];

    for (int i = 0; i < count; i++)
    {
        tempStack.Push(startStack.Pop());
    }

    for (int i = 0; i < count; i++)
    {
        endStack.Push(tempStack.Pop());
    }
}

char[] resultTwo = new char[stacks.Count];

foreach (var (key, stack) in stacks)
{
    resultTwo[key] = stack.Peek();
}

Console.WriteLine(resultTwo);




static Dictionary<int, Stack<char>> CloneDictionary(Dictionary<int, Stack<char>> dict)
    => dict.ToDictionary(kv => kv.Key, kv =>
    {
        var newStack = new Stack<char>();

        foreach (var el in kv.Value)
        {
            newStack.Push(el);
        }

        return newStack;
    });
