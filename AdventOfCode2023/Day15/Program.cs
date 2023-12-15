using Common;

var lines = Helpers.ReadInputFile();


List<string> tokens = lines.SelectMany(x => x.Split(',')).ToList();


var resultOne = tokens.Sum(x => CalculateHash(x));
Console.WriteLine(resultOne);



Dictionary<int, List<(string Label, int FocalLength)>> boxes = [];

foreach (var token in tokens)
{
    var index = token.IndexOfAny(['=', '-']);

    var label = token[..index];
    var hash = CalculateHash(label);

    if (!boxes.TryGetValue(hash, out var box)) { boxes[hash] = box = []; }

    var lensIndex = box.FindIndex(x => x.Label == label);

    if (token[index] == '=')
    {
        var focalLength = int.Parse(token.AsSpan()[(index + 1)..]);

        if (lensIndex > -1) { box[lensIndex] = (label, focalLength); }
        else { box.Add((label, focalLength)); }
    }
    else
    {
        if (lensIndex > -1) { box.RemoveAt(lensIndex); }
    }
}


var resultTwo = boxes.Sum(x => x.Value.Select((y, i) => (y.FocalLength, Index: i + 1)).Sum(y => (x.Key + 1) * y.Index * y.FocalLength));
Console.WriteLine(resultTwo);




static int CalculateHash(ReadOnlySpan<char> input)
{
    var hash = 0;
    for (int i = 0; i < input.Length; i++)
    {
        hash += input[i];
        hash *= 17;
        hash %= 256;
    }
    return hash;
}
