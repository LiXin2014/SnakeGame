using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Draw game area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
            Snake snake = new Snake(GameArea);
        }

        private void DrawGameArea()
        {
            int nextX = 0, nextY = 0;
            int next = 0;
            int rowNum = 0;
            while (true)
            {
                Rectangle rec = new Rectangle();
                rec.Width = Constants.SquareSize;
                rec.Height = Constants.SquareSize;
                SolidColorBrush colorBrush = new SolidColorBrush();
                colorBrush.Color = (next + rowNum) % 2 == 0 ? Colors.LightGreen : Color.Multiply(Colors.PaleGreen, 0.9f);
                rec.Fill = colorBrush;
                GameArea.Children.Add(rec);
                Canvas.SetTop(rec, nextY);
                Canvas.SetLeft(rec, nextX);

                nextX += Constants.SquareSize;

                if (nextX >= GameArea.ActualWidth)
                {
                    nextX = 0;
                    nextY += Constants.SquareSize;
                    rowNum++;
                }

                if (nextY >= GameArea.ActualHeight)
                {
                    break;
                }
                next++;
            }
        }
    }
}
