using Common;

var lines = Helpers.ReadInputFile();

Dictionary<string, List<string>> tree = [];

foreach (var line in lines)
{
    var span = line.AsSpan();

    List<string> outputs = [];
    var device = span[..3].ToString();

    tree.Add(device, outputs);

    var outputSpan = span[5..];

    foreach (var range in outputSpan.Split(' '))
    {
        outputs.Add(outputSpan[range].ToString());
    }
}



Queue<string> q = [];
var resultOne = 0;

q.Enqueue("you");

while (q.Count > 0)
{
    var current = q.Dequeue();
    if (current == "out") { resultOne++; }
    else
    {
        foreach (var neighbor in tree[current]) { q.Enqueue(neighbor); }
    }
}

Console.WriteLine(resultOne);