using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace ZipAttempt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //folder to be backed up that may contain other folders/files
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)// zip the folder to backup location selected
        {
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox3.Text = saveFileDialog1.FileName;

                //check to see if .zip was added to filename
                Regex rgx = new Regex("(.zip)$");
                if (!rgx.IsMatch(textBox3.Text))
                { 
                    //if .zip not found at end of filename then add .zip
                    textBox3.Text += ".zip";
                }

                //see if file already exists and erase if it does to avoid exception
                if (File.Exists(textBox3.Text))
                {
                    File.Delete(textBox3.Text);
                }

                ZipFile.CreateFromDirectory(textBox1.Text, textBox3.Text);

                //if finished let user know it is complete and then reset the text boxes
                textBox1.Text = "";
                MessageBox.Show("File was successfully zipped");
                Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)//create script(.ps1) from folders list
        {
            DialogResult result = saveFileDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox4.Text = saveFileDialog2.FileName;
            }

            //check to see if .ps1 was added to filename
            Regex rgx = new Regex("(.ps1)$");
            if (!rgx.IsMatch(textBox4.Text))
            {
                //if .zip not found at end of filename then add .zip
                textBox4.Text += ".ps1";
            }

            //see if file already exists and erase if it does to avoid exception
            if (File.Exists(textBox4.Text))
            {
                File.Delete(textBox4.Text);
            }

            //File.Create is used to make the file first;
            FileStream fs = File.Create(textBox4.Text);

            //Byte[] is needed to add charachters to the opened file.create file textBox2 is the path to backup to
            Byte[] script = new UTF8Encoding(true).GetBytes("del " + textBox2.Text + " -recurse -force \n" + 
                "Add-Type -A System.IO.Compression.FileSystem \n" +
                "[IO.Compression.ZipFile]::CreateFromDirectory('" + textBox1.Text + "', '" + textBox2.Text + "')");

            //writes the byte[] to the file
            fs.Write(script, 0, script.Length);

            //must dispose of the filestream opened with file.create to release resources
            fs.Dispose();

            //if finished let user know it is complete and then reset the text boxes
            MessageBox.Show("File was successfully created");
            Close();
        }

        private void button2_Click(object sender, EventArgs e)//select backup folder and filename for script
        {
            DialogResult result = saveFileDialog3.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = saveFileDialog3.FileName;

                //check to see if .zip was added to filename
                Regex rgx = new Regex("(.zip)$");
                if (!rgx.IsMatch(textBox2.Text))
                {
                    //if .zip not found at end of filename then add .zip
                    textBox2.Text += ".zip";
                }

            }
        }
    }
}
