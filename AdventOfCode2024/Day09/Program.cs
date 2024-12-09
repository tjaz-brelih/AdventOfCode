using Common;


var line = Helpers.ReadInputFile()[0];

var length = (int) line.Sum(char.GetNumericValue);
var arrayOriginal = new int[length];
var index = 0;

for (int i = 0; i < line.Length; i++)
{
    var id = (i % 2) == 0 ? i / 2 : -1;

    var blockSize = (int) char.GetNumericValue(line, i);
    for (var j = 0; j < blockSize; j++) { arrayOriginal[index + j] = id; }
    index += blockSize;
}


var indexEmpty = 0;
var array = (int[]) arrayOriginal.Clone();

for (int i = length - 1; i >= 0; i--)
{
    if (array[i] < 0) { continue; }
    while (array[indexEmpty] >= 0) { indexEmpty++; }
    if (indexEmpty > i) { break; }

    array[indexEmpty] = array[i];
    array[i] = -1;
}

var resultOne = 0L;
for (int i = 0; i < length; i++) { resultOne += array[i] > 0 ? i * array[i] : 0; }

Console.WriteLine(resultOne);


var jump = 1;
array = (int[]) arrayOriginal.Clone();

for (int i = length - 1; i >= 0; i -= jump)
{
    if (array[i] < 0) { jump = 1; continue; }

    var j = i - 1;
    while (j > 0 && array[j] == array[i]) { j--; }
    j++;

    var fileLength = i - j + 1;

    var (indexStart, indexEnd) = (0, 0);

    while (indexStart < j)
    {
        while (array[indexStart] >= 0) { indexStart++; }
        indexEnd = indexStart;

        while (indexEnd < length && array[indexEnd] < 0) { indexEnd++; }
        indexEnd--;

        if (indexEnd - indexStart + 1 >= fileLength) { break; }

        indexStart = indexEnd + 1;
    }

    jump = fileLength;

    if (indexStart >= j) { continue; }

    for (var k = 0; k < fileLength; k++)
    {
        array[indexStart + k] = array[j + k];
        array[j + k] = -1;
    }
}

var resultTwo = 0L;
for (int i = 0; i < length; i++) { resultTwo += array[i] > 0 ? i * array[i] : 0; }

Console.WriteLine(resultTwo);