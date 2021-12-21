using var file = new StreamReader("inputTest.txt");

var lookup = file.ReadLine();
file.ReadLine();

List<string> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(line);
}

var input = new byte[lines.Count, lines[0].Length];
for (int i = 0; i < lines.Count; i++)
{
    for (int j = 0; j < lines[0].Length; j++)
    {
        input[i, j] = (byte) (lines[i][j] == '#' ? 1 : 0);
    }
}



var inputOne = (byte[,]) input.Clone();
for (int c = 0; c < 2; c++)
{
    inputOne = ResizeArray(inputOne);
    var output = new byte[inputOne.GetLength(0), inputOne.GetLength(1)];

    for (int i = 0; i < inputOne.GetLength(0); i++)
    {
        for (int j = 0; j < inputOne.GetLength(1); j++)
        {
            var value = GetNumericValue(inputOne, i, j, lookup![0] == '#' && c % 2 == 1);
            output[i, j] = (byte) (lookup![value] == '#' ? 1 : 0);
        }
    }

    inputOne = output;
    //PrintArray(inputOne);
}

var resultOne = 0;
for (int i = 0; i < inputOne.GetLength(0); i++)
{
    for (int j = 0; j < inputOne.GetLength(1); j++)
    {
        resultOne += inputOne[i, j];
    }
}
Console.WriteLine(resultOne);



static byte[,] ResizeArray(byte[,] arr, int resize = 2)
{
    var output = new byte[arr.GetLength(0) + resize, arr.GetLength(1) + resize];
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        for (int j = 0; j < arr.GetLength(1); j++)
        {
            output[i + resize / 2, j + resize / 2] = arr[i, j];
        }
    }
    return output;
}

static int GetNumericValue(byte[,] arr, int x, int y, bool fillOnes = false)
{
    int value = 0;
    for (int i = -1; i <= 1; i++)
    {
        for (int j = -1; j <= 1; j++)
        {
            value <<= 1;

            if (x + i < 0 || y + j < 0 || x + i >= arr.GetLength(0) || y + j >= arr.GetLength(1))
            {
                if (fillOnes)
                {
                    value |= 1;
                }

                continue;
            }

            value |= arr[x + i, y + j];
        }
    }
    return value;
}

static void PrintArray(byte[,] arr)
{
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        for (int j = 0; j < arr.GetLength(1); j++)
        {
            Console.Write(arr[i, j] == 1 ? '#' : '.');
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}