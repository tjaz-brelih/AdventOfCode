using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



using var file = new StreamReader("input.txt");
var startingNumbers = file.ReadLine().Split(',').Select(n => int.Parse(n));



Dictionary<int, (int, int)> spokenAtTurns = new();

var lastSpokenNumber = 0;
var currentTurn = 1;

foreach (var number in startingNumbers)
{
    SpeakNumber(spokenAtTurns, number, currentTurn);

    lastSpokenNumber = number;
    currentTurn++;
}

while (currentTurn <= 2020)
{
    var spokenBefore = spokenAtTurns.TryGetValue(lastSpokenNumber, out var lastTurns);

    lastSpokenNumber = (spokenBefore && lastTurns.Item2 != 0) switch
    {
        true => lastTurns.Item1 - lastTurns.Item2,
        false => 0
    };

    SpeakNumber(spokenAtTurns, lastSpokenNumber, currentTurn);

    currentTurn++;
}

var resultOne = lastSpokenNumber;
Console.WriteLine(resultOne);



spokenAtTurns.Clear();
lastSpokenNumber = 0;
currentTurn = 1;

foreach (var number in startingNumbers)
{
    SpeakNumber(spokenAtTurns, number, currentTurn);

    lastSpokenNumber = number;
    currentTurn++;
}

while (currentTurn <= 30000000)
{
    var spokenBefore = spokenAtTurns.TryGetValue(lastSpokenNumber, out var lastTurns);

    lastSpokenNumber = (spokenBefore && lastTurns.Item2 != 0) switch
    {
        true => lastTurns.Item1 - lastTurns.Item2,
        false => 0
    };

    SpeakNumber(spokenAtTurns, lastSpokenNumber, currentTurn);

    currentTurn++;
}

var resultTwo = lastSpokenNumber;
Console.WriteLine(resultTwo);





static void SpeakNumber(Dictionary<int, (int, int)> turns, int valueSpoken, int currentTurn)
{
    turns[valueSpoken] = turns.TryGetValue(valueSpoken, out var lastTurns) switch
    {
        true => (currentTurn, lastTurns.Item1),
        false => (currentTurn, 0)
    };
}