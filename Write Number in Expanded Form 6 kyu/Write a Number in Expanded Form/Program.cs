internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter a number: ");
        long num = long.Parse(Console.ReadLine());


        // Start off as empty sentence

        string sentence = "";

        string numString = num.ToString();
        int length = numString.Length;

        for (int i = 0; i < length; i++) // Go through each digit
        {
            string zeros = "";
            int digit = int.Parse(numString[i].ToString());
            if (digit != 0) // e.g. in 70304 we are at 7
            {
                int num_of_zeros = numString.Substring(i + 1).Length; // Length of 0304 which is 4

                for (int j = 0; j < num_of_zeros; j++)
                {
                    zeros += "0"; // Now zeros is 0000
                }

                string expanded = digit.ToString() + zeros;
                sentence += $" + {expanded}";
            }
        }

        Console.WriteLine(sentence.Substring(3));
        Console.ReadKey();
    }
}