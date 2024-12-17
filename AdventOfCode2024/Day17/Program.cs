using Common;

var lines = Helpers.ReadInputFile();

var a = long.Parse(lines[0].AsSpan()[12..]);
var program = lines[4][9..].Split(',').Select(int.Parse).ToArray();


var output = RunProgram(program, a);
var resultOne = string.Join(',', output);
Console.WriteLine(resultOne);


a = 124681572752;

while (true)
{
    output = RunProgram(program, a, true);
    if (output.SequenceEqual(program)) { break; }

    a++;
}

var resultTwo = a;
Console.WriteLine(resultTwo);



static List<int> RunProgram(int[] program, long a, bool checkOutput = false)
{
    List<int> output = [];
    var (b, c) = (0L, 0L);

    for (int i = 0; i < program.Length; i += 2)
    {
        var opcode = program[i];
        var literal = program[i + 1];

        var combo = literal switch
        {
            4 => a,
            5 => b,
            6 => c,
            _ => literal
        };

        if (opcode == 0) { a >>= (int) combo; }
        else if (opcode == 1) { b ^= literal; }
        else if (opcode == 2) { b = combo % 8; }
        else if (opcode == 3 && a != 0) { i = literal - 2; }
        else if (opcode == 4) { b ^= c; }
        else if (opcode == 5)
        {
            output.Add((int) (combo % 8));

            if (checkOutput && (output.Count >= program.Length || !StartsWith(program, output))) { break; }
        }
        else if (opcode == 6) { b = a >> (int) combo; }
        else if (opcode == 7) { c = a >> (int) combo; }
    }

    return output;
}

static bool StartsWith(IList<int> program, IList<int> output)
{
    for (var i = 0; i < output.Count; i++) { if (output[i] != program[i]) { return false; } }

    return true;
}