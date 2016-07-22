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
                    using (StreamWriter writer = new StreamWriter(save.OpenFile(), System.Text.Encoding.Default))
                    {
                        foreach (Items item in _items)
                        {
                            string output = String.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"", item.Name, item.Id, item.BarCode, item.Unit);                           
                            writer.WriteLine(output);
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bindingSourceItems.DataSource = _items;
        }



        private void loader_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var reader = new StreamReader((string)e.Argument, System.Text.Encoding.Default))          
            {
                string line;
                line = reader.ReadLine();
                line = line.Replace("\"", "").Trim();
                string[] elements = line.Split(';');               
                Invoke(new Action(() =>
                {
                    nameDataGridViewTextBoxColumn.HeaderText = elements[0];
                    idDataGridViewTextBoxColumn.HeaderText = elements[1];
                    barCodeDataGridViewTextBoxColumn.HeaderText = elements[2];
                    unitDataGridViewTextBoxColumn.HeaderText = elements[3];
                }));

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    line = line.Replace("\"", "").Trim();
                    elements = line.Split(';');                    
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

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form2 form = new Form2())
            {
                Items current = new Items();            
                if (bindingSourceItems.Current != null)                  
                {
                   
                    current = (Items)bindingSourceItems.Current;
                    form.Items.Name = current.Name;
                    form.Items.Id = current.Id;
                    form.Items.BarCode = current.BarCode;
                    form.Items.Unit = current.Unit;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _items[bindingSourceItems.Position] = form.Items;
                    }
                } else
                {
                    MessageBox.Show("Nincs kijelölt Áru!");
                }
            }
        }
    }
}
