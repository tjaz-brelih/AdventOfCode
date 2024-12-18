using var file = new StreamReader("input.txt");

List<Robot> robots = [];
Span<Range> ranges = stackalloc Range[2];

while (file.ReadLine() is string line)
{
    var span = line.AsSpan();

    span.Split(ranges, ' ');

    var position = span[ranges[0]][2..];
    var velocity = span[ranges[1]][2..];

    position.Split(ranges, ',');

    var x = int.Parse(position[ranges[0]]);
    var y = int.Parse(position[ranges[1]]);

    velocity.Split(ranges, ',');

    var vx = int.Parse(velocity[ranges[0]]);
    var vy = int.Parse(velocity[ranges[1]]);

    robots.Add(new(x, y, vx, vy));
}


var space = (X: 101, Y: 103);
var seconds = 100;

for (int i = 0; i < seconds; i++)
{
    foreach (var robot in robots)
    {
        robot.PositionX += robot.Velocity.X;
        robot.PositionY += robot.Velocity.Y;

        if (robot.PositionX < 0) { robot.PositionX += space.X; }
        else if (robot.PositionX >= space.X) { robot.PositionX -= space.X; }

        if (robot.PositionY < 0) { robot.PositionY += space.Y; }
        else if (robot.PositionY >= space.Y) { robot.PositionY -= space.Y; }
    }
}


var resultOne = robots.Where(x => x.PositionX != space.X / 2 && x.PositionY != space.Y / 2).GroupBy(x => (x.PositionX < space.X / 2, x.PositionY < space.Y / 2)).Aggregate(1L, (acc, x) => acc * x.Count());
Console.WriteLine(resultOne);



class Robot(int x, int y, int vx, int vy)
{
    public int PositionX { get; set; } = x;
    public int PositionY { get; set; } = y;

    public (int X, int Y) StartingPosition { get; } = (x, y);
    public (int X, int Y) Velocity { get; } = (vx, vy);
}