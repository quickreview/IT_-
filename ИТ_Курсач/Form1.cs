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
        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(); // конвертация в 16 
                }
            }
        }
        static string HashSHA1(string filename)
        {

            using (var sha1 = SHA1Managed.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = sha1.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(); // конвертация в 16 
                }
            }
          
        }
        static string HashSHA256(string filename)
        {

            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = sha256.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant(); // конвертация в 16 
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

                    string[] virusMD5 = new string[] {
                        "e94c782487c45e05daa15b57afa20889" , "c9693388f76469e05257f698675d93c1" ,
                        "b59bc37d6441d96785bda7ab2ae98f75" , "4424c7d314027baa42379860b24cef22",
                        "8efd9a3429817db5ee742fa2177e99df"};




                    string[] virusSHA1 = new string[] { "" +
                        "319cf082e242fec1b5df1bfba3aa4e5755196857", "48da403ce7d0b0a18956d9b62a72c0e99b6adf0e",
                        "9cf278fa132bb5c546c2f45f50d7ce7edd2cfe43" ,"4ee6978b82fa0540e37704c9b659f9aa45996fae",
                        "ec23cf7aa6dff4b7214cfe90f467ee90e20f9430" , "283a7c774bcdc99085ba7193427d9915428ed84c"};

                    string[] virusSHA256 = new string[] {

                        "912d70844a72211d1113bbcaba6b0fc6507946235c557d3680b16eaf82443cee",
                        "37fb477df0125023ad9f66d5c52fd723b7d100c89da944d6ff07d504c3f4e43c",
                        "bf96648169ba89c284b3e94108074c7d5e5806c7b9498031aceded5ca139ed69",
                        "ed196ed82fa6ab0c1487cc64ac19fa3694404ec6a8fb4ffb7bcb2a14b5361790",
                        "cfdb3838ab8a096b5df680a841cc651a5b385a43dab58eb26263b0c20b2c7e22"};

                    foreach (string st in virusMD5)
                    {

                        if ( CalculateMD5(item).Equals(st) )
                        {
                            viruses += 1;
                            listBox1.Items.Add(item);
                            progressBar1.Increment(1);
                        }
                      

                        else if (HashSHA1(item).Equals(st))
                        {
                            viruses += 1;
                            listBox1.Items.Add(item);
                            progressBar1.Increment(1);
                        }
                       
                        else if (HashSHA256(item).Equals(st))
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

            label3.Hide(); //  подсказка 
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
    }
}
