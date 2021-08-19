using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oni_ab_tag
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            label1.Text = openFileDialog1.FileName;
            string[] tags = File.ReadAllLines(openFileDialog1.FileName);
            label2.Text = "Число тегов: "+ Convert.ToString(tags.Length - 1);

            string[] ts = tags[3].Split(',');
            if (ts.Length > 1) label3.Text = "Тип: US";
            else
            {
                string[] ts2 = tags[3].Split(';');
                if (ts2.Length > 1) label3.Text = "Тип: EU";
            }


            if (label3.Text == "Тип: EU" || label3.Text == "Тип: US") button2.Enabled = true;
            



        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (label3.Text == "Тип: EU" || label3.Text == "Тип: US") button2.Enabled = true;
            else button2.Enabled = false;
            button3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            saveFileDialog1.DefaultExt = "csv";
            saveFileDialog1.ShowDialog();
            

            using (var sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.Default))
            {
                char rawspl = ',';

                if (label3.Text == "Тип: EU") rawspl = ';';

                string spl = ",";

                if (checkBox1.Checked) spl = ";";


                sw.WriteLine("Name" + spl+ "DataType" + spl+"StationNo"+spl+ "Version");

                string[] rawtags = File.ReadAllLines(openFileDialog1.FileName);
                for (int i = 2; i<rawtags.Length;i++)
                {
                   
                    string[] tags = rawtags[i].Split(rawspl);

                    sw.WriteLine(tags[0] + spl + tags[1] + spl + "1" + spl + "1");
                    


                }
                
            }


            if (System.IO.File.Exists(saveFileDialog1.FileName))
            {
                label4.Text = "Успешно";
                label4.BackColor  = Color.FromName("Green");
                button3.Visible = true;
            }
                

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(saveFileDialog1.FileName);

        }
    }
}
