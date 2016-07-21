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

namespace AruCikkOlvaso
{
    public partial class Form1 : Form
    {
        private BindingList<Items> _items = new BindingList<Items>();
        public Form1()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (loader.IsBusy) return;
            bindingSourceItems.CancelEdit();
            using (OpenFileDialog open = new OpenFileDialog())
            {                
                open.CheckFileExists = true;
                open.Multiselect = false;
                open.Filter = "Csv fájlok|*.csv|Minden fájl|*.*";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    loader.RunWorkerAsync(open.FileName);
                }

            }      
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save = new SaveFileDialog())
            {
                save.Filter = "Csv fájlok| *.csv|Minden fájl| *.*";
                save.OverwritePrompt = true;
                save.AddExtension = true;
                save.DefaultExt = ".csv";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(save.FileName);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bindingSourceItems.DataSource = _items;
        }



        private void loader_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var reader = (new StreamReader((string)e.Argument)))
            {
                string line;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    //Thread.Sleep(1000);
                    line = line.Replace('"', ' ');
                    string[] elements = line.Split(';');                    
                    Items item = new Items();
                    item.Name = elements[0];
                    item.Id = elements[1];
                    item.BarCode = elements[2];
                    item.Unit = elements[3];
                    
                    Invoke(new Action(() =>
                    {
                        _items.Add(item);
                    }));                    
                }
            }
        }

        private void addNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form2 form = new Form2())
            {

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Items item = form.Items;
                    bindingSourceItems.CancelEdit();
                    _items.Add(item);

                }
            }
        }
    }
}
