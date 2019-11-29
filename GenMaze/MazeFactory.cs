using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenMaze
{
    public class MazeFactory
    {
        public Maze Generate(int height, int width)
        {
            Maze generatedMaze = new Maze(height, width);

            Cell startCell = generatedMaze.GetMaze()[1, 1];

            Cell currentCell = startCell;

            currentCell.StatusCell = Status.VisitedField;

            Cell neighbourCell;

            var cells = new Stack<Cell>();

            do
            {
                var neighboursCount = GetCountNeighbours(generatedMaze, currentCell);

                if (neighboursCount != 0)
                {
                    cells.Push(currentCell);

                    neighbourCell = GetRandomNeighbourCell(generatedMaze, currentCell, neighboursCount);

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
                    currentCell = GetRandomUnvisitedCell(generatedMaze);

                    currentCell.StatusCell = Status.VisitedField;
                }
            } while (GetCountUnvisitedCells(generatedMaze) > 0);

            return generatedMaze;
        }

        private Cell GetRandomUnvisitedCell(Maze maze)
        {
            var random = new Random();
            return GetUnvisitedCell(maze.GetMaze())[random.Next(0, GetUnvisitedCell(maze.GetMaze()).Count)];
        }
        private int GetCountUnvisitedCells(Maze maze)
        {
            return GetUnvisitedCell(maze.GetMaze()).Count;
        }
        private Cell GetRandomNeighbourCell(Maze maze, Cell currentCell, int neighboursCount)
        {
            var random = new Random();
            return GetNeighbours(currentCell, maze.GetMaze())[random.Next(0, neighboursCount)];
        }
        private int GetCountNeighbours(Maze maze, Cell cell)
        {
            return GetNeighbours(cell, maze.GetMaze()).Count;
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
            if (CheckStatusLeftCell(currentCell, fields))
                neighbours.Add(fields[currentCell.X, currentCell.Y - 2]);

            if (CheckStatusRightCell(currentCell, fields))
                neighbours.Add(fields[currentCell.X, currentCell.Y + 2]);

            if (CheckStatusTopCell(currentCell, fields))
                neighbours.Add(fields[currentCell.X - 2, currentCell.Y]);

            if (CheckStatusLowerCell(currentCell, fields))
                neighbours.Add(fields[currentCell.X + 2, currentCell.Y]);

            return neighbours;
        }

        private bool CheckStatusLeftCell(Cell currentCell, Cell[,] fields)
        {
            if (currentCell.Y - 2 > 0 && fields[currentCell.X, currentCell.Y - 2].StatusCell is Status.NotVisitedField)
                return true;
            else
                return false;
        }
        private bool CheckStatusRightCell(Cell currentCell, Cell[,] fields)
        {
            if (currentCell.Y + 2 < fields.GetLength(0) && fields[currentCell.X, currentCell.Y + 2].StatusCell is Status.NotVisitedField)
                return true;
            else
                return false;
        }
        private bool CheckStatusTopCell(Cell currentCell, Cell[,] fields)
        {
            if (currentCell.X - 2 > 0 && fields[currentCell.X - 2, currentCell.Y].StatusCell is Status.NotVisitedField)
                return true;
            else
                return false;
        }
        private bool CheckStatusLowerCell(Cell currentCell, Cell[,] fields)
        {
            if (currentCell.X + 2 < fields.GetLength(0) && fields[currentCell.X + 2, currentCell.Y].StatusCell is Status.NotVisitedField)
                return true;
            else
                return false;
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
