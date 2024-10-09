using System;

namespace HumanReadableTime
{
    class Program
    {
        static void Main(string[] args)
        {
            //https://www.codewars.com/kata/52742f58faf5485cae000b9a/train/csharp

            Console.Write("Enter how many seconds you want to convert: ");
            string input = Console.ReadLine();

            int seconds = Convert.ToInt32(input);

            if (seconds == 0)
                Console.WriteLine("now");

            int years = seconds / 31536000; // 60 * 60 * 24 * 365

            int days = (seconds % 31536000) / 86400; // 60 * 60 * 24

            int hours = (seconds % 86400) / 3600; // 60 * 60

            int minutes = (seconds % 3600) / 60;

            int remainingSeconds = seconds % 60;

            //Formatting
            //If any period of time after the current one is greater than 0, then add a comma
            //If the current period of time is > 1, then add 's' to pluralise


            int tracker = 0;

            string formatted = "";

            if (years > 0) {
                formatted += $"{years} year{((years > 1) ? "s" : "")}{((days > 0 | hours > 0 | minutes > 0 | remainingSeconds > 0) ? "," : "")} ";
                tracker += 1;
            }

            if (days > 0) {
                formatted += $"{days} day{((days > 1) ? "s" : "")}{((hours > 0 | minutes > 0 | remainingSeconds > 0) ? "," : "")} ";
                tracker += 1;
            }

            if (hours > 0) {
                formatted += $"{hours} hour{(hours > 1 ? "s" : "")}{((minutes > 0) ? "," : "")} ";
                tracker += 1;
            }

            if (minutes > 0) {
                formatted += $"{minutes} minute{(minutes > 1 ? "s" : "")} {((remainingSeconds > 0) ? "and" : "")} ";
                tracker += 1;
            }

            if (remainingSeconds > 0) {

                formatted += $"{remainingSeconds} second{(remainingSeconds > 1 ? "s" : "")}";
                tracker += 1;
            }


            //Grammar: Error Correction
            //If we have more than 2 periods of times, and no 'and', then replace last comma with 'and'

            if (tracker >= 2 && !formatted.Contains("and"))
            {

                int place = formatted.LastIndexOf(",");
                if (place != -1) //Comma was found
                {
                    formatted = $"{formatted.Substring(0, place)} and {formatted.Substring(place + 2)}"; //Reason why ',' not included in substring parameters is due to the ' '
                }
            }

            Console.WriteLine($"{formatted.Trim()}.");

            Console.ReadKey();
        }
    }
}