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
        byte[] arrBytes = null;

        string inputText;
        Crypto newEncrypt;
        OpenFileDialog openDiag;

        public CryptoMain()
        {
            InitializeComponent();
        }

        private void CryptoMain_Load(object sender, EventArgs e)
        {
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = false;

            newEncrypt = new Crypto();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to exit","Exit dialog?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newEncrypt.CIPHERTEXT = arrBytes;
            newEncrypt.decryptBytesToString();
            rchtxtOutput.Text =  newEncrypt.OUTPUT;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newEncrypt.INPUTTEXT = inputText;
            newEncrypt.encryptText();
            rchtxtOutput.Text = newEncrypt.OUTPUT;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void normalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDiag = new OpenFileDialog();

            openDiag.Title = "Open Text File";
            openDiag.Filter = "Text Files|*.txt";
            openDiag.InitialDirectory = @"C:\";

            if(openDiag.ShowDialog()== DialogResult.OK)
            {
                //Process File here.

                try
                {
                    fileLines = File.ReadAllLines(openDiag.FileName);

                    rchInputBox.Clear();

                    for (int i = 1; i < fileLines.Length; i++)
                    {
                        string line = fileLines[i];
                        
                        rchInputBox.AppendText(line);

                        inputText = rchInputBox.Text;
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error opening File.\n Original Message " + ex.Message);
                }

                btnEncrypt.Enabled = true;
            }
        }

        private void decryptedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void encryptedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDiag = new OpenFileDialog();

            openDiag.Title = "Open Encrypted Binary File";
            openDiag.Filter = "Binary Files|*.bin";
            openDiag.InitialDirectory = @"C:\";

            if (openDiag.ShowDialog() == DialogResult.OK)
            {
                //Process File here.
                try
                {
                    arrBytes = File.ReadAllBytes(@"C:\temp\encyptedText.bin");

                    rchInputBox.Clear();

                    rchInputBox.Text = newEncrypt.displayText(arrBytes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening File.\n Original Message " + ex.Message);
                }

                btnDecrypt.Enabled = true;
            }
        }
    }
}
