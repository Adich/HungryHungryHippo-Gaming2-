using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace HungryHungry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Food> foodList = new List<Food>();
        private List<Point> playerParts = new List<Point>();

        private Brush foodColor = Brushes.AliceBlue;
        private Brush playerColor = Brushes.PaleVioletRed;

        private enum Movingdirection
        {
            Left = 2, Right = 4
        };
        private enum SIZE
        {
            THIN = 4,
            NORMAL = 6,
            THICK = 8
        };

        private TimeSpan FAST = new TimeSpan(1);
        private TimeSpan MODERATE = new TimeSpan(1000);
        private TimeSpan DAMNSLOW = new TimeSpan(50000);


        private Point startingPoint = new Point(400, 400);
        private Point currentPosition = new Point();

        private int pointSize = (int)SIZE.THICK;
        private int length = 75;
        private int playerSpeed = 30;
        private int playerLenght = 50;

        private int score = 0;
        private Random rnd = new Random();


        public MainWindow()
        {
            InitializeComponent();
            /*
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = MODERATE;
            timer.Start();*/


            DispatcherTimer foodTimer = new DispatcherTimer();
            foodTimer.Tick += new EventHandler(food_Timer_Tick);
            foodTimer.Interval = DAMNSLOW;
            foodTimer.Start();


            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            paintPlayer(startingPoint);
            currentPosition = startingPoint;

            addFood();

        }
        /*
        private void timer_Tick(object sender, EventArgs e)
        {
            // Expand the body of the snake to the direction of movement
            switch (direction)
            {
                case (int)Movingdirection.Left:
                    currentPosition.X -= 1;
                    paintPlayer(currentPosition);
                    break;
                case (int)Movingdirection.Right:
                    currentPosition.X += 1;
                    paintPlayer(currentPosition);
                    break;
            }

        }*/
        private void food_Timer_Tick(object sender, EventArgs e)
        {
            paintCanvas.Children.Clear();

            if (length == 0)
            {
            length = rnd.Next(20, 200);
            addFood();

            }
            else
            {
                length--;
            }

            foreach (Food food in foodList)
            {
                food.position.Y = food.position.Y += 1;
                paintFood(food);
            }

            int n = 0;
            foreach (Food food in foodList)
            {

                if ((Math.Abs(food.position.X - currentPosition.X) < (playerLenght/2)) &&
                    (Math.Abs(food.position.Y - currentPosition.Y) < pointSize))
                {

                    score++;

                    Scorelbl.Content = score;
                    
                    foodList.RemoveAt(n);
                    
                    break;
                }
                n++;
            }

        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    currentPosition.X = currentPosition.X + playerSpeed;
                    paintPlayer(currentPosition);

                    break;
                case Key.Left:
                    currentPosition.X = currentPosition.X - playerSpeed;
                    paintPlayer(currentPosition);

                    break;

            }
        }

        private void paintPlayer(Point currentposition)
        {
            PlayerArea.Children.Clear();
      
            for(int i = -1*(playerLenght/2); i < (playerLenght/2); i++)
            {
            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = playerColor;
            newEllipse.Width = pointSize;
            newEllipse.Height = pointSize;

            Canvas.SetTop(newEllipse, currentposition.Y);
            Canvas.SetLeft(newEllipse, currentposition.X - i);

            //int count = paintCanvas.Children.Count;

            PlayerArea.Children.Add(newEllipse);
            }
            playerParts.Add(currentposition);

        }

        private void paintFood(Food food)
        {
            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = Brushes.Red;
            newEllipse.Width = pointSize;
            newEllipse.Height = pointSize;

            Canvas.SetTop(newEllipse, food.position.Y);
            Canvas.SetLeft(newEllipse, food.position.X);
            paintCanvas.Children.Add(newEllipse);
        }

        private void addFood()
        {
            Food food = new Food(rnd.Next(5, 620), 10);
            foodList.Add(food);
            paintFood(food);
        }
    }
}
