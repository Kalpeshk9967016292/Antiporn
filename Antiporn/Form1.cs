using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Antiporn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
                       
        }

        public List<string> block = new List<string>();
            


        private void Form1_Load(object sender, EventArgs e)
        {
            refreshblocklist();
            timer1.Enabled = true;
            timer1.Start();
            MessageBox.Show("Antiporn Started");
        }

        private void refreshblocklist()
        {
            string addblock = File.ReadAllText(@"block.txt");
            foreach (string val in addblock.Split('\n'))
            {
                block.Add(val.Trim());
                
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcesses())
            {
                string cp = p.MainWindowTitle.ToString();
                foreach (string value in block)
                {
                    if (cp.Contains(value.ToString()))
                    {
                        timer1.Stop();
                        timer1.Enabled = false;
                        p.Kill();
                        MessageBox.Show("Sorry I am afraid i can't let you do that "+ value.ToString()+" ;");
                        Log();
                        timer1.Enabled = true;
                        timer1.Start();
                        
                    }
                }
            }
        }

        private static void Log()
        {
            TextWriter tw = new StreamWriter(@"D:\C#\blocklog.txt");
            tw.WriteLine("You watched porn at " + DateTime.Now.ToString());
            tw.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                MessageBox.Show("Antiporn closing");
            }
            else if (e.CloseReason == CloseReason.TaskManagerClosing)
            {
                timer1.Stop();
                timer1.Enabled = false;
                //Application.Restart();
                MessageBox.Show("Sorry I am afraid i can't let you do that TM");
                timer1.Enabled = true;
                timer1.Start();
                e.Cancel = true;
            }
            else if (e.CloseReason == CloseReason.UserClosing)
            {
                timer1.Stop();
                timer1.Enabled = false;
                MessageBox.Show("Sorry I am afraid i can't let you do that US");
                //Application.Restart();
                timer1.Enabled = true;
                timer1.Start();
                e.Cancel = true;
            }
            else
            {
                Log();
            }
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Restart();
        }
    }
}
