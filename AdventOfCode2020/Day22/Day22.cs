using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;



using var file = new StreamReader("input.txt");

List<string> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(line);
}



List<Queue<int>> players = new();
Queue<int> currentQueue = null;

foreach (var line in lines)
{
    if (line.StartsWith("Player"))
    {
        currentQueue = new();
        players.Add(currentQueue);
    }
    else if (int.TryParse(line, out var number))
    {
        currentQueue.Enqueue(number);
    }
}

var winnerOne = PlayGame(players[0], players[1]);

var resultOne = winnerOne.Reverse().Select((c, i) => c * (i + 1)).Sum();
Console.WriteLine(resultOne);




players.Clear();

foreach (var line in lines)
{
    if (line.StartsWith("Player"))
    {
        currentQueue = new();
        players.Add(currentQueue);
    }
    else if (int.TryParse(line, out var number))
    {
        currentQueue.Enqueue(number);
    }
}

var winnerTwo = PlayGameRecursive(players[0], players[1]);

var resultTwo = winnerTwo.Reverse().Select((c, i) => c * (i + 1)).Sum();
Console.WriteLine(resultTwo);




static Queue<int> PlayGame(Queue<int> playerOne, Queue<int> playerTwo)
{
    while (playerOne.Count > 0 && playerTwo.Count > 0)
    {
        var p1Card = playerOne.Dequeue();
        var p2Card = playerTwo.Dequeue();

        var winner = p1Card > p2Card ? playerOne : playerTwo;
        (int winCard, int loseCard) = (winner == playerOne) ? (p1Card, p2Card) : (p2Card, p1Card);

        winner.Enqueue(winCard);
        winner.Enqueue(loseCard);
    }


    return playerOne.Count > playerTwo.Count ? playerOne : playerTwo;
}


static Queue<int> PlayGameRecursive(Queue<int> playerOne, Queue<int> playerTwo)
{
    HashSet<string> handsPlayerOne = new();
    HashSet<string> handsPlayerTwo = new();

    while (playerOne.Count > 0 && playerTwo.Count > 0)
    {
        var currentHandPlayerOne = IntsToString(playerOne);
        var currentHandPlayerTwo = IntsToString(playerTwo);

        if (handsPlayerOne.Contains(currentHandPlayerOne) || handsPlayerTwo.Contains(currentHandPlayerTwo))
        {
            return playerOne;
        }

        handsPlayerOne.Add(currentHandPlayerOne);
        handsPlayerTwo.Add(currentHandPlayerTwo);



        var p1Card = playerOne.Dequeue();
        var p2Card = playerTwo.Dequeue();

        Queue<int> winner = null;

        if (p1Card <= playerOne.Count && p2Card <= playerTwo.Count)
        {
            var copyPlayerOne = CloneQueue(playerOne, p1Card);
            var copyPlayerTwo = CloneQueue(playerTwo, p2Card);

            var winnerTemp = PlayGameRecursive(copyPlayerOne, copyPlayerTwo);

            winner = winnerTemp == copyPlayerOne ? playerOne : playerTwo;
        }

        winner ??= p1Card > p2Card ? playerOne : playerTwo;
        (int winCard, int loseCard) = (winner == playerOne) ? (p1Card, p2Card) : (p2Card, p1Card);

        winner.Enqueue(winCard);
        winner.Enqueue(loseCard);
    }


    return playerOne.Count > playerTwo.Count ? playerOne : playerTwo;
}


static string IntsToString(IEnumerable<int> input)
{
    StringBuilder builder = new();

    foreach (var item in input)
    {
        builder.AppendFormat("{0}", item);
    }

    return builder.ToString();
}


static Queue<T> CloneQueue<T>(Queue<T> input, int number)
{
    var output = new Queue<T>();
    var count = 0;

    foreach (var item in input)
    {
        if (count++ >= number)
        {
            break;
        }

        output.Enqueue(item);
    }

    return output;
}