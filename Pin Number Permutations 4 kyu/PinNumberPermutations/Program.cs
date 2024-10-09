using System;
using System.Collections.Generic;

     static List<string> GetPINs(string observed)
    {

        Dictionary<int, List<int>> adjacentKeys = new Dictionary<int, List<int>>()
    {
    { 1, new List<int> { 1, 2, 4 } },
    { 2, new List<int> { 1, 2, 3, 5 } },
    { 3, new List<int> { 2, 3, 6 } },
    { 4, new List<int> { 1, 4, 5, 7 } },
    { 5, new List<int> { 2, 4, 5, 6, 8 } },
    { 6, new List<int> { 3, 5, 6, 9 } },
    { 7, new List<int> { 4, 7, 8 } },
    { 8, new List<int> { 5, 7, 8, 9, 0 } },
    { 9, new List<int> { 6, 8, 9 } },
    { 0, new List<int> { 0, 8 } }
    };

        char[] charArray = observed.ToCharArray();
        List<string> possiblePins = new List<string> { "" };

        for (int i = 0; i < charArray.Length; i++)
        {
            List<string> currentVariation = new List<string>();
            int currentNum = (charArray[i] - '0');
            List<int> variations = adjacentKeys[currentNum];

            foreach (var combination in possiblePins)
            {
                foreach (var variation in variations)
                {
                    currentVariation.Add(combination + variation.ToString());
                }
            }
            possiblePins = currentVariation;
        }
        return possiblePins;
}

Console.WriteLine("Enter a pin of length 1 to 8 inclusive: ");
string observed = Console.ReadLine();
Console.WriteLine();
foreach (string guess in GetPINs(observed))
{
    Console.WriteLine(guess);
}