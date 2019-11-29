using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GenMaze;
using Microsoft.Win32;

namespace GenMazeDesktop
{
    public partial class MainWindow : Window
    {

        Maze maze;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateMazeUI(Maze maze)
        {
            MazeUI.Children.Clear();
            MazeUI.ColumnDefinitions.Clear();
            MazeUI.RowDefinitions.Clear();

            for (int column = 0; column < maze.Width; column++)
            {
                MazeUI.RowDefinitions.Add(new RowDefinition());

                for (int row = 0; row < maze.Height; row++)
                {
                    MazeUI.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

                    Cell cell = maze.GetMaze()[row, column];

                    Rectangle field = new Rectangle();

                    field.Height = 20;
                    field.Width = 20;

                    field.Fill = (cell.StatusCell == Status.Wall) ? Brushes.Black : Brushes.White;
                   
                    MazeUI.Children.Add(field);

                    Grid.SetColumn(field, column);
                    Grid.SetRow(field, row);
                }

            }
        }

        private void GenerationMaze_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(inputWidth.Text, out int width) && int.TryParse(inputHeight.Text, out int heigth))
            {
                CreateMazeUI(new MazeFactory().GetGeneratedMaze(heigth, width));
            }

        }

        private void SaveMaze_Click(object sender, RoutedEventArgs e)
        {
            if (maze != null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();


                if (saveFileDialog1.ShowDialog() == true)
                {
                    MazeFileUtil class1 = new MazeFileUtil();
                    class1.FileWriter(saveFileDialog1.FileName, maze);
                }
            }
        }

        private void SaveMazePng_Click(object sender, RoutedEventArgs e)
        {
            if (maze != null)
            {
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(1280, 720, 96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(MazeUI);
                PngBitmapEncoder pngImage = new PngBitmapEncoder();
                pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();


                if (saveFileDialog1.ShowDialog() == true)
                {
                    using (Stream fileStream = File.Create(saveFileDialog1.FileName))
                    {
                        pngImage.Save(fileStream);
                    }
                }
            }
            

            
        }
    }
}
