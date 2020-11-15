using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SnakeGame
{
    public class GameState
    {
        public Snake Snake { get; set; }
        public SnakeFood SnakeFood { get; set; }

        public static int Score { get; set; }
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private Canvas gameAreaCanvas;

        public GameState(Canvas gameAreaCanvas)
        {
            this.gameAreaCanvas = gameAreaCanvas;
            this.gameTimer.Interval = TimeSpan.FromSeconds(0.5);
            this.Snake = new Snake(gameAreaCanvas, this.gameTimer);
            this.SnakeFood = new SnakeFood(gameAreaCanvas, this.Snake);
            this.gameTimer.Start();
        }
    }
}
