using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeattestFile
{
    public partial class Form1 : Form
    {
        public object secs { get; private set; }
        string authors;
        public Form1()
        {
            InitializeComponent();
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection();
            SqlCommand cmd1 = new SqlCommand();
            con1.ConnectionString = VFConnectionStrings.GetConnectionString.VFConString("VF Connect", "VFProd", "C:\\LeakTest\\LeakTest.ini");
            con1.Open();
            cmd1.Connection = con1;
            // cmd.Connection = con;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "LeaktestResults";
            cmd1.Parameters.AddWithValue("@Partno", textBox2.Text);
            cmd1.Parameters.AddWithValue("@Date", textBox1.Text);
            cmd1.Parameters.AddWithValue("@Leaktest",textBox3.Text);            
            cmd1.ExecuteNonQuery();
            try
            {
                MessageBox.Show("Saved Successfully !");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }       

        private void Form1_Load(object sender, EventArgs e)
        {
            authors = leaktestvalue();


            //authors.Substring(57, authors.IndexOf("PLR"));
            // Get a substring between two strings     
            Displayresults(authors);

            //string partNo = richTextBox1.Text.Substring(0, richTextBox1.Text.IndexOf("PLR"));
            //textBox2.Text = partNo;
        }

        private void Displayresults(string authors)
        {
            int firstStringPosition = authors.IndexOf("LR");
            int secondStringPosition = authors.IndexOf("PLR");
            string stringBetweenTwoStrings = authors.Substring(
                firstStringPosition + 5, secondStringPosition - 94);
            //secondStringPosition-84);

            string stringBetweenTwoStrings1 = authors.Substring(
                 firstStringPosition - 13, secondStringPosition - 85);
            //string stringBetweenTwoStrings = authors.Substring(
            //    stringBetweenTwoStrings2 + stringBetweenTwoStrings1);
            string date = richTextBox1.Text.Substring(14, 18);
            textBox1.Text = date;
            //textBox2.Text = authors.Substring(firstStringPosition ^ 4);
            textBox2.Text = stringBetweenTwoStrings1;

            int pfrom = authors.IndexOf("PLR ") + "PLR ".Length;
            int pTo = authors.LastIndexOf("LR");
            string status = authors.Substring(pfrom, pTo - pfrom);


            //int pfrom1 = authors.IndexOf("PLR ") + "PLR ".Length;
            //int pTo1 = authors.LastIndexOf("PLR");
            //string status1 = authors.Substring(pTo1-4);
            //status 
            //string stringBetweenTwoStrings = authors.Substring(
            // firstStringPosition + 5, secondStringPosition - 94);

            if (status == "  P  ")
            {
                textBox3.Text = "PASS";
                textBox3.BackColor = Color.Green;
                textBox3.ForeColor = Color.White;
            }
            else if (status == "  F  ")
            {
                textBox3.Text = "FAIL";
                textBox3.BackColor = Color.Red;
            }
        }

        private string leaktestvalue()
        {
            const string url = "http://192.168.80.34/report_test_res_last_1.txt";
            richTextBox1.Text = GetTextFile(url);
            richTextBox1.Select(0, 0);

            string authors = richTextBox1.Text;
            return authors;
        }

        private string GetTextFile(string url)
        {
            try
            {
                url = url.Trim();
                if (!url.ToLower().StartsWith("http")) url = "http://" + url;
                WebClient web_client = new WebClient();
                MemoryStream image_stream =
                    new MemoryStream(web_client.DownloadData(url));
                StreamReader reader = new StreamReader(image_stream);
                string result = reader.ReadToEnd();
                reader.Close();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error downloading file " +
                    url + '\n' + ex.Message,
                    "Download Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            return "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox3.BackColor = Color.White;
            richTextBox1.Clear();
           // this.Controls.Clear();
           
            //this.Load += new System.EventHandler(this.Form1_Load);
            //this.Form.Refresh();
            //timer1.Interval = 3000;
            //timer1.Enabled = true;
            //timer1.Start();
            leaktestvalue();
            Displayresults(authors);
            Form1 fr = new Form1();
            //this.Hide();
            fr.Show();
        }
        //private void TimeUp(object sender, EventArgs e)
        //{
        //    secs = ++secs;
        //    progressBar1.PerformStep();
        //    if (secs == 10)
        //    {
        //        //TransCheck.Stop();
        //        secs = 0;
        //        progressBar1.Value = 0;
        //        //if (!StopUpdates) { SetCompleted(); }
        //        ClearForm();
        //        GetData();
        //        TransCheck.Start();
        //    }
        //}
        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
