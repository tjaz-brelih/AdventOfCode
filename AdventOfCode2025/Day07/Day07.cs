using Common;

var lines = Helpers.ReadInputFile();


var resultOne = 0;
HashSet<int> beams = [lines[0].IndexOf('S')];

foreach (var line in lines)
{
    foreach (var beam in beams.Where(x => line[x] == '^').ToList())
    {
        beams.Add(beam - 1);
        beams.Add(beam + 1);
        beams.Remove(beam);

        resultOne += 1;
    }
}

Console.WriteLine(resultOne);



HashSet<int> remove = [];
List<(int, ulong)> add = [];
Dictionary<int, ulong> beamsDict = new() { [lines[0].IndexOf('S')] = 1 };

foreach (var line in lines)
{
    foreach (var beam in beamsDict.Keys.Where(x => line[x] == '^'))
    {
        add.Add((beam - 1, beamsDict[beam]));
        add.Add((beam + 1, beamsDict[beam]));
        remove.Add(beam);
    }

    foreach (var (beam, value) in add)
    {
        if (beamsDict.ContainsKey(beam)) { beamsDict[beam] += value; }
        else { beamsDict[beam] = value; }
    }

    foreach (var beam in remove) { beamsDict.Remove(beam); }

    add.Clear();
    remove.Clear();
}

Console.WriteLine(beamsDict.Values.Aggregate(0ul, (acc, x) => x + acc));
