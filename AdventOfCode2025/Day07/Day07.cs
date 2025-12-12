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
