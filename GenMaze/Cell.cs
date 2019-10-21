using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenMaze
{
    public enum Status
    {
        Wall,
        NotVisitedField,
        VisitedField
    }

    public class Cell
    {
        public Status StatusCell { get; set; }
        public int X { get; }
        public int Y { get; }

        private List<String> collection;

        public Cell(int x, int y, Status status = Status.Wall)
        {
            StatusCell = status;
            X = x;
            Y = y;
        }

        public List<Cell> GetNeighbours(Maze currentMaze)
        {
            var neighbours = new List<Cell>();
            if (Y - 2 > 0 && currentMaze.GetMaze()[X][Y - 2].StatusCell is Status.NotVisitedField)
                neighbours.Add(currentMaze.GetMaze()[X][Y - 2]);

            if (Y + 2 < currentMaze.Width && currentMaze.GetMaze()[X][Y + 2].StatusCell is Status.NotVisitedField)
                neighbours.Add(currentMaze.GetMaze()[X][Y + 2]);

            if (X - 2 > 0 && currentMaze.GetMaze()[X - 2][Y].StatusCell is Status.NotVisitedField)
                neighbours.Add(currentMaze.GetMaze()[X - 2][Y]);

            if (X + 2 < currentMaze.Height && currentMaze.GetMaze()[X + 2][Y].StatusCell is Status.NotVisitedField)
                neighbours.Add(currentMaze.GetMaze()[X + 2][Y]);

            return neighbours;
        }

        public string GetCoordCell()
        {
            return $"{X};{Y}";
        }

        public override string ToString()
        {
            return StatusCell is Status.Wall ? "■" : "*";
        }
    }
}
