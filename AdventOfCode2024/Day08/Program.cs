using Common;


var lines = Helpers.ReadInputFile();

var (height, width) = (lines.Count, lines[0].Length);

Dictionary<char, List<Coordinates>> antennae = [];

for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        var ch = lines[i][j];
        if (ch == '.') { continue; }

        var antenna = new Coordinates(j, i);

        if (antennae.TryGetValue(ch, out var coordinates)) { coordinates.Add(antenna); }
        else { antennae[ch] = [antenna]; }
    }
}



HashSet<Coordinates> antinodes = [];

foreach (var (_, antennas) in antennae)
{
    for (var i = 0; i < antennas.Count - 1; i++)
    {
        for (var j = i + 1; j < antennas.Count; j++)
        {
            var delta = antennas[i] - antennas[j];
            var (first, second) = (antennas[i] + delta, antennas[j] - delta);

            if (first.IsValid(height, width)) { antinodes.Add(first); }
            if (second.IsValid(height, width)) { antinodes.Add(second); }
        }
    }
}

Console.WriteLine(antinodes.Count);



antinodes.Clear();

foreach (var (_, antennas) in antennae)
{
    for (var i = 0; i < antennas.Count - 1; i++)
    {
        for (var j = i + 1; j < antennas.Count; j++)
        {
            var delta = antennas[i] - antennas[j];

            var current = antennas[i];
            while (current.IsValid(height, width)) { antinodes.Add(current); current += delta; }

            current = antennas[j];
            while (current.IsValid(height, width)) { antinodes.Add(current); current -= delta; }
        }
    }
}

Console.WriteLine(antinodes.Count);



record Coordinates(int X, int Y)
{
    public bool IsValid(int height, int width) => this.X >= 0 && this.X < width && this.Y >= 0 && this.Y < height;
    public static Coordinates operator +(Coordinates a, Coordinates b) => new(a.X + b.X, a.Y + b.Y);
    public static Coordinates operator -(Coordinates a, Coordinates b) => new(a.X - b.X, a.Y - b.Y);
}