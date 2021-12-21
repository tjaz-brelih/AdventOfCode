using var file = new StreamReader("input.txt");

List<int> startingPositions = new()
{
    int.Parse(file.ReadLine().AsSpan()[28..]),
    int.Parse(file.ReadLine().AsSpan()[28..])
};



var dice = 1;
var diceRolled = 0;
var winningCondition = false;
var players = startingPositions.Select(p => new Player { Position = p }).ToList();

while (!winningCondition)
{
    foreach (var player in players)
    {
        var value = 0;
        for (int j = 0; j < 3; j++)
        {
            value += dice++;
            if (dice > 100)
            {
                dice = 1;
            }
        }
        diceRolled += 3;

        player.Position += value;
        player.Position = (player.Position % 10 == 0 ? 10 : 0) + (player.Position % 10);

        player.Score += player.Position;
        if (player.Score >= 1000)
        {
            winningCondition = true;
            break;
        }
    }
}

var resultOne = diceRolled * players.Min(p => p.Score);
Console.WriteLine(resultOne);



var (p1Wins, p2Wins) = SimulateGame2(startingPositions[0], startingPositions[1]);
var resultTwo = Math.Max(p1Wins, p2Wins);
Console.WriteLine(resultTwo);



static (ulong, ulong) SimulateGame2(int p1Pos, int p2Pos, int p1Score = 0, int p2Score = 0, bool isPlayer1 = true)
{
    var scoreThreshold = 21;

    var (position, score) = isPlayer1 ? (p1Pos, p1Score) : (p2Pos, p2Score);
    var p1Wins = 0ul;
    var p2Wins = 0ul;

    for (int i = 3; i <= 9; i++)
    {
        var positionNew = position + i;
        positionNew = (positionNew % 10 == 0 ? 10 : 0) + (positionNew % 10);
        var scoreNew = score + positionNew;

        if (scoreNew >= scoreThreshold)
        {
            var scoring = Scoring(i);

            if (isPlayer1) p1Wins += scoring;
            else p2Wins += scoring;

            continue;
        }

        var (p1PosNew, p2PosNew, p1ScoreNew, p2ScoreNew) = isPlayer1 ? (positionNew, p2Pos, scoreNew, p2Score) : (p1Pos, positionNew, p1Score, scoreNew);
        var (p1, p2) = SimulateGame2(p1PosNew, p2PosNew, p1ScoreNew, p2ScoreNew, !isPlayer1);

        p1Wins += p1 * Scoring(i);
        p2Wins += p2 * Scoring(i);
    }

    return (p1Wins, p2Wins);
}


static ulong Scoring(int x)
    => x switch
    {
        3 or 9 => 1,
        4 or 8 => 3,
        5 or 7 => 6,
        _ => 7
    };


class Player
{
    public int Position { get; set; }
    public int Score { get; set; }
}