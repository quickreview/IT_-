using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace ИТ_Курсач
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private int viruses = 0;

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        static  List<Segment> Test(string fileName)
        {
            var list = new List<Segment>();

            using (var hasher = new SHA256Managed())
            {
                var streamBreaker = new StreamMy();
                using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    list.AddRange(streamBreaker.GetSegments(file, file.Length, hasher));
                }
            }

            return list;
        }

        static string HashSHA256(string filename)
        {

            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                   
                    List<Segment> listSegment = Test(filename);

                    string signature = "";



                    foreach ( var file in listSegment )
                    {
                       
                           signature += BitConverter.ToString(file.Hash).Replace("-", "").ToLowerInvariant() + " ";
                     
                    }
                   // MessageBox.Show(signature  + " " + filename );

                    // var hash = sha256.ComputeHash(stream);



                    // return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(); // конвертация в 16 
                    return signature;
                }
            }

        }
      

        private void scanButton_Click(object sender, EventArgs e)
        {
            viruses = 0;
            listBox1.Items.Clear();

            string[] search = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
        
            progressBar1.Maximum = search.Length;

            foreach (string item in search)
            {
               
                   

                    StreamReader stream = new StreamReader(item);

                    string read = stream.ReadToEnd();

                
                    string[] virusSHA256 = new string[] {

                        "912d70844a72211d1113bbcaba6b0fc6507946235c557d3680b16eaf82443cee",
                        "37fb477df0125023ad9f66d5c52fd723b7d100c89da944d6ff07d504c3f4e43c",
                        "bf96648169ba89c284b3e94108074c7d5e5806c7b9498031aceded5ca139ed69",
                        "ed196ed82fa6ab0c1487cc64ac19fa3694404ec6a8fb4ffb7bcb2a14b5361790",
                        "cfdb3838ab8a096b5df680a841cc651a5b385a43dab58eb26263b0c20b2c7e22",
                        "9f2d7d369", "71d3e489" , "862742" , "0ffe1abd1a0" ,"28b57"
                    };

                    foreach (string st in virusSHA256)
                    {

                      
                        if (HashSHA256(item).Contains(st))
                        {
                            viruses += 1;
                            listBox1.Items.Add(item);
                            progressBar1.Increment(1);
                        }

                        else progressBar1.Increment(1);


                    }
                pictureBox8.Hide();
                listBox1.Show();
                label4.Hide();
                label5.Show();
                label7.Show();

                label7.Text = "Detected : " + viruses.ToString();
                    removeButton.Show();



            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            scanButton.Hide();
            removeButton.Hide();
            listBox1.Hide();
            progressBar1.Hide();

            label4.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            label6.Text = "Selected Location : " + folderBrowserDialog1.SelectedPath;
            viruses = 0;
            progressBar1.Value = 0;
            listBox1.Items.Clear();
            progressBar1.Show();

            label3.Hide(); 
            label4.Show();
            label6.Show();
            scanButton.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void removeButton_Click(object sender, EventArgs e)
        {
          


       
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            try
            {
                File.Delete(this.listBox1.Text);
                
                MessageBox.Show(this.listBox1.Text + " был удален!");
                listBox1.Items.Remove(this.listBox1.Text);

            }
            catch
            {
                MessageBox.Show("Выберите файл для удаления !");
            }
           
          
          

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

     

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
