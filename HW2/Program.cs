using System;
using System.IO;

namespace HW2
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            // Get path of file to process
            Console.Write("Enter name of file containing integers: ");
            var fileName = Console.ReadLine();

            // Check if file exists
            if (fileName == null || !File.Exists(fileName))
            {
                Console.WriteLine("Unable to open file: {0}", fileName);
                Console.Write("Press Return to exit...");
                Console.ReadLine();
                return 1;
            }

            // Read file contents and split into strings at spaces, tabs, and newlines
            var fileContents = File.ReadAllText(fileName);
            var fileStrings = fileContents.Split(new[] {' ', '\t', '\r', '\n'},
                StringSplitOptions.RemoveEmptyEntries);

            // Convert each string into an integer and store in `fileInts[]`
            var fileInts = Array.ConvertAll(fileStrings, int.Parse);

            // Sort the numbers
            Array.Sort(fileInts);

            // Print the array fileInts[]
            Console.WriteLine("------------------------------");
            Array.ForEach(fileInts, i => Console.WriteLine("{0}", i));

            // Exit the program
            Console.WriteLine("------------------------------");
            Console.Write("Press Return to exit...");
            Console.ReadLine();
            return 0;
        }
    }
}
