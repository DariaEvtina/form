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
    class StartVorm : System.Windows.Forms.Form
    {
        int saal;
        int filmID;
        Button film;
        Button film1;
        List<string> films = new List<string>() { "cats.png", "emotions.png", "nemo.png", "madakascar.png" };
        public StartVorm()
        {
            MainMenu menu = new MainMenu();
            MenuItem fileM = new MenuItem("file");
            fileM.MenuItems.Add("Välju", new EventHandler(fileM_EXIT_Select));
            fileM.MenuItems.Add("Admin vorm", new EventHandler(Admin_Click));
            menu.MenuItems.Add(fileM);
            this.Menu = menu;
            this.Size = new Size(500, 500);
            /*Button Start_btn = new Button
            {
                Size=new System.Drawing.Size(100,30),
                Text="Minu oma aaken",
                Location=new System.Drawing.Point(10,10)
            };
            Start_btn.Click += Start_btn_Click;
            this.Controls.Add(Start_btn);*/
            Button kino_btn = new Button
            {
                Size = new System.Drawing.Size(100, 30),
                Text = "saal 1",
                Location = new System.Drawing.Point(10, 50)
            };
            Button kino1_btn = new Button
            {
                Size = new System.Drawing.Size(100, 30),
                Text = "saal 2",
                Location = new System.Drawing.Point(110, 50)
            };
            Button kino2_btn = new Button
            {
                Size = new System.Drawing.Size(100, 30),
                Text = "saal 3",
                Location = new System.Drawing.Point(210, 50)
            };
            kino1_btn.Click += new EventHandler(Kino_btn_Click);
            kino2_btn.Click += new EventHandler(Kino_btn_Click);
            kino_btn.Click += new EventHandler(Kino_btn_Click);
            this.Controls.Add(kino_btn);
            this.Controls.Add(kino1_btn);
            this.Controls.Add(kino2_btn);
            Label titl = new Label
            {
                Text = "Kino Vorm",
                Size = new Size(200, 25),
                Location = new Point(10, 10),
                Font = new Font("Calibri", 16, FontStyle.Bold)
            };
            Label titl2 = new Label
            {
                Text = "Filmid",
                Size = new Size(200, 25),
                Location = new Point(10, 100),
                Font = new Font("Calibri", 16, FontStyle.Bold)
            };
            Button left = new Button
            {
                Text="<",
                Location = new System.Drawing.Point(10, 130),
                Size = new Size(20, 20),
            };
            Button right = new Button
            {
                Text = ">",
                Location = new System.Drawing.Point(30, 130),
                Size = new Size(20, 20),
            };
            right.Click += Right_Click;
            left.Click += Left_Click;
            film = new Button
            {
                Image = Image.FromFile(@"..\..\films\cats.png"),
                Location = new System.Drawing.Point(10, 150),
                Size = new Size(111, 138),
                Name = "cats"
            };
            film1 = new Button
            {
                Image = Image.FromFile(@"..\..\films\nemo.png"),
                Location = new System.Drawing.Point(140, 150),
                Size = new Size(111, 138),
                Name = "nemo"
            };
            this.Controls.Add(film);
            this.Controls.Add(film1);
            this.Controls.Add(left);
            this.Controls.Add(right);
            this.Controls.Add(titl);
            this.Controls.Add(titl2);
            film1.Click += new EventHandler(Film_Click);
            film.Click += new EventHandler(Film_Click);
        }

        private void Right_Click(object sender, EventArgs e)
        {
            if (film.Name == "cats")
            {
                film.Image = Image.FromFile(@"..\..\films\" + films[2]);
                film1.Image = Image.FromFile(@"..\..\films\" + films[3]);
                film.Name = "nemo";
                film1.Name = "madakascar";
            }
            else if (film.Name == "nemo")
            {
                film.Image = Image.FromFile(@"..\..\films\" + films[3]);
                film1.Image = Image.FromFile(@"..\..\films\" + films[1]);
                film.Name = "madakascar";
                film1.Name = "emotions";
            }
            else if (film.Name == "madakascar")
            {
                film.Image = Image.FromFile(@"..\..\films\" + films[0]);
                film1.Image = Image.FromFile(@"..\..\films\" + films[2]);
                film.Name = "cats";
                film1.Name = "nemo";
            }
        }

        private void Left_Click(object sender, EventArgs e)
        {
            if (film.Name=="cats")
            {
                film.Image = Image.FromFile(@"..\..\films\" + films[1]);
                film1.Image = Image.FromFile(@"..\..\films\" + films[0]);
                film.Name = "emotions";
                film1.Name = "cats";
            }
            else if (film.Name == "emotions")
            {
                film.Image = Image.FromFile(@"..\..\films\" + films[3]);
                film1.Image = Image.FromFile(@"..\..\films\" + films[1]);
                film.Name = "madakascar";
                film1.Name = "emotions";
            }
            else if (film.Name == "madakascar")
            {
                film.Image = Image.FromFile(@"..\..\films\" + films[0]);
                film1.Image = Image.FromFile(@"..\..\films\" + films[2]);
                film.Name = "cats";
                film1.Name = "nemo";
            }

        }

        private void Admin_Click(object sender, EventArgs e)
        {
            admin vorm = new admin();
            vorm.Show();
        }
        private void fileM_EXIT_Select(object sender, EventArgs e)
        {
            this.Close();
        }
        string filminimetus;
        private void Film_Click(object sender, EventArgs e)
        {
            Button btnClick = (Button)sender;
            filminimetus = Film(btnClick);
        }
        private string Film(Button btn)
        {
            if (btn.Name == "cats")
            {
                filmID = 1;
                filminimetus = "cats";
            }
            else if (btn.Name == "nemo")
            {
                filmID = 2;
                filminimetus = "nemo";
            }
            else if (btn.Name == "madakascar")
            {
                filmID = 3;
                filminimetus = "madakascar";
            }
            else if (btn.Name == "emotions")
            {
                filmID = 4;
                filminimetus = "emotions";
            }
            return filminimetus;

        }
        private void Kino_btn_Click(object sender, EventArgs e)
        {
            Button btnClick = (Button)sender;
            if (filmID!=0)
            {
                if (btnClick.Text == "saal 1")
                {
                    saal = 1;
                    MyForm uus_Aken = new MyForm(5, 5, filminimetus, saal, filmID);
                    uus_Aken.StartPosition = FormStartPosition.CenterScreen;
                    uus_Aken.ShowDialog();
                }
                else if (btnClick.Text == "saal 2")
                {
                    saal = 2;
                    MyForm uus_Aken = new MyForm(10, 10, filminimetus, saal, filmID);
                    uus_Aken.StartPosition = FormStartPosition.CenterScreen;
                    uus_Aken.ShowDialog();
                }
                else if (btnClick.Text == "saal 3")
                {
                    saal = 3;
                    MyForm uus_Aken = new MyForm(15, 15, filminimetus, saal, filmID);
                    uus_Aken.StartPosition = FormStartPosition.CenterScreen;
                    uus_Aken.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Vali filmi palun!");
            }

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StartVorm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "StartVorm";
            this.Load += new System.EventHandler(this.StartVorm_Load);
            this.ResumeLayout(false);

        }

        private void StartVorm_Load(object sender, EventArgs e)
        {

        }

        /*private void Start_btn_Click(object sender, EventArgs e)
        {
            MyVorm uus_Aken = new MyVorm("cats:musical","characters","rt tugger","macavity","alonzo","victoria");
            uus_Aken.StartPosition = FormStartPosition.CenterScreen;
            uus_Aken.ShowDialog();
        }*/
    }
}
