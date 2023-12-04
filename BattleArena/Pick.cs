using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using BattleArena.Properties;
using System.IO.Compression;
using System.IO;
using System.Media;

namespace BattleArena
{
    public partial class Pick : Form
    {
        List<Hero> heroes = new List<Hero>();
        List<Hero> pickedheroes = new List<Hero>();
        List<Hero> bannedheroes = new List<Hero>();
        List<Label> init = new List<Label>();
        List<Panel> hoverpanels = new List<Panel>();
        List<PictureBox> placePB = new List<PictureBox>();
        List<Label> winrate = new List<Label>();
        Hero placeHero = new Hero();
        System.Windows.Media.MediaPlayer Player = new System.Windows.Media.MediaPlayer();
        public string FileTMP;
        System.Media.SoundPlayer sp2 = new System.Media.SoundPlayer();

        int pickturn = 0;
        int initiative = 1;
        string teampick = "";
        bool placement = false;

        int[][] teamPICK01 = new int[2][];
        int rand = 0;
        public Pick()
        {
            InitializeComponent();
        }

        private void Pick_Load(object sender, EventArgs e)
        {
            pb.BringToFront();
            pb2.BringToFront();
            CreateHeroes();
            FillWinRate();
            FillHeroes();
            AbilitiesInfo();            
            
            teamPICK01[0] = new int[] { 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 2};
            teamPICK01[1] = new int[] { 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 2};

            Random rnd = new Random();
            int i = rnd.Next(1,3);
            rand = i - 1;

            if (teamPICK01[rand][pickturn] == 0)
                teampick = "Blue";
            else
                teampick = "Red";

            panel4.Visible = true;
            placeHero = null;
            button1.Visible = false;
            panel5.Visible = true;

            init.Add(b1);
            init.Add(b2);
            init.Add(b3);
            init.Add(r1);
            init.Add(r2);
            init.Add(r3);

            hoverpanels.Add(panelHover1);
            hoverpanels.Add(panelHover2);
            hoverpanels.Add(panelHover3);
            hoverpanels.Add(panelHover4);

            placePB.Add(PlaceB1);
            placePB.Add(PlaceB2);
            placePB.Add(PlaceB3);
            placePB.Add(PlaceR1);
            placePB.Add(PlaceR2);
            placePB.Add(PlaceR3);
            
            foreach (var n in init)
            {
                n.Visible = false;
            }

            RefreshView();

            Player.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "PickHeroes.wav")));
            Player.Play();
            LoadInfo();
            LoadCheck();
            GatesOff();
        }

        async void GatesOff()
        {
            /*PictureBox pb = new PictureBox();
            Form1.ActiveForm.Controls.Add(pb);
            pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("GateUp");
            pb.Size = new Size(1920, 541);
            pb.Location = new Point(0, 1);
            pb.BringToFront();

            PictureBox pb2 = new PictureBox();
            Form1.ActiveForm.Controls.Add(pb2);
            pb2.Image = (Image)Properties.Resources.ResourceManager.GetObject("GateDown");
            pb2.Size = new Size(1920, 541);
            pb2.Location = new Point(0, 538);
            pb2.BringToFront();*/
                        
            while (pb.Location.Y <= 1)
            {
                pb.Location = new Point(0, pb.Location.Y - 2);
                pb2.Location = new Point(0, pb2.Location.Y + 2);
                await Task.Delay(1);
            }
            while (pb.Location.Y < -50)
            {
                pb.Location = new Point(0, pb.Location.Y - 4);
                pb2.Location = new Point(0, pb2.Location.Y + 4);
                await Task.Delay(1);
            }
            while (pb.Location.Y < -150)
            {
                pb.Location = new Point(0, pb.Location.Y - 6);
                pb2.Location = new Point(0, pb2.Location.Y + 6);
                await Task.Delay(1);
            }
            if (pb.Location.Y < 0)
            {
                ActiveForm.Controls.Remove(pb);
                ActiveForm.Controls.Remove(pb2);
                return;
            }
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

                Form.ActiveForm.Close();
                Form1 battle = new Form1();
                battle.ShowDialog();
            }
        }
        private async void LoadCheck()
        {
            if (Form1.Gamemode == "random")
            {
                await Task.Delay(15);
                button2.PerformClick();
            }
        }
        private void RefreshView()
        {
            if (teampick == "Blue")
            {
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject("LeftPick");
                pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject("RightEmpty");
            }
            else
            {
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject("LeftEmpty");
                pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject("RightPick");
            }

            if (teampick == "Blue")
            {
                if (pickturn > 11)
                    label5.Text = "Синяя команда решает кто из бойцов получит " + initiative + " инициативу!";
                else if (pickturn == 2 || pickturn == 3 || pickturn == 7 || pickturn == 6 || pickturn == 11 || pickturn == 10)
                    label5.Text = "PICK";
                else
                    label5.Text = "BAN";
                int c = label5.Width / 2;
                label5.Location = new Point(597 - c, 11);
                label5.ForeColor = Color.Blue;
            }
            else
            {
                if (pickturn > 11)
                    label5.Text = "Красная команда решает кто из бойцов получит " + initiative + " инициативу!";
                else if (pickturn == 2 || pickturn == 3 || pickturn == 7 || pickturn == 6 || pickturn == 11 || pickturn == 10)
                    label5.Text = "PICK";
                else
                    label5.Text = "BAN";
                int c = label5.Width / 2;
                label5.Location = new Point(597 - c, 11);
                label5.ForeColor = Color.Red;
            }

            if (placement == true)
            {
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject("LeftEmpty");
                pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject("RightEmpty");

                label5.Text = "Время расстановки!";
                int c = label5.Width / 2;
                label5.Location = new Point(597 - c, 11);
                label5.ForeColor = Color.DarkGray;
            }
        }

        private void PickClick(object sender, EventArgs e)
        {
            
            string Name = Convert.ToString((sender as PictureBox).Tag);
            Hero hero = heroes.Find(x => x.NAME == Name);

            if (teampick == "Blue")
            {
                hero.TEAM = "B";
                
                if (teamPICK01[rand][pickturn+1] == 0)
                    teampick = "Blue";
                else
                    teampick = "Red";

                if (pickturn == 2 || pickturn == 3 || pickturn == 7 || pickturn == 6 || pickturn == 11 || pickturn == 10)
                    pickedheroes.Add(hero);
                else
                    bannedheroes.Add(hero);
            }
            else
            {
                hero.TEAM = "R";                
                if (teamPICK01[rand][pickturn+1] == 0)
                    teampick = "Blue";
                else
                    teampick = "Red";

                if (pickturn == 2 || pickturn == 3 || pickturn == 7 || pickturn == 6 || pickturn == 11 || pickturn == 10)
                    pickedheroes.Add(hero);
                else
                    bannedheroes.Add(hero);
            }
            pickturn++;
            if (pickturn > 11)
            {
                panel3.Enabled = false;
                Random rnd = new Random();
                int i = rnd.Next(1, 3);

                if (i == 1)
                    teampick = "Blue";
                else
                    teampick = "Red";
            }
            FillHeroes();
            RefreshView();
        }
        public string ExtractAudioResource()
        {
            string res_name = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), ".wav"));
            System.IO.Stream ReadResource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(Application.ProductName + ".PickHeroes.wav");
            byte[] b = new byte[ReadResource.Length];
            using (ReadResource) { ReadResource.Read(b, 0, (int)(ReadResource.Length)); }
            System.IO.File.WriteAllBytes(res_name, b);
            return res_name;
        }
        void Player_MediaEnded(object sender, EventArgs e)
        {
            Player.Stop(); Player.Play();
        }

        void Player_MediaOpened(object sender, EventArgs e)
        {
            System.IO.File.Delete(FileTMP);//удаляем файл.
        }
        async void Player_Off()
        {
            for (int i = 0; i < 100; i++)
            {
                Player.Volume = 0.5 - (Convert.ToDouble(i) / 200);
                await Task.Delay(10);
            }
            Player.Stop();
        }
        private void Hover(object sender, EventArgs e)
        {
            string Name = Convert.ToString((sender as PictureBox).Tag);
            Hero hero = heroes.Find(w => w.NAME == Name);
            string na = hero.NAME;
            na = na.Replace(' ', '_');
            pictureBox4.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
            label4.Text = hero.NAME;
            label3.Text = hero.MAXHP.ToString();
            labelArmorHover.Text = hero.ARMOR.ToString();
            int dmg1 = hero.DMG + hero.STATUS.DMGPASSIVE + hero.STATUS.DMG - hero.RAZBROS;
            int dmg2 = hero.DMG + hero.STATUS.DMGPASSIVE + hero.STATUS.DMG + hero.RAZBROS;
            labelDmgHover.Text = Convert.ToString(dmg1 + " - " + dmg2 + " ур.");

            if (hero.RANGE == 1)
                pictureBoxAttack.Image = (Image)Properties.Resources.ResourceManager.GetObject("Attack");
            else
                pictureBoxAttack.Image = (Image)Properties.Resources.ResourceManager.GetObject("AttackRange");

            PictureBox pb = new PictureBox();
            pb.Size = new Size(50, 50);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            panel4.Visible = true;

            if (hero != null)
            {
                pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Passive");
                panelHover1.Controls.Clear();
                panelHover1.Controls.Add(pb);
                pb.Location = new Point(0, 0);
                panelHover2.Controls.Clear();
                panelHover3.Controls.Clear();
                panelHover4.Controls.Clear();
                pb = new PictureBox();
                pb.Size = new Size(50, 50);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                for (int i = 0; i < 3; i++)
                {
                    if (hero.NEEDAP[i] == 1)
                    {
                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("MAP");
                        hoverpanels[i + 1].Controls.Add(pb);
                        pb.Location = new Point(0, 0);
                        pb = new PictureBox();
                        pb.Size = new Size(50, 50);
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (hero.NEEDAP[i] == 2)
                    {
                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("BAP");
                        hoverpanels[i + 1].Controls.Add(pb);
                        pb.Location = new Point(0, 0);
                        pb = new PictureBox();
                        pb.Size = new Size(50, 50);
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (hero.NEEDAP[i] == 3)
                    {
                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("BMAP");
                        hoverpanels[i + 1].Controls.Add(pb);
                        pb.Location = new Point(0, 0);
                        pb = new PictureBox();
                        pb.Size = new Size(50, 50);
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (hero.NEEDAP[i] == 0)
                    {
                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("XAP");
                        hoverpanels[i + 1].Controls.Add(pb);
                        pb.Location = new Point(0, 0);
                        pb = new PictureBox();
                        pb.Size = new Size(50, 50);
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }

                labelAbilties1.Text = hero.ABILITIES[0];
                labelAbilties2.Text = hero.ABILITIES[1];
                labelAbilties3.Text = hero.ABILITIES[2];
                labelAbilties4.Text = hero.ABILITIES[3];

                labelAbilitiesInfo1.Text = hero.ABILITIESINFO[0];
                labelAbilitiesInfo2.Text = hero.ABILITIESINFO[1];
                labelAbilitiesInfo3.Text = hero.ABILITIESINFO[2];
                labelAbilitiesInfo4.Text = hero.ABILITIESINFO[3];

                int x = label4.Width / 2;

                label4.Location = new Point(525 - x, 459);
            }

        }
        private void LoadInfo()
        {
            Random rnd = new Random();
            int r = rnd.Next(0, heroes.Count);

            Hero hero = heroes[r];
            string na = hero.NAME;
            na = na.Replace(' ', '_');
            pictureBox4.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
            label4.Text = hero.NAME;
            label3.Text = hero.MAXHP.ToString();
            labelArmorHover.Text = hero.ARMOR.ToString();
            int dmg1 = hero.DMG + hero.STATUS.DMGPASSIVE + hero.STATUS.DMG - hero.RAZBROS;
            int dmg2 = hero.DMG + hero.STATUS.DMGPASSIVE + hero.STATUS.DMG + hero.RAZBROS;
            labelDmgHover.Text = Convert.ToString(dmg1 + " - " + dmg2 + " ур.");

            if (hero.RANGE == 1)
                pictureBoxAttack.Image = (Image)Properties.Resources.ResourceManager.GetObject("Attack");
            else
                pictureBoxAttack.Image = (Image)Properties.Resources.ResourceManager.GetObject("AttackRange");

            PictureBox pb = new PictureBox();
            pb.Size = new Size(50, 50);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            panel4.Visible = true;

            if (hero != null)
            {
                pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Passive");
                panelHover1.Controls.Clear();
                panelHover1.Controls.Add(pb);
                pb.Location = new Point(0, 0);
                panelHover2.Controls.Clear();
                panelHover3.Controls.Clear();
                panelHover4.Controls.Clear();
                pb = new PictureBox();
                pb.Size = new Size(50, 50);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;

                for (int i = 0; i < 3; i++)
                {
                    if (hero.NEEDAP[i] == 1)
                    {
                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("MAP");
                        hoverpanels[i + 1].Controls.Add(pb);
                        pb.Location = new Point(0, 0);
                        pb = new PictureBox();
                        pb.Size = new Size(50, 50);
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (hero.NEEDAP[i] == 2)
                    {
                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("BAP");
                        hoverpanels[i + 1].Controls.Add(pb);
                        pb.Location = new Point(0, 0);
                        pb = new PictureBox();
                        pb.Size = new Size(50, 50);
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (hero.NEEDAP[i] == 3)
                    {
                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("BMAP");
                        hoverpanels[i + 1].Controls.Add(pb);
                        pb.Location = new Point(0, 0);
                        pb = new PictureBox();
                        pb.Size = new Size(50, 50);
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (hero.NEEDAP[i] == 0)
                    {
                        pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("XAP");
                        hoverpanels[i + 1].Controls.Add(pb);
                        pb.Location = new Point(0, 0);
                        pb = new PictureBox();
                        pb.Size = new Size(50, 50);
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }

                labelAbilties1.Text = hero.ABILITIES[0];
                labelAbilties2.Text = hero.ABILITIES[1];
                labelAbilties3.Text = hero.ABILITIES[2];
                labelAbilties4.Text = hero.ABILITIES[3];

                labelAbilitiesInfo1.Text = hero.ABILITIESINFO[0];
                labelAbilitiesInfo2.Text = hero.ABILITIESINFO[1];
                labelAbilitiesInfo3.Text = hero.ABILITIESINFO[2];
                labelAbilitiesInfo4.Text = hero.ABILITIESINFO[3];

                int x = label4.Width / 2;

                label4.Location = new Point(525 - x, 459);
            }

        }

        private void Place(object sender, EventArgs e)
        {
            PictureBox pb = (sender as PictureBox);

            if (placeHero != null)
            {
                if (pb.Name.Contains(placeHero.TEAM) == true)
                {
                    string na = placeHero.NAME;
                    na = na.Replace(' ', '_');
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
                    pb.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + placeHero.TEAM);

                    foreach (var n in placePB)
                    {
                        if (n.Tag != null)
                        {
                            if (n.Tag.ToString() == placeHero.NAME)
                            {
                                n.Tag = null;
                                n.Image = null;
                            }
                        }
                    }
                    pb.Tag = placeHero.NAME.ToString();

                    Hero h = pickedheroes.Find(x => x.NAME == placeHero.NAME);
                    if (h.TEAM == "B")
                    {
                        if (PlaceB1.Tag != null)
                        {
                            if (PlaceB1.Tag.ToString() == h.NAME)
                                h.COORD = new int[3] { -2, 4, -2 };
                        }
                        if (PlaceB2.Tag != null)
                        {
                            if (PlaceB2.Tag.ToString() == h.NAME)
                                h.COORD = new int[3] { -3, 3, 0 };
                        }
                        if (PlaceB3.Tag != null)
                        {
                            if (PlaceB3.Tag.ToString() == h.NAME)
                                h.COORD = new int[3] { -4, 2, 2 };
                        }
                    }
                    else
                    {
                        if (PlaceR1.Tag != null)
                        {
                            if (PlaceR1.Tag.ToString() == h.NAME)
                                h.COORD = new int[3] { 4, -2, -2 };
                        }
                        if (PlaceR2.Tag != null)
                        {
                            if (PlaceR2.Tag.ToString() == h.NAME)
                                h.COORD = new int[3] { 3, -3, 0 };
                        }
                        if (PlaceR3.Tag != null)
                        {
                            if (PlaceR3.Tag.ToString() == h.NAME)
                                h.COORD = new int[3] { 2, -4, 2 };
                        }
                    }

                    int ready = 0;
                    foreach (var m in placePB)
                    {
                        if (m.Tag != null)
                            ready++;
                    }
                    if (ready >= 6)
                    {
                        button1.Visible = true;
                    }
                    else
                        button1.Visible = false;

                    placeHero = null;
                }
            }
        }

        private void InitiativePick(object sender, EventArgs e)
        {
            string Name = Convert.ToString((sender as PictureBox).Tag);
            Hero hero = pickedheroes.Find(w => w.NAME == Name);
            
            if (hero.TEAM[0] == teampick[0] && pickturn > 6 && initiative <= 6 && hero.INITIATIV < 1)
            {
                if (teampick == "Blue" && initiative != 4)
                    teampick = "Red";
                else if (initiative != 4)
                    teampick = "Blue";

                Label l = init.Find(x => x.Tag.ToString() == hero.NAME);
                l.Text = initiative.ToString();
                l.Visible = true;

                hero.INITIATIV = initiative;
                initiative++;

                if (initiative > 6)
                {
                    placement = true;
                    panel5.Visible = true;

                }
                RefreshView();
            }
            else if (placement == true && hero != null)
            {
                placeHero = hero;
            }

        }


        private void CreateHeroes()
        {
            Hero Crossbowman = new Hero("Crossbowman", "", 200, 30, 4, 5);
            Crossbowman.RANGE = 2;            
            Crossbowman.KD = new int[] { 4, 3, 3 };
            Crossbowman.NEEDAP = new int[] { 2, 2, 1 };            
            Crossbowman.ABILITIES = new string[] { "Подготовка", "Пригвоздить", "Шиловидный болт", "Метка снайпера" };
            Crossbowman.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Berserker = new Hero("Berserker", "", 225, 30, 5, 8);
            Berserker.RANGE = 1;            
            Berserker.KD = new int[] { 3, 3, 4 };
            Berserker.NEEDAP = new int[] { 2, 2, 1 };            
            Berserker.ABILITIES = new string[] { "Ярость", "Вихрь топоров", "Наскок", "Запугивание" };
            Berserker.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Aeroturg = new Hero("Aeroturg", "", 200, 24, 3, 3);
            Aeroturg.RANGE = 2;            
            Aeroturg.KD = new int[] { 4, 4, 3 };
            Aeroturg.NEEDAP = new int[] { 2, 1, 2 };
            Aeroturg.ABILITIES = new string[] { "Электроразряд", "Цепная молния", "Рокировка", "Вакуум" };
            Aeroturg.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Butcher = new Hero("Butcher", "", 340, 30, 10, 0);
            Butcher.RANGE = 1;
            Butcher.KD = new int[] { 4, 3, 5 };
            Butcher.NEEDAP = new int[] { 2, 1, 2 };
            Butcher.ABILITIES = new string[] { "Плотоядство", "Шинковка", "Крюк", "Казнь" };
            Butcher.ATTACKABILITIES = new bool[] { true, false, true };

            Hero Witchdoctor = new Hero("Witch doctor", "", 200, 28, 4, 3);
            Witchdoctor.RANGE = 2;
            Witchdoctor.KD = new int[] { 3, 3, 4 };
            Witchdoctor.NEEDAP = new int[] { 2, 1, 1 };
            Witchdoctor.ABILITIES = new string[] { "Живая сила", "Исцеление", "Протекторат", "Очищение" };
            Witchdoctor.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Aramusha = new Hero("Aramusha", "", 220, 28, 4, 7);
            Aramusha.RANGE = 1;
            Aramusha.KD = new int[] { 4, 3, 4 };
            Aramusha.NEEDAP = new int[] { 2, 1, 1 };
            Aramusha.ABILITIES = new string[] { "Инь-Янь", "Двойное сечение", "Пинок", "Возмездие" };
            Aramusha.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Knight = new Hero("Knight", "", 220, 28, 4, 9);
            Knight.RANGE = 1;
            Knight.KD = new int[] { 2, 3, 4 };
            Knight.NEEDAP = new int[] { 1, 1, 2 };
            Knight.ABILITIES = new string[] { "Щит", "Оборона", "Клятва верности", "Провокация" };
            Knight.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Priest = new Hero("Priest", "", 210, 28, 4, 7);
            Priest.RANGE = 1;
            Priest.KD = new int[] { 4, 4, 4 };
            Priest.NEEDAP = new int[] { 1, 2, 2 };
            Priest.ABILITIES = new string[] { "Святой дух", "Благословение", "Кара Божья", "Милость Господа" };
            Priest.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Geomant = new Hero("Geomant", "", 200, 26, 4, 0);
            Geomant.RANGE = 2;
            Geomant.KD = new int[] { 2, 4, 4 };
            Geomant.NEEDAP = new int[] { 1, 2, 1 };
            Geomant.ABILITIES = new string[] { "Мощь земли", "Столоктит", "Каменный кулак", "Геопанцирь" };
            Geomant.ATTACKABILITIES = new bool[] { false, true, false };

            Hero Necromancer = new Hero("Necromancer", "B", 200, 27, 4, 3);
            Necromancer.RANGE = 2;            
            Necromancer.KD = new int[] { 4, 4, 6 };
            Necromancer.NEEDAP = new int[] { 3, 1, 3 };            
            Necromancer.ABILITIES = new string[] { "Вампиризм", "Иссушение", "Могильный холод", "Поднятие трупа" };
            Necromancer.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Warlock = new Hero("Warlock", "", 200, 27, 5, 3);
            Warlock.RANGE = 2;            
            Warlock.KD = new int[] { 4, 3, 10 };
            Warlock.NEEDAP = new int[] { 2, 1, 3 };
            Warlock.ABILITIES = new string[] { "Гниение", "Каррозия", "Проклятие", "Сделка с дьяволом" };
            Warlock.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Temporarium = new Hero("Temporarium", "", 200, 27, 4, 3);
            Temporarium.RANGE = 2;            
            Temporarium.KD = new int[] { 5, 4, 0 };
            Temporarium.NEEDAP = new int[] { 1, 2, 0 };            
            Temporarium.ABILITIES = new string[] { "Пески времени", "Временная петля", "Скоротечность", "Обнуление" };
            Temporarium.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Metamorph = new Hero("Metamorph", "", 220, 29, 4, 6);
            Metamorph.RANGE = 1;            
            Metamorph.KD = new int[] { 2, 4, 0 };
            Metamorph.NEEDAP = new int[] { 1, 2, 0 };            
            Metamorph.ABILITIES = new string[] { "Регенерация", "Крылья", "Удар щупальцем", "Дублирование" };
            Metamorph.ATTACKABILITIES = new bool[] { false, true, false };
            Metamorph.STATUS.REGENERATION = true;

            Hero Guardian = new Hero("Guardian", "", 220, 30, 3, 7);
            Guardian.RANGE = 1;           
            Guardian.KD = new int[] { 2, 3, 4 };
            Guardian.NEEDAP = new int[] { 2, 1, 1 };
            Guardian.ABILITIES = new string[] { "Размашистый удар", "Выпад", "Приказ", "Воодушевление" };
            Guardian.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Cryomant = new Hero("Cryomant", "", 200, 28, 2, 4);
            Cryomant.RANGE = 2;
            Cryomant.COORD = new int[] { -2, 4, -2 };
            Cryomant.KD = new int[] { 3, 4, 4 };
            Cryomant.NEEDAP = new int[] { 2, 3, 2 };
            Cryomant.ABILITIES = new string[] { "Лютый холод", "Град", "Взрыв стужи", "Криостазис" };
            Cryomant.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Cultist = new Hero("Cultist", "", 200, 25, 3, 5);
            Cultist.RANGE = 1;
            Cultist.KD = new int[] { 4, 4, 4 };
            Cultist.NEEDAP = new int[] { 2, 2, 1 };
            Cultist.ABILITIES = new string[] { "Культ крови", "Кровопускание", "Жертвоприношение", "Скверна" };
            Cultist.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Archer = new Hero("Archer", "R", 200, 26, 5, 3);
            Archer.RANGE = 2;
            Archer.KD = new int[] { 3, 4, 4 };
            Archer.NEEDAP = new int[] { 2, 2, 1 };
            Archer.ABILITIES = new string[] { "Свободные руки", "В яблочко!", "Залп", "Лечение ран" };
            Archer.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Shaman = new Hero("Shaman", "", 215, 15, 4, 7);
            Shaman.RANGE = 1;
            Shaman.KD = new int[] { 2, 3, 4 };
            Shaman.NEEDAP = new int[] { 1, 1, 1 };
            Shaman.ABILITIES = new string[] { "Дух земли", "Дух огня", "Дух ветра", "Дух воды" };
            Shaman.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Mage = new Hero("Mage", "", 200, 28, 4, 3);
            Mage.RANGE = 2;
            Mage.KD = new int[] { 5, 4, 4 };
            Mage.NEEDAP = new int[] { 2, 2, 1 };
            Mage.ABILITIES = new string[] { "Потоки маны", "Магический выброс", "Иллюзия", "Магический щит" };
            Mage.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Thief = new Hero("Thief", "", 210, 29, 4, 5);
            Thief.RANGE = 1;
            Thief.KD = new int[] { 3, 4, 5 };
            Thief.NEEDAP = new int[] { 2, 2, 1 };
            Thief.ABILITIES = new string[] { "Инстинкт убийцы", "Кульбит", "Отравленный клинок", "Дымовая бомба" };
            Thief.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Pyromant = new Hero("Pyromant", "", 200, 28, 4, 3);
            Pyromant.RANGE = 2;
            Pyromant.KD = new int[] { 4, 4, 4 };
            Pyromant.NEEDAP = new int[] { 2, 2, 1 };
            Pyromant.ABILITIES = new string[] { "Языки пламени", "Ожоги", "Воспламенение", "Адское клеймо" };
            Pyromant.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Golem = new Hero("Golem", "", 240, 27, 3, 5);
            Golem.RANGE = 1;
            Golem.KD = new int[] { 4, 4, 5 };
            Golem.NEEDAP = new int[] { 2, 1, 2 };
            Golem.ABILITIES = new string[] { "Укрепление", "Активная броня", "Кровь камня", "Апперкот" };
            Golem.ATTACKABILITIES = new bool[] { true, false, true };

            Hero Angel = new Hero("Angel", "", 200, 27, 4, 0);
            Angel.RANGE = 1;
            Angel.KD = new int[] { 4, 4, 4 };
            Angel.NEEDAP = new int[] { 2, 3, 3 };
            Angel.ABILITIES = new string[] { "Возрождение", "Небесный кулак", "Прорыв", "Ангел хранитель" };
            Angel.ATTACKABILITIES = new bool[] { true, false, true };

            Hero Spirit = new Hero("Spirit", "", 180, 26, 4, 0);
            Spirit.RANGE = 2;
            Spirit.KD = new int[] { 4, 3, 5 };
            Spirit.NEEDAP = new int[] { 2, 1, 3 };
            Spirit.ABILITIES = new string[] { "Осквернение души", "Внедрение", "Бесплотность", "Онемение" };
            Spirit.ATTACKABILITIES = new bool[] { false, false, true };

            heroes.Add(Crossbowman);
            heroes.Add(Berserker);
            heroes.Add(Aeroturg);
            heroes.Add(Butcher);
            heroes.Add(Witchdoctor);
            heroes.Add(Aramusha);
            heroes.Add(Knight);
            heroes.Add(Priest);
            heroes.Add(Geomant);
            heroes.Add(Necromancer);
            heroes.Add(Warlock);
            heroes.Add(Temporarium);
            heroes.Add(Metamorph);
            heroes.Add(Guardian);
            heroes.Add(Cryomant);
            heroes.Add(Cultist);
            heroes.Add(Archer);
            heroes.Add(Shaman);
            heroes.Add(Mage);
            heroes.Add(Thief);
            heroes.Add(Pyromant);
            heroes.Add(Golem);
            heroes.Add(Angel);
            heroes.Add(Spirit);
        }

        private void AbilitiesInfo()
        {
            string abinfo = "";

            foreach (var n in heroes)
            {
                if (n.NAME == "Crossbowman")
                {
                    abinfo = "Можете потратить дополнительное действие, чтобы нанести больше урона с атаки.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Проведите атаку по врагу. После чего враг не сможет ходить в свой ход.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Выберите напрвление по прямой и атакуйте всех врагов на этой линии сквозь броню.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Отметьте врага. Все атаки по нему будут наносить больше урона.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Berserker")
                {
                    abinfo = "Если вокруг Вас 2 врага и больше, вы наносите дополнительный урон с атак.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Проведите атаку по всем врагам вокруг себя.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Выберите врага по прямой от Вас в 2-х клетках, преместитесь к нему и атакуйте с бонусом к урону.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Все враги вокруг Вас получают оглушение и теряют дополнительное действие в свой ход.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Aeroturg")
                {
                    abinfo = "Все ваши атаки и способности игнорируют броню врага.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Атакуйте всех врагов, находящихся в непрерывной цепи от цели атаки.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Поменяйте местами двух любых существ в пределах 2-х клеток от себя.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "В радиусе 2-х клеток все враги получают небольшой урон и немоту в свой ход.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Butcher")
                {
                    abinfo = "При добивании врага, восстановите себе 60 хп.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Атакуйте врага 3 раза с небольшим штрафом к урону.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Выберите врага по прямой от Вас в пределах 2-х клеток и притяните к себе. Врга получит небольшой урон.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Атакуйте врага с бонусом к урону. Если на момент атаки у него менее 20% хп, он тут же погибает.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Witch doctor")
                {
                    abinfo = "Все герои в Вашем отряде получают +30 к максимальному значению хп.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Исцелите здороьве себе или союзнику в радиусе 2-х клеток.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Защитите своих союзников в радиусе 2-х клеток. 50% чистого урона, полученого ими, перенесется Вам.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "В радиусе 2-х клеток союзники снимают все отрицательные эффекты, а враги - положительные.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Aramusha")
                {
                    abinfo = "В начале каждого своего хода выберите Инь (+6 урона) или Янь (+3 брони и +8 хп).";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Атакуйте врага 2 раза с небольшим штрафом к урону.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Нанесите небольшой чистый урон врагу и оттолкните его на 1 клетку от Вас. Если это невозможно - враг оглушается.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Получите бонус +2 к урону за каждые 15 потерянных хп на этот ход.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Knight")
                {
                    abinfo = "Если Вас атакуют не в ближнем бою, вы получаете +3 к броне.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Вы встаете в защитную стойку. Пока Вы находитесь на своей клетке, Вы и Ваши союзники вокруг получают +3 брони.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Даруйте себе или союзнику в 2-х клетках от себя 30% бонус к урону от атак в свой ход.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Спровоцируйте врагов вокруг себя. Они не смогут ничего делать в свой ход, кроме как атаковать Вас.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Priest")
                {
                    abinfo = "В конце Вашего хода все союзники вокруг восстанавливают 8 хп.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Благословите себя или союзника в 2-х клетках. Оно повышает урон и броню в зависимости от потерянного здоровья.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Выберите врага в 2-х клетках от себя и атакуйте его. Атака наносит дополнительный урон за каждую способность в откате у врага.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Восстановите здоровье себе или союзнику в зависимости от потерянного хп в 2-х клетках и снимите все негативные эффеткы. ";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Geomant")
                {
                    abinfo = "Вы получаете +4 к урону и +3 к броне за каждый столоктит в радиусе 2-х клеток от Вас.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Поставьте столоктит в свободную клетку. Он нанесет небольшой урон врагам вокруг.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Атакуйте врага по прямой от Вас с бонусом к урону и оттолкните его на 1 клетку. Если за его спиной преграда, то атака нанесет больше урона.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Защитите себя или союзника в 2-х клетках геопанцирем, который даёт +7 брони и не дает возможность сдвигать обладателя.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Necromancer")
                {
                    abinfo = "Вы восстанавливаете 35% хп от потерянных у врага в результате атаки.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Нанесите повышенный урон врагу рядом. Враг получает иссушение и теряет основное действие и броню.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "До конца Вашего следующего хода враги, атакующие Вас, теряют хп равное 50% от нанесенного урона.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Призовите союзного мертвеца в соседнюю клетку.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Warlock")
                {
                    abinfo = "В конце Вашего хода враги вокруг Вас теряют 8 хп.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Выберите врага. Он потеряет всю броню до конца Вашего следующего хода, а также получит урон в зависимости от своей брони.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Прокляните врага. Проклятие снижает наносимый врагом урон на 40%.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Обменяйтесь здоровьем с рядомстоящим героем.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Temporarium")
                {
                    abinfo = "В начале Вашего хода Вы можете ускорить откат одной способности.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Вы совершаете еще один ход вне очереди.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Выберите врага. Все его неиспользованные способности блокируются на 1 ход, а также он получает урон, зависящий от количества таких способностей.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Откатите способность рядомстоящего союзника.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Metamorph")
                {
                    abinfo = "Каждый ход Вы получаете статус регенерации, который восстанавливает 15 хп в конце хода. Если Вы потеряете здоровье, статус пропадет.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Пролетите до двух клеток, игнорируя препятствия.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Атакуйте врага в 2-х клетках с бонусом к урону. Он не сможет атаковать и использовать атакующие навыки в свой ход.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Скопируйте и примените способность рядомстоящего врага.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Guardian")
                {
                    abinfo = "Когда Вы атакуете врага в ближнем бою, то враги стоящие рядом с ним и Вами получают 50% урона.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Атакуйте врага по прямой в 2-х клетках, игнорируя 4 брони цели.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Выберите союзника в 2-х клетках, он получит +1 дополнительное действие в свой ход.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Вы и союзники в радиусе 2-х клеток получают бонус к урону в зависимости от количества врагов вокруг них.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Cryomant")
                {
                    abinfo = "Враги, атакующие Вас в ближнем бою, наносят на 20% меньше урона.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Выберите зону в 2-х клетках от Вас. Враги в этой клетке и вокруг неё получат урон, зависящий от базовой атаки.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Атакуйте врагов в радиусе 2-х клеток. Враги рядом получат больше урона и оглушение.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Наложите на себя или союзника эффект заморозки восстанавливающий хп и снимающий негативные эффекты. Цель под заморозкой не будет получать урон, но пропустит свой ход.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Cultist")
                {
                    abinfo = "Атаки по врагам с негативными эффектами наносят на 10 урона больше.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Проведите атаку по варгу, который получит кровотечение на 2 хода.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Пожертвуйте до 30 хп. Союзник в 2-х клетках от Вас получит лечение в двойном размере от потерянного Вами здоровья.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Выберите область в радиусе 2-х клеток. В начале Вашего следующего хода все враги в ней потеряют 35 хп.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Archer")
                {
                    abinfo = "Если вокруг Вас нет врагов, Ваши атаки наносят дополнительный урон.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Проведите атаку, игнорирующую броню, по врагу по прямой на расстоянии от 2-х до 3-х клеток.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Выберите направление и атакуйте всех врагов на этом и смежных направлениях на расстоянии до 2-х клеток.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Восстановите здоровье в зависимости от свободных клеток вокруг Вас.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Shaman")
                {
                    abinfo = "Когда Вы атакуете, Вы наносите +3 урона за каждую свободную клетку вокруг Вас.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Вы получаете бонус +30% к урону в этот ход.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Переместите до 3-х любых героев, кроме Вас, в радиусе 2-х клеток на соседнюю с ними клетку.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Вы и союзники вокруг Вас восстанавливают немного здоровья, а враги теряют его.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Mage")
                {
                    abinfo = "В конце Вашего хода Вы получаете +1 ману.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Вы тратите всю накопленную ману. Враги в радиусе 2-х клеток потеряют, а союзники восстановят хп, зависящее от количества маны.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Создайте иллюзию. Враги оказавшиеся рядом с ней, в свой ход могут только атаковать и только её.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Наложите магический щит на себя или союзника. Он снимает негативные эффекты и защищает от получения новых.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Thief")
                {
                    abinfo = "Вы наносите тем больше урона, чем меньше здоровья у врага.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Проведите атаку и поменяйтесь местами с врагом.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Атакуйте врага и отравите его. Враг получит дополнительнй урон в конце своего хода, а его атаки будут наносить меньше урона.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Бросьте дымовую бомбу и исчезнете с поля боя. Враги рядом оглушаются. Вы появитесь в следующем ходу на той же или соседней клетке.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Pyromant")
                {
                    abinfo = "Враги оказавшиеся в свой ход рядом с вами теряют 8 хп.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Враг теряет 15% хп от максимального здоровья, а его урон умеьшается на процент от недостающего здоровья.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Выберите направление и атакуйте всех врагов в зоне шириной 3 клетки и длиной 2 клетки от Вас.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Отметьте врага. В начале Вашего следующего хода он и все враги вокруг него получат урон сквозь броню.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Golem")
                {
                    abinfo = "Вы получаете +1 дополнительную броню за каждую свободную клетку вокруг Вас.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Атакуйте всех врагов вокруг Вас. Урон зависит от Вашей брони. После чего Вы лишаетесь 1 брони.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Наложите на себя или союзника эффект крови камня на 2 круга, которыйв  начале хода восстановит хп в зависимости от своей брони.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Атакуйте врага и бросьте его в свободную клетку в радиусе 2-х клеток от Вас.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Angel")
                {
                    abinfo = "Умерев первый раз, Вы возрождаетесь с 50% здоровья.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Атакуйте врага в 2-х клетках от Вас. Он получает оглушение.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Переместитесь вплотную к любому врагу и атакуйте его.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Переместитесь вплотную к любому союзнику и восстановите ему здоровье.";
                    n.ABILITIESINFO[3] = abinfo;
                }
                else if (n.NAME == "Spirit")
                {
                    abinfo = "Враги причинившие Вам вред теряют 10 от своего максимального здоровья.";
                    n.ABILITIESINFO[0] = abinfo;
                    abinfo = "Вселитесь в союзника. Он получит доп. действие и Вашу пассивную способность. Вы появитесь на следующий ход.";
                    n.ABILITIESINFO[1] = abinfo;
                    abinfo = "Становясь бесплотным на 1 ход, Вы снижаете весь входящий урон на 50%.";
                    n.ABILITIESINFO[2] = abinfo;
                    abinfo = "Наносит небольшой урон врагу и накладывает на него слабость, безмолвие и оцепенение.";
                    n.ABILITIESINFO[3] = abinfo;
                }
            }
        }

        private void FillHeroes()
        {
            panel3.Controls.Clear();
            panel1.Controls.Clear();
            panel2.Controls.Clear();
            int x = 0;
            int y = 0;
            foreach (var n in heroes)
            {
                PictureBox pb = new PictureBox();
                Label l1 = new Label();

                l1 = winrate.Find(z => z.Tag.ToString() == n.NAME);

                pb.Size = new Size(125, 125);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                string na = n.NAME;
                na = na.Replace(' ', '_');
                pb.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
                panel3.Controls.Add(pb);
                panel3.Controls.Add(l1);
                pb.Location = new Point(143 * x + 18, 143 * y + 18);
                l1.Location = new Point(pb.Location.X, pb.Location.Y);
                l1.BringToFront();

                pb.Click += PickClick;
                pb.MouseEnter += Hover;
                pb.Tag = n.NAME;

                if (pickedheroes.Find(w => w.NAME == n.NAME) != null || bannedheroes.Find(w => w.NAME == n.NAME) != null)
                {
                    pb.Visible = false;
                    l1.Visible = false;
                }

                x++;
                if (x > 7)
                {
                    x = 0;
                    y++;
                }
            }            

            int b, r;
            b = r = 0;
            foreach (var n in pickedheroes)
            {
                PictureBox pb = new PictureBox();
                pb.Size = new Size(125, 125);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                string na = n.NAME;
                na = na.Replace(' ', '_');
                pb.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);                
                pb.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                pb.Tag = n.NAME;
                pb.MouseEnter += Hover;

                if (n.TEAM == "B")
                {
                    panel1.Controls.Add(pb);
                    pb.Location = new Point(18, 143 * b + 18);
                    if (b == 0)
                        b1.Tag = n.NAME;
                    else if (b == 1)
                        b2.Tag = n.NAME;
                    else if (b == 2)
                        b3.Tag = n.NAME;
                    b++;
                }
                else
                {
                    panel2.Controls.Add(pb);
                    pb.Location = new Point(18, 143 * r + 18);
                    if (r == 0)
                        r1.Tag = n.NAME;
                    else if (r == 1)
                        r2.Tag = n.NAME;
                    else if (r == 2)
                        r3.Tag = n.NAME;
                    r++;
                }
                pb.Click += InitiativePick;
            }
        }

        private void FillWinRate()
        {
            try
            {
                using (ExcelHelper helper = new ExcelHelper())
                {
                    if (helper.Open(path: Path.Combine(Environment.CurrentDirectory, "WinRate.xlsx")))
                    {
                        foreach (var n in heroes)
                        {
                            Label lwr = new Label();
                            lwr.BorderStyle = BorderStyle.FixedSingle;
                            lwr.Size = new Size(50,22);
                            double a = Convert.ToDouble(helper.GetValue(n.NAME));
                            string b = a.ToString() + ",0000";

                            if(a >= 100)
                                b = b.Substring(0, 3);
                            else
                                b = b.Substring(0, 4);

                            if (a >= 75)
                                lwr.BackColor = Color.Green;
                            else if (a >= 65)
                                lwr.BackColor = Color.LightGreen;
                            else if (a >= 55)
                                lwr.BackColor = Color.GreenYellow;
                            else if (a >= 45)
                                lwr.BackColor = Color.Yellow;
                            else if (a >= 35)
                                lwr.BackColor = Color.Orange;
                            else if (a >= 25)
                                lwr.BackColor = Color.OrangeRed;
                            else 
                                lwr.BackColor = Color.Red;

                            lwr.Text = b + "%";
                            lwr.Font = new Font(this.Font, FontStyle.Bold);

                            lwr.Tag = n.NAME;
                            //Color col = helper.GetColor(n.NAME);

                            winrate.Add(lwr);

                        }                       
                    }
                }
                ShowWinRate(0);
            }
            catch (Exception ex)
            {

            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.heroesPicked = this.pickedheroes;            
            Player_Off();
            Gates();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[][] coords = new int[6][];
            coords[0] = new int[] { -2, 4, -2 };
            coords[1] = new int[] { -3, 3, 0 };
            coords[2] = new int[] { -4, 2, 2 };
            coords[3] = new int[] { 4, -2, -2 };
            coords[4] = new int[] { 3, -3, 0 };
            coords[5] = new int[] { 2, -4, 2 };

            int[] initiative = new int[6];

            int[][] init1 = new int[6][];
            init1[0] = new int[] { 1, 3, 6 };
            init1[1] = new int[] { 3, 1, 6 };
            init1[2] = new int[] { 6, 3, 1 };
            init1[3] = new int[] { 6, 1, 3 };
            init1[4] = new int[] { 3, 6, 1 };
            init1[5] = new int[] { 1, 6, 3 };

            int[][] init2 = new int[6][];
            init2[0] = new int[] { 2, 4, 5 };
            init2[1] = new int[] { 2, 5, 4 };
            init2[2] = new int[] { 5, 4, 2 };
            init2[3] = new int[] { 5, 2, 4 };
            init2[4] = new int[] { 4, 5, 2 };
            init2[5] = new int[] { 4, 2, 5 };

            Random random = new Random();

            int bbb = random.Next(0, 6);
            Thread.Sleep(bbb);
            int rrr = random.Next(0, 6);

            int i = 0;
            while (i < 3)
            {
                Random rnd = new Random();
                int r = rnd.Next(0, heroes.Count);
                Hero hero = heroes[r];
                Hero pickedHero = pickedheroes.Find(x => x.NAME == hero.NAME);
                if (pickedHero == null)
                {
                    hero.TEAM = "B";
                    hero.COORD = coords[i];

                    if (teampick == "Blue")
                        hero.INITIATIV = init1[bbb][i];
                    else
                        hero.INITIATIV = init2[bbb][i];
                    pickedheroes.Add(hero);
                    i++;
                    Thread.Sleep(r);
                }
            }
            while (i < 6)
            {
                Random rnd = new Random();
                int r = rnd.Next(0, heroes.Count);
                Hero hero = heroes[r];
                Hero pickedHero = pickedheroes.Find(x => x.NAME == hero.NAME);
                if (pickedHero == null)
                {
                    hero.TEAM = "R";
                    hero.COORD = coords[i];

                    if (teampick == "Red")
                        hero.INITIATIV = init1[rrr][i - 3];
                    else
                        hero.INITIATIV = init2[rrr][i - 3];
                    pickedheroes.Add(hero);
                    i++;
                    Thread.Sleep(r);
                }
            }

            Form1.heroesPicked = this.pickedheroes;
            if (Form1.Gamemode == "random")
                Player.Stop();
            else
                Player_Off();
            Form.ActiveForm.Close();
            if (Form1.Gamemode == "battle" || Form1.Gamemode == "random")
            {
                Form1 battle = new Form1();
                battle.ShowDialog();
            }
        }

        private void Pick_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.W)
                ShowWinRate(1);
        }
        private void Pick_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.W)
                ShowWinRate(0);
        }
        private void ShowWinRate(int a)
        {
            if (a == 1)
            {
                foreach (var n in winrate)
                    n.Visible = true;
            }
            else
            {
                foreach (var n in winrate)
                    n.Visible = false;
            }
        }
    }
}
