using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HungryHungry
{
    class Food
    {
        public Point position;

        public Food(double xPos, double yPos)
        {
            position = new Point(xPos, yPos);
        }
    }


}
