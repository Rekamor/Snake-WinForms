using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public partial class Form1
    {
        Brush[,] colors = 
        { 
            {Brushes.Gray, Brushes.Gray, Brushes.Gray, Brushes.DarkGreen,  Brushes.Red,  Brushes.ForestGreen, Brushes.Green},
            {Brushes.Green, Brushes.ForestGreen, Brushes.Gold, Brushes.Indigo,Brushes.DarkGreen, Brushes.Orange, Brushes.DarkOrange} 
        };
    }
}
