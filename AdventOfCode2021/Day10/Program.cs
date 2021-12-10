using var file = new StreamReader("input.txt");

List<string> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(line);
}

HashSet<char> openings = new() { '(', '[', '{', '<' };
Dictionary<char, (uint Part1, uint Part2)> scoring = new() { { ')', (3, 1) }, { ']', (57, 2) }, { '}', (1197, 3) }, { '>', (25137, 4) } };



ulong resultOne = 0;
Stack<char> stack = new();

foreach (var line in lines)
{
    foreach (var c in line)
    {
        if (openings.Contains(c))
        {
            stack.Push(CloseBracket(c));
        }
        else if (stack.Pop() != c)
        {
            resultOne += scoring[c].Part1;
            break;
        }
    }

    stack.Clear();
}

Console.WriteLine(resultOne);



List<ulong> scores = new();

foreach (var line in lines)
{
    var corrupted = false;

    foreach (var c in line)
    {
        if (openings.Contains(c))
        {
            stack.Push(CloseBracket(c));
        }
        else if (stack.Pop() != c)
        {
            corrupted = true;
            break;
        }
    }

    if (!corrupted)
    {
        scores.Add(stack.Aggregate(0ul, (a, c) => a * 5 + scoring[c].Part2));
    }

    stack.Clear();
}

var resultTwo = scores.OrderByDescending(x => x).Skip(scores.Count / 2).First();
Console.WriteLine(resultTwo);



static char CloseBracket(char c)
    => c switch
    {
        '(' => ')',
        '[' => ']',
        '{' => '}',
        '<' => '>',
        _ => ' '
    };