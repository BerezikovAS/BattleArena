using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace BattleArena
{
    public partial class Menu : Form
    {
        System.Windows.Media.MediaPlayer Player = new System.Windows.Media.MediaPlayer();
        public Menu()
        {
            InitializeComponent();
        }
        private void buttonMouseEnter(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("ButtonBGEnter");
        }
        private void buttonMouseLeave(object sender, EventArgs e)
        {
            Button b = sender as Button;
            b.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("ButtonBG");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.Gamemode = "battle";
            Player_Off();
            Gates();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.Gamemode = "random";            
            Player_Off();
            Gates();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.Gamemode = "exit";
            Form.ActiveForm.Close();
            Player_Off();
        }
        async void Player_Off()
        {
            for (int i = 0; i < 100; i++)
            {
                Player.Volume = 0.5 - (Convert.ToDouble(i) / 200);
                await Task.Delay(10);
                //Thread.Sleep(10);                
            }
            Player.Stop();
        }

        async void Gates()
        {
            PictureBox pb = new PictureBox();
            Form1.ActiveForm.Controls.Add(pb);
            pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("GateUp");
            pb.Size = new Size(1920, 541);
            pb.Location = new Point(0, -541);
            pb.BringToFront();

            PictureBox pb2 = new PictureBox();
            Form1.ActiveForm.Controls.Add(pb2);
            pb2.Image = (Image)Properties.Resources.ResourceManager.GetObject("GateDown");
            pb2.Size = new Size(1920, 541);
            pb2.Location = new Point(0, 1080);
            pb2.BringToFront();


            while (pb.Location.Y < -150)
            {
                pb.Location = new Point(0, pb.Location.Y + 6);
                pb2.Location = new Point(0, pb2.Location.Y - 6);
                await Task.Delay(1);
            }
            while (pb.Location.Y < -50)
            {
                pb.Location = new Point(0, pb.Location.Y + 4);
                pb2.Location = new Point(0, pb2.Location.Y - 4);
                await Task.Delay(1);
            }
            while (pb.Location.Y < 0)
            {
                pb.Location = new Point(0, pb.Location.Y + 2);
                pb2.Location = new Point(0, pb2.Location.Y - 2);
                await Task.Delay(1);
            }
            if (pb.Location.Y >= 0)
            {
                Thread.Sleep(500);
                Form.ActiveForm.Hide();
                
                if (Form1.Gamemode == "battle" || Form1.Gamemode == "random")
                {
                    Pick pick = new Pick();
                    pick.Show();
                }                
            }
        }



        private void Menu_Load(object sender, EventArgs e)
        {
            Player.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "MainMenu.wav")));
            Player.Play();
        }
    }
}
