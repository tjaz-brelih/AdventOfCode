﻿using var file = new StreamReader("input.txt");

List<string> input = new();

while (file.ReadLine() is string line)
{
    input.Add(line);
}