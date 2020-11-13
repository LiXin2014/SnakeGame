using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame
{
    public class SnakeFood
    {
        public Point Position { get; private set; }
        public Shape UiElement { get; private set; }

        private static readonly SolidColorBrush foodColorBrush = new SolidColorBrush(Colors.MediumVioletRed);
        private Random random = new Random();
        private Canvas gameAreaCanvas;
        private Snake snake;

        public SnakeFood(Canvas gameAreaCanvas, Snake snake)
        {
            this.gameAreaCanvas = gameAreaCanvas;
            this.snake = snake;

            this.UiElement = new Rectangle();
            this.UiElement.Height = Constants.SquareSize;
            this.UiElement.Width = Constants.SquareSize;
            this.UiElement.Fill = foodColorBrush;
            this.GenerateNewSnakeFood();
        }

        public void GenerateNewSnakeFood()
        {
            // Remove this uiElement first, otherwise it will throw exception trying to add same uiElement.
            this.gameAreaCanvas.Children.Remove(this.UiElement);

            int x = random.Next(30);
            int y = random.Next(30);

            foreach (var snakePart in snake.TheSnake)
            {
                if (snakePart.Position.X == x && snakePart.Position.Y == y)
                {
                    this.GenerateNewSnakeFood();
                }
            }

            this.Position = new Point(x, y);
            this.DrawSnakeFood();
        }

        private void DrawSnakeFood()
        {
            this.gameAreaCanvas.Children.Add(this.UiElement);
            Canvas.SetTop(this.UiElement, this.Position.Y * Constants.SquareSize);
            Canvas.SetLeft(this.UiElement, this.Position.X * Constants.SquareSize);
        }
    }
}
