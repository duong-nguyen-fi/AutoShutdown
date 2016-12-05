using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace windowAutoShutDown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int hour;
    
        private int min;
        private int sec;

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTimeNow.Visible = true;
            labelTimeNow.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // comboBoxAction.SelectedIndex = 0;
        }

        private void checkBoxRemind_CheckedChanged(object sender, EventArgs e)
        {
            numRemind.Visible = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if ((numMint.Value == 0) && (numHour.Value == 0) && (numSec.Value==0))
            {
                MessageBox.Show("Please select time","Error", MessageBoxButtons.OK);
            }
            else
            {
                hour = (int)numHour.Value;
                min = (int)numMint.Value;
                sec = (int)numSec.Value;
                timer2.Start();
                timer1.Enabled = true;
                buttonCancle.Enabled = true;
                buttonStart.Enabled = false;
            }


        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            buttonStart.Enabled = true;
            buttonCancle.Enabled = false;
            labelTimeRemain.Text = "00:00:00";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {


            if (hour > 0 || min > 0 || sec > 0)
            {
                // change the time/ countdown
                if (min == 0 && hour > 0)
                {
                    min = 59;
                    hour -= 1;
                    sec = 60;
                }
                if (sec == 0 && min > 0)
                {
                    sec = 60;
                    min -= 1;
                }

                sec -= 1;


                labelTimeRemain.Text = string.Format("{0}:{1}:{2}", formatTime(hour), formatTime(min), formatTime(sec));


                // If the checkBox is checked this will show a alert
                if (checkBoxRemind.Checked)
                {
                    string msg = string.Format("Your computer will {0} in {1} minute. Please be alerted and save uncompleted work", comboBoxAction.Text, min);
                    if (sec == 0 && min == numRemind.Value)
                        MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                timer2.Stop();
                int select = comboBoxAction.SelectedIndex;
                switch (select)
                {
                    default:
                    case 0:
                        // logoff
                        Process.Start("Shutdown.exe", "-l");
                        break;
                    case 1:
                        //restart
                        Process.Start("Shutdown.exe", "-r -t 5");
                        break;
                    case 2:
                        //shutdown
                        Process.Start("Shutdown.exe", "-s -t 5");
                        break;

                }
            }
         



        } // timer tick

        private string formatTime(int times) // help with layout 0:1:2 -> 00:01:02
        {
            string t = times.ToString();
            if (times < 10)
            {
                t = "0" + t;
            }
            return t;
        }

        private void numSec_ValueChanged(object sender, EventArgs e)
        {

        }
    } //class
} // program
