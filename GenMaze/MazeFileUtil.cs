using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenMaze
{
    public class MazeFileUtil
    {
        public string[] FileReader(string path)
        {
            var values = new List<string>();

            using (var streamReader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                while (streamReader.Peek() >= 0)
                {
                    var input = streamReader.ReadLine();
                    if (input != null) values = input.Split(' ').ToList();
                }
            }
            return values.ToArray();
        }

        public bool FileWriter(string path, Maze maze)
        {
            using (var streamWriter = new StreamWriter(new FileStream(path, FileMode.Create)))
            {
                return true;
            }
        }

    }
}
