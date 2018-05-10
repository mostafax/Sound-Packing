using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace Algorithms_SoundPacking
{
    public partial class Form1 : Form
    {
        public static string Source = "";
        public static string Dstination = "";
        public static string dstination = "";
        //int sec;
        //int hours;
       // int min;
        public Form1()
        {
            InitializeComponent();
            //sec = hours = min = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            dstination = Dstination + @"\[3] WorstFitPriority";
            FittingOperations.worst_fitp(Source, dstination);
            textBox4.Text = time.Elapsed.ToString();
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Source = Convert.ToString(textBox1.Text);
            string File1 = Source + @"\AudiosInfo.txt";
            string File2 = Source + @"\readme.txt";
            BasicOperations.Initlaize(File1, File2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch time=new Stopwatch();
            time.Start();
          
            button2.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;

            dstination = Dstination + @"\[1] WorstFit";
            FittingOperations.worst_fit(Source, dstination);
            
           textBox3.Text =time.Elapsed.ToString();
           
          
            button2.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            button3.Enabled = false;
            button4.Enabled = false;
            button2.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            dstination = Dstination + @"\[4] WorstFitDecreasingPriority";
            FittingOperations.worst_fitp_dec(Source, dstination);
            textBox6.Text = time.Elapsed.ToString();
           
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button2.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            Task.Run(() =>
            {
                dstination = Dstination + @"\[3] FirsttFit";
               FittingOperations.First_fit(Source, dstination);
            }).Wait();

          // dstination = Dstination + @"\[5] FirstFit";
          // FittingOperations.First_fit(Source, dstination);
            textBox7.Text = time.Elapsed.ToString();
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button2.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button2.Enabled = false;
            dstination = Dstination + @"\[2] WorstFitDecreasing";
            FittingOperations.worst_fit_dec(Source, dstination);
            textBox5.Text = time.Elapsed.ToString();
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button2.Enabled = true;
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button2.Enabled = false;
            button8.Enabled = false;
           FittingOperations.BestFit.Best_fit();
           FittingOperations.BestFit.BestFitFilling(Source, Dstination);
           textBox8.Text = time.Elapsed.ToString();
           button3.Enabled = true;
           button4.Enabled = true;
           button5.Enabled = true;
           button6.Enabled = true;
           button2.Enabled = true;
           button8.Enabled = true;
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            button3.Enabled = false;
            button2.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            Task.Run(() =>
            {

                dstination = Dstination + @"\[6] FolderFilling";
                FittingOperations.filling.write(Source, dstination);
            }).Wait();
            textBox9.Text = time.Elapsed.ToString();
            button3.Enabled = true;
            button2.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
        }

       

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Dstination = Convert.ToString(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button10_Click(object sender, EventArgs e)
        {
            
            List<Task> MyTasks = new List<Task>();
            Stopwatch time = new Stopwatch();
            time.Start();
            MyTasks.Add(Task.Run( () => {
                    dstination = Dstination + @"\[1] WorstFit";
                        FittingOperations.worst_fit(Source, dstination);
                               } ));
            MyTasks.Add(Task.Run(() =>
            {
                dstination = Dstination + @"\[2] WorstFitDecreasing";
                FittingOperations.worst_fit_dec(Source, dstination);
            }));

            MyTasks.Add(Task.Run(() =>
            {
                dstination = Dstination + @"\[3] FirsttFit";
                FittingOperations.First_fit(Source, dstination);
            }));

            MyTasks.Add(Task.Run(() =>
            {
                dstination = Dstination + @"\[4] WorstFit";
                FittingOperations.worst_fitp(Source, dstination);
            }));
            MyTasks.Add(Task.Run(() =>
            {
                dstination = Dstination + @"\[5] WorstFitDecreasing";
                FittingOperations.worst_fitp_dec(Source, dstination);
            }));

            MyTasks.Add(Task.Run(() =>
            {
                FittingOperations.BestFit.Best_fit();
                //FittingOperations.BestFit.BestFitFilling(Source, Dstination);
            }));

            MyTasks.Add(Task.Run(() =>
            {
                dstination = Dstination + @"\[6] FolderFilling";
                FittingOperations.filling.write(Source, dstination);
            }));

            await Task.WhenAll(MyTasks.ToArray());
            time.Stop();
            textBox10.Text = time.Elapsed.ToString();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
