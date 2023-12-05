using var file = new StreamReader("inputTest.txt");

List<Valve> valves = new();

while (file.ReadLine() is string line)
{
    var span = line.AsSpan();

    var index = span.IndexOf(';');

    var valve = new Valve(span[6..8].ToString(), int.Parse(span[23..index]));
    valves.Add(valve);

    span = span[(index + 23)..];
    span = span[(span.IndexOf(' ') + 1)..];

    while (true)
    {
        index = span.IndexOf(',');
        if (index == -1)
        {
            valve.TunnelNames.Add(span.ToString());
            break;
        }

        valve.TunnelNames.Add(span[..index].ToString());

        span = span[(index + 2)..];
    }
}

foreach (var valve in valves)
{
    foreach (var name in valve.TunnelNames)
    {
        valve.Tunnels.Add(valves.First(x => x.Name == name));
    }
}


// Part 1
var currentValve = valves.First();
var isOpening = false;

var resultOne = 0;

for (int i = 1; i <= 30; i++)
{
    if (isOpening)
    {
        currentValve.IsOpen = true;
        isOpening = false;
    }

    if (currentValve.IsOpen || currentValve.FlowRate == 0)
    {
        // Move to a better valve
        var newValve = currentValve.Tunnels.Where(x => !x.IsOpen).MaxBy(x => x.FlowRate);
        newValve ??= currentValve.Tunnels.MaxBy(x => x.FlowRate)!;

        currentValve = newValve;
    }
    else
    {
        // Open the valve
        isOpening = true;
    }

    var flowRate = valves.Where(x => x.IsOpen).Sum(x => x.FlowRate);
    resultOne += flowRate;

    Console.WriteLine($"Minute {i}");
    Console.WriteLine(flowRate);
    Console.Write("Valves open: ");
    foreach (var valve in valves)
    {
        if (valve.IsOpen)
        {
            Console.Write($"{valve.Name}, ");
        }
    }
    Console.WriteLine();
}

Console.WriteLine(resultOne);



static List<Valve> PathToFlowiestValve(List<Valve> valves, Valve currentValve)
{
    var path = new List<Valve>();

    var flowiestValve = valves.Where(x => !x.IsOpen).MaxBy(x => x.FlowRate);
    if (flowiestValve is null)
    {
        return path;
    }


}


class Valve
{
    public string Name { get; }
    public int FlowRate { get; }

    public bool IsOpen { get; set; } = false;

    public List<string> TunnelNames { get; } = new();
    public List<Valve> Tunnels { get; } = new();


    public Valve(string name, int flowRate)
    {
        this.Name = name;
        this.FlowRate = flowRate;
    }
}