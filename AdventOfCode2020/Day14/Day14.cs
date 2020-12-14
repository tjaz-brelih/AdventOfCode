using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



using var file = new StreamReader("input.txt");

List<string> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(line);
}



string mask = string.Empty;
Dictionary<ulong, ulong> memory = new();

foreach (var line in lines)
{
    var tokens = line.Split('=');

    if (tokens[0].Trim() == "mask")
    {
        mask = tokens[1].Trim();
    }
    else
    {
        var lastBracket = tokens[0].IndexOf(']');

        var location = ulong.Parse(tokens[0][4..lastBracket]);
        var value = ApplyMask(ulong.Parse(tokens[1].Trim()), mask);

        memory[location] = value;
    }
}

var resultOne = memory.Aggregate(0UL, (acc, kv) => acc + kv.Value);
Console.WriteLine(resultOne);




static ulong ApplyMask(ulong value, string mask)
{
    ulong bitMaskZero = 0;
    ulong bitMaskOne = 0;

    for (int i = 0; i < mask.Length; i++)
    {
        if (mask[mask.Length - i - 1] == '0')
        {
            bitMaskZero |= (1UL) << i;
        }
        else if(mask[mask.Length - i - 1] == '1')
        {
            bitMaskOne |= (1UL) << i;
        }
    }

    return (value & (~bitMaskZero)) | bitMaskOne;
}


static List<ulong> ApplyMaskMultiple(ulong value, string mask)
{
    return Enumerable.Empty<ulong>().ToList();
}
