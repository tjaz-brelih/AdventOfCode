using System;
using System.IO;
using System.Collections.Generic;


using var file = new StreamReader("input.txt");

List<string> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(line);
}


HashSet<char> directions = new() { 'N', 'S', 'E', 'W' };


var angle = 0;
(int X, int Y) position = (0, 0);

foreach (var line in lines)
{
    var span = line.AsSpan();

    var action = span[0];
    var value = int.Parse(span[1..]);


    if (directions.Contains(action))
    {
        MoveDirection(ref position, action, value);
    }
    else if (action == 'F')
    {
        MoveAngle(ref position, angle, value);
    }
    else
    {
        _ = action switch
        {
            'L' => angle += value,
            'R' => angle -= value,

            _ => throw new Exception()
        };
    }
}

var resultOne = Math.Abs(position.X) + Math.Abs(position.Y);
Console.WriteLine(resultOne);



angle = 0;
position = (0, 0);
(int X, int Y) waypoint = (10, 1);

foreach (var line in lines)
{
    var span = line.AsSpan();

    var action = span[0];
    var value = int.Parse(span[1..]);


    if (directions.Contains(action))
    {
        MoveDirection(ref waypoint, action, value);
    }
    else if (action == 'F')
    {
        MoveToWaypoint(ref position, waypoint, value);
    }
    else if (action == 'L' || action == 'R')
    {
        RotateWaypoint(ref waypoint, action, value);
    }
}

var resultTwo = Math.Abs(position.X) + Math.Abs(position.Y);
Console.WriteLine(resultTwo);




static void MoveAngle(ref (int X, int Y) position, int angle, int amount)
{
    angle %= 360;

    if (angle < 0)
    {
        angle += 360;
    }

    var direction = angle switch
    {
        0   => 'E',
        90  => 'N',
        180 => 'W',
        270 => 'S',
        _   => throw new Exception()
    };

    MoveDirection(ref position, direction, amount);
}


static void MoveDirection(ref (int X, int Y) position, char direction, int amount)
{
    _ = direction switch
    {
        'N' => position.Y += amount,
        'S' => position.Y -= amount,
        'E' => position.X += amount,
        'W' => position.X -= amount,
        _ => throw new Exception()
    };
}


static void MoveToWaypoint(ref (int X, int Y) position, (int X, int Y) waypoint, int amount)
{
    position.X += waypoint.X * amount;
    position.Y += waypoint.Y * amount;
}


static void RotateWaypoint(ref (int X, int Y) waypoint, char direction, int amount)
{
    Func<(int, int), (int, int)> map = direction switch
    {
        'L' => ((int x, int y) i) => (-i.y, i.x),
        'R' => ((int x, int y) i) => (i.y, -i.x),
        _  => throw new Exception()
    };

    var times = amount / 90;

    for (int i = 0; i < times; i++)
    {
        waypoint = map.Invoke(waypoint);
    }
}