using Common;
using ButtonList = System.Collections.Generic.List<System.Collections.Generic.List<int>>;

var lines = Helpers.ReadInputFile();

List<Machine> machines = [];

foreach (var line in lines)
{
    var span = line.AsSpan();


    var index = span.IndexOf(']');
    var state = 0;
    var i = 0;

    foreach (var x in span[1..index])
    {
        state |= x == '#' ? (1 << i) : 0;
        i++;
    }


    span = span[index..];
    ButtonList buttons = [];

    while (true)
    {
        var (start, end) = (span.IndexOf('('), span.IndexOf(')'));
        if (start < 0) { break; }

        buttons.Add([]);

        var lightSpan = span[(start + 1)..end];
        foreach (var range in lightSpan.Split(',')) { buttons.Last().Add(int.Parse(lightSpan[range])); }

        span = span[(end + 1)..];
    }


    var (startJ, endJ) = (span.IndexOf('{'), span.IndexOf('}'));
    var joltSpan = span[(startJ + 1)..endJ];
    List<int> jolts = [];

    foreach (var range in joltSpan.Split(','))
    {
        jolts.Add(int.Parse(joltSpan[range]));
    }


    machines.Add(new(state, jolts, buttons));
}

var resultOne = machines.Sum(BFS1);
Console.WriteLine(resultOne);



static int BFS1(Machine machine)
{
    Queue<(int State, int Depth)> q = [];
    q.Enqueue((0, 0));

    while (q.Count > 0)
    {
        var (state, depth) = q.Dequeue();
        if (state == machine.State) { return depth; }

        foreach (var item in machine.Buttons)
        {
            q.Enqueue((MutateState(state, item), depth + 1));
        }
    }

    return 0;
}

static int MutateState(int state, List<int> buttons)
{
    for (var i = 0; i < buttons.Count; i++) { state ^= 1 << buttons[i]; }
    return state;
}

record Machine(int State, List<int> Jolts, ButtonList Buttons);