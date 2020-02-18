using GenMaze;
using System;
using System.IO;

namespace GenMazeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выбирите способ представления ввода и вывода.\n" +
                "FILE_MODE - для записи в файл и чтения из файла\n" +
                "CONSOLE_MODE - для ввода и вывода в консоле");

            var command = Console.ReadLine().ToLower();

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

        private static void FileModeGenerationMaze()
        {
            Console.Write("Введите путь файла для входных данных: ");
            var pathInput = Console.ReadLine();

            Console.Write("Введи путь файла для записи данных: ");
            var pathOutput = Console.ReadLine();

            if (Path.GetExtension(pathInput) != ".txt" || !File.Exists(pathInput))
            {
                throw new Exception($"{pathInput} is not found or file extension has invalid extension.");
            }

            if (Path.GetExtension(pathOutput) != ".txt")
            {
                throw new Exception($"{pathOutput} is not found or file extension has invalid extension.");
            }

            var class1 = new MazeFileUtil();

            var values = class1.FileReader(pathInput);
            var maze = new Maze(int.Parse(values[0]), int.Parse(values[1]));

            class1.FileWriter(pathOutput, maze);

            Console.WriteLine("Генерация завершена.");

        }

        static void ConsoleModeGenerationMaze()
        {

            Console.Write("Введите высоту лабиринта: ");
            var height = int.Parse(Console.ReadLine());

            Console.Write("Введите ширину лабиринта: ");
            var width = int.Parse(Console.ReadLine());

            var maze = new Maze(height, width);
        }
    }
}
