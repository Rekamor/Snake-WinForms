using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        bool ismoded = true;
        bool isblocksspawning = false;
        bool iscleared = false;
        int color = 1;

        Random rand = new Random();
        bool issnakedied;
        byte direction = 0;
        int score = 1;

        int[,] world = new int[64, 36];
        byte[,] groundtexture = new byte[64, 36];
        public Form1()
        {
            InitializeComponent();
            world[Convert.ToInt32(Math.Round(Convert.ToDouble(world.GetLength(0)/2))), Convert.ToInt32(Math.Round(Convert.ToDouble(world.GetLength(1) / 2)))] = 1;
            MakeApple(world, ref world);

            for (int i = 0; i < groundtexture.GetLength(0); i++)
            {
                for (int j = 0; j < groundtexture.GetLength(1); j++)
                {
                    groundtexture[i,j] = (byte)rand.Next(2);
                }
            }
        }

        Graphics graphics;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphics = CreateGraphics();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    direction = 0;
                    break;
                case Keys.Right:
                    direction = 1;
                    break;
                case Keys.Down:
                    direction = 2;
                    break;
                case Keys.Left:
                    direction = 3;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            world = Process(world, direction);

            Print(world);

            if (issnakedied) { Close(); }
        }

        private void MakeApple(int[,] proc, ref int[,] wrld)
        {
            Random random = new Random();

            int randx = random.Next(proc.GetLength(0));
            int randy = random.Next(proc.GetLength(1));

            if (proc[randx,randy] == 0)
            {
                wrld[randx, randy] = 2;
            }
            else
            {
                MakeApple(proc, ref wrld);
            }
        }

        private int[,] Process(int[,] arr, byte dir)
        {
            issnakedied = true;

            int[,] newarr = new int[arr.GetLength(0), arr.GetLength(1)];

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    switch(arr[i, j])
                    {
                        case 0:
                            break;
                        case 1:
                            issnakedied = false;

                            int newpointx = i;
                            int newpointy = j;

                            switch (dir)
                            {
                                case 0:
                                    newpointy = j - 1;
                                    break ;
                                case 1:
                                    newpointx = i + 1;
                                    break;
                                case 2:
                                    newpointy = j + 1;
                                    break;
                                case 3:
                                    newpointx = i - 1;
                                    break;
                            }

                            if (ismoded)
                            {
                                newpointx = (newpointx + arr.GetLength(0)) % arr.GetLength(0);
                                newpointy = (newpointy + arr.GetLength(1)) % arr.GetLength(1);
                            }

                            try
                            {
                                if (arr[newpointx, newpointy] == 2) { score++; MakeApple(world, ref newarr); }

                                if (arr[newpointx, newpointy] < 3) newarr[newpointx, newpointy] = 1;
                            }
                            catch {}

                            newarr[i, j] = score + 2;

                            break;
                        case 2:
                            if (newarr[i, j] != 1) { newarr[i, j] = 2; }
                            break;
                        default:
                            issnakedied = false;
                            newarr[i, j] = arr[i, j] - 1;
                            if (newarr[i, j] == 2) { newarr[i, j] = 0; }
                            break;

                    }
                }
            }

            return newarr;
        }

        private void Print(int[,] array)
        {
            if (iscleared)
            {
                graphics.Clear(BackColor);
            }

            Brush brush;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    switch (array[i, j])
                    {
                        case 0:
                            brush = colors[color,groundtexture[i,j]];
                            break;

                        case 1:
                            brush = colors[color, 2];
                            break;

                        case 2:
                            brush = colors[color,3];
                            break;

                        default:
                            if (array[i, j] % 2 == 0)
                            {
                                brush = colors[color, 5];
                            }
                            else
                            {
                                brush = colors[color, 6];
                            }
                            break;
                    }


                    graphics.FillRectangle(brush, ClientSize.Width / array.GetLength(0) * i, ClientSize.Height / array.GetLength(1) * j, ClientSize.Width / array.GetLength(0), ClientSize.Height / array.GetLength(1));
                }
            }
        }
    }
}
