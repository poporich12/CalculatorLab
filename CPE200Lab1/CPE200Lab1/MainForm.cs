using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterOperater1;
        private bool isAfterEqual;
        private string firstOperand;
        private string operate;
        private string Memmory;
        int countclickOperand=0;
        CalculatorEngine engine;

        private void resetAll()
        {

            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterOperater1 = false;
            isAfterEqual = false;
        }

        

        public MainForm()
        {
            InitializeComponent();
            engine = new CalculatorEngine();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            //55555
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater|| isAfterOperater1)
            {
                lblDisplay.Text = "0";
            }
            isAfterOperater1 = false;
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }

            if (((Button)sender).Text == "%")
                lblDisplay.Text = ((Convert.ToDouble(lblDisplay.Text) / 100) * Convert.ToDouble(firstOperand)).ToString();
            else
            {
                if ((countclickOperand > 0 && !isAfterEqual)) btnEqual.PerformClick();

                operate = ((Button)sender).Text;
                switch (operate)
                {
                    case "+":
                    case "-":
                    case "X":
                    case "÷":
                        firstOperand = lblDisplay.Text;
                        isAfterOperater = true;
                        countclickOperand++;
                        break;
                    case "^(1/2)":
                    case "^(-1) ":
                        firstOperand = lblDisplay.Text;
                        countclickOperand++;
                        break;
                    case "MC":
                        Memmory = "0";
                        countclickOperand = 0;
                        isAfterOperater1 = true;
                        break;
                    case "MR":
                        lblDisplay.Text = Memmory;
                        countclickOperand = 0;
                        isAfterOperater1 = true;
                        break;
                    case "M+":
                        Memmory = (Convert.ToDouble(Memmory) + Convert.ToDouble(lblDisplay.Text)).ToString();
                        countclickOperand = 0;
                        isAfterOperater1 = true;
                        break;
                    case "M-":
                        Memmory = (Convert.ToDouble(Memmory) - Convert.ToDouble(lblDisplay.Text)).ToString();
                        countclickOperand = 0;
                        isAfterOperater1 = true;
                        break;
                    case "MS":
                        Memmory = lblDisplay.Text;
                        countclickOperand = 0;
                        isAfterOperater1 = true;
                        break;
                }


                isAllowBack = false;

            }
                
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }

                string secondOperand;
                secondOperand = lblDisplay.Text;
                string result = engine.calculate(operate, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                { 
                    lblDisplay.Text = result;
                }
                isAfterEqual = true;
            
            
        }


        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Memmory = "0";
           
            countclickOperand = 0;
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void btnMemory_Click(object sender, EventArgs e)
        {
            
                



        }
    }
}
