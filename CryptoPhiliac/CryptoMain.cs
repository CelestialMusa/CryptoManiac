using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace CryptoPhiliac
{
    public partial class CryptoMain : Form
    {
        string[] fileLines = null;

        public CryptoMain()
        {
            InitializeComponent();
        }

        private void CryptoMain_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit","Exit dialog?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void normalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDiag = new OpenFileDialog();

            openDiag.Title = "Open Text File";
            openDiag.Filter = "Text Files|*.txt";
            openDiag.InitialDirectory = @"C:\";

            if(openDiag.ShowDialog()== DialogResult.OK)
            {
                //Process File here.

                try
                {
                    fileLines = File.ReadAllLines(openDiag.FileName);

                    for (int i = 1; i < fileLines.Length; i++)
                    {
                        string line = fileLines[i];

                        rchInputBox.AppendText(line);
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error opening File.\n Original Message " + ex.Message);
                }
                 
            }
        }
    }
}
