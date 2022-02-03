using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinuVorm
{
    class filmid: System.Windows.Forms.Form
    {
        TableLayoutPanel TLP = new TableLayoutPanel();
        ListBox listBox3;
        public filmid()
        {
            this.Size = new Size(500, 200);
            this.TLP.ColumnCount = 2;
            this.TLP.RowCount = 5;
            this.TLP.ColumnStyles.Clear();
            this.TLP.RowStyles.Clear();
            int i, j;
            listBox3 = new ListBox();
            listBox3.Items.Add("vali žanr");
            listBox3.Items.Add("komöödia");
            listBox3.Items.Add("põnevik");
            listBox3.Items.Add("roomanid");
            listBox3.Location = new Point(10, 10);
            listBox3.Width = 100;
            listBox3.Height = 100;
            listBox3.SelectedIndexChanged += new EventHandler(listBox3_SelectedIndexChanged);
            this.Controls.Add(listBox3);
        }
        void listBox3_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (listBox3.SelectedItem.ToString())
            {
                case ("vali žanr"): MessageBox.Show("Vali žanr") ; break;
                case ("komöödia"): ; break;
                case ("põnevik"): ; break;
                case ("roomanid"): ; break;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // filmid
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "filmid";
            this.Load += new System.EventHandler(this.filmid_Load);
            this.ResumeLayout(false);

        }

        private void filmid_Load(object sender, EventArgs e)
        {

        }
    }
}
