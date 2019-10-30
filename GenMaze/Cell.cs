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

        public Cell(int x, int y, Status status = Status.Wall)
        {
            StatusCell = status;
            X = x;
            Y = y;
        }

        public List<Cell> GetNeighbours(IReadOnlyList<IReadOnlyList<Cell>> fields)
        {
            var neighbours = new List<Cell>();
            if (Y - 2 > 0 && fields[X][Y - 2].StatusCell is Status.NotVisitedField)
                neighbours.Add(fields[X][Y - 2]);

            if (Y + 2 < fields[0].Count && fields[X][Y + 2].StatusCell is Status.NotVisitedField)
                neighbours.Add(fields[X][Y + 2]);

            if (X - 2 > 0 && fields[X - 2][Y].StatusCell is Status.NotVisitedField)
                neighbours.Add(fields[X - 2][Y]);

            if (X + 2 < fields.Count && fields[X + 2][Y].StatusCell is Status.NotVisitedField)
                neighbours.Add(fields[X + 2][Y]);

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
