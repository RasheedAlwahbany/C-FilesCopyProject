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

namespace Copy_Files
{
    public partial class Form1 : Form
    {
        string file_name = "",file_name2="";
        int file_size=0,counter=1;
        public void copy_files()
        {
            file_size = 100 / (file_size + 1);
            file_name = file_name2;
            progress_copy.Value = 0;
            try
            {
                string str = "";
                char chars = ' ';
                string name = file_name + "=";
                for (int i = 0; i < name.Length; i++)
                {
                    if (name[i] == '=')
                        break;
                    else if (chars == '.')
                        str = str + name[i];

                    else
                        chars = name[i];
                }
                // MessageBox.Show();
                string[] array = { "txt", "text", "php", "sql", "html", "js", "css", "db", "doc", "cs", "cpp", "h", "dll" };
                if (array.Contains(str.ToLower()))
                {

                    StreamReader reads = new StreamReader(tool_txt_from_path.Text);

                    string error = "no";

                    if (File.Exists(tool_txt_to_path.Text + file_name))
                    {
                        DialogResult dialog = MessageBox.Show(this, "File Exist You want to replace it:", "Info", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        if (dialog == DialogResult.Yes)
                        {
                            File.Delete(tool_txt_to_path.Text + file_name);

                        }
                        else if (dialog == DialogResult.No)
                        {
                            counter = 1;

                            file_name = files_name_changes(file_name2);

                            while (File.Exists(tool_txt_to_path.Text + file_name))
                            {
                                file_name = files_name_changes(file_name2);
                            }
                        }
                        else
                        {
                            error = "yes";
                        }

                    }
                    if (error != "yes")
                    {
                        StreamWriter writer = new StreamWriter(tool_txt_to_path.Text + file_name, true);
                        while (reads.Peek() >= 0)
                        {

                            writer.WriteLine(reads.ReadLine());
                            if (progress_copy.Value + file_size < 100)
                                progress_copy.Value = progress_copy.Value + file_size;
                            else
                                progress_copy.Value = 100;

                        }
                        progress_copy.Value = 100;
                        writer.Close();
                    }
                    //string str = reads.ReadToEnd();



                    reads.Close();
                }
                else
                {
                    File.Copy(tool_txt_from_path.Text, tool_txt_to_path.Text + file_name, true);
                    progress_copy.Value = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }
        }
        public string files_name_changes(string name)
        {
            string name2="",name_ext="";
            char ext=' ';
            for(int i=0; i< name.Length; i++)
            {
                if (name[i] == '.')
                    ext = '.';
                if (ext == '.')
                {
                    name_ext = name_ext + name[i];
                }
                else
                    name2 = name2 + name[i];
                   
            }

            name2 = name2 + "(copy " + counter + ")" + name_ext;
            counter += 1;
            return name2; 
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {


        }

        private void tool_txt_to_path_Click(object sender, EventArgs e)
        {

            

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();

            if (file.ShowDialog() == DialogResult.OK)
            {
                //string[] files = file.FileNames;
                int count = 0;

                string[] str_array = new string[10];
                string str = "", files = file.FileName + "\\";
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i] == '\\')
                    {
                        str_array[count] = str;
                        count += 1;
                        str = "";
                    }
                    else
                    {
                        str = str + files[i];
                    }


                }

                file_name = str_array[count - 1];
                file_name2 = file_name;
                // tool_txt_to_path.Text = str_array[count-1];
                tool_txt_from_path.Text = file.FileName;
                StreamReader reads = new StreamReader(file.FileName);
                while (reads.Peek() >= 0)
                {
                    reads.ReadLine();
                    file_size += 1;
                }
                reads.Close();

            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            if (folder.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(folder.SelectedPath);
                tool_txt_to_path.Text = folder.SelectedPath + "\\";

            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(this,"This is App under RAGT2019 Copy Rights\nThanks For Visit us.\nEng.Rasheed Adnan Al-Wahbany ^_^","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(tool_txt_from_path.Text!=""&&tool_txt_to_path.Text!="")
            copy_files();
        }
    }
}
