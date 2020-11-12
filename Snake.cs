using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeGame
{
    public class Snake
    {
        public List<SnakePart> TheSnake { get; private set; }
        public Direction Direction { get; set; }
        private Canvas gameAreaCanvas;

        public Snake(Canvas gameAreaCanvas)
        {
            this.TheSnake = new List<SnakePart>();
            this.Direction = Direction.Right;
            this.gameAreaCanvas = gameAreaCanvas;

            // Initialize the snake with two snake parts. One head and one body
            SnakePart head = new SnakePart(4, 15, true);
            SnakePart body = new SnakePart(3, 15);
            this.TheSnake.Add(head);
            this.TheSnake.Add(body);

            this.DrawSnake();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += this.MoveSnake;
            timer.Start();
        }

        private void DrawSnake()
        {
            foreach (var snakePart in this.TheSnake)
            {
                this.gameAreaCanvas.Children.Add(snakePart.UiElement);
                Canvas.SetTop(snakePart.UiElement, snakePart.Position.Y * Constants.SquareSize);
                Canvas.SetLeft(snakePart.UiElement, snakePart.Position.X * Constants.SquareSize);
            }
        }

        public void MoveSnake(object sender, EventArgs e)
        {
            var length = this.TheSnake.Count;

            // remove tail, the last snake part in list
            SnakePart snakeTail = this.TheSnake[length - 1];
            this.gameAreaCanvas.Children.Remove(snakeTail.UiElement);
            this.TheSnake.RemoveAt(length - 1);

            // set snake head to part of body
            SnakePart oldHead = this.TheSnake[0];
            oldHead.IsHead = false;

            // compute the position of new head.
            double newX = oldHead.Position.X;
            double newY = oldHead.Position.Y;

            if (this.Direction == Direction.Right)
            {
                newX += 1;
            }
            else if (this.Direction == Direction.Left)
            {
                newX -= 1;
            }
            else if (this.Direction == Direction.Up)
            {
                newY -= 1;
            }
            else if (this.Direction == Direction.Down)
            {
                newY += 1;
            }

            // add new snake head
            SnakePart newHead = new SnakePart(newX, newY, true);
            this.TheSnake.Insert(0, newHead);
            this.gameAreaCanvas.Children.Add(newHead.UiElement);
            Canvas.SetTop(newHead.UiElement, newHead.Position.Y * Constants.SquareSize);
            Canvas.SetLeft(newHead.UiElement, newHead.Position.X * Constants.SquareSize);
        }
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public class SnakePart
    {
        public Point Position { get; private set; }
        public Shape UiElement { get; private set; }
        public bool IsHead
        {
            get { return this.isHead; }
            set
            {
                if (value)
                {
                    this.UiElement.Fill = headColorBrush;
                }
                else
                {
                    this.UiElement.Fill = bodyColorBrush;
                }
                this.isHead = value;
            }
        }

        private bool isHead;
        private static readonly SolidColorBrush headColorBrush = new SolidColorBrush(Colors.BlueViolet);
        private static readonly SolidColorBrush bodyColorBrush = new SolidColorBrush(Colors.LightBlue);

        public SnakePart(double x, double y, bool isHead = false)
        {
            this.Position = new Point(x, y);
            this.UiElement = new Rectangle();
            this.UiElement.Height = Constants.SquareSize;
            this.UiElement.Width = Constants.SquareSize;
            this.IsHead = isHead;
        }
    }
}
