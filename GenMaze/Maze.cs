namespace GenMaze
{
    public class Maze
    {
        private Cell[,] Fields { get; }
        public int Height => Fields.GetLength(0);
        public int Width => Fields.GetLength(1);

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

        public Cell[,] GetMaze()
        {
            return Fields;
        }

        public Cell GetCell(int x, int y)
        {
            return Fields[x, y];
        }

        private Cell[,] FillingMaze(int height, int width)
        {
            var fields = new Cell[height, width];
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
    }
}