using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }
        List<String> list = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
   
            openFileDialog1.Filter = "Текстовый файл|*.txt|Любой файл(*.*)|*.*";
            openFileDialog1.Title = "Выберите текстовый файл";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.FileName = "";
            try
                {
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        System.IO.StreamReader sr = new
                           System.IO.StreamReader(openFileDialog1.FileName);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(line);
                        Console.WriteLine(line);
                    }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ОШИБКА, НЕВОЗМОЖНО ОТКРЫТЬ ФАЙЛ: " + ex.Message);
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (list.Count != 0)
            {
                String fileName = "Чистый" + openFileDialog1.SafeFileName;
                TextWriter tw = new StreamWriter(fileName);
                MessageBox.Show(fileName);
                saveFileDialog1.Filter = "Текстовый файл | *.txt";
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.FileName = fileName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                       foreach(String li in list)
                        writer.WriteLine(li);
                    writer.Dispose();
                    writer.Close();
                }
                 }else {
                MessageBox.Show("Выберите файл");
            }
        }

    }
}
