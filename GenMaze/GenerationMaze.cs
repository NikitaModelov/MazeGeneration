using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenMaze
{
    public class GenerationMaze
    {

        public Maze Generate(int height, int width)
        {
            var generatedMaze = new Maze(height, width);

            var startCell = generatedMaze.GetMaze()[1, 1];

            var currentCell = startCell;

            currentCell.StatusCell = Status.VisitedField;

            Cell neighbourCell;

            var cells = new Stack<Cell>();

            var random = new Random();

            do
            {
                var neighboursCount = GetNeighbours(currentCell, generatedMaze.GetMaze()).Count;

                if (neighboursCount != 0)
                {
                    cells.Push(currentCell);

                    neighbourCell = GetNeighbours(currentCell, generatedMaze.GetMaze())[random.Next(0, neighboursCount)];

                    RemoveWall(currentCell, neighbourCell, generatedMaze.GetMaze());

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
                    currentCell = GetUnvisitedCell(generatedMaze.GetMaze())
                        [random.Next(0, GetUnvisitedCell(generatedMaze.GetMaze()).Count)];

                    currentCell.StatusCell = Status.VisitedField;
                }
            } while (GetUnvisitedCell(generatedMaze.GetMaze()).Count > 0);

            return generatedMaze;
        }

        private void RemoveWall(Cell first, Cell second, Cell[,] fields)
        {
            Cell wallCell;

            int xDiff = second.X - first.X;
            int yDiff = second.Y - first.Y;

            int addX = (xDiff != 0) ? (xDiff / Math.Abs(xDiff)) : 0;
            int addY = (yDiff != 0) ? (yDiff / Math.Abs(yDiff)) : 0;

            wallCell = fields[first.X + addX, first.Y + addY];
            wallCell.StatusCell = Status.VisitedField;
        }

        private IReadOnlyList<Cell> GetNeighbours(Cell currentCell, Cell[,] fields)
        {
            var neighbours = new List<Cell>();
            if (currentCell.Y - 2 > 0
                && fields[currentCell.X, currentCell.Y - 2].StatusCell is Status.NotVisitedField)
                neighbours.Add(fields[currentCell.X, currentCell.Y - 2]);

            if (currentCell.Y + 2 < fields.GetLength(0)
                && fields[currentCell.X, currentCell.Y + 2].StatusCell is Status.NotVisitedField)
                neighbours.Add(fields[currentCell.X, currentCell.Y + 2]);

            if (currentCell.X - 2 > 0
                && fields[currentCell.X - 2, currentCell.Y].StatusCell is Status.NotVisitedField)
                neighbours.Add(fields[currentCell.X - 2, currentCell.Y]);

            if (currentCell.X + 2 < fields.GetLength(0)
                && fields[currentCell.X + 2, currentCell.Y].StatusCell is Status.NotVisitedField)
                neighbours.Add(fields[currentCell.X + 2, currentCell.Y]);

            return neighbours;
        }

        private IReadOnlyList<Cell> GetUnvisitedCell(Cell [,] Fields)
        {
            var unvisitedCell = new List<Cell>();

            for (int row = 0; row < Fields.GetLength(0); row++)
            {
                for (int column = 0; column < Fields.GetLength(1); column++)
                {
                    if (Fields[row, column].StatusCell == Status.NotVisitedField)
                    {
                        unvisitedCell.Add(Fields[row, column]);
                    }
                }
            }

            return unvisitedCell;
        }

    }
}
