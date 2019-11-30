using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenMaze
{
    public class Maze
    {
        private Cell[,] Fields { get; }
        public int Height { get { return Fields.GetLength(0); } }
        public int Width { get { return Fields.GetLength(1); } }
        public Maze(int height, int width)
        {
            if (height >= 3 && width >= 3)
            {
                Fields = FillingMaze(height, width);
            }
            else
            {
                throw new MazeNotCreatedException
                    ("The size of maze can't contain values less 3.");
            }
        }

        private Cell[,] FillingMaze(int height, int width)
        {
            Cell[,] fields = new Cell[height, width]; 
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0 && (i < height - 1 && j < width - 1))
                    {
                        fields[i, j] = new Cell(i, j, Status.NotVisitedField);
                    }
                    else
                    {
                        fields[i, j] = new Cell(i, j);
                    }
                }
            }

            return fields;
        }

        public Cell[,] GetMaze()
        {
            return Fields;
        }
    }
}
