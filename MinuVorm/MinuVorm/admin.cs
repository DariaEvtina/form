using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinuVorm
{
    class admin:Form
    {
        DataGridView dataGridView_p;
        ComboBox com_f;
        Button valju;
        Button valjuf;
        Button naita;
        Button film_kustuta;
        Button fail_lisa;
        Button film_lisa;
        Button film_uuenda;
        static string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\Source\Repos\form\MinuVorm\MinuVorm\AppData\Ostpilet.mdf;Integrated Security=True";
        SqlConnection connect = new SqlConnection(conn);
        SqlCommand command;
        SqlDataAdapter adapter;
        public admin()
        {
            this.Size = new System.Drawing.Size(800, 400);
            this.Text = "Admin Vorm";
            naita = new Button();
            naita.Text = "Film näita";
            naita.Location = new System.Drawing.Point(20, 5);
            this.Controls.Add(naita);
            naita.Click += Film_naita_Click;
            Button naitaP = new Button();
            naitaP.Text = "piletid näita";
            naitaP.Location = new System.Drawing.Point(100, 5);
            this.Controls.Add(naitaP);
            naitaP.Click += Pilet_naita_Click;
            film_uuenda = new Button
            {
                Location = new System.Drawing.Point(600, 75),
                Size = new System.Drawing.Size(80, 25),
                Text = "Uuendamine",
                Visible = false

            };
            this.Controls.Add(film_uuenda);
            film_uuenda.Click += Film_uuenda_Click;
            film_kustuta = new Button
            {
                Location = new System.Drawing.Point(650, 100),
                Size = new System.Drawing.Size(80, 25),
                Text = "Kustutamine",
            };
            film_kustuta.Click += Film_kustuta_Click;
            this.Controls.Add(film_kustuta);

            fail_lisa = new Button
            {
                Location = new System.Drawing.Point(650, 125),
                Size = new System.Drawing.Size(80, 25),
                Text = "Faili valimine",
            };
            this.Controls.Add(fail_lisa);
            fail_lisa.Click += Fail_lisa_Click;
            film_lisa = new Button
            {
                Location = new System.Drawing.Point(650, 150),
                Size = new System.Drawing.Size(80, 25),
                Text = "Lisamine",
            };
            this.Controls.Add(film_lisa);
            film_lisa.Click += Film_lisa_Click;
            film_lisa.Visible = false;
            film_kustuta.Visible = false;
            fail_lisa.Visible = false;
        }

        private void Film_kustuta_Click(object sender, EventArgs e)
        {
            if (film_txt.Text != "" && aasta_txt.Text != "" && poster_txt.Text != "" && poster.Image != null)
            {
                connect.Open();
                command = new SqlCommand("DELETE FROM film WHERE Id_films=@id", connect);

                command.Parameters.AddWithValue("@id", Id);
                command.ExecuteNonQuery();
                connect.Close();
                ClearData();
                Data();
                MessageBox.Show("Andmed kustatud");
            }
            else
            {
                MessageBox.Show("Viga");
            }
        }

        int Id;
        private void Film_uuenda_Click(object sender, EventArgs e)
        {

            if (film_txt.Text != "" && aasta_txt.Text != "" && poster_txt.Text != "" && poster.Image != null)
            {
                connect.Open();
                command = new SqlCommand("UPDATE film  SET nimetus=@film,aeg=@aasta,img=@poster WHERE Id_films=@id", connect);

                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@film", film_txt.Text);
                command.Parameters.AddWithValue("@aasta", aasta_txt.Text);
                command.Parameters.AddWithValue("@poster", poster_txt.Text);
                //string file_pilt = poster_txt.Text + ".jpg";
                //command.Parameters.AddWithValue("@poster", file_pilt);
                command.ExecuteNonQuery();
                connect.Close();
                ClearData();
                Data();
                MessageBox.Show("Andmed uuendatud");
            }
            else
            {
                MessageBox.Show("Viga");
            }

        }
        SaveFileDialog save;
        private void Fail_lisa_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
            open.InitialDirectory = Path.GetFullPath(@"C:\Users\opilane\Pictures\");//kust
            if (open.ShowDialog() == DialogResult.OK)
            {
                save = new SaveFileDialog();
                //save.FileName = poster_txt.Text;
                save.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
                save.InitialDirectory = Path.GetFullPath(@"..\..\films");//kuhu

                if (save.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(open.FileName, save.FileName);
                    save.RestoreDirectory = true;
                    poster.Image = Image.FromFile(save.FileName);
                    film_txt.Text = Path.GetFileNameWithoutExtension(save.FileName);
                    poster_txt.Text = Path.GetFileNameWithoutExtension(save.FileName) + ".JPG";
                }

            }
        }





        private void Film_lisa_Click(object sender, EventArgs e)
        {
            if (film_txt.Text != "" && aasta_txt.Text != "" && poster_txt.Text != "")
            {
                try
                {
                    connect.Open();
                    command = new SqlCommand("INSERT INTO film(nimetus,aeg,img) VALUES(@film,@aasta,@poster)", connect);

                    command.Parameters.AddWithValue("@film", film_txt.Text);
                    command.Parameters.AddWithValue("@aasta", aasta_txt.Text);
                    command.Parameters.AddWithValue("@poster", poster_txt.Text);

                    //string file_pilt = poster_txt.Text + ".jpg";
                    //command.Parameters.AddWithValue("@pilt", file_pilt);

                    command.ExecuteNonQuery();
                    connect.Close();
                    ClearData();
                    Data();
                    MessageBox.Show("Andmed on lisatud");
                }
                catch (Exception)
                {

                    MessageBox.Show("Viga lisamisega");
                }
            }
            else
            {
                MessageBox.Show("Viga else");
            }
        }
        
        private void Pilet_naita_Click(object sender, EventArgs e)
        {
            connect.Open();
            DataTable tabel_p = new DataTable();
            dataGridView_p = new DataGridView();
            DataSet dataset_p = new DataSet();
            SqlDataAdapter adapter_p = new SqlDataAdapter("SELECT rida,koht,filmID,saalID FROM [dbo].[piletid]; SELECT nimetus FROM [dbo].[film]", connect);

            //adapter_p.TableMappings.Add("Piletid", "Rida");
            //adapter_p.TableMappings.Add("Filmid", "Filmi_nimetus");
            //adapter_p.Fill(dataset_p);
            adapter_p.Fill(tabel_p);
            dataGridView_p.DataSource = tabel_p;
            dataGridView_p.Location = new System.Drawing.Point(10, 75);
            dataGridView_p.Size = new System.Drawing.Size(400, 200);
            SqlDataAdapter adapter_f = new SqlDataAdapter("SELECT nimetus FROM [dbo].[film]", connect);
            DataTable tabel_f = new DataTable();
            DataSet dataset_f = new DataSet();
            adapter_f.Fill(tabel_f);
            /*fkc = new ForeignKeyConstraint(tabel_f.Columns["Id"], tabel_p.Columns["Film_Id"]);
            tabel_p.Constraints.Add(fkc);*/


            DataGridViewComboBoxCell cbc = new DataGridViewComboBoxCell();
            com_f = new ComboBox();
            com_f.Location = new Point(500, 150);
            foreach (DataRow row in tabel_f.Rows)
            {
                com_f.Items.Add(row["nimetus"]);
                cbc.Items.Add(row["nimetus"]);
            }
            cbc.Value = com_f;
            connect.Close();
            valju = new Button();
            valju.Location = new Point(600, 200);
            valju.Text = "Valju";
            this.Controls.Add(valju);
            valju.Click += Valju_Click;
            this.Controls.Add(dataGridView_p);
            this.Controls.Add(com_f);
            dataGridView_p.Visible = true;
            com_f.Visible = true;
            valju.Visible = true;
        }

        private void Valju_Click(object sender, EventArgs e)
        {
            dataGridView_p.Visible = false;
            com_f.Visible = false;
            valju.Visible = false;
        }

        TextBox film_txt, aasta_txt, poster_txt;
        PictureBox poster;
        DataGridView dataGridView;
        private void Film_naita_Click(object sender, EventArgs e)
        {
            film_uuenda.Visible = true;
            film_txt = new TextBox
            { Location = new System.Drawing.Point(450, 75) };
            aasta_txt = new TextBox
            { Location = new System.Drawing.Point(450, 100) };
            poster_txt = new TextBox
            { Location = new System.Drawing.Point(450, 125) };
            poster = new PictureBox
            {
                Size = new System.Drawing.Size(111, 138),
                Location = new System.Drawing.Point(450, 150),
                Image=Image.FromFile("../../films/Start.png")

            };
            valjuf = new Button();
            valjuf.Location = new Point(600, 200);
            valjuf.Text = "Valju";
            valjuf.Click += Valjuf_Click;
            Data();
            dataGridView.Visible = true;
            poster_txt.Visible = true;
            aasta_txt.Visible = true;
            film_kustuta.Visible = true;
            film_lisa.Visible = true;
            film_uuenda.Visible = true;
            fail_lisa.Visible = true;

        }

        private void Valjuf_Click(object sender, EventArgs e)
        {
            film_uuenda.Visible=false;
            dataGridView.Visible = false;
            poster.Visible = false;
            film_txt.Visible = false;
            aasta_txt.Visible = false;
            poster_txt.Visible = false;
            film_kustuta.Visible = false;
            film_lisa.Visible = false;
            fail_lisa.Visible = false;
            valjuf.Visible = false;
        }
        public void Data()
        {
            connect.Open();
            DataTable tabel = new DataTable();
            dataGridView = new DataGridView();
            dataGridView.RowHeaderMouseClick += DataGridView_RowHeaderMouseClick;
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [dbo].[film]", connect);//, Kategooria WHERE Toodetable.Kategooria_Id=Kategooria.Id
            adapter.Fill(tabel);
            dataGridView.DataSource = tabel;
            dataGridView.Location = new System.Drawing.Point(10, 75);
            dataGridView.Size = new System.Drawing.Size(400, 200);
            connect.Close();
            this.Controls.Add(valjuf);
            this.Controls.Add(dataGridView);
            this.Controls.Add(film_txt);
            this.Controls.Add(aasta_txt);
            this.Controls.Add(poster_txt);
            this.Controls.Add(poster);
        }
        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Id = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            film_txt.Text = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            aasta_txt.Text = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            poster_txt.Text = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            poster.Image = Image.FromFile(@"..\..\films\" + dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());
            //string v = dataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
            //comboBox1.SelectedIndex = Int32.Parse(v) - 1;
        }
        private void ClearData()
        {
            //Id = 0;
            film_txt.Text = "";
            aasta_txt.Text = "";
            poster_txt.Text = "";
            //save.FileName = "";
            poster.Image = Image.FromFile("../../films/Start.png");

        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // admin
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "admin";
            this.Load += new System.EventHandler(this.admin_Load);
            this.ResumeLayout(false);

        }
        private void admin_Load(object sender, EventArgs e)
        {

        }
    }
}
