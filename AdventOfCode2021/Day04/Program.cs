using var file = new StreamReader("input.txt");

var drawingNumbers = file.ReadLine()!.Split(',').Select(n => int.Parse(n)).ToList();

List<Board> boards = new();
Board? currentBoard = null;
int currentPosition = 0;

while (file.ReadLine() is string line)
{
    if (string.IsNullOrEmpty(line))
    {
        currentPosition = 0;
        currentBoard = new(new int[5, 5]);
        boards.Add(currentBoard);
    }
    else
    {
        var array = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < array.Length; i++)
        {
            currentBoard!.Numbers[currentPosition, i] = int.Parse(array[i]);
        }

        currentPosition++;
    }
}



int finalNumber = 0;
Board? winningBoard = null;
HashSet<int> drawnNumbers = new();

foreach (var number in drawingNumbers)
{
    drawnNumbers.Add(number);

    foreach (var board in boards)
    {
        if (board.MarkNumber(number))
        {
            winningBoard = board;
            break;
        }
    }

    if (winningBoard is not null)
    {
        finalNumber = number;
        break;
    }
}

var resultOne = winningBoard!.Score(drawnNumbers) * finalNumber;
Console.WriteLine(resultOne);



foreach (var board in boards)
{
    board.Reset();
}

drawnNumbers = new();

foreach (var number in drawingNumbers)
{
    drawnNumbers.Add(number);

    foreach (var board in boards.Where(b => !b.IsComplete))
    {
        if (board.MarkNumber(number))
        {
            winningBoard = board;
        }
    }

    if (boards.All(b => b.IsComplete))
    {
        finalNumber = number;
        break;
    }
}

var resultTwo = winningBoard!.Score(drawnNumbers) * finalNumber;
Console.WriteLine(resultTwo);
