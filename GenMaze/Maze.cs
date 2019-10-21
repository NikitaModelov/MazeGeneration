using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenMaze
{
    public class Maze
    {
        private List<List<Cell>> Fields { get; }
        public int Height { get; }
        public int Width { get; }

        public Maze(int height, int width)
        {
            if (height >= 3 && width >= 3)
            {
                Fields = new List<List<Cell>>();

                Height = height;

                Width = width;

                FillingMaze();

                GenerationMaze();
            }
            else
            {
                throw new MazeNotCreatedException("The size of maze can't contain values less 3.");
            }
        }

        private void GenerationMaze()
        {
            var startCell = Fields[1][1];

            var currentCell = startCell;

            currentCell.StatusCell = Status.VisitedField;

            Cell neighbourCell;

            var cells = new Stack<Cell>();

            var random = new Random();

            do
            {
                var neighboursCount = currentCell.GetNeighbours(this).Count;

                if (neighboursCount != 0)
                {
                    cells.Push(currentCell);

                    neighbourCell = currentCell.GetNeighbours(this)[random.Next(0, neighboursCount)];

                    RemoveWall(currentCell, neighbourCell);

                    currentCell = neighbourCell;

                    neighbourCell.StatusCell = Status.VisitedField;

                }
                else if (cells.Count > 0)
                {
                    startCell = cells.Pop();

                    currentCell = startCell;
                }
                else
                {
                    currentCell = GetUnvisitedCell()[random.Next(0, GetUnvisitedCell().Count)];

                    currentCell.StatusCell = Status.VisitedField;
                }
            } while (GetUnvisitedCell().Count > 0);
        }

        private void RemoveWall(Cell first, Cell second)
        {
            Cell wallCell;

            int xDiff = second.X - first.X;
            int yDiff = second.Y - first.Y;

            int addX = (xDiff != 0) ? (xDiff / Math.Abs(xDiff)) : 0;
            int addY = (yDiff != 0) ? (yDiff / Math.Abs(yDiff)) : 0;

            wallCell = Fields[first.X + addX][first.Y + addY];
            wallCell.StatusCell = Status.VisitedField;
        }

        private List<Cell> GetUnvisitedCell()
        {
            var unvisitedCell = new List<Cell>();

            foreach (var row in Fields)
            {
                foreach (var cell in row)
                {
                    if (cell.StatusCell == Status.NotVisitedField)
                    {
                        unvisitedCell.Add(cell);
                    }
                }
            }

            return unvisitedCell;
        }

        private void FillingMaze()
        {
            for (var i = 0; i < Height; i++)
            {
                Fields.Add(new List<Cell>());
                for (var j = 0; j < Width; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0 &&
                        (i < Height - 1 && j < Width - 1))
                    {
                        Fields[i].Add(new Cell(i, j, Status.NotVisitedField));
                    }
                    else
                    {
                        Fields[i].Add(new Cell(i, j));
                    }
                }
            }
        }

        public string PrintMaze()
        {
            var maze = new StringBuilder();
            foreach (var row in Fields)
            {
                foreach (var field in row)
                {
                    maze.Append(field);
                }
                maze.AppendLine();
            }

            return maze.ToString();
        }

        public List<List<Cell>> GetMaze()
        {
            return Fields;
        }
    }
}
