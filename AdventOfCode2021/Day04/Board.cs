public class Board
{
    public int[,] Numbers { get; }

    public int[] Rows { get; }
    public int[] Columns { get; }

    public bool IsComplete => this.Rows.Any(r => r == this.Numbers.GetLength(0)) || this.Columns.Any(c => c == this.Numbers.GetLength(0));



    public Board(int[,] numbers)
    {
        this.Numbers = numbers;
        this.Rows = new int[numbers.GetLength(0)];
        this.Columns = new int[numbers.GetLength(0)];
    }



    public void Reset()
    {
        for (int i = 0; i < this.Numbers.GetLength(0); i++)
        {
            this.Rows[i] = 0;
            this.Columns[i] = 0;
        }
    }


    public bool MarkNumber(int number)
    {
        for (int i = 0; i < this.Numbers.GetLength(0); i++)
        {
            for (int j = 0; j < this.Numbers.GetLength(1); j++)
            {
                if (this.Numbers[i, j] == number)
                {
                    this.Rows[i]++;
                    this.Columns[j]++;
                }
            }
        }

        return this.IsComplete;
    }

    public int Score(IEnumerable<int> drawnNumbers)
    {
        var sum = 0;
        foreach (var number in this.Numbers)
        {
            if (!drawnNumbers.Contains(number))
            {
                sum += number;
            }
        }
        return sum;
    }
}
