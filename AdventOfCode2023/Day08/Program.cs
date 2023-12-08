using Common;

var lines = Helpers.ReadInputFile();


var instructions = lines[0];

Dictionary<string, (string Left, string Right)> map = [];

foreach (var line in lines.Skip(2))
{
    var name = line[..3];
    var left = line[7..10];
    var right = line[12..15];

    map.Add(name, (left, right));
}


var steps = 0;
var currentNode = "AAA";

while (currentNode != "ZZZ")
{
    currentNode = GetExitNode(map, currentNode, instructions[steps++ % instructions.Length]);
}

Console.WriteLine(steps);



List<long> nodeSteps = [];
var currentNodes = map.Keys.Where(x => x[^1] == 'A').ToArray();

foreach (var startNode in currentNodes)
{
    var stepCount = 0;
    var node = startNode;

    while (node[^1] != 'Z')
    {
        node = GetExitNode(map, node, instructions[stepCount++ % instructions.Length]);
    }

    nodeSteps.Add(stepCount);
}

var resultTwo = Helpers.LeastCommonMultiple(nodeSteps);
Console.WriteLine(resultTwo);




static string GetExitNode(Dictionary<string, (string Left, string Right)> map, string currentNode, char instruction)
    => instruction switch
    {
        'L' => map[currentNode].Left,
        'R' => map[currentNode].Right,
        _ => throw new Exception()
    };