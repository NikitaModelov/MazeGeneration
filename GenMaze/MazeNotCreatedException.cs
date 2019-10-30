using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenMaze
{
     public class MazeNotCreatedException : ApplicationException
     {
        public MazeNotCreatedException() { }

        public MazeNotCreatedException(string message) : base(message) { }

        public MazeNotCreatedException(string message, Exception inner) : base(message, inner) { }

     }
}
