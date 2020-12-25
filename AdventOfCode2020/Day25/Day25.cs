using System;
using System.IO;



using var file = new StreamReader("input.txt");

var pkCard = ulong.Parse(file.ReadLine());
var pkDoor = ulong.Parse(file.ReadLine());



var divider = 20201227U;
var subjectNumber = 7U;

var loopSizeCard = FindLoopSize(pkCard, divider, subjectNumber);
var encryptionKeyCard = GenerateEncryptionKey(pkDoor, loopSizeCard, divider);
Console.WriteLine(encryptionKeyCard);




static ulong FindLoopSize(ulong pk, ulong divider, ulong subjectNumber, ulong value = 1U)
{
    var loopSize = 0U;

    while (value != pk)
    {
        value *= subjectNumber;
        value %= divider;

        loopSize++;
    }

    return loopSize;
}


static ulong GenerateEncryptionKey(ulong pk, ulong loopSize, ulong divider, ulong value = 1U)
{
    for (ulong i = 0; i < loopSize; i++)
    {
        value *= pk;
        value %= divider;
    }

    return value;
}