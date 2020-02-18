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

        public override string ToString()
        {
            return StatusCell is Status.Wall ? "■" : "*";
        }   
    }
}
