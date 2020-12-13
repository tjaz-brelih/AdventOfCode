using System;
using System.IO;
using System.Linq;



using var file = new StreamReader("input2.txt");

var earliest = int.Parse(file.ReadLine());
var busIds = file.ReadLine().Split(',').AsEnumerable();



var resultOne = busIds
    .Where(b => b != "x")
    .Select(b => new
    {
        Id = int.Parse(b),
        Arrival = ((decimal) earliest / int.Parse(b))
    })
    .OrderByDescending(b => b.Arrival % 1)
    .First();

Console.WriteLine(resultOne.Id * ((resultOne.Id * Math.Ceiling(resultOne.Arrival)) - earliest));



