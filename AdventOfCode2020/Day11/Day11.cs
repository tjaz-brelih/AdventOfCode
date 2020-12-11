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


var rowsCount = lines.Count + 2;
var columnsCount = lines.First().Length + 2;

var currentState = new char[rowsCount, columnsCount];
var prevState = new char[rowsCount, columnsCount];

PopulateArrays(lines, ref currentState, ref prevState);



while (true)
{
    for (int i = 1; i < rowsCount - 1; i++)
    {
        for (int j = 1; j < columnsCount - 1; j++)
        {
            var seatState = prevState[i, j];
            if (seatState == '.')
            {
                continue;
            }

            var occupiedCount = 0;

            occupiedCount += prevState[i - 1, j - 1] == '#' ? 1 : 0;
            occupiedCount += prevState[i - 1, j + 1] == '#' ? 1 : 0;
            occupiedCount += prevState[i - 1, j] == '#' ? 1 : 0;

            occupiedCount += prevState[i + 1, j - 1] == '#' ? 1 : 0;
            occupiedCount += prevState[i + 1, j + 1] == '#' ? 1 : 0;
            occupiedCount += prevState[i + 1, j] == '#' ? 1 : 0;

            occupiedCount += prevState[i, j - 1] == '#' ? 1 : 0;
            occupiedCount += prevState[i, j + 1] == '#' ? 1 : 0;


            var newSeatState = occupiedCount switch
            {
                0 => '#',
                var x when x >= 4 => 'L',
                _ => seatState
            };

            currentState[i, j] = newSeatState;
        }
    }


    if (AreEqual(currentState, prevState))
    {
        break;
    }


    Copy(currentState, ref prevState);
}

var resultOne = Count(currentState);
Console.WriteLine(resultOne);




PopulateArrays(lines, ref currentState, ref prevState);

while (true)
{
    for (int i = 1; i < rowsCount - 1; i++)
    {
        for (int j = 1; j < columnsCount - 1; j++)
        {
            var seatState = prevState[i, j];
            if (seatState == '.')
            {
                continue;
            }

            var occupiedCount = 0;

            occupiedCount += FirstSeat(prevState, (i, j), (-1, -1)) == '#' ? 1 : 0;
            occupiedCount += FirstSeat(prevState, (i, j), (-1, 0)) == '#' ? 1 : 0;
            occupiedCount += FirstSeat(prevState, (i, j), (-1, 1)) == '#' ? 1 : 0;
            
            occupiedCount += FirstSeat(prevState, (i, j), (1, -1)) == '#' ? 1 : 0;
            occupiedCount += FirstSeat(prevState, (i, j), (1, 0)) == '#' ? 1 : 0;
            occupiedCount += FirstSeat(prevState, (i, j), (1, 1)) == '#' ? 1 : 0;
            
            occupiedCount += FirstSeat(prevState, (i, j), (0, -1)) == '#' ? 1 : 0;
            occupiedCount += FirstSeat(prevState, (i, j), (0, 1)) == '#' ? 1 : 0;


            var newSeatState = occupiedCount switch
            {
                0 => '#',
                var x when x >= 5 => 'L',
                _ => seatState
            };

            currentState[i, j] = newSeatState;
        }
    }


    if (AreEqual(currentState, prevState))
    {
        break;
    }


    Copy(currentState, ref prevState);
}

var resultTwo = Count(currentState);
Console.WriteLine(resultTwo);





static void PopulateArrays(IList<string> input, ref char[,] first, ref char[,] second)
{
    var rowsCount = input.Count + 2;
    var columnsCount = input.First().Length + 2;

    for (int i = 0; i < rowsCount; i++)
    {
        for (int j = 0; j < columnsCount; j++)
        {
            first[i, j] = '.';
            second[i, j] = '.';
        }
    }

    for (int i = 0; i < rowsCount - 2; i++)
    {
        for (int j = 0; j < columnsCount - 2; j++)
        {
            first[i + 1, j + 1] = input[i][j];
            second[i + 1, j + 1] = input[i][j];
        }
    }
}


static void Print(char[,] input)
{
    for (int i = 0; i < input.GetLength(0); i++)
    {
        for (int j = 0; j < input.GetLength(1); j++)
        {
            Console.Write(input[i, j]);
        }

        Console.WriteLine();
    }
}


static void Copy(char[,] from, ref char[,] to)
{
    for (int i = 0; i < from.GetLength(0); i++)
    {
        for (int j = 0; j < from.GetLength(1); j++)
        {
            to[i, j] = from[i, j];
        }
    }
}


static bool AreEqual(char[,] first, char[,] second)
{
    for (int i = 0; i < first.GetLength(0); i++)
    {
        for (int j = 0; j < first.GetLength(1); j++)
        {
            if (first[i, j] != second[i, j])
            {
                return false;
            }
        }
    }

    return true;
}


static char FirstSeat(char[,] input, (int x, int y) location, (int x, int y) direction)
{
    while (true)
    {
        location.x += direction.x;
        location.y += direction.y;

        if (location.x <= 0 || location.y <= 0 || location.x >= input.GetLength(0) || location.y >= input.GetLength(1))
        {
            break;
        }

        var currentSeat = input[location.x, location.y];

        if (currentSeat == '#' || currentSeat == 'L')
        {
            return currentSeat;
        }
    }


    return '.';
}


static int Count(char[,] input, char term = '#')
{
    var count = 0;

    for (int i = 0; i < input.GetLength(0); i++)
    {
        for (int j = 0; j < input.GetLength(1); j++)
        {
            if (input[i, j] == term)
            {
                count++;
            }
        }
    }

    return count;
}