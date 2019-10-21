using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenMaze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenMaze.Tests
{
    [TestClass()]
    public class MazeTests
    {

        [DataTestMethod]
        [DataRow(63, 45, 63, 45)]
        [DataRow(3, 45, 3, 45)]
        [DataRow(21, 3, 21, 3)]
        public void MazeTest(int width, int height,
                            int expectedWidth, int expectedHeight)
        {
            var maze = new Maze(height, width);
            Assert.AreEqual(expectedHeight, maze.Height);
            Assert.AreEqual(expectedWidth, maze.Width);
        }

        [TestMethod()]
        public void GetMazeTest()
        {
            var maze = new Maze(5, 5);
            Assert.IsNotNull(maze.GetMaze());
        }

        [DataTestMethod()]
        [DataRow(2, 10)]
        [DataRow(10, 2)]
        [DataRow(-5, 19)]
        [DataRow(19, -5)]
        [DataRow(0, 65)]
        [DataRow(33, 0)]
        public void ExceptionMaze(int width, int height)
        {
            Assert.ThrowsException<MazeNotCreatedException>(() => new Maze(width, height));
        }

    }
}