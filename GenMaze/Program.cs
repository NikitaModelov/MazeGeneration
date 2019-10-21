using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenMaze
{
    class Program
    {
        private static void Main(string[] args)
        {
            string PathInput = "input.txt";
            string PathOutput = "output.txt";
            
            if (args.Length == 2)
            {
                PathInput = args[0];
                if (Path.GetExtension(PathInput) != ".txt" || !File.Exists(PathInput))
                {
                    throw new Exception($"{PathInput} is not found or file extension has invalid extension.");
                }
                PathOutput = args[1];
                if (Path.GetExtension(PathOutput) != ".txt")
                {
                    throw new Exception($"{PathOutput} is not found or file extension has invalid extension.");
                }
            }

            Maze maze = null;

            try
            {
                using (var streamReader = new StreamReader(new FileStream(PathInput, FileMode.Open)))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        var input = streamReader.ReadLine();

                        if (input != null) args = input.Split(' ');
                    }
                }

                maze = new Maze(int.Parse(args[0]), int.Parse(args[1]));

                maze.PrintMaze();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                if (maze != null)
                {
                    using (var streamWriter = new StreamWriter(new FileStream(PathOutput, FileMode.Create)))
                    {
                        streamWriter.Write(maze.PrintMaze());
                    }
                    Console.WriteLine("Generation done.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
