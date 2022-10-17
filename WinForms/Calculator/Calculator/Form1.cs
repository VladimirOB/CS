using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public partial class Form1 : Form
    {
        byte cmd = 0;
        byte dot = 0;
        bool flag = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbCalc.Text += 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbCalc.Text += 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbCalc.Text += 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tbCalc.Text += 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tbCalc.Text += 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tbCalc.Text += 6;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tbCalc.Text += 7;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tbCalc.Text += 8;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tbCalc.Text += 9;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                tbCalc.Text += "+";
                cmd = 1;
                flag = true;
            }
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if(!flag)
            {
                tbCalc.Text += "-";
                cmd = 2;
                flag = true;
            }
        }

        private void buttonMul_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                tbCalc.Text += "*";
                cmd = 3;
                flag = true;
            }
        }
        private void buttonDiv_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                tbCalc.Text += "/";
                cmd = 4;
                flag = true;
            }
        }
        private void buttonPlusMinus_Click(object sender, EventArgs e)
        {
            if (tbCalc.Text.Length == 0)
                tbCalc.Text = "-";
            else if (tbCalc.Text.Length == 1 && tbCalc.Text.Contains("-"))
                tbCalc.Text = "";

        }

        private void buttonPercent_Click(object sender, EventArgs e)
        {
            if(flag && cmd != 0)
            {
               
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if(tbCalc.Text.Length > 0)
            {
                tbCalc.Text = tbCalc.Text.Remove(tbCalc.Text.Length-1, 1);
            }
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            dot = 0;
            cmd = 0;
            tbCalc.Text = "";
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            if(!flag)
            {
                dot = 0;
                cmd = 0;
                tbCalc.Text = "";
            }
            else
            {
                if(tbCalc.Text.Length > 0)
                {
                    string[] str = tbCalc.Text.Split(new[] { '-', '+', '*', '/' });
                    tbCalc.Text = str[0];
                    if (cmd == 1)
                        tbCalc.Text += '+';
                    if (cmd == 2)
                        tbCalc.Text += '-';
                    if (cmd == 3)
                        tbCalc.Text += '*';
                    if (cmd == 4)
                        tbCalc.Text += '/';
                }

            }
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            string[]? str;
            double result;
            char last = tbCalc.Text.Last();
            if (cmd != 0 && !last.Equals('+') && !last.Equals('-') && !last.Equals('*') && !last.Equals('/') && !last.Equals(','))
            {
                if (dot == 2)
                    dot--;
                else
                    dot = 0;
                flag = false;
                switch (cmd)
                {
                    case 1:
                        {
                            str = tbCalc.Text.Split('+');
                            result = Convert.ToDouble(str[0]) + Convert.ToDouble(str[1]);
                            tbCalc.Text = result.ToString();
                            cmd = 0;
                            break;
                        }
                    case 2:
                        {
                            str = tbCalc.Text.Split('-');
                            result = Convert.ToDouble(str[0]) - Convert.ToDouble(str[1]);
                            tbCalc.Text = result.ToString();
                            cmd = 0;
                            break;
                        }
                    case 3:
                        {
                            str = tbCalc.Text.Split('*');
                            result = Convert.ToDouble(str[0]) * Convert.ToDouble(str[1]);
                            tbCalc.Text = result.ToString();
                            cmd = 0;
                            break;
                        }
                    case 4:
                        {
                            str = tbCalc.Text.Split('/');
                            if (str[0] == "0" || str[1] == "0")
                            {
                                MessageBox.Show("На ноль делить нельзя!", "Error!");
                                tbCalc.Text = "";
                                cmd = 0;
                                break;
                            }
                            result = Convert.ToDouble(str[0]) / Convert.ToDouble(str[1]);
                            tbCalc.Text = result.ToString();
                            cmd = 0;
                            break;
                        }
                }
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if(tbCalc.Text.Length == 1 && tbCalc.Text.Contains('0'))
            tbCalc.Text = "0";
            else
            tbCalc.Text += 0;
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            if (dot == 0 && !flag)
            {
                tbCalc.Text += ",";
                dot++;
            }
            else if (dot == 1 && flag)
            {
                tbCalc.Text += ",";
                dot++;
            }
            else if (dot == 0 && flag)
            {
                tbCalc.Text += ',';
                dot += 2;
            }
        }
    }
}