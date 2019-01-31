using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstYearIACourse
{
    public partial class CubicForm : Form
    {
        private Button[][] cubicMatrix;
        private Color BLUE = Color.MidnightBlue;
        private Color GREEN = Color.Green;
        private Color RED = Color.Firebrick;

        public CubicForm()
        {
            InitializeComponent();
            cubicMatrix = InitMatrix();
            InitClickEvent();
            ReinitColors();
            label1.Text = "0";
        }

        private Button[][] InitMatrix()
        {
            return new Button[][] {
                new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 },
                new Button[] { button10, button11, button12, button13, button14, button15, button16, button17, button18 },
                new Button[] { button19, button20, button21, button22, button23, button24, button25, button26, button27 }
            };
        }

        private void InitClickEvent()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cubicMatrix[i][j].Click += new EventHandler(OnClick);
                }
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            if(((Button)sender).BackColor == BLUE)
            {
                ((Button)sender).BackColor = GREEN;
                AttributeError();
            }
            else if (((Button)sender).BackColor == GREEN)
            {
                ((Button)sender).BackColor = RED;
                AttributeError();
            }
            else if (((Button)sender).BackColor == RED)
            {
                ((Button)sender).BackColor = BLUE;
                AttributeError();
            }
        }

        private void ReinitColors()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cubicMatrix[i][j].BackColor = Color.White;
                }
            }
        }

        private void FillInitialColors()
        {
            ReinitColors();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    switch (i)
                    {
                        case 0:
                            cubicMatrix[i][j].BackColor = BLUE;
                            break;
                        case 1:
                            cubicMatrix[i][j].BackColor = GREEN;
                            break;
                        case 2:
                            cubicMatrix[i][j].BackColor = RED;
                            break;
                    }
                }
            }
        }

        private void FillRandomColors()
        {
            ReinitColors();
            FillColor(BLUE);
            FillColor(GREEN);
            FillColor(RED);
        }

        private void FillColor(Color filler)
        {
            Random rnd = new Random();

            for (int i = 0; i < 9; i++)
            {
                int abs;
                int ord;
                do
                {
                    abs = rnd.Next(9);
                    ord = rnd.Next(3);
                } while (new Color[] { BLUE, GREEN, RED }.Contains(cubicMatrix[ord][abs].BackColor));

                cubicMatrix[ord][abs].BackColor = filler;
            }
        }

        private void FillOriginal_Click(object sender, EventArgs e)
        {
            FillInitialColors();
            AttributeError();
        }

        private void FillRandom_Click(object sender, EventArgs e)
        {
            FillRandomColors();
            AttributeError();
        }

        private int CalculateTotalError()
        {
            int totalError = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    totalError += CalculateErrorForOne(i, j);
                }
            }

            return totalError;

        }

        private int CalculateErrorForOne(int ord, int abs)
        {
            Color backColor = cubicMatrix[ord][abs].BackColor;
            int tempError = 0;

            for (int i = 0; i < 3; i++)
            {
                if (ord == i) continue;
                for (int j = 0; j < 9; j++)
                {
                    if (cubicMatrix[i][j].BackColor != backColor) continue;

                    tempError += (i > ord ? i - ord : ord - i);
                }
            }

            return tempError;
        }

        private int AttributeError()
        {
            string error = CalculateTotalError().ToString();
            label1.Invoke(new MethodInvoker(delegate
            {
                label1.Text = error;
            }));
            return int.Parse(error);
        }

        private async void NaiveSearch_Click(object sender, EventArgs e)
        {
            DisplayButtons(false);
            await Task.Run(() => LocaleNaiveSearch(1000));
            DisplayButtons(true);
        }

        private void DisplayButtons(bool display)
        {
            naiveSearch.Enabled = display;
            fillOriginal.Enabled = display;
            FillRandom.Enabled = display;
        }

        private async void LocaleNaiveSearch(int iteration)
        {
            Random rnd = new Random();

            for (int i = 1; i <= iteration; i++)
            {
                int abs = rnd.Next(9);
                int ord = rnd.Next(3);

                PermutateOneCube(abs, ord, rnd);

                int error = AttributeError();
                if (error == 0) return;
                Thread.Sleep(15);
            }
        }

        private void PermutateOneCube(int abs, int ord, Random rnd)
        {
            bool ok;
            int error = CalculateTotalError();
            do
            {
                int perm = rnd.Next(4);
                ok = CheckIfOkCoordonates(abs, ord, perm, out int newAbs, out int newOrd);

                if(ok)
                {
                    PermutateTwoCubes(abs, ord, newAbs, newOrd);
                    int newError = CalculateTotalError();

                    if(newError > error)
                    {
                        PermutateTwoCubes(abs, ord, newAbs, newOrd);
                    }
                }
            } while (!ok);
        }

        private bool CheckIfOkCoordonates(int abs, int ord, int perm, out int newAbs, out int newOrd)
        {
            newAbs = abs;
            newOrd = ord;

            switch (perm)
            {
                case 0:
                    newAbs = abs - 1;
                    break;
                case 1:
                    newOrd = ord + 1;
                    break;
                case 2:
                    newAbs = abs + 1;
                    break;
                case 3:
                    newOrd = ord - 1;
                    break;
            }

            return (newAbs >= 0 && newAbs < 9) && (newOrd >= 0 && newOrd < 3);
        }

        private void PermutateTwoCubes(int abs, int ord, int abs2, int ord2)
        {
            var colorTemp = cubicMatrix[ord][abs].BackColor;

            cubicMatrix[ord][abs].BackColor = cubicMatrix[ord2][abs2].BackColor;
            cubicMatrix[ord2][abs2].BackColor = colorTemp;
        }

    }
}
