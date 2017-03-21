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
                int n, m, i;
                m = 2;
                if (list[0].Contains("var"))
                {
                    n = list.Count - 2;
                    i = 1;
                }
                else
                {
                    n = list.Count - 1;
                    i = 0;
                }
                
                Double[][] listDouble = new Double[n][];
                for (int x = 0; x < listDouble.Length; x++)
                {
                    listDouble[x] = new Double[m];
                }
                Char delimiter = ';';
                int j = 0;
                for ( ;i < list.Count -1; i++) {
                    String[] substrings = list[i].Split(delimiter);
                    Double.TryParse(substrings[0], out listDouble[j][0]);
                    Double.TryParse(substrings[1], out listDouble[j][1]);
                    j++;
                }
                List<String> clearList = new List<string>();
                bool saveValue = false;
                bool firstValue=true;
                for (int k = 0; k < listDouble.Length; k++) {
                    if (listDouble[k][0] < -1)
                    {
                        if (!saveValue) {
                            for (int p = k; p < k + 5; p++)
                            {
                                if (listDouble[p][0] < -1)
                                {
                                    saveValue = true;
                                }
                                else {
                                    saveValue = false;
                                    break;
                                }
                            } }
                        if (saveValue)
                            {
                            int d = k;
                            if (firstValue) {
                                if (k - 1 >= 0)
                                    d = k - 1;
                                firstValue = false;
                            }
                            clearList.Add(listDouble[d][0] + " ; " + listDouble[d][1]);
                            }

                    }
                    else {
                        if (saveValue) {
                            firstValue = true;
                            clearList.Add(listDouble[k][0] + " ; " + listDouble[k][1]);
                            goto suda;
                        }
                        saveValue = false;

                    }
                }
                suda:
                MessageBox.Show(fileName);
                saveFileDialog1.Filter = "Текстовый файл | *.txt";
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.FileName = fileName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                       foreach(String li in clearList)
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
