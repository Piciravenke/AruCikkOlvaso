using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AruCikkOlvaso
{
    public partial class Form2 : Form
    {
        public Items Items = new Items();       

        public Form2()
        {
            InitializeComponent();
        }        

        private void Form2_Load(object sender, EventArgs e)
        {           
            bindingSource1.DataSource = Items;
            comboBox1.SelectedValue= Items.Unit;                 
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                bindingSource1.EndEdit();
            }
        }
    }    
}
