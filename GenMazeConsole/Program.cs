using GenMaze;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenMazeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выбирите способ представления ввода и вывода.\n" +
                "FILE_MODE - для записи в файл и чтения из файла\n" +
                "CONSOLE_MODE - для ввода и вывода в консоле");

            string command = Console.ReadLine().ToLower();

            try
            {
                switch (command)
                {
                    case "file_mode":
                        FileModeGenerationMaze();
                        break;
                    case "console_mode":
                        ConsoleModeGenerationMaze();
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();

        }

        static void FileModeGenerationMaze()
        {
            // TODO: Сделать проверку на правильность пути

            Console.Write("Введите путь файла для входных данных: ");
            string PathInput = Console.ReadLine();

            Console.Write("Введи путь файла для записи данных: ");
            string PathOutput = Console.ReadLine();

            if (Path.GetExtension(PathInput) != ".txt" || !File.Exists(PathInput))
            {
                throw new Exception($"{PathInput} is not found or file extension has invalid extension.");
            }

            if (Path.GetExtension(PathOutput) != ".txt")
            {
                throw new Exception($"{PathOutput} is not found or file extension has invalid extension.");
            }

            MazeFileUtil class1 = new MazeFileUtil();

            string[] values = class1.FileReader(PathInput);
            Maze maze = new Maze(int.Parse(values[0]), int.Parse(values[1]));

            class1.FileWriter(PathOutput, maze);

            Console.WriteLine("Генерация завершена.");

        }

        static void ConsoleModeGenerationMaze()
        {

            Console.Write("Введите высоту лабиринта: ");
            int height = int.Parse(Console.ReadLine());

            Console.Write("Введите ширину лабиринта: ");
            int width = int.Parse(Console.ReadLine());

            Maze maze = new Maze(height, width);

            Console.WriteLine(maze.PrintMaze());
        }
    }
}
