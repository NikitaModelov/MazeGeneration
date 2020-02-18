using System;
using System.Collections.Generic;

namespace GenMaze
{
    public class MazeFactory
    {
        public Maze Generate(int height, int width)
        {
            var generatedMaze = new Maze(height, width);

            var startCell = generatedMaze.GetCell(1, 1);
            var currentCell = startCell;
            var cells = new Stack<Cell>();

            currentCell.StatusCell = Status.VisitedField;

            do
            {
                var neighborsCount = GetCountNeighbors(generatedMaze, currentCell);
                if (neighborsCount != 0)
                {
                    cells.Push(currentCell);

                    var neighborCell = GetRandomNeighborsCell(generatedMaze, currentCell, neighborsCount);
                    RemoveWall(currentCell, neighborCell, generatedMaze.GetMaze());
                    currentCell = neighborCell;
                    neighborCell.StatusCell = Status.VisitedField;
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

        private IReadOnlyList<Cell> GetUnvisitedCell(Cell[,] fields)
        {
            var unvisitedCell = new List<Cell>();

            for (var row = 0; row < fields.GetLength(0); row++)
            {
                for (var column = 0; column < fields.GetLength(1); column++)
                {
                    if (fields[row, column].StatusCell == Status.NotVisitedField)
                    {
                        unvisitedCell.Add(fields[row, column]);
                    }
                }
            }

            return unvisitedCell;
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

        private Cell GetRandomNeighborsCell(Maze maze, Cell currentCell, int neighborsCount)
        {
            var random = new Random();
            return GetNeighbors(currentCell, maze.GetMaze())[random.Next(0, neighborsCount)];
        }

        private int GetCountNeighbors(Maze maze, Cell cell)
        {
            return GetNeighbors(cell, maze.GetMaze()).Count;
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

        private IReadOnlyList<Cell> GetNeighbors(Cell currentCell, Cell[,] fields)
        {
            var neighbours = new List<Cell>();
            if (CheckStatusLeftCell(currentCell, fields))
            {
                neighbours.Add(fields[currentCell.X, currentCell.Y - 2]);
            }

            if (CheckStatusRightCell(currentCell, fields))
            {
                neighbours.Add(fields[currentCell.X, currentCell.Y + 2]);
            }

            if (CheckStatusTopCell(currentCell, fields))
            {
                neighbours.Add(fields[currentCell.X - 2, currentCell.Y]);
            }

            if (CheckStatusLowerCell(currentCell, fields))
            {
                neighbours.Add(fields[currentCell.X + 2, currentCell.Y]);
            }

            return neighbours;
        }

        private bool CheckStatusLeftCell(Cell currentCell, Cell[,] fields)
        {
            return currentCell.Y - 2 > 0 &&
                   fields[currentCell.X, currentCell.Y - 2].StatusCell is Status.NotVisitedField;
        }

        private bool CheckStatusRightCell(Cell currentCell, Cell[,] fields)
        {
            return (currentCell.Y + 2 < fields.GetLength(0) &&
                    fields[currentCell.X, currentCell.Y + 2].StatusCell is Status.NotVisitedField);
        }

        private bool CheckStatusTopCell(Cell currentCell, Cell[,] fields)
        {
            return (currentCell.X - 2 > 0 &&
                    fields[currentCell.X - 2, currentCell.Y].StatusCell is Status.NotVisitedField);
        }

        private bool CheckStatusLowerCell(Cell currentCell, Cell[,] fields)
        {
            return (currentCell.X + 2 < fields.GetLength(0) &&
                    fields[currentCell.X + 2, currentCell.Y].StatusCell is Status.NotVisitedField);
        }
    }
}