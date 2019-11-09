using System;
using System.Collections;
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

        //в maze


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
