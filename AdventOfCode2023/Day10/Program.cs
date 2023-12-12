using Common;
using System.Reflection.Metadata.Ecma335;
using Location = (int X, int Y);

var lines = Helpers.ReadInputFile("test.txt");


Location start = (0, 0);

for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == 'S')
        {
            start.X = j;
            start.Y = i;
            break;
        }
    }
}


var loc = start;
var prevLoc = start;
var steps = 0L;

while (true)
{
    var c = lines[loc.Y][loc.X];

    if (c == 'S' && steps > 0) break;


    Location[] potentials = c switch
    {
        'S' => [(-1, 0), (1, 0), (0, -1), (0, 1)],
        '|' => [(0, -1), (0, 1)],
        '-' => [(-1, 0), (1, 0)],
        'L' => [(1, 0), (0, 1)],
        'J' => [(1, 0), (0, -1)],
        '7' => [(-1, 0), (0, 1)],
        'F' => [(1, 0), (0, -1)],
        _ => throw new Exception()
    };


    foreach (var (x, y) in potentials)
    {
        Location pLoc = (loc.X + x, loc.Y + y);

        if (pLoc == prevLoc) continue;
        if (pLoc.X < 0 || pLoc.Y < 0 || pLoc.X >= lines[0].Length || pLoc.Y >= lines.Count) continue;


        var newC = lines[pLoc.Y][pLoc.X];

        var isValid = (c, newC) switch
        {
            (_, '.') => false,
            (_, 'S') => true,
            ('S', _) => true,
                    
            ('|', '-') => false,
            ('-', '|') => false,


            ('|', '|') => true,
            ('|', 'L') => true,
            ('|', 'J') => true,
            ('|', _) => false,

            ('-', '-') => true,
            ('-', 'J') => true,
            ('-', '7') => true,
            ('-', _) => false,

            ('L', '-') => true,
            ('L', '|') => true,
            ('L', _) => false,

            ('J', '-') => true,
            ('J', _) => false,

            ('7', '|') => true,
            ('7', _) => false,

            ('F', '')

        };
    }




    prevLoc = loc;
    loc = ;

    steps++;
}