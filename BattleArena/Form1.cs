using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace BattleArena
{
    public partial class Form1 : Form
    {
        int[] TESTcoord = new int[3];
        int[] InjectCoord = new int[3];
        int[] InjectCoordMet = new int[3];
        List<int[]> directions = new List<int[]>();
        List<Button> Hex = new List<Button>();
        List<Hero> heroes = new List<Hero>();
        public static List<Hero> heroesPicked = new List<Hero>();
        List<Panel> panels = new List<Panel>();
        List<Panel> hoverpanels = new List<Panel>();
        List<Hero> guardianPassive = new List<Hero>();
        List<Hero> spiritofwind = new List<Hero>();
        List<Button> inintPB = new List<Button>();
        List<Button> skverna = new List<Button>();
        List<Button> smoke = new List<Button>();
        List<Button> smokeMet = new List<Button>();
        List<Button> inj = new List<Button>();
        List<Button> injMet = new List<Button>();
        List<PictureBox> frames = new List<PictureBox>();
        List<PictureBox> framesHP = new List<PictureBox>();

        Hero replace = null;
        Hero CurrentHero = new Hero();
        string Mode = "none";
        int turn = 0;
        int initView = 6;
        int skvernaTurn = 0;
        int SpiritOfWind = 0;
        int pbHP = 0;
        int countAttack = 1;
        int dmgCount = 0;
        int dmgProtect = 0;
        bool bugWin = false;

        System.Windows.Media.MediaPlayer Player = new System.Windows.Media.MediaPlayer();
        public string FileTMP;
        int composition = 0;
        string[] music = new string[5];
        int volume = 5;

        System.Windows.Media.MediaPlayer SoundEffect = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer SoundAttack = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer SoundHod = new System.Windows.Media.MediaPlayer();

        bool[] CrossbowmanPassive = new bool[2] { false, false };
        bool ButcherPassive = false;
        bool GuradPassive = true;
        bool WitchdoctorPassive = false;
        bool GeomantPassive = false;
        bool TemporariumPassive = true;
        bool AngelPassive = false;
        bool Smoke = false;
        bool SmokeMet = false;
        bool Inj = false;
        bool InjMet = false;
        bool[] SkipFirstTurn = new bool[2] { false, false };

        public static string AramushaPassive = "0";

        public static Hero heroAbilities = new Hero();
        public static Hero hMetamorph = new Hero();
        public static string abilitiesMode = "";
        public static int abilitieChoose = 5;

        public static string Gamemode = "";

        public Form1()
        {
            InitializeComponent();
            TESTcoord[0] = -2;
            TESTcoord[1] = 4;
            TESTcoord[2] = -2;

            int[] dir1 = { 0, 1, -1 };
            int[] dir2 = { 1, 0, -1 };
            int[] dir3 = { 1, -1, 0 };
            int[] dir4 = { 0, -1, 1 };
            int[] dir5 = { -1, 0, 1 };
            int[] dir6 = { -1, 1, 0 };

            directions.Add(dir1);
            directions.Add(dir2);
            directions.Add(dir3);
            directions.Add(dir4);
            directions.Add(dir5);
            directions.Add(dir6);
            {
                Hex.Add(bm24m2);
                Hex.Add(bm14m3);
                Hex.Add(bm13m2);
                Hex.Add(b03m3);
                Hex.Add(b02m2);
                Hex.Add(b12m3);
                Hex.Add(b11m2);
                Hex.Add(b21m3);
                Hex.Add(b20m2);
                Hex.Add(b30m3);
                Hex.Add(b3m1m2);
                Hex.Add(b4m1m3);
                Hex.Add(b4m2m2);
                Hex.Add(bm23m1);
                Hex.Add(bm330);
                Hex.Add(bm12m1);
                Hex.Add(bm220);
                Hex.Add(b01m1);
                Hex.Add(bm110);
                Hex.Add(b10m1);
                Hex.Add(b000);
                Hex.Add(b2m1m1);
                Hex.Add(b1m10);
                Hex.Add(b3m2m1);
                Hex.Add(b2m20);
                Hex.Add(b3m30);
                Hex.Add(bm321);
                Hex.Add(bm211);
                Hex.Add(bm101);
                Hex.Add(b0m11);
                Hex.Add(b1m21);
                Hex.Add(b2m31);
                Hex.Add(b1m43);
                Hex.Add(b0m33);
                Hex.Add(bm1m23);
                Hex.Add(bm2m13);
                Hex.Add(bm303);
                Hex.Add(bm413);
                Hex.Add(b2m42);
                Hex.Add(b1m32);
                Hex.Add(b0m22);
                Hex.Add(bm1m12);
                Hex.Add(bm202);
                Hex.Add(bm312);
                Hex.Add(bm422);

            }

            hoverpanels.Add(panelHover1);
            hoverpanels.Add(panelHover2);
            hoverpanels.Add(panelHover3);
            hoverpanels.Add(panelHover4);

        }
        public string ExtractAudioResource(string MusicName)
        {
            string res_name = System.IO.Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), ".mp3"));
            System.IO.Stream ReadResource = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(Application.ProductName + MusicName + ".mp3");
            byte[] b = new byte[ReadResource.Length];
            using (ReadResource) { ReadResource.Read(b, 0, (int)(ReadResource.Length)); }
            System.IO.File.WriteAllBytes(res_name, b);
            return res_name;
        }
        async void Player_MediaEnded(object sender, EventArgs e)
        {
            Player.Stop();
            await Task.Delay(2000);
            composition++;
            PlayMusic();
        }

        void Player_MediaOpened(object sender, EventArgs e)
        {
            System.IO.File.Delete(FileTMP);//удаляем файл.
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Menu menu = new Menu();
            //menu.ShowDialog();
            SoundEffect.Volume = 0.9;
            SoundAttack.Volume = 0.9;
            SoundHod.Volume = 0.9;

            /*if (Gamemode == "random" || Gamemode == "battle")
            {
                Pick pick = new Pick();
                pick.ShowDialog();
            }
            else
            {
                LoadCheck();
                return;
            }*/

            {/*Hero Crossbowman = new Hero("Crossbowman", "B", 200, 32, 4, 5);
            Crossbowman.RANGE = 2;
            Crossbowman.COORD = new int[] {-2,4,-2 };
            Crossbowman.KD = new int[] { 4, 3, 3 };
            Crossbowman.NEEDAP = new int[] { 2, 2, 1 };
            Crossbowman.INITIATIV = 1;
            Crossbowman.ABILITIES = new string[] { "Подготовка", "Пригвоздить", "Шиловидный болт", "Метка снайпера" };
            Crossbowman.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Berserker = new Hero("Berserker", "B", 225, 31, 5, 8);
            Berserker.RANGE = 1;
            Berserker.COORD = new int[] { -4, 2, 2 };
            Berserker.KD = new int[] { 3, 3, 4 };
            Berserker.NEEDAP = new int[] { 2, 2, 1 };
            Berserker.INITIATIV = 6;
            Berserker.ABILITIES = new string[] { "Ярость", "Вихрь топоров", "Наскок", "Запугивание" };
            Berserker.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Aeroturg = new Hero("Aeroturg", "R", 200, 24, 3, 3);
            Aeroturg.RANGE = 2;
            Aeroturg.COORD = new int[] { 2, -4, 2 };
            Aeroturg.KD = new int[] { 4, 4, 3 };
            Aeroturg.NEEDAP = new int[] { 2, 1, 2 };
            Aeroturg.INITIATIV = 5;
            Aeroturg.ABILITIES = new string[] { "Электроразряд", "Цепная молния", "Рокировка", "Вакуум" };
            Aeroturg.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Butcher = new Hero("Butcher", "B", 340, 30, 10, 0);
            Butcher.RANGE = 1;
            Butcher.COORD = new int[] { -3, 3, 0 };
            Butcher.KD = new int[] { 4, 3, 4 };
            Butcher.NEEDAP = new int[] { 2, 1, 2 };
            Butcher.INITIATIV = 4;
            Butcher.ABILITIES = new string[] { "Плотоядство", "Шинковка", "Крюк", "Казнь" };
            Butcher.ATTACKABILITIES = new bool[] { true, false, true };

            Hero Witchdoctor = new Hero("Witch doctor", "R", 200, 28, 4, 3);
            Witchdoctor.RANGE = 2;
            Witchdoctor.COORD = new int[] { 2, -4, 2 };
            Witchdoctor.KD = new int[] { 3, 3, 4 };
            Witchdoctor.NEEDAP = new int[] { 2, 1, 1 };
            Witchdoctor.INITIATIV = 5;
            Witchdoctor.ABILITIES = new string[] { "Живая сила", "Исцеление", "Протекторат", "Очищение" };
            Witchdoctor.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Aramusha = new Hero("Aramusha", "B", 220, 28, 4, 7);
            Aramusha.RANGE = 1;
            Aramusha.COORD = new int[] { -4, 2, 2 };
            Aramusha.KD = new int[] { 4, 3, 4 };
            Aramusha.NEEDAP = new int[] { 2, 1, 1 };
            Aramusha.INITIATIV = 6;
            Aramusha.ABILITIES = new string[] { "Инь-Янь", "Двойное сечение", "Пинок", "Возмездие" };
            Aramusha.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Knight = new Hero("Knight", "B", 220, 28, 4, 9);
            Knight.RANGE = 1;
            Knight.COORD = new int[] { -2, 4, -2 };
            Knight.KD = new int[] { 2, 3, 4 };
            Knight.NEEDAP = new int[] { 1, 1, 2 };
            Knight.INITIATIV = 1;
            Knight.ABILITIES = new string[] { "Щит", "Оборона", "Клятва верности", "Провокация" };
            Knight.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Priest = new Hero("Priest", "R", 210, 28, 4, 7);
            Priest.RANGE = 1;
            Priest.COORD = new int[] { 4, -2, -2 };
            Priest.KD = new int[] { 4, 4, 4 };
            Priest.NEEDAP = new int[] { 1, 2, 2 };
            Priest.INITIATIV = 5;
            Priest.ABILITIES = new string[] { "Святой дух", "Благословение", "Кара Божья", "Милость Господа" };
            Priest.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Geomant = new Hero("Geomant", "R", 200, 24, 4, 0);
            Geomant.RANGE = 2;
            Geomant.COORD = new int[] { 3, -3, 0 };
            Geomant.KD = new int[] { 2, 4, 4 };
            Geomant.NEEDAP = new int[] { 1, 2, 1 };
            Geomant.INITIATIV = 3;
            Geomant.ABILITIES = new string[] { "Мощь земли", "Столоктит", "Каменный кулак", "Геопанцирь" };
            Geomant.ATTACKABILITIES = new bool[] { false, true, false };

            Hero Necromancer = new Hero("Necromancer", "B", 200, 27, 4, 3);
            Necromancer.RANGE = 2;
            Necromancer.COORD = new int[] { -2, 4, -2 };
            Necromancer.KD = new int[] { 4, 3, 4 };
            Necromancer.NEEDAP = new int[] { 3, 1, 3 };
            Necromancer.INITIATIV = 1;
            Necromancer.ABILITIES = new string[] { "Вампиризм", "Иссушение", "Могильный холод", "Поднятие трупа" };
            Necromancer.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Warlock = new Hero("Warlock", "B", 200, 27, 5, 3);
            Warlock.RANGE = 2;
            Warlock.COORD = new int[] { -2, 4, -2 };
            Warlock.KD = new int[] { 4, 3, 10 };
            Warlock.NEEDAP = new int[] { 2, 1, 3 };
            Warlock.INITIATIV = 1;
            Warlock.ABILITIES = new string[] { "Гниение", "Каррозия", "Проклятие", "Сделка с дьяволом" };
            Warlock.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Temporarium = new Hero("Temporarium", "B", 200, 27, 4, 3);
            Temporarium.RANGE = 2;
            Temporarium.COORD = new int[] { -2, 4, -2 };
            Temporarium.KD = new int[] { 4, 4, 0 };
            Temporarium.NEEDAP = new int[] { 1, 2, 1 };
            Temporarium.INITIATIV = 1;
            Temporarium.ABILITIES = new string[] { "Пески времени", "Временная петля", "Скоротечность", "Обнуление" };
            Temporarium.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Metamorph = new Hero("Metamorph", "R", 220, 29, 4, 6);
            Metamorph.RANGE = 1;
            Metamorph.COORD = new int[] { 4, -2, -2 };
            Metamorph.KD = new int[] { 2, 4, 0 };
            Metamorph.NEEDAP = new int[] { 1, 2, 1 };
            Metamorph.INITIATIV = 2;
            Metamorph.ABILITIES = new string[] { "Регенерация", "Крылья", "Удар щупальцем", "Дублирование" };
            Metamorph.ATTACKABILITIES = new bool[] { false, true, false };
            Metamorph.STATUS.REGENERATION = true;

            Hero Guardian = new Hero("Guardian", "R", 220, 30, 3, 7);
            Guardian.RANGE = 1;
            Guardian.COORD = new int[] { 3, -3, 0 };
            Guardian.KD = new int[] { 2, 3, 4 };
            Guardian.NEEDAP = new int[] { 2, 1, 1 };
            Guardian.INITIATIV = 3;
            Guardian.ABILITIES = new string[] { "Размашистый удар", "Выпад", "Приказ", "Воодушевление" };
            Guardian.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Cryomant = new Hero("Cryomant", "B", 200, 28, 2, 4);
            Cryomant.RANGE = 2;
            Cryomant.COORD = new int[] { -2, 4, -2 };
            Cryomant.KD = new int[] { 3, 4, 4 };
            Cryomant.NEEDAP = new int[] { 2, 3, 2 };
            Cryomant.INITIATIV = 1;
            Cryomant.ABILITIES = new string[] { "Лютый холод", "Град", "Взрыв стужи", "Криостазис" };
            Cryomant.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Cultist = new Hero("Cultist", "B", 200, 25, 3, 5);
            Cultist.RANGE = 1;
            Cultist.COORD = new int[] { -2, 4, -2 };
            Cultist.KD = new int[] { 4, 4, 4 };
            Cultist.NEEDAP = new int[] { 2, 2, 1 };
            Cultist.INITIATIV = 1;
            Cultist.ABILITIES = new string[] { "Культ крови", "Кровопускание", "Жертвоприношение", "Скверна" };
            Cultist.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Archer = new Hero("Archer", "R", 200, 26, 5, 3);
            Archer.RANGE = 2;
            Archer.COORD = new int[] { 4, -2, -2 };
            Archer.KD = new int[] { 3, 4, 4 };
            Archer.NEEDAP = new int[] { 2, 2, 1 };
            Archer.INITIATIV = 2;
            Archer.ABILITIES = new string[] { "Свободные руки", "В яблочко!", "Залп", "Лечение ран" };
            Archer.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Shaman = new Hero("Shaman", "R", 215, 15, 4, 7);
            Shaman.RANGE = 1;
            Shaman.COORD = new int[] { 4, -2, -2 };
            Shaman.KD = new int[] { 2, 3, 4 };
            Shaman.NEEDAP = new int[] { 1, 1, 1 };
            Shaman.INITIATIV = 2;
            Shaman.ABILITIES = new string[] { "Дух земли", "Дух огня", "Дух ветра", "Дух воды" };
            Shaman.ATTACKABILITIES = new bool[] { false, false, false };

            Hero Mage = new Hero("Mage", "R", 200, 28, 4, 3);
            Mage.RANGE = 2;
            Mage.COORD = new int[] { 3, -3, 0 };
            Mage.KD = new int[] { 5, 4, 4 };
            Mage.NEEDAP = new int[] { 2, 2, 1 };
            Mage.INITIATIV = 3;
            Mage.ABILITIES = new string[] { "Потоки маны", "Магический выброс", "Иллюзия", "Магический щит" };
            Mage.ATTACKABILITIES = new bool[] { true, false, false };

            Hero Thief = new Hero("Thief", "B", 210, 29, 4, 5);
            Thief.RANGE = 1;
            Thief.COORD = new int[] { -2, 4, -2 };
            Thief.KD = new int[] { 3, 4, 5 };
            Thief.NEEDAP = new int[] { 2, 2, 1 };
            Thief.INITIATIV = 1;
            Thief.ABILITIES = new string[] { "Инстинкт убийцы", "Кульбит", "Отравленный клинок", "Дымовая бомба" };
            Thief.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Pyromant = new Hero("Pyromant", "B", 200, 28, 4, 3);
            Pyromant.RANGE = 2;
            Pyromant.COORD = new int[] { -2, 4, -2 };
            Pyromant.KD = new int[] { 4, 4, 4 };
            Pyromant.NEEDAP = new int[] { 2, 2, 1 };
            Pyromant.INITIATIV = 1;
            Pyromant.ABILITIES = new string[] { "Языки пламени", "Ожоги", "Воспламенение", "Адское клеймо" };
            Pyromant.ATTACKABILITIES = new bool[] { true, true, false };

            Hero Golem = new Hero("Golem", "B", 240, 27, 3, 5);
            Golem.RANGE = 1;
            Golem.COORD = new int[] { -2, 4, -2 };
            Golem.KD = new int[] { 4, 4, 5 };
            Golem.NEEDAP = new int[] { 2, 1, 2 };
            Golem.INITIATIV = 1;
            Golem.ABILITIES = new string[] { "Укрепление", "Активная броня", "Кровь камня", "Апперкот" };
            Golem.ATTACKABILITIES = new bool[] { true, false, true };

            Hero Angel = new Hero("Angel", "B", 200, 27, 4, 0);
            Angel.RANGE = 1;
            Angel.COORD = new int[] { -2, 4, -2 };
            Angel.KD = new int[] { 4, 4, 4 };
            Angel.NEEDAP = new int[] { 2, 3, 3 };
            Angel.INITIATIV = 1;
            Angel.ABILITIES = new string[] { "Возрождение", "Небесный кулак", "Прорыв", "Ангел хранитель" };
            Angel.ATTACKABILITIES = new bool[] { true, false, true };

            Hero Bard = new Hero("Bard", "B", 200, 27, 3, 3);
            Bard.RANGE = 2;
            Bard.COORD = new int[] { -2, 4, -2 };
            Bard.KD = new int[] { 3, 4, 7 };
            Bard.NEEDAP = new int[] { 1, 1, 3 };
            Bard.INITIATIV = 1;
            Bard.ABILITIES = new string[] { "Легенды песен", "Марш", "Триумф", "Финал" };
            Bard.ATTACKABILITIES = new bool[] { false, false, true };*/
            }


            music = new string[] { ".Battle1", ".Battle2", ".Battle3", ".Battle4", ".Battle5" };

            foreach (var n in Hex)
            {
                n.Click += Move_Click;
                n.MouseEnter += HoverInfo;
                n.PerformClick();
            }
            //Geomant.HP = 5;
            /*heroes.Add(Bard);
            heroes.Add(Metamorph);
            heroes.Add(Mage);
            heroes.Add(Butcher);
            heroes.Add(Aeroturg);
            heroes.Add(Berserker);*/
            heroes = heroesPicked;

            if (heroes.Count == 0)
            {
                LoadCheck();
                return;
            }
            CurrentHero = heroes[heroes.Count - 1];

            Mode = "...";

            //AbilitiesInfo();
            turn = heroes.Count;
            RefreshAP();
            Refresh();
            PicturesHero();
            CheckPassive();
            StatusView();
            InitiativePanel();
            ModeView();
            button6.PerformClick();



            Hero check = heroes.Find(x => x.NAME == "Mage");
            if (check != null)
            {
                //check.MANA -= 1;
                Hero Illusion = new Hero("Illusion", check.TEAM, 30, 0, 0, 0);
                Illusion.COORD = new int[] { -5, -5, -5 };
                Illusion.NEEDAP = new int[] { -5, -5, -5 };
                Illusion.INITIATIV = check.INITIATIV + 1;
                Illusion.HP = 0;
                heroes.Add(Illusion);

                int i = Illusion.INITIATIV;
                heroes = heroes.OrderBy(x => x.INITIATIV).ToList();
                foreach (var n in heroes)
                {
                    if (n.INITIATIV == i && (n.NAME != "Illusion" || ((n.NAME == "Illusion") && n.TEAM != Illusion.TEAM)))
                    {
                        n.INITIATIV += 1;
                        i++;
                    }
                }

                check = heroes.Find(x => x.NAME == "Metamorph");
                if (check != null)
                {
                    Hero Illusion2 = new Hero("Illusion", check.TEAM, 30, 0, 0, 0);
                    Illusion2.COORD = new int[] { -5, -5, -5 };
                    Illusion2.NEEDAP = new int[] { -5, -5, -5 };
                    Illusion2.INITIATIV = check.INITIATIV + 1;
                    Illusion2.HP = 0;
                    heroes.Add(Illusion2);

                    int k = Illusion2.INITIATIV;
                    heroes = heroes.OrderBy(x => x.INITIATIV).ToList();
                    foreach (var n in heroes)
                    {
                        if (n.INITIATIV == k && (n.NAME != "Illusion" || ((n.NAME == "Illusion") && n.TEAM != Illusion2.TEAM)))
                        {
                            n.INITIATIV += 1;
                            k++;
                        }
                    }
                }
            }

            check = heroes.Find(x => x.NAME == "Geomant");
            if (check != null)
            {
                GeomantPassive = true;
                Hero st1 = new Hero("Stoloktit", check.TEAM, 20, 0, 0, 0);
                st1.COORD = new int[] { -5, -5, -5 };
                st1.NEEDAP = new int[] { -5, -5, -5 };
                st1.INITIATIV = heroes.Count + 1;
                heroes.Add(st1);

                Hero st2 = new Hero("Stoloktit", check.TEAM, 20, 0, 0, 0);
                st2.COORD = new int[] { -5, -5, -5 };
                st2.NEEDAP = new int[] { -5, -5, -5 };
                st2.INITIATIV = heroes.Count + 1;
                heroes.Add(st2);

                Hero st3 = new Hero("Stoloktit", check.TEAM, 20, 0, 0, 0);
                st3.COORD = new int[] { -5, -5, -5 };
                st3.NEEDAP = new int[] { -5, -5, -5 };
                st3.INITIATIV = heroes.Count + 1;
                heroes.Add(st3);

                check = heroes.Find(x => x.NAME == "Metamorph");
                if (check != null)
                {
                    Hero st4 = new Hero("Stoloktit", check.TEAM, 20, 0, 0, 0);
                    st4.COORD = new int[] { -5, -5, -5 };
                    st4.NEEDAP = new int[] { -5, -5, -5 };
                    st4.INITIATIV = heroes.Count + 1;
                    heroes.Add(st4);
                }
                foreach (var n in heroes)
                {
                    if (n.NAME == "Stoloktit")
                        n.HP = 0;
                }
            }
            check = heroes.Find(x => x.NAME == "Necromancer");
            if (check != null)
            {
                Hero Zombie = new Hero("Zombie", check.TEAM, 80, 20, 4, 4);
                Zombie.COORD = new int[] { -5, -5, -5 };
                Zombie.NEEDAP = new int[] { -5, -5, -5 };
                Zombie.INITIATIV = check.INITIATIV + 1;
                Zombie.HP = 0;
                heroes.Add(Zombie);

                int i = Zombie.INITIATIV;
                heroes = heroes.OrderBy(x => x.INITIATIV).ToList();
                foreach (var n in heroes)
                {
                    if (n.INITIATIV == i && (n.NAME != "Zombie" || ((n.NAME == "Zombie") && n.TEAM != Zombie.TEAM)))
                    {
                        n.INITIATIV += 1;
                        i++;
                    }
                }
                check = heroes.Find(x => x.NAME == "Metamorph" && Zombie.TEAM != x.TEAM);
                if (check != null)
                {
                    Hero Zombie2 = new Hero("Zombie", check.TEAM, 80, 20, 4, 4);
                    Zombie2.COORD = new int[] { -5, -5, -5 };
                    Zombie2.NEEDAP = new int[] { -5, -5, -5 };
                    Zombie2.INITIATIV = check.INITIATIV + 1;
                    Zombie2.HP = 0;
                    heroes.Add(Zombie2);
                    int k = Zombie2.INITIATIV;

                    heroes = heroes.OrderBy(x => x.INITIATIV).ToList();
                    foreach (var n in heroes)
                    {
                        if (n.INITIATIV == k && (n.NAME != "Zombie" || ((n.NAME == "Zombie") && n.TEAM != Zombie2.TEAM)))
                        {
                            n.INITIATIV += 1;
                            k++;
                        }
                    }

                }
            }

            pictureBoxVictory.Visible = false;
            label6.Visible = false;
            pictureBox4.Visible = false;
            label2.Text = CurrentHero.NAME;
            if (CurrentHero.TEAM == "B")
                label2.ForeColor = Color.Blue;
            else
                label2.ForeColor = Color.Red;
            CurrentHero = heroes[0];
            LoadInfo();

            composition = 0;
            Player.MediaEnded += Player_MediaEnded;
            PlayMusic();
            GatesOff();
            SetSkillsPictures();
        }

        private async void PlayMusic()
        {
            await Task.Delay(1100);
            if (composition == 0)
            {
                Random r = new Random();
                composition = r.Next(1, 6);
            }

            if (composition == 1)
            {
                Player.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Battle1.wav")));
                Player.Play();
            }
            else if (composition == 2)
            {
                Player.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Battle2.wav")));
                Player.Play();
            }
            else if (composition == 3)
            {
                Player.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Battle3.wav")));
                Player.Play();
            }
            else if (composition == 4)
            {
                Player.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Battle4.wav")));
                Player.Play();
            }
            else if (composition == 5)
            {
                Player.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Battle5.wav")));
                Player.Play();
            }
            else if (composition > 5)
            {
                composition = 1;
                PlayMusic();
            }


        }
        private async void PlaySound(string soundName)
        {
            SoundEffect.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\" + soundName)));
            SoundEffect.Play();

        }
        private async void PlaySoundHod()
        {
            if (Mode == "Движение" && (CurrentHero.NAME == "Knight" || CurrentHero.NAME == "Priest" || CurrentHero.NAME == "Guardian" || CurrentHero.NAME == "Golem"))
                SoundHod.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\HodArmor.wav")));
            else if (Mode == "Движение")
                SoundHod.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Hod.wav")));
            SoundHod.Play();

        }
        private async void PlaySoundAttack()
        {
            if (Mode == "Атака" && (CurrentHero.NAME == "Temporarium" || CurrentHero.NAME == "Witch doctor" || CurrentHero.NAME == "Mage"))
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Magic.wav")));
            else if (Mode == "Атака" && (CurrentHero.NAME == "Metamorph" || CurrentHero.NAME == "Shaman"))
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Spear.wav")));
            else if (Mode == "Атака" && (CurrentHero.NAME == "Knight" || CurrentHero.NAME == "Angel"))
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Sword.wav")));
            else if (Mode == "Атака" && (CurrentHero.NAME == "Butcher" || CurrentHero.NAME == "Cultist"))
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Knife.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Aramusha")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\DoubleSwords.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Crossbowman")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Crossbow.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Berserker")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\DoubleAxes.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Aeroturg")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Electro.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Priest")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Mace.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Geomant")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Stone.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Necromancer")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Moskits.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Warlock")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\BlackMagic.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Guardian")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Twohanded.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Cryomant")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Water.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Archer")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Bow.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Thief")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Daggers.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Pyromant")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Fire.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Golem")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Punch.wav")));
            else if (Mode == "Атака" && CurrentHero.NAME == "Spirit")
                SoundAttack.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "sounds\\attack\\Spirit.wav")));

            SoundAttack.Play();

        }

        private async void LoadCheck()
        {
            await Task.Delay(15);

            if (heroes.Count < 1)
            {
                Form1.ActiveForm.Close();
            }
        }
        private void Step(int[] entitypos, int[] steppos)
        {
            bool free = true;
            foreach (var n in heroes)
            {
                if (n.COORD[0] == steppos[0] && n.COORD[1] == steppos[1] && n.COORD[2] == steppos[2])
                    free = false;
            }

            if (free == true)
            {
                int[] ent = new int[3];
                ent[0] = entitypos[0];
                ent[1] = entitypos[1];
                ent[2] = entitypos[2];
                ent[0] -= steppos[0];
                ent[1] -= steppos[1];
                ent[2] -= steppos[2];

                bool agree = false;
                foreach (var n in directions)
                {
                    if (ent[0] == n[0] && ent[1] == n[1] && ent[2] == n[2])
                        agree = true;
                }

                if (agree == true)
                {
                    if (CurrentHero.HODM > 0 && Mode != "Наскок")
                        CurrentHero.HODM -= 1;
                    else if (CurrentHero.HODB > 0 && Mode != "Наскок")
                        CurrentHero.HODB -= 1;

                    Hero hero = heroes.Find(x => x.COORD[0] == entitypos[0] && x.COORD[1] == entitypos[1] && x.COORD[2] == entitypos[2]);

                    if (Mode != "Крюк" && Mode != "Пинок" && Mode != "Каменный кулак" && Mode != "Дух ветра")
                    {
                        CurrentHero.COORD = steppos;
                        //heroes.RemoveAll(x => x.NAME == CurrentHero.NAME);
                        //heroes.Add(CurrentHero);
                        if (CurrentHero.STATUS.ARMORED == true)
                            CurrentHero.STATUS.ARMORED = false;
                        if (CurrentHero.STATUS.INJECT[0] == 1)
                        {
                            Hero her = heroes.Find(x => x.TEAM == CurrentHero.TEAM && x.NAME == "Spirit");
                            if (her != null)
                                InjectCoord = steppos;
                            else
                                InjectCoordMet = steppos;      
                        }
                    }
                    else if (hero.STATUS.GEOSHIELD[0] != 1)
                    {
                        bool a = false;
                        foreach (var h in Hex)
                        {
                            string s = h.Name.Replace('m', '-');
                            int[] coord = new int[3];
                            coord = Coordinata(s);

                            if (coord[0] == steppos[0] && coord[1] == steppos[1] && coord[2] == steppos[2])
                                a = true;
                        }
                        if (a == true)
                        {
                            if (hero.NAME == "Illusion")
                            {
                                hero.HP = 0;
                                AngryDead(hero);
                                hero.COORD = new int[] { 5, 5, 5 };
                                listBox1.Items.Add("Иллюзия исчезла!");
                                Refresh();
                            }
                            else
                            {
                                hero.COORD = steppos;
                                AngryMove(hero);
                                if (hero.STATUS.ARMORED == true)
                                    hero.STATUS.ARMORED = false;
                                if (hero.STATUS.INJECT[0] == 1)
                                {
                                    Hero her = heroes.Find(x => x.TEAM == CurrentHero.TEAM && x.NAME == "Spirit");
                                    if (her != null)
                                        InjectCoord = steppos;
                                    else
                                        InjectCoordMet = steppos;
                                }
                            }
                        }
                    }

                    TemporariumPassive = false;
                    Refresh();
                    ZoneAbilities();
                    CheckPassive();
                    PyromantPassive();
                    PlaySoundHod();
                }
            }

        }
        private void Refresh()
        {
            bm24m2.BackgroundImage = this.bm14m3.BackgroundImage = bm13m2.BackgroundImage = this.b03m3.BackgroundImage = this.b02m2.BackgroundImage = this.b12m3.BackgroundImage =
                b11m2.BackgroundImage = b21m3.BackgroundImage = b20m2.BackgroundImage = b30m3.BackgroundImage = b3m1m2.BackgroundImage = b4m1m3.BackgroundImage =
                b4m2m2.BackgroundImage = bm23m1.BackgroundImage = bm330.BackgroundImage = bm12m1.BackgroundImage = bm220.BackgroundImage = b01m1.BackgroundImage =
                bm110.BackgroundImage = b10m1.BackgroundImage = b000.BackgroundImage = b2m1m1.BackgroundImage = b1m10.BackgroundImage = b3m2m1.BackgroundImage =
                b2m20.BackgroundImage = b3m30.BackgroundImage = bm321.BackgroundImage = bm211.BackgroundImage = bm101.BackgroundImage = b0m11.BackgroundImage =
                b1m21.BackgroundImage = b2m31.BackgroundImage = b1m43.BackgroundImage = b0m33.BackgroundImage = bm1m23.BackgroundImage = bm2m13.BackgroundImage =
                bm303.BackgroundImage = bm413.BackgroundImage = b2m42.BackgroundImage = b1m32.BackgroundImage = b0m22.BackgroundImage = bm1m12.BackgroundImage =
                bm202.BackgroundImage = bm312.BackgroundImage = bm422.BackgroundImage = null;

            foreach (var n in Hex)
                n.Image = null;

            foreach (var n in heroes)
            {
                string Tagg = "b";
                Tagg += Convert.ToString(n.COORD[0]);
                Tagg += Convert.ToString(n.COORD[1]);
                Tagg += Convert.ToString(n.COORD[2]);
                Tagg = Tagg.Replace('-', 'm');
                Button b = Hex.Find(x => x.Name.Contains(Tagg));

                if (b != null)
                {
                    string na = n.NAME;
                    na = na.Replace(' ', '_');
                    (this.Controls[b.Name] as Button).Image = (Image)Properties.Resources.ResourceManager.GetObject(na + "2");
                    (this.Controls[b.Name] as Button).BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                }
                if (n.NAME == "Stoloktit" && b != null)
                {
                    (this.Controls[b.Name] as Button).BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Stoloktit" + n.TEAM);
                }
            }

            RefreshAP();
            RefreshHP();
        }
        private void HoverInfo(object sender, EventArgs e)
        {
            Hero hero = new Hero();
            string coord = (sender as Button).Name;
            coord = coord.Replace('m', '-');
            int[] fieldcoord = Coordinata(coord);

            hero = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
            PictureBox pb = new PictureBox();
            pb.Size = new Size(50, 50);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            if (hero != null)
            {
                if (hero.NAME == "Mage")
                {
                    label6.Text = hero.MANA.ToString();
                    pictureBox4.Image = (Image)Properties.Resources.ResourceManager.GetObject("Mana");
                    label6.ForeColor = Color.FromArgb(1, 0, 192, 192);
                    pictureBox4.Visible = true;
                    label6.Visible = true;
                }
                else if (hero.NAME == "Bard")
                {
                    label6.Text = hero.MANA.ToString();
                    pictureBox4.Image = (Image)Properties.Resources.ResourceManager.GetObject("Note");
                    label6.ForeColor = Color.FromArgb(1);
                    pictureBox4.Visible = true;
                    label6.Visible = true;
                }
                else
                {
                    pictureBox4.Visible = false;
                    label6.Visible = false;
                }

                pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Passive");
                panelHover1.Controls.Clear();
                if (hero.ABILITIES[0] != "")
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

                double hp = (Convert.ToDouble(hero.HP) / Convert.ToDouble(hero.MAXHP)) * 300;
                pictureBox2.Size = new Size(Convert.ToInt32(hp), 32);
                labelHoverHP.Text = Convert.ToString(hero.HP);

                if (hero.RANGE == 1)
                    pictureBoxAttack.Image = (Image)Properties.Resources.ResourceManager.GetObject("Attack");
                else
                    pictureBoxAttack.Image = (Image)Properties.Resources.ResourceManager.GetObject("AttackRange");

                pictureBoxArmor.Image = (Image)Properties.Resources.ResourceManager.GetObject("Armor");

                if (hero.TEAM == "B")
                    pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject("HealthbarB");
                else
                    pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject("HealthbarR");

                string na = hero.NAME;
                na = na.Replace(' ', '_');
                pictureBoxTarget.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
                pictureBoxTarget.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + hero.TEAM);

                int dmg1 = hero.DMG + hero.STATUS.DMGPASSIVE + hero.STATUS.DMG - hero.RAZBROS;
                int dmg2 = hero.DMG + hero.STATUS.DMGPASSIVE + hero.STATUS.DMG + hero.RAZBROS;
                labelDmgHover.Text = Convert.ToString(dmg1 + " - " + dmg2 + " ур.");
                int ex = 0;
                if (hero.STATUS.ARMORED == true || hero.STATUS.ARMORED2 == true)
                    ex = 3;
                labelArmorHover.Text = Convert.ToString(hero.ARMOR + hero.STATUS.ARMORPASSIVE + hero.STATUS.BLESS[3] + hero.STATUS.ARMOR + ex);


                if (hero.NOWKD[0] > 0)
                    label3.Text = hero.NOWKD[0].ToString() + "⧖";
                else
                    label3.Text = "";

                if (hero.NOWKD[1] > 0)
                    label4.Text = hero.NOWKD[1].ToString() + "⧖";
                else
                    label4.Text = "";

                if (hero.NOWKD[2] > 0)
                    label5.Text = hero.NOWKD[2].ToString() + "⧖";
                else
                    label5.Text = "";

            }
            if (Mode == "Град")
            {
                foreach (var n in Hex)
                {
                    n.FlatAppearance.BorderSize = 1;
                    n.FlatAppearance.BorderColor = Color.Black;
                }
                if (skvernaTurn != 0)
                {
                    foreach (var n in skverna)
                    {
                        n.FlatAppearance.BorderSize = 5;
                        n.FlatAppearance.BorderColor = Color.Brown;
                    }
                }

                foreach (var n in Hex)
                {
                    string c = n.Name;
                    c = c.Replace('m', '-');
                    int[] fc = Coordinata(c);
                    if (Dist(fieldcoord, CurrentHero.COORD) <= 2 && Dist(fieldcoord, fc) <= 1)
                    {
                        n.FlatAppearance.BorderSize = 5;
                        n.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Воспламенение")
            {
                foreach (var n in Hex)
                {
                    n.FlatAppearance.BorderSize = 1;
                    n.FlatAppearance.BorderColor = Color.Black;
                }
                if (skvernaTurn != 0)
                {
                    foreach (var n in skverna)
                    {
                        n.FlatAppearance.BorderSize = 5;
                        n.FlatAppearance.BorderColor = Color.Brown;
                    }
                }
                if (CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2] && Dist(CurrentHero.COORD, fieldcoord) <= 2 && Dist(CurrentHero.COORD, fieldcoord) > 0)
                {
                    int[] direction = new int[3] { fieldcoord[0] - CurrentHero.COORD[0], fieldcoord[1] - CurrentHero.COORD[1], fieldcoord[2] - CurrentHero.COORD[2] };
                    for (int i = 0; i < 3; i++)
                    {
                        if (Math.Abs(direction[i]) > 1)
                            direction[i] = direction[i] / Math.Abs(direction[i]);
                    }

                    fieldcoord[0] = CurrentHero.COORD[0] + direction[0];
                    fieldcoord[1] = CurrentHero.COORD[1] + direction[1];
                    fieldcoord[2] = CurrentHero.COORD[2] + direction[2];
                    for (int i = 0; i < 2; i++)
                    {
                        string name = "b";
                        name += fieldcoord[0].ToString();
                        name += fieldcoord[1].ToString();
                        name += fieldcoord[2].ToString();
                        name = name.Replace("-", "m");

                        Button b = Hex.Find(x => x.Name == name);
                        if (b != null)
                        {
                            b.FlatAppearance.BorderSize = 5;
                            b.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                        fieldcoord[0] += direction[0];
                        fieldcoord[1] += direction[1];
                        fieldcoord[2] += direction[2];
                    }
                    fieldcoord[0] = CurrentHero.COORD[0] + direction[0];
                    fieldcoord[1] = CurrentHero.COORD[1] + direction[1];
                    fieldcoord[2] = CurrentHero.COORD[2] + direction[2];

                    foreach (var n in Hex)
                    {
                        string coord3 = n.Name;
                        coord3 = coord3.Replace('m', '-');
                        int[] fieldcoord3 = Coordinata(coord3);

                        if (Dist(CurrentHero.COORD, fieldcoord3) == 1 && Dist(fieldcoord, fieldcoord3) == 1)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                string name = "b";
                                name += fieldcoord3[0].ToString();
                                name += fieldcoord3[1].ToString();
                                name += fieldcoord3[2].ToString();
                                name = name.Replace("-", "m");

                                Button b = Hex.Find(x => x.Name == name);
                                if (b != null)
                                {
                                    b.FlatAppearance.BorderSize = 5;
                                    b.FlatAppearance.BorderColor = Color.DarkGreen;
                                }
                                fieldcoord3[0] += direction[0];
                                fieldcoord3[1] += direction[1];
                                fieldcoord3[2] += direction[2];
                            }
                        }
                    }
                }
            }
            else if (Mode == "Залп")
            {
                foreach (var n in Hex)
                {
                    n.FlatAppearance.BorderSize = 1;
                    n.FlatAppearance.BorderColor = Color.Black;
                }
                if (skvernaTurn != 0)
                {
                    foreach (var n in skverna)
                    {
                        n.FlatAppearance.BorderSize = 5;
                        n.FlatAppearance.BorderColor = Color.Brown;
                    }
                }
                if ((CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2]) && Dist(CurrentHero.COORD, fieldcoord) > 0)
                {

                    int[] direction = new int[3] { fieldcoord[0] - CurrentHero.COORD[0], fieldcoord[1] - CurrentHero.COORD[1], fieldcoord[2] - CurrentHero.COORD[2] };
                    for (int i = 0; i < 3; i++)
                    {
                        if (Math.Abs(direction[i]) > 1)
                            direction[i] = direction[i] / Math.Abs(direction[i]);
                    }

                    fieldcoord[0] = CurrentHero.COORD[0];
                    fieldcoord[1] = CurrentHero.COORD[1];
                    fieldcoord[2] = CurrentHero.COORD[2];
                    for (int i = 0; i < 2; i++)
                    {
                        fieldcoord[0] += direction[0];
                        fieldcoord[1] += direction[1];
                        fieldcoord[2] += direction[2];

                        string name = "b";
                        name += fieldcoord[0].ToString();
                        name += fieldcoord[1].ToString();
                        name += fieldcoord[2].ToString();
                        name = name.Replace("-", "m");

                        Button b = Hex.Find(x => x.Name == name);
                        if (b != null)
                        {
                            b.FlatAppearance.BorderSize = 5;
                            b.FlatAppearance.BorderColor = Color.DarkGreen;
                        }

                    }
                    fieldcoord[0] = CurrentHero.COORD[0] + direction[0];
                    fieldcoord[1] = CurrentHero.COORD[1] + direction[1];
                    fieldcoord[2] = CurrentHero.COORD[2] + direction[2];

                    foreach (var n in Hex)
                    {
                        string coord3 = n.Name;
                        coord3 = coord3.Replace('m', '-');
                        int[] fieldcoord3 = Coordinata(coord3);

                        string f2 = "b" + fieldcoord3[0].ToString() + fieldcoord3[1].ToString() + fieldcoord3[2].ToString();
                        f2 = f2.Replace('-', 'm');

                        if (Dist(CurrentHero.COORD, fieldcoord3) == 1 && Dist(fieldcoord, fieldcoord3) == 1)
                        {
                            direction = new int[3] { fieldcoord3[0] - CurrentHero.COORD[0], fieldcoord3[1] - CurrentHero.COORD[1], fieldcoord3[2] - CurrentHero.COORD[2] };

                            fieldcoord3[0] = CurrentHero.COORD[0];
                            fieldcoord3[1] = CurrentHero.COORD[1];
                            fieldcoord3[2] = CurrentHero.COORD[2];
                            for (int i = 0; i < 2; i++)
                            {
                                fieldcoord3[0] += direction[0];
                                fieldcoord3[1] += direction[1];
                                fieldcoord3[2] += direction[2];

                                string name = "b";
                                name += fieldcoord3[0].ToString();
                                name += fieldcoord3[1].ToString();
                                name += fieldcoord3[2].ToString();
                                name = name.Replace("-", "m");

                                Button b = Hex.Find(x => x.Name == name);
                                if (b != null)
                                {
                                    b.FlatAppearance.BorderSize = 5;
                                    b.FlatAppearance.BorderColor = Color.DarkGreen;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void LoadInfo()
        {
            Hero hero = heroes[0];
            PictureBox pb = new PictureBox();
            pb.Size = new Size(50, 50);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Passive");
            panelHover1.Controls.Clear();
            if (hero.ABILITIES[0] != "")
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

            double hp = (Convert.ToDouble(hero.HP) / Convert.ToDouble(hero.MAXHP)) * 300;
            pictureBox2.Size = new Size(Convert.ToInt32(hp), 32);
            labelHoverHP.Text = Convert.ToString(hero.HP);

            if (hero.RANGE == 1)
                pictureBoxAttack.Image = (Image)Properties.Resources.ResourceManager.GetObject("Attack");
            else
                pictureBoxAttack.Image = (Image)Properties.Resources.ResourceManager.GetObject("AttackRange");

            pictureBoxArmor.Image = (Image)Properties.Resources.ResourceManager.GetObject("Armor");

            if (hero.TEAM == "B")
                pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject("HealthbarB");
            else
                pictureBox2.Image = (Image)Properties.Resources.ResourceManager.GetObject("HealthbarR");

            string na = hero.NAME;
            na = na.Replace(' ', '_');
            pictureBoxTarget.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
            pictureBoxTarget.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + hero.TEAM);

            int dmg1 = hero.DMG + hero.STATUS.DMGPASSIVE + hero.STATUS.DMG - hero.RAZBROS;
            int dmg2 = hero.DMG + hero.STATUS.DMGPASSIVE + hero.STATUS.DMG + hero.RAZBROS;
            labelDmgHover.Text = Convert.ToString(dmg1 + " - " + dmg2 + " ур.");
            int ex = 0;
            if (hero.STATUS.ARMORED == true || hero.STATUS.ARMORED2 == true)
                ex = 3;
            labelArmorHover.Text = Convert.ToString(hero.ARMOR + hero.STATUS.ARMORPASSIVE + hero.STATUS.ARMOR + ex);


            if (hero.NOWKD[0] > 0)
                label3.Text = hero.NOWKD[0].ToString() + "⧖";
            else
                label3.Text = "";

            if (hero.NOWKD[1] > 0)
                label4.Text = hero.NOWKD[1].ToString() + "⧖";
            else
                label4.Text = "";

            if (hero.NOWKD[2] > 0)
                label5.Text = hero.NOWKD[2].ToString() + "⧖";
            else
                label5.Text = "";
        }
        private void SandsOfTime()
        {
            if (CurrentHero.NAME == "Temporarium" && TemporariumPassive == true && CurrentHero.HP > 0)
            {
                if (CurrentHero.NOWKD[0] > 0)
                    buttonSOT1.Visible = true;
                else
                    buttonSOT1.Visible = false;

                if (CurrentHero.NOWKD[1] > 0)
                    buttonSOT2.Visible = true;
                else
                    buttonSOT2.Visible = false;

                if (CurrentHero.NOWKD[2] > 0)
                    buttonSOT3.Visible = true;
                else
                    buttonSOT3.Visible = false;
            }
            else
                buttonSOT1.Visible = buttonSOT2.Visible = buttonSOT3.Visible = false;
        }
        private void SandsOfTimeClick(object sender, EventArgs e)
        {
            string number = (sender as Button).Name;

            if (number.Contains("1") == true)
                CurrentHero.NOWKD[0] -= 1;
            else if (number.Contains("2") == true)
                CurrentHero.NOWKD[1] -= 1;
            else if (number.Contains("3") == true)
                CurrentHero.NOWKD[2] -= 1;

            buttonSOT1.Visible = buttonSOT2.Visible = buttonSOT3.Visible = false;
            RefreshAP();
        }
        private void InitiativePanel()
        {
            heroes = heroes.OrderBy(x => x.INITIATIV).ToList();
            int i = 0;
            foreach (var n in heroes)
            {
                Button pb = new Button();
                pb.FlatStyle = FlatStyle.Flat;
                string na = n.NAME;
                na = na.Replace(' ', '_');
                pb.Image = (Image)Properties.Resources.ResourceManager.GetObject(na + "2");
                pb.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                //pb.SizeMode = PictureBoxSizeMode.CenterImage;
                pb.Size = new Size(90, 90);
                initiativePanel.Controls.Add(pb);
                inintPB.Add(pb);
                pb.Location = new Point(i * 91, 0);
                i++;
            }
            PictureBox pb2 = new PictureBox();
            pb2.Image = (Image)Properties.Resources.ResourceManager.GetObject("Init");
            pb2.SizeMode = PictureBoxSizeMode.StretchImage;
            pb2.Size = new Size(547, 25);
            initiativePanel.Controls.Add(pb2);
            pb2.Location = new Point(0, 91);
        }
        private void InitView()
        {
            initView++;
            if (initView >= 6)
                initView = 0;
            foreach (var n in inintPB)
            {
                n.FlatAppearance.BorderSize = 0;
            }
            inintPB[initView].FlatAppearance.BorderSize = 3;
        }
        private void ModeView()
        {
            label1.Text = Mode;
            label1.Location = new Point(305 - label1.Width / 2, 964);
        }
        private void CheckPassive()
        {
            if (CurrentHero.ABILITIES[0] == "Подготовка" && (Mode == "Атака" || Mode == "Пригвоздить" || Mode == "Шиловидный болт") && CurrentHero.HODM > 0 && CrossbowmanPassive[0] == false)
            {
                DialogResult dr = MessageBox.Show("Использовать способность подготовка, чтобы нанести дополнительный урон?", "Использовать дополнительное действие?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    CrossbowmanPassive = new bool[2] { false, true };
                    CurrentHero.STATUS.DMGPASSIVE = 8;
                    CurrentHero.HODM -= 1;
                }
                else
                {
                    CurrentHero.STATUS.DMGPASSIVE = 0;
                    CrossbowmanPassive[1] = false;
                    CrossbowmanPassive[0] = true;
                }
            }
            /*else if (CurrentHero.ABILITIES[0] == "Подготовка")
            {
                CrossbowmanPassive = new bool[2] { false, false };
                CurrentHero.STATUS.DMGPASSIVE = 0;
            }*/

            if (CurrentHero.ABILITIES[0] == "Ярость")
            {
                int i = 0;
                int[] pos = new int[3];
                foreach (var n in directions)
                {
                    Hero h = new Hero();
                    pos[0] = CurrentHero.COORD[0] + n[0];
                    pos[1] = CurrentHero.COORD[1] + n[1];
                    pos[2] = CurrentHero.COORD[2] + n[2];

                    if (heroes.Find(x => x.COORD[0] == pos[0] && x.COORD[1] == pos[1] && x.COORD[2] == pos[2] && x.TEAM != CurrentHero.TEAM) != null)
                        i++;
                }
                if (i > 1)
                    CurrentHero.STATUS.DMGPASSIVE = 8;
                else
                    CurrentHero.STATUS.DMGPASSIVE = 0;

            }
            if (CurrentHero.ABILITIES[0] == "Электроразряд")
            {
                CurrentHero.STATUS.ARMORPIERCING = true;
            }
            if (CurrentHero.ABILITIES[0] == "Плотоядство" && ButcherPassive == true)
            {
                int health = 60;
                CurrentHero.HP += 60;
                if (CurrentHero.HP > CurrentHero.MAXHP)
                {
                    health = CurrentHero.MAXHP - CurrentHero.HP + 60;
                    CurrentHero.HP = CurrentHero.MAXHP;
                }
                HPplus(CurrentHero, health);
                listBox1.Items.Add(CurrentHero.NAME + " восстанавливает " + health.ToString() + " хп");
                ButcherPassive = false;
                RefreshHP();
            }
            if (WitchdoctorPassive == false)
            {
                WitchdoctorPassive = true;
                foreach (var n in heroes)
                {
                    if (n.NAME == "Witch doctor")
                    {
                        foreach (var m in heroes)
                        {
                            if (m.TEAM == n.TEAM)
                            {
                                m.MAXHP += 30;
                                m.HP = m.MAXHP;
                            }
                        }
                    }
                }
            }
            if (CurrentHero.ABILITIES[0] == "Инь-Янь" && AramushaPassive == "")
            {
                InYan InYa = new InYan();
                InYa.ShowDialog();

                if (AramushaPassive == "Инь")
                {
                    CurrentHero.STATUS.DMGPASSIVE = 6;
                    CurrentHero.STATUS.ARMORPASSIVE = 0;
                }
                else if (AramushaPassive == "Янь")
                {
                    CurrentHero.STATUS.DMGPASSIVE = 0;
                    CurrentHero.STATUS.ARMORPASSIVE = 3;
                    CurrentHero.HP += 8;
                    HPplus(CurrentHero, 8);
                    if (CurrentHero.HP > CurrentHero.MAXHP)
                        CurrentHero.HP = CurrentHero.MAXHP;
                }
                else
                {
                    CurrentHero.STATUS.DMGPASSIVE = CurrentHero.STATUS.ARMORPASSIVE = 0;
                }
            }
            if (GeomantPassive == true)
            {
                Hero h = heroes.Find(x => x.NAME == "Geomant");
                int i = 0;

                if (h.HP > 0)
                {
                    foreach (var n in heroes)
                    {
                        if (n.HP > 0 && n.NAME == "Stoloktit" && Dist(n.COORD, h.COORD) <= 2 && n.TEAM == h.TEAM)
                            i++;
                    }
                    h.STATUS.DMGPASSIVE = 4 * i;
                    h.STATUS.ARMORPASSIVE = 3 * i;
                }
            }
            if (CurrentHero.ABILITIES[0] == "Свободные руки")
            {
                int i = 0;
                int[] pos = new int[3];
                foreach (var n in directions)
                {
                    Hero h = new Hero();
                    pos[0] = CurrentHero.COORD[0] + n[0];
                    pos[1] = CurrentHero.COORD[1] + n[1];
                    pos[2] = CurrentHero.COORD[2] + n[2];

                    if (heroes.Find(x => x.COORD[0] == pos[0] && x.COORD[1] == pos[1] && x.COORD[2] == pos[2] && x.TEAM != CurrentHero.TEAM) != null)
                        i++;
                }
                if (i < 1)
                    CurrentHero.STATUS.DMGPASSIVE = 7;
                else
                    CurrentHero.STATUS.DMGPASSIVE = 0;
            }
            if (CurrentHero.ABILITIES[0] == "Дух земли")
            {
                int i = 0;
                int[] pos = new int[3];
                foreach (var n in directions)
                {
                    Hero h = new Hero();
                    pos[0] = CurrentHero.COORD[0] + n[0];
                    pos[1] = CurrentHero.COORD[1] + n[1];
                    pos[2] = CurrentHero.COORD[2] + n[2];

                    string m = Convert.ToString("b" + pos[0] + pos[1] + pos[2]);
                    m = m.Replace('-', 'm');
                    Button b = Hex.Find(y => y.Name == m);

                    if (heroes.Find(x => x.COORD[0] == pos[0] && x.COORD[1] == pos[1] && x.COORD[2] == pos[2]) == null && b != null)
                        i++;
                }
                CurrentHero.STATUS.DMGPASSIVE = i * 3;

            }

            GolemPassive();
        }
        private void GuardianPassive(Hero h)
        {
            if (CurrentHero.ABILITIES[0] == "Размашистый удар" && Mode == "Атака" && GuradPassive == true)
            {
                GuradPassive = false;
                foreach (var n in heroes)
                {
                    Hero pop = guardianPassive.Find(x => x.INITIATIV == n.INITIATIV);

                    if (pop == null)
                    {
                        if (n.NAME != h.NAME && Dist(n.COORD, h.COORD) == 1 && Dist(n.COORD, CurrentHero.COORD) == 1 && n.TEAM != CurrentHero.TEAM)
                            guardianPassive.Add(n);
                    }

                }
                foreach (var m in guardianPassive)
                {
                    CurrentHero.STATUS.DMGPASSIVE -= (CurrentHero.DMG + CurrentHero.STATUS.DMG) / 2;
                    CurrentHero.HODB += 1;
                    Attack(CurrentHero.COORD, m.COORD);
                    Hero hhh = heroes.Find(x => x.INITIATIV == m.INITIATIV);
                    hhh = m;
                    CurrentHero.STATUS.DMGPASSIVE = 0;
                }
                guardianPassive.Clear();
            }
        }
        private void GolemPassive()
        {
            Hero h = heroes.Find(x => x.NAME == "Golem");
            if (h != null)
            {
                int i = 0;
                int[] pos = new int[3];
                foreach (var n in directions)
                {
                    Hero hero = new Hero();
                    pos[0] = CurrentHero.COORD[0] + n[0];
                    pos[1] = CurrentHero.COORD[1] + n[1];
                    pos[2] = CurrentHero.COORD[2] + n[2];

                    string m = Convert.ToString("b" + pos[0] + pos[1] + pos[2]);
                    m = m.Replace('-', 'm');
                    Button b = Hex.Find(y => y.Name == m);

                    if (heroes.Find(x => x.COORD[0] == pos[0] && x.COORD[1] == pos[1] && x.COORD[2] == pos[2]) == null && b != null)
                        i++;
                }
                h.STATUS.ARMORPASSIVE = i;
            }
        }
        private void CheckWin()
        {
            int blue = 0;
            int red = 0;

            foreach (var n in heroes)
            {
                if (n.HP > 0 && n.TEAM == "B" && n.NAME != "Stoloktit" && n.NAME != "Illusion" && n.NAME != "Zombie")
                    blue++;
                else if (n.HP > 0 && n.TEAM == "R" && n.NAME != "Stoloktit" && n.NAME != "Illusion" && n.NAME != "Zombie")
                    red++;
            }

            if (blue < 1 && turn != 0 && CurrentHero.HP > 0 && !bugWin)
            {
                bugWin = true;
                for (int i = 0; i < 100; i++)
                {
                    Player.Volume = Convert.ToDouble(volume) / 10 - (Convert.ToDouble(i) * (Convert.ToDouble(volume) / 1000));
                    Thread.Sleep(10);
                }
                Player.Stop();
                Player.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "RedWin.wav")));
                Player.Volume = Convert.ToDouble(volume) / 10;
                Player.Play();

                WriteWinRate("R");
                pictureBoxVictory.Visible = true;
                pictureBoxVictory.Image = (Image)Properties.Resources.ResourceManager.GetObject("RedVictory");
                pictureBoxVictory.Size = new Size(1200, 750);
                pictureBoxVictory.Location = new Point(360, 120);
                pictureBoxVictory.BringToFront();
                pictureBoxVictory.Show();
                //MessageBox.Show("КРАСНАЯ КОМАНДА ПОБЕДИЛА!!!");
            }
            if (red < 1 && turn != 0 && CurrentHero.HP > 0 && !bugWin)
            {
                bugWin = true;
                for (int i = 0; i < 100; i++)
                {
                    Player.Volume = Convert.ToDouble(volume) / 10 - (Convert.ToDouble(i) * (Convert.ToDouble(volume) / 1000));
                    Thread.Sleep(10);
                }
                Player.Stop();
                Player.Open(new Uri(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "BlueWin.wav")));
                Player.Volume = Convert.ToDouble(volume) / 10;
                Player.Play();

                WriteWinRate("B");
                pictureBoxVictory.Visible = true;
                pictureBoxVictory.Image = (Image)Properties.Resources.ResourceManager.GetObject("BlueVictory");
                pictureBoxVictory.Size = new Size(1200, 750);
                pictureBoxVictory.Location = new Point(360, 120);
                pictureBoxVictory.BringToFront();
                pictureBoxVictory.Show();
                //MessageBox.Show("СИНЯЯ КОМАНДА ПОБЕДИЛА!!!");
            }
        }
        private void Move_Click(object sender, EventArgs e)
        {
            string coord = (sender as Button).Name;
            coord = coord.Replace('m', '-');
            int[] fieldcoord = Coordinata(coord);

            if (Mode == "Движение" && (CurrentHero.HODM > 0 || CurrentHero.HODB > 0))
            {
                Step(CurrentHero.COORD, fieldcoord);
            }
            else if (Mode == "Атака" && CurrentHero.HODB > 0)
            {
                CheckPassive();
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= CurrentHero.RANGE)
                    Attack(CurrentHero.COORD, fieldcoord);
                //CheckPassive();
            }

            //Crossbowman +
            else if (Mode == "Пригвоздить" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                        if (hAttacked.STATUS.MAGICSHIELD[0] != 1)
                            hAttacked.STATUS.ROOTS = true;
                        Attack(CurrentHero.COORD, fieldcoord);
                        listBox1.Items.Add(hAttacked.NAME + " не может ходить!");
                        StatusView();
                        PlaySound("Crossbowman_gvozd.wav");
                    }
                }
            }//+++
            else if (Mode == "Шиловидный болт" && CurrentHero.HODB > 0)
            {
                if ((CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2]) && Dist(CurrentHero.COORD, fieldcoord) > 0)
                {
                    CheckPassive();
                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                    else
                        CurrentHero.NOWKD[1] = CurrentHero.KD[1];
                    int[] direction = new int[3] { fieldcoord[0] - CurrentHero.COORD[0], fieldcoord[1] - CurrentHero.COORD[1], fieldcoord[2] - CurrentHero.COORD[2] };
                    for (int i = 0; i < 3; i++)
                    {
                        if (Math.Abs(direction[i]) > 1)
                            direction[i] = direction[i] / Math.Abs(direction[i]);
                    }
                    CurrentHero.STATUS.DMG -= 10;
                    CurrentHero.STATUS.ARMORPIERCING = true;
                    fieldcoord[0] = CurrentHero.COORD[0];
                    fieldcoord[1] = CurrentHero.COORD[1];
                    fieldcoord[2] = CurrentHero.COORD[2];
                    for (int i = 0; i < 6; i++)
                    {
                        fieldcoord[0] += direction[0];
                        fieldcoord[1] += direction[1];
                        fieldcoord[2] += direction[2];

                        Thread.Sleep(7);

                        Attack(CurrentHero.COORD, fieldcoord);
                    }
                    if (CrossbowmanPassive[0] == true)
                    {
                        CrossbowmanPassive = new bool[2] { false, false };
                        CurrentHero.STATUS.DMGPASSIVE = 0;
                    }
                    CurrentHero.STATUS.DMG += 10;
                    CurrentHero.STATUS.ARMORPIERCING = false;
                    RefreshAP();
                    PlaySound("Crossbowman_shilo.wav");
                }
            }//+++
            else if (Mode == "Метка снайпера" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM && hAttacked.STATUS.MAGICSHIELD[0] != 1)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                        else
                            CurrentHero.NOWKD[2] = CurrentHero.KD[2];
                        hAttacked.STATUS.METKA = new int[2] { 1, heroes.Count() + 1 };
                        listBox1.Items.Add(hAttacked.NAME + " теперь отмечен!");
                        StatusView();
                        if (CurrentHero.HODM > 0)
                            CurrentHero.HODM -= 1;
                        else
                            CurrentHero.HODB -= 1;
                        RefreshAP();
                        PlaySound("Crossbowman_metka.wav");
                    }
                }
            }//+++

            //Bersrker +
            else if (Mode == "Вихрь топоров" && CurrentHero.HODB > 0)
            {
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                else
                    CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                CheckPassive();
                int hB = CurrentHero.HODB;
                foreach (var n in directions)
                {
                    fieldcoord[0] = n[0] + CurrentHero.COORD[0];
                    fieldcoord[1] = n[1] + CurrentHero.COORD[1];
                    fieldcoord[2] = n[2] + CurrentHero.COORD[2];

                    Thread.Sleep(7);
                    Attack(CurrentHero.COORD, fieldcoord);
                }
                PlaySound("Berserker_krug.wav");
                CurrentHero.HODB = hB - 1;
                RefreshAP();
            }//+++
            else if (Mode == "Наскок" && CurrentHero.HODB > 0 && CurrentHero.STATUS.ROOTS == false)
            {
                Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                if ((CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2]) && Dist(CurrentHero.COORD, fieldcoord) <= 2 && hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                {

                    int[] direction = new int[3] { fieldcoord[0] - CurrentHero.COORD[0], fieldcoord[1] - CurrentHero.COORD[1], fieldcoord[2] - CurrentHero.COORD[2] };
                    for (int i = 0; i < 3; i++)
                    {
                        if (Math.Abs(direction[i]) > 1)
                            direction[i] = direction[i] / Math.Abs(direction[i]);
                    }

                    direction[0] += CurrentHero.COORD[0];
                    direction[1] += CurrentHero.COORD[1];
                    direction[2] += CurrentHero.COORD[2];

                    if (direction[0] == fieldcoord[0] && direction[1] == fieldcoord[1] && direction[2] == fieldcoord[2])
                    {
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                        CurrentHero.STATUS.DMG += 12;
                        CheckPassive();
                        Step(CurrentHero.COORD, direction);
                        Attack(CurrentHero.COORD, fieldcoord);
                        CurrentHero.STATUS.DMG -= 12;
                        PlaySound("Berserker_naskok.wav");
                        RefreshAP();
                    }
                    else
                    {
                        Hero h = heroes.Find(x => x.COORD[0] == direction[0] && x.COORD[1] == direction[1] && x.COORD[2] == direction[2]);
                        if (h == null)
                        {
                            if (CurrentHero.NAME == "Metamorph")
                                CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                            else
                                CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                            CurrentHero.STATUS.DMG += 12;
                            CheckPassive();
                            Step(CurrentHero.COORD, direction);
                            Attack(CurrentHero.COORD, fieldcoord);
                            CurrentHero.STATUS.DMG -= 12;
                            PlaySound("Berserker_naskok.wav");

                            RefreshAP();
                        }
                        else
                            return;
                    }

                }
            }//+++
            else if (Mode == "Запугивание" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                else
                    CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                foreach (var n in directions)
                {
                    fieldcoord[0] = n[0] + CurrentHero.COORD[0];
                    fieldcoord[1] = n[1] + CurrentHero.COORD[1];
                    fieldcoord[2] = n[2] + CurrentHero.COORD[2];

                    Hero h = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2] && x.TEAM != CurrentHero.TEAM);
                    if (h != null && h.TEAM != CurrentHero.TEAM && h.STATUS.MAGICSHIELD[0] != 1)
                    {
                        h.STATUS.STUN = true;
                        listBox1.Items.Add(h.NAME + " в ужасе!");
                    }
                }
                PlaySound("Berserker_zapug.wav");
                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;
                RefreshAP();
                StatusView();
            }//+++

            //Aeroturg +
            else if (Mode == "Цепная молния" && CurrentHero.HODB > 0)

            {
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                else
                    CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                List<Hero> here = new List<Hero>();
                List<Hero> ready = new List<Hero>();

                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null)
                    {
                        int hod = CurrentHero.HODB;
                        here.Add(hAttacked);
                        CheckPassive();
                        bool br = false;
                        while (br == false)
                        {
                            ready.Add(hAttacked);
                            foreach (var m in here)
                            {
                                foreach (var n in directions)
                                {
                                    fieldcoord[0] = n[0] + m.COORD[0];
                                    fieldcoord[1] = n[1] + m.COORD[1];
                                    fieldcoord[2] = n[2] + m.COORD[2];

                                    Hero hAttacked2 = heroes.Find(x => x.Target(fieldcoord) == true);
                                    if (hAttacked2 != null && Thunder(ready, hAttacked2.NAME) == true)
                                    {
                                        ready.Add(hAttacked2);
                                    }
                                }
                            }
                            if (here.Count == ready.Count)
                                br = true;
                            here.Clear();
                            foreach (var o in ready)
                                here.Add(o);
                            ready.Clear();
                        }
                        foreach (var n in heroes)
                        {
                            foreach (var m in here)
                            {
                                if (m.TEAM != CurrentHero.TEAM && n.NAME == m.NAME)
                                {
                                    Attack(CurrentHero.COORD, n.COORD);
                                }
                            }

                        }
                        CurrentHero.HODB = hod - 1;
                        RefreshAP();
                        PlaySound("Aeroturg_molniya.wav");
                    }
                }

            }//+++
            else if (Mode == "Рокировка" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (replace == null && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null)
                    {
                        if (hAttacked.STATUS.GEOSHIELD[0] != 1)
                        {
                            replace = hAttacked;
                            ViewAction();
                            return;
                        }
                    }
                }
                else if (Dist(CurrentHero.COORD, fieldcoord) <= CurrentHero.RANGE && replace != null)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.NAME != replace.NAME && hAttacked.STATUS.GEOSHIELD[0] != 1)
                    {
                        int[] c = new int[3] { hAttacked.COORD[0], hAttacked.COORD[1], hAttacked.COORD[2] };
                        Hero hAttacked2 = heroes.Find(x => x.Target(replace.COORD) == true);
                        if (hAttacked2 != null)
                        {
                            CheckPassive();
                            if (CurrentHero.NAME == "Metamorph")
                                CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                            else
                                CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                            hAttacked.COORD = replace.COORD;
                            hAttacked2.COORD = c;

                            if (hAttacked.STATUS.ARMORED == true)
                                hAttacked.STATUS.ARMORED = false;
                            if (hAttacked2.STATUS.ARMORED == true)
                                hAttacked2.STATUS.ARMORED = false;

                            if (CurrentHero.HODM > 0)
                                CurrentHero.HODM -= 1;
                            else
                                CurrentHero.HODB -= 1;
                            replace = null;
                            AngryMove(hAttacked);
                            AngryMove(hAttacked2);

                            if (hAttacked.NAME == "Illusion")
                            {
                                hAttacked.HP = 0;
                                AngryDead(hAttacked);
                                hAttacked.COORD = new int[] { 5, 5, 5 };
                                listBox1.Items.Add("Иллюзия исчезла!");
                                Refresh();
                            }
                            else if (hAttacked2.NAME == "Illusion")
                            {
                                hAttacked2.HP = 0;
                                AngryDead(hAttacked2);
                                hAttacked2.COORD = new int[] { 5, 5, 5 };
                                listBox1.Items.Add("Иллюзия исчезла!");
                                Refresh();
                            }

                            RefreshAP();
                            Refresh();
                            ZoneAbilities();
                            listBox1.Items.Add(hAttacked.NAME + " и " + hAttacked2.NAME + " меняются местами!");
                            PlaySound("Aeroturg_swap.wav");
                        }
                    }
                }
                else
                    replace = null;
            }//+++
            else if (Mode == "Вакуум" && CurrentHero.HODB > 0)
            {
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                else
                    CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                int[] dmg = new int[2] { CurrentHero.DMG, CurrentHero.STATUS.DMG };
                CurrentHero.STATUS.DMG = 0;
                CurrentHero.DMG = 13;
                int raz = CurrentHero.RAZBROS;
                CurrentHero.RAZBROS = 3;
                int hG = CurrentHero.HODB;

                CurrentHero.STATUS.ARMORPIERCING = true;
                foreach (var n in heroes)
                {
                    if (n.TEAM != CurrentHero.TEAM && Dist(CurrentHero.COORD, n.COORD) <= 2)
                    {
                        Attack(CurrentHero.COORD, n.COORD);

                        if (n.STATUS.MAGICSHIELD[0] != 1)
                        {
                            listBox1.Items.Add(n.NAME + " находится под действием немоты!");
                            n.STATUS.SILENCE = true;
                        }
                    }
                }
                CurrentHero.DMG = dmg[0];
                CurrentHero.STATUS.DMG = dmg[1];
                CurrentHero.RAZBROS = raz;
                CurrentHero.HODB = hG - 1;
                CurrentHero.STATUS.ARMORPIERCING = false;

                PlaySound("Aeroturg_vakuum.wav");
                CheckPassive();
                RefreshHP();
                StatusView();
                RefreshAP();
            }//+++

            //Butcher +
            else if (Mode == "Шинковка" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 1)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                        CurrentHero.STATUS.DMG -= 12;
                        Attack(CurrentHero.COORD, fieldcoord);
                        Thread.Sleep(7);
                        Attack(CurrentHero.COORD, fieldcoord);
                        Thread.Sleep(7);
                        Attack(CurrentHero.COORD, fieldcoord);
                        CurrentHero.HODB += 2;
                        CurrentHero.STATUS.DMG += 12;
                        CheckPassive();
                        PlaySound("Butcher_shink.wav");
                    }
                }
            }//+++
            else if (Mode == "Крюк" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2] && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        int f = CurrentHero.HODM;
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];
                        CurrentHero.HODB += 1;
                        int[] direction = new int[3] { CurrentHero.COORD[0] - fieldcoord[0], CurrentHero.COORD[1] - fieldcoord[1], CurrentHero.COORD[2] - fieldcoord[2] };
                        for (int i = 0; i < 3; i++)
                        {
                            if (Math.Abs(direction[i]) > 1)
                                direction[i] = direction[i] / Math.Abs(direction[i]);
                        }

                        fieldcoord[0] += direction[0];
                        fieldcoord[1] += direction[1];
                        fieldcoord[2] += direction[2];

                        Step(hAttacked.COORD, fieldcoord);
                        CurrentHero.RAZBROS -= 7;
                        CurrentHero.STATUS.DMG -= 10;
                        Attack(CurrentHero.COORD, hAttacked.COORD);
                        CheckPassive();
                        CurrentHero.RAZBROS += 7;
                        CurrentHero.STATUS.DMG += 10;
                        PlaySound("Butcher_hook.wav");
                        if (f == CurrentHero.HODM)
                            CurrentHero.HODM = f - 1;
                    }

                    RefreshAP();
                }
            }//+++
            else if (Mode == "Казнь" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 1)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        if ((Convert.ToDouble(hAttacked.MAXHP) / 100 * 20) >= hAttacked.HP)
                        {
                            CheckPassive();
                            if (CurrentHero.NAME == "Metamorph")
                                CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                            else
                                CurrentHero.NOWKD[2] = CurrentHero.KD[2];
                            hAttacked.HP = 0;
                            CurrentHero.HODB -= 1;
                            if (hAttacked.HP < 1 && (hAttacked.NAME != "Angel" || AngelPassive == true))
                            {
                                Hero m = heroes.Find(x => x.NAME == hAttacked.NAME);
                                m.COORD = new int[] { 5, 5, 5 };
                                //heroes.RemoveAll(x => x.NAME == hAttacked.NAME);
                                listBox1.Items.Add(m.NAME + " казнён!!! ");
                                Refresh();
                                if (CurrentHero.NAME == "Butcher")
                                    ButcherPassive = true;
                            }
                            else
                            {
                                AngelPassivka();
                            }
                            CheckPassive();
                            RefreshHP();
                        }
                        else
                        {
                            CurrentHero.NOWKD[2] = CurrentHero.KD[2];
                            CurrentHero.STATUS.DMG += 12;
                            Attack(CurrentHero.COORD, fieldcoord);
                            CurrentHero.STATUS.DMG -= 12;
                            CheckPassive();
                        }
                        RefreshAP();
                        PlaySound("Butcher_kazn.wav");
                    }
                }
            }//+++

            //Witch doctor +
            else if (Mode == "Исцеление" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                        Random rnd = new Random();
                        int heal = rnd.Next(40 - CurrentHero.RAZBROS, 41 + CurrentHero.RAZBROS);
                        hAttacked.HP += heal;
                        HPplus(hAttacked, heal);
                        if (hAttacked.HP > hAttacked.MAXHP)
                            hAttacked.HP = hAttacked.MAXHP;
                        CurrentHero.HODB -= 1;
                        listBox1.Items.Add(hAttacked.NAME + " восстанавливает " + heal + " хп");
                        RefreshAP();
                        PlaySound("Witch_hil.wav");
                    }
                }
            }//+++
            else if (Mode == "Протекторат" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                else
                    CurrentHero.NOWKD[1] = CurrentHero.KD[1];
                CurrentHero.STATUS.PROTECT = new int[2] { 1, heroes.Count + 1 };

                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;
                RefreshAP();
                ZoneAbilities();
                PlaySound("Witch_def.wav");
            }//+++
            else if (Mode == "Очищение" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                foreach (var n in heroes)
                {
                    if (Dist(CurrentHero.COORD, n.COORD) <= 2)
                    {
                        if (CurrentHero.TEAM == n.TEAM)
                        {
                            ClearEffects(n, 0);
                            ClearDebaff(n.INITIATIV);
                        }
                        else
                        {
                            ClearEffects(n, 1);
                            n.STATUS.ARMORED = false;
                            n.STATUS.POWER = false;
                            if (n.STATUS.BLESS[0] == 1)
                            {
                                n.STATUS.DMG -= n.STATUS.BLESS[2];
                                n.STATUS.ARMOR -= n.STATUS.BLESS[3];
                                n.STATUS.BLESS = new int[] { 0, 0, 0, 0 };
                            }
                            n.STATUS.DEATHBREATH = new int[] { 0, 0 };
                            if (n.STATUS.GEOSHIELD[0] == 1)
                            {
                                n.STATUS.ARMOR -= 7;
                                n.STATUS.GEOSHIELD = new int[] { 0, 0 };
                            }
                            n.STATUS.PROTECT = new int[] { 0, 0 };
                            n.STATUS.REGENERATION = false;
                            if (n.STATUS.ENCOURAGE[0] == 1)
                            {
                                n.STATUS.DMG -= n.STATUS.ENCOURAGE[1];
                                n.STATUS.ENCOURAGE = new int[] { 0, 0, 0 };
                            }
                            n.STATUS.BOOST = false;
                            n.STATUS.MAGICSHIELD = new int[] { 0, 0 };
                            n.STATUS.STONEBLOOD = new int[] { 0, 0 };
                            n.STATUS.CRYOSTASYS = new int[] { 0, 0 };
                            n.STATUS.INJECT = new int[] { 0, 0 };
                            n.STATUS.UNFLESH = new int[] { 0, 0 };
                        }
                    }
                }
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                else
                    CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;
                RefreshAP();
                StatusView();
                ZoneAbilities();
                PlaySound("Witch_clear.wav");
            }//+++

            //Aramusha +
            else if (Mode == "Двойное сечение" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 1)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                        CurrentHero.STATUS.DMG -= 8;
                        Attack(CurrentHero.COORD, fieldcoord);
                        Thread.Sleep(7);
                        Attack(CurrentHero.COORD, fieldcoord);
                        CurrentHero.HODB += 1;
                        CurrentHero.STATUS.DMG += 8;
                        CheckPassive();
                        RefreshAP();
                        PlaySound("Aramusha_sechka.wav");
                    }
                }
            }//+++
            else if (Mode == "Пинок" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2] && Dist(CurrentHero.COORD, fieldcoord) <= 1)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        int f = CurrentHero.HODM;
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];
                        CurrentHero.HODB += 1;
                        int[] direction = new int[3] { fieldcoord[0] - CurrentHero.COORD[0], fieldcoord[1] - CurrentHero.COORD[1], fieldcoord[2] - CurrentHero.COORD[2] };
                        for (int i = 0; i < 3; i++)
                        {
                            if (Math.Abs(direction[i]) > 1)
                                direction[i] = direction[i] / Math.Abs(direction[i]);
                        }

                        fieldcoord[0] += direction[0];
                        fieldcoord[1] += direction[1];
                        fieldcoord[2] += direction[2];

                        Step(hAttacked.COORD, fieldcoord);
                        CurrentHero.STATUS.DMG -= 12;
                        CurrentHero.STATUS.ARMORPIERCING = true;
                        if (Dist(hAttacked.COORD, fieldcoord) != 0 && hAttacked.STATUS.MAGICSHIELD[0] != 1)
                            hAttacked.STATUS.STUN = true;

                        if (AramushaPassive == "Инь")
                            CurrentHero.STATUS.DMG -= 6;
                        Attack(CurrentHero.COORD, hAttacked.COORD);
                        CheckPassive();
                        CurrentHero.STATUS.ARMORPIERCING = false;
                        CurrentHero.STATUS.DMG += 12;
                        if (AramushaPassive == "Инь")
                            CurrentHero.STATUS.DMG += 6;
                        if (f == CurrentHero.HODM)
                            CurrentHero.HODM = f - 1;
                    }
                    StatusView();
                    RefreshAP();
                    PlaySound("Aramusha_pinok.wav");
                }
            }//+++
            else if (Mode == "Возмездие" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                else
                    CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                int bonus = Convert.ToInt32(Convert.ToDouble((CurrentHero.MAXHP - CurrentHero.HP) / 15 * 2));

                CurrentHero.STATUS.DMG += bonus;
                CurrentHero.STATUS.REVENGE = true;

                listBox1.Items.Add(CurrentHero.NAME + " получает +" + bonus + " к урону");

                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;
                RefreshAP();
                StatusView();
                PlaySound("Aramusha_vozm.wav");
            }//+++

            //Knight +
            else if (Mode == "Оборона" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                else
                    CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                CurrentHero.STATUS.ARMORED = true;

                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;
                RefreshAP();
                ZoneAbilities();
                PlaySound("Knight_defence.wav");
            }//+++
            else if (Mode == "Клятва верности" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                        hAttacked.STATUS.POWER = true;
                        listBox1.Items.Add(hAttacked.NAME + " теперь наносит на 30% больше урона!");
                        if (CurrentHero.HODM > 0)
                            CurrentHero.HODM -= 1;
                        else
                            CurrentHero.HODB -= 1;
                        RefreshAP();
                        StatusView();
                        PlaySound("Knight_baff.wav");
                    }
                }
            }//+++
            else if (Mode == "Провокация" && CurrentHero.HODB > 0)
            {
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                else
                    CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                foreach (var n in directions)
                {
                    fieldcoord[0] = n[0] + CurrentHero.COORD[0];
                    fieldcoord[1] = n[1] + CurrentHero.COORD[1];
                    fieldcoord[2] = n[2] + CurrentHero.COORD[2];

                    Hero h = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
                    if (h != null)
                    {
                        if (h.TEAM != CurrentHero.TEAM && h.STATUS.MAGICSHIELD[0] != 1)
                            h.STATUS.ANGRY = true;
                    }
                }
                CurrentHero.HODB -= 1;
                CurrentHero.STATUS.ROOTS = true;
                CurrentHero.STATUS.ANGRY1 = new int[] { 1, heroes.Count };
                RefreshAP();
                StatusView();
                PlaySound("Knight_taunt.wav");

            }//+++

            //Priest +
            else if (Mode == "Благословение" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                        Random rnd = new Random();
                        int extraarmor = (hAttacked.MAXHP - hAttacked.HP) / 15;
                        int extradmg = (hAttacked.MAXHP - hAttacked.HP) / 12;

                        hAttacked.STATUS.BLESS = new int[4] { 1, heroes.Count + 1, extradmg, extraarmor };
                        hAttacked.STATUS.DMG += extradmg;
                        //hAttacked.STATUS.ARMOR += extraarmor;

                        if (CurrentHero.HODM > 0)
                            CurrentHero.HODM -= 1;
                        else
                            CurrentHero.HODB -= 1;
                        listBox1.Items.Add(hAttacked.NAME + " получает +" + extradmg + " к урону");
                        listBox1.Items.Add(hAttacked.NAME + " получает +" + extraarmor + " к броне");
                        StatusView();
                        RefreshAP();
                        PlaySound("Priest_bless.wav");
                    }
                }
            }//+++
            else if (Mode == "Кара Божья" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];
                        CurrentHero.STATUS.ARMORPIERCING = true;
                        CurrentHero.STATUS.DMG -= (CurrentHero.DMG - 10);
                        for (int i = 0; i < 3; i++)
                        {
                            if (hAttacked.NOWKD[i] > 0)
                                CurrentHero.STATUS.DMGPASSIVE += 12;
                        }

                        Attack(CurrentHero.COORD, fieldcoord);
                        CurrentHero.STATUS.DMGPASSIVE = 0;
                        CurrentHero.STATUS.DMG += (CurrentHero.DMG - 10);
                        CurrentHero.STATUS.ARMORPIERCING = false;
                        CheckPassive();
                        StatusView();
                        PlaySound("Priest_kara.wav");
                    }
                }
            }//+++
            else if (Mode == "Милость Господа" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                        else
                            CurrentHero.NOWKD[2] = CurrentHero.KD[2];
                        Random rnd = new Random();
                        int extraheal = (hAttacked.MAXHP - hAttacked.HP) / 8;

                        int heal = rnd.Next(10 + extraheal - CurrentHero.RAZBROS, 10 + extraheal + CurrentHero.RAZBROS);
                        hAttacked.HP += heal;
                        HPplus(hAttacked, heal);
                        ClearDebaff(hAttacked.INITIATIV);

                        if (hAttacked.HP > hAttacked.MAXHP)
                            hAttacked.HP = hAttacked.MAXHP;
                        CurrentHero.HODB -= 1;
                        listBox1.Items.Add(hAttacked.NAME + " восстанавливает " + heal + " хп");
                        listBox1.Items.Add(hAttacked.NAME + " снимает с себя негативные эффекты");
                        StatusView();
                        RefreshAP();
                        PlaySound("Priest_hil.wav");
                    }
                }
            }//+++

            //Geomant +
            else if (Mode == "Столоктит" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                Hero h = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
                if (h == null && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    int i = 100;
                    foreach (var n in heroes)
                    {
                        if (n.NAME == "Stoloktit" && n.HP <= 0 && n.TEAM == CurrentHero.TEAM)
                        {
                            i = n.INITIATIV;
                            break;
                        }
                    }
                    if (i <= heroes.Count)
                    {
                        Hero st = heroes.Find(x => x.INITIATIV == i);
                        st.HP = st.MAXHP;
                        st.COORD = fieldcoord;

                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                        CurrentHero.STATUS.DMGPASSIVE = 0;
                        CurrentHero.STATUS.DMG -= 12;

                        foreach (var n in heroes)
                        {
                            if (Dist(fieldcoord, n.COORD) <= 1 && n.TEAM != CurrentHero.TEAM)
                            {
                                Attack(fieldcoord, n.COORD);
                                CurrentHero.HODB += 1;
                            }
                        }

                        CurrentHero.STATUS.DMG += 12;
                        if (CurrentHero.HODM > 0)
                            CurrentHero.HODM -= 1;
                        else
                            CurrentHero.HODB -= 1;

                        CheckPassive();
                        RefreshAP();
                        RefreshHP();
                        Refresh();
                        PlaySound("Geomant_stoloktit.wav");
                    }
                }
            }//+++
            else if (Mode == "Каменный кулак" && CurrentHero.HODB > 0)
            {
                if (CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2] && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        int f = CurrentHero.HODM;
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];
                        int[] direction = new int[3] { fieldcoord[0] - CurrentHero.COORD[0], fieldcoord[1] - CurrentHero.COORD[1], fieldcoord[2] - CurrentHero.COORD[2] };
                        for (int i = 0; i < 3; i++)
                        {
                            if (Math.Abs(direction[i]) > 1)
                                direction[i] = direction[i] / Math.Abs(direction[i]);
                        }

                        fieldcoord[0] += direction[0];
                        fieldcoord[1] += direction[1];
                        fieldcoord[2] += direction[2];

                        Step(hAttacked.COORD, fieldcoord);
                        CurrentHero.STATUS.DMG += 8;

                        if (Dist(hAttacked.COORD, fieldcoord) != 0)
                            CurrentHero.STATUS.DMGPASSIVE += 12;

                        Attack(CurrentHero.COORD, hAttacked.COORD);
                        CheckPassive();

                        CurrentHero.STATUS.DMG -= 8;

                        if (f != CurrentHero.HODM)
                            CurrentHero.HODM = f;
                    }
                    StatusView();
                    RefreshAP();
                    PlaySound("Geomant_kulak.wav");

                }
            }//+++
            else if (Mode == "Геопанцирь" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                        else
                            CurrentHero.NOWKD[2] = CurrentHero.KD[2];
                        hAttacked.STATUS.GEOSHIELD = new int[2] { 1, heroes.Count + 1 };
                        hAttacked.STATUS.ARMOR += 7;

                        if (CurrentHero.HODM > 0)
                            CurrentHero.HODM -= 1;
                        else
                            CurrentHero.HODB -= 1;

                        listBox1.Items.Add(hAttacked.NAME + " получает +7 брони за счет геопанциря!");
                        StatusView();
                        RefreshAP();
                        PlaySound("Geomant_panz.wav");
                    }
                }
            }//+++

            //Necromancer +
            else if (Mode == "Иссушение" && ((CurrentHero.HODB > 0 && CurrentHero.HODM > 0) || CurrentHero.HODB > 1))
            {
                Hero h = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
                if (h != null && Dist(CurrentHero.COORD, fieldcoord) <= 1 && h.TEAM != CurrentHero.TEAM)
                {
                    CheckPassive();
                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                    else
                        CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                    if (h.STATUS.MAGICSHIELD[0] != 1)
                        h.STATUS.SOULBURN = new int[] { 1, heroes.Count + 1 };
                    CurrentHero.STATUS.DMG += 10;
                    Attack(CurrentHero.COORD, h.COORD);
                    CurrentHero.STATUS.DMG -= 10;

                    listBox1.Items.Add(h.NAME + " иссушен!");
                    if (CurrentHero.HODM > 0)
                        CurrentHero.HODM -= 1;
                    else
                        CurrentHero.HODB -= 1;

                    CheckPassive();
                    RefreshAP();
                    StatusView();
                    RefreshHP();
                    PlaySound("Necromancer_issush.wav");
                }
            }//+++
            else if (Mode == "Могильный холод" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                else
                    CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                CurrentHero.STATUS.DEATHBREATH = new int[] { 1, heroes.Count + 1 };
                listBox1.Items.Add(CurrentHero.NAME + " получил эффект могильного холода!");

                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;
                RefreshAP();
                StatusView();
                PlaySound("Necromancer_holod.wav");
            }//+++
            else if (Mode == "Поднятие трупа" && ((CurrentHero.HODB > 0 && CurrentHero.HODM > 0) || CurrentHero.HODB > 1))
            {
                Hero h = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
                if (h == null && Dist(CurrentHero.COORD, fieldcoord) <= 1)
                {

                    Hero st = heroes.Find(x => x.NAME == "Zombie" && x.TEAM == CurrentHero.TEAM);
                    st.HP = st.MAXHP;
                    st.COORD = fieldcoord;

                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                    else
                        CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                    CurrentHero.HODB -= 1;
                    if (CurrentHero.HODM > 0)
                        CurrentHero.HODM -= 1;
                    else
                        CurrentHero.HODB -= 1;

                    CheckPassive();
                    RefreshAP();
                    RefreshHP();
                    Refresh();
                    PlaySound("Necromancer_prizyv.wav");
                }
            }//+++

            //Warlock +
            else if (Mode == "Каррозия" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                        int r = CurrentHero.RAZBROS;
                        int dmg = (hAttacked.ARMOR + hAttacked.STATUS.ARMOR + hAttacked.STATUS.ARMORPASSIVE) * 4;
                        CurrentHero.STATUS.DMG -= CurrentHero.DMG;
                        CurrentHero.STATUS.DMG += dmg;
                        CurrentHero.RAZBROS = 0;
                        if (hAttacked.STATUS.MAGICSHIELD[0] != 1)
                            hAttacked.STATUS.CORROSIVE = new int[] { 1, heroes.Count + 1 };

                        Attack(CurrentHero.COORD, hAttacked.COORD);
                        listBox1.Items.Add(hAttacked.NAME + " подвержен каррозии!");
                        CurrentHero.STATUS.DMG += CurrentHero.DMG;
                        CurrentHero.STATUS.DMG -= dmg;
                        CurrentHero.RAZBROS = r;

                        CheckPassive();
                    }
                    StatusView();
                    RefreshAP();
                    PlaySound("Warlock_corrosive.wav");

                }
            }//+++
            else if (Mode == "Проклятие" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                        if (hAttacked.STATUS.MAGICSHIELD[0] != 1)
                            hAttacked.STATUS.CURSE = true;
                        listBox1.Items.Add(hAttacked.NAME + " теперь проклят!");
                        StatusView();
                        if (CurrentHero.HODM > 0)
                            CurrentHero.HODM -= 1;
                        else
                            CurrentHero.HODB -= 1;
                        RefreshAP();
                        PlaySound("Warlock_curse.wav");
                    }
                }
            }//+++
            else if (Mode == "Сделка с дьяволом" && ((CurrentHero.HODB > 0 && CurrentHero.HODM > 0) || CurrentHero.HODB > 1))
            {
                Hero h = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
                if (h != null && Dist(CurrentHero.COORD, fieldcoord) <= 1)
                {
                    CheckPassive();
                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                    else
                        CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                    int hp = CurrentHero.HP;

                    CurrentHero.HP = h.HP;
                    h.HP = hp;

                    listBox1.Items.Add(CurrentHero.NAME + " обменялся здоровьем с " + h.NAME);;
                    CurrentHero.HODB -= 1;
                    if (CurrentHero.HODM > 0)
                        CurrentHero.HODM -= 1;
                    else
                        CurrentHero.HODB -= 1;

                    CheckPassive();
                    RefreshAP();
                    StatusView();
                    RefreshHP();
                    PlaySound("Warlock_sunder.wav");
                }
            }//+++

            //Temporarium +
            else if (Mode == "Временная петля" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                else
                    CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                CurrentHero.STATUS.ROOTS = false;
                CurrentHero.STATUS.SILENCE = false;
                CurrentHero.STATUS.REVENGE = false;
                CurrentHero.STATUS.STUN = false;
                CurrentHero.STATUS.POWER = false;
                CurrentHero.STATUS.ANGRY = false;
                CurrentHero.STATUS.CURSE = false;
                CurrentHero.STATUS.WEEKNESS = false;
                CurrentHero.STATUS.DMG = 0;

                TemporariumPassive = false;
                SandsOfTime();

                CurrentHero.HODB = 1;
                CurrentHero.HODM = 1;

                listBox1.Items.Add(CurrentHero.NAME + " совершает еще один ход!");
                RefreshAP();
                StatusView();
                PlaySound("Temporarium_povtor.wav");
            }//+++
            else if (Mode == "Скоротечность" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];
                        CurrentHero.STATUS.ARMORPIERCING = true;
                        CurrentHero.STATUS.DMG -= (CurrentHero.DMG - 10);
                        for (int i = 0; i < 3; i++)
                        {
                            if (hAttacked.NOWKD[i] <= 0)
                            {
                                CurrentHero.STATUS.DMGPASSIVE += 8;
                                hAttacked.NOWKD[i] = 1;
                            }
                        }

                        TemporariumPassive = false;
                        SandsOfTime();

                        Attack(CurrentHero.COORD, fieldcoord);
                        CurrentHero.STATUS.DMGPASSIVE = 0;
                        CurrentHero.STATUS.DMG += (CurrentHero.DMG - 10);
                        CurrentHero.STATUS.ARMORPIERCING = false;
                        CheckPassive();
                        StatusView();
                        RefreshAP();
                        PlaySound("Temporarium_skorotech.wav");
                    }
                }
            }//+++
            else if (Mode == "Обнуление" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) == 1)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        heroAbilities = hAttacked;
                        abilitiesMode = "Обнуление";

                        Abilities ab = new Abilities();
                        ab.ShowDialog();

                        if (abilitieChoose >= 0 && abilitieChoose < 3)
                        {
                            CheckPassive();
                            if (CurrentHero.NAME == "Metamorph")
                                CurrentHero.NOWKD[2] = hAttacked.NOWKD[abilitieChoose] + 1;
                            else
                                CurrentHero.NOWKD[2] = hAttacked.NOWKD[abilitieChoose] + 1;
                            hAttacked.NOWKD[abilitieChoose] = 0;
                            listBox1.Items.Add(hAttacked.NAME + " откатывает способность " + hAttacked.ABILITIES[abilitieChoose + 1] + " y " + hAttacked.NAME);
                            TemporariumPassive = false;
                            if (CurrentHero.HODM > 0)
                                CurrentHero.HODM -= 1;
                            else
                                CurrentHero.HODB -= 1;
                            RefreshAP();
                            StatusView();
                            PlaySound("Temporarium_refresh.wav");
                        }
                    }
                }
            }//+++

            //Metamorph +
            else if (Mode == "Крылья" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0) && CurrentHero.STATUS.ROOTS == false)
            {
                Hero h = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
                if (h == null && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                    CurrentHero.COORD = fieldcoord;

                    if (CurrentHero.HODM > 0)
                        CurrentHero.HODM -= 1;
                    else
                        CurrentHero.HODB -= 1;

                    CurrentHero.STATUS.ARMORED = false;
                    CheckPassive();
                    RefreshAP();
                    ZoneAbilities();
                    Refresh();
                    PyromantPassive();
                    PlaySound("Metamorph_polet.wav");
                }
            }//+++
            else if (Mode == "Удар щупальцем" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        CurrentHero.NOWKD[1] = CurrentHero.KD[1];
                        if (hAttacked.STATUS.MAGICSHIELD[0] != 1)
                            hAttacked.STATUS.WEEKNESS = true;
                        CurrentHero.STATUS.DMG += 9;
                        Attack(CurrentHero.COORD, fieldcoord);
                        CurrentHero.STATUS.DMG -= 9;
                        CheckPassive();
                        RefreshAP();
                        StatusView();
                        PlaySound("Metamorph_shup.wav");
                    }
                }
            }//+++
            else if (Mode == "Дублирование" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) == 1)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        heroAbilities = hAttacked;
                        abilitiesMode = "Дублирование";
                        hMetamorph = CurrentHero;

                        Abilities ab = new Abilities();
                        ab.ShowDialog();

                        if (abilitieChoose >= 0 && abilitieChoose < 3)
                        {
                            Mode = hAttacked.ABILITIES[abilitieChoose + 1];
                            label1.Text = hAttacked.ABILITIES[abilitieChoose + 1];
                            ViewAction();
                            return;
                        }
                    }
                }
            }//+++

            //Guardian +
            else if (Mode == "Выпад" && CurrentHero.HODB > 0)
            {
                if (CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2] && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                        int arm = hAttacked.ARMOR;
                        hAttacked.ARMOR -= 4;
                        if (hAttacked.ARMOR < 0)
                            hAttacked.ARMOR = 0;

                        Attack(CurrentHero.COORD, hAttacked.COORD);
                        hAttacked.ARMOR = arm;

                    }
                    RefreshAP();
                    PlaySound("Guardian_vypad.wav");
                }
            }//+++
            else if (Mode == "Приказ" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                        if (hAttacked.STATUS.STUN == true)
                            hAttacked.STATUS.STUN = false;
                        else
                            hAttacked.STATUS.BOOST = true;

                        if (CurrentHero.HODM > 0)
                            CurrentHero.HODM -= 1;
                        else
                            CurrentHero.HODB -= 1;

                        CheckPassive();
                        RefreshAP();
                        StatusView();
                        PlaySound("Guardian_prikaz.wav");
                    }
                }
            }//+++
            else if (Mode == "Воодушевление" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                else
                    CurrentHero.NOWKD[2] = CurrentHero.KD[2];
                foreach (var n in heroes)
                {
                    if (n.TEAM == CurrentHero.TEAM && Dist(n.COORD, CurrentHero.COORD) <= 2)
                    {
                        int dmg = 3;
                        foreach (var m in heroes)
                        {
                            if (m.TEAM != CurrentHero.TEAM && Dist(n.COORD, m.COORD) <= 2)
                            {
                                dmg += 2;
                            }
                        }
                        n.STATUS.ENCOURAGE = new int[] { 1, dmg, heroes.Count + 1 };
                        n.STATUS.DMG += dmg;
                    }
                }
                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;

                CheckPassive();
                RefreshAP();
                StatusView();
                PlaySound("Guardian_voodush.wav");
            }//+++

            //Cryomant +
            else if (Mode == "Град" && CurrentHero.HODB > 0)
            {
                foreach (var n in heroes)
                {
                    if (n.TEAM != CurrentHero.TEAM && Dist(fieldcoord, n.COORD) <= 1)
                    {
                        CurrentHero.HODB += 1;
                        CurrentHero.STATUS.DMG -= 6;
                        Attack(CurrentHero.COORD, n.COORD);
                        CurrentHero.STATUS.DMG += 6;
                    }
                    CurrentHero.HODB -= 1;
                    RefreshAP();
                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                    else
                        CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                    PlaySound("Cryomant_grad.wav");

                }
            }
            else if (Mode == "Взрыв стужи" && ((CurrentHero.HODB > 0 && CurrentHero.HODM > 0) || CurrentHero.HODB > 1))
            {
                foreach (var n in heroes)
                {
                    if (n.TEAM != CurrentHero.TEAM && Dist(CurrentHero.COORD, n.COORD) <= 1)
                    {
                        CurrentHero.HODB += 1;
                        CurrentHero.STATUS.DMG += 3;
                        Attack(CurrentHero.COORD, n.COORD);
                        if (n.STATUS.MAGICSHIELD[0] != 1)
                            n.STATUS.STUN = true;
                        CurrentHero.STATUS.DMG -= 3;
                    }
                    else if (n.TEAM != CurrentHero.TEAM && Dist(CurrentHero.COORD, n.COORD) == 2)
                    {
                        CurrentHero.HODB += 1;
                        CurrentHero.STATUS.DMG -= 9;
                        Attack(CurrentHero.COORD, n.COORD);
                        CurrentHero.STATUS.DMG += 9;
                    }
                    CurrentHero.HODB -= 1;
                    if (CurrentHero.HODM > 0)
                        CurrentHero.HODM -= 1;
                    else
                        CurrentHero.HODB -= 1;

                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                    else
                        CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                }
                RefreshAP();
                StatusView();
                PlaySound("Cryomant_stuzha.wav");
            }//+++
            else if (Mode == "Криостазис" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                        else
                            CurrentHero.NOWKD[2] = CurrentHero.KD[2];
                        Random rnd = new Random();

                        hAttacked.STATUS.CRYOSTASYS = new int[] { 1, heroes.Count + 1 };
                        CurrentHero.HODB -= 1;
                        listBox1.Items.Add(hAttacked.NAME + " находится в заморозке");
                        StatusView();
                        RefreshAP();
                        if (hAttacked.INITIATIV == CurrentHero.INITIATIV)
                            button6.PerformClick();
                        PlaySound("Cryomant_stazis.wav");
                    }
                }
            }//+++

            //Cultist + 
            else if (Mode == "Кровопускание" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 1)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                        if (hAttacked.STATUS.MAGICSHIELD[0] != 1)
                            hAttacked.STATUS.BLEED = new int[] { 1, (heroes.Count * 2) + 1 };

                        Attack(CurrentHero.COORD, fieldcoord);
                        CheckPassive();
                        RefreshAP();
                        StatusView();
                        PlaySound("Cultist_nozh.wav");
                    }
                }
            }//+++
            else if (Mode == "Жертвоприношение" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                        Random rnd = new Random();
                        int heal = rnd.Next(25, 31);
                        if (heal > CurrentHero.HP)
                        {
                            heal = CurrentHero.HP;
                        }

                        CurrentHero.HP -= heal;
                        listBox1.Items.Add(CurrentHero.NAME + " жертвует " + heal + " хп");
                        hAttacked.HP += heal * 2;
                        HPplus(hAttacked, heal * 2);
                        listBox1.Items.Add(hAttacked.NAME + " восстанавливает " + heal * 2 + " хп");

                        RefreshHP();
                        if (hAttacked.HP > hAttacked.MAXHP)
                            hAttacked.HP = hAttacked.MAXHP;
                        CurrentHero.HODB -= 1;
                        RefreshAP();

                        if (CurrentHero.HP < 1)
                        {
                            CurrentHero.COORD = new int[] { -5, -5, -5 };
                            button6.PerformClick();
                        }
                        PlaySound("Cultist_zhertva.wav");
                    }
                }
            }//+++
            else if (Mode == "Скверна" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {

                if (Dist(CurrentHero.COORD, fieldcoord) <= 2 && skverna.Count == 0)
                {
                    string f1 = "b" + fieldcoord[0].ToString() + fieldcoord[1].ToString() + fieldcoord[2].ToString();
                    f1 = f1.Replace('-', 'm');
                    Button b = Hex.Find(x => x.Name == f1);
                    if (b != null)
                    {
                        b.FlatAppearance.BorderColor = Color.Green;
                        b.FlatAppearance.BorderSize = 5;
                        skverna.Add(b);
                        return;
                    }
                }

                else if (skverna.Count == 1)
                {
                    string c = skverna[0].Name;
                    c = c.Replace('m', '-');
                    int[] sk = new int[3];
                    sk = Coordinata(c);

                    if (Dist(sk, fieldcoord) == 1)
                    {
                        string coord2 = skverna[0].Name;
                        coord2 = coord2.Replace('m', '-');
                        int[] fieldcoord2 = Coordinata(coord2);
                        if (Dist(fieldcoord2, fieldcoord) == 1)
                        {
                            string f1 = "b" + fieldcoord[0].ToString() + fieldcoord[1].ToString() + fieldcoord[2].ToString();
                            f1 = f1.Replace('-', 'm');
                            Button b = Hex.Find(x => x.Name == f1);
                            if (b != null)
                            {
                                b.FlatAppearance.BorderColor = Color.Green;
                                b.FlatAppearance.BorderSize = 5;
                                skverna.Add(b);
                            }

                            foreach (var n in Hex)
                            {
                                string coord3 = n.Name;
                                coord3 = coord3.Replace('m', '-');
                                int[] fieldcoord3 = Coordinata(coord3);

                                string f2 = "b" + fieldcoord3[0].ToString() + fieldcoord3[1].ToString() + fieldcoord3[2].ToString();
                                f2 = f2.Replace('-', 'm');

                                if (Dist(sk, fieldcoord3) == 1 && Dist(fieldcoord, fieldcoord3) == 1)
                                {
                                    n.FlatAppearance.BorderColor = Color.Green;
                                    n.FlatAppearance.BorderSize = 5;
                                    skverna.Add(n);
                                }
                            }
                            if (CurrentHero.NAME == "Metamorph")
                                CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                            else
                                CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                            if (CurrentHero.HODM > 0)
                                CurrentHero.HODM -= 1;
                            else
                                CurrentHero.HODB -= 1;

                            skvernaTurn = 1;
                            Refresh();
                            RefreshAP();
                            PlaySound("Cultist_skverna.wav");
                        }

                    }
                    else
                    {
                        foreach (var n in skverna)
                        {
                            n.FlatAppearance.BorderColor = Color.Black;
                            n.FlatAppearance.BorderSize = 1;
                        }
                        skverna.Clear();
                    }
                }
                else
                {
                    foreach (var n in skverna)
                    {
                        n.FlatAppearance.BorderColor = Color.Black;
                        n.FlatAppearance.BorderSize = 1;
                    }
                    skverna.Clear();
                }
            }

            //Archer +
            else if (Mode == "В яблочко!" && CurrentHero.HODB > 0)
            {
                if (CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2] && Dist(CurrentHero.COORD, fieldcoord) <= 3 && Dist(CurrentHero.COORD, fieldcoord) > 1)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {

                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                        CurrentHero.STATUS.ARMORPIERCING = true;
                        Attack(CurrentHero.COORD, hAttacked.COORD);
                        CurrentHero.STATUS.ARMORPIERCING = false;
                        PlaySound("Archer_headshot.wav");

                    }
                    StatusView();
                    RefreshAP();
                }
            }//+++
            else if (Mode == "Залп" && CurrentHero.HODB > 0)
            {
                if ((CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2]) && Dist(CurrentHero.COORD, fieldcoord) > 0)
                {
                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                    else
                        CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                    int[] direction = new int[3] { fieldcoord[0] - CurrentHero.COORD[0], fieldcoord[1] - CurrentHero.COORD[1], fieldcoord[2] - CurrentHero.COORD[2] };
                    for (int i = 0; i < 3; i++)
                    {
                        if (Math.Abs(direction[i]) > 1)
                            direction[i] = direction[i] / Math.Abs(direction[i]);
                    }

                    fieldcoord[0] = CurrentHero.COORD[0];
                    fieldcoord[1] = CurrentHero.COORD[1];
                    fieldcoord[2] = CurrentHero.COORD[2];
                    for (int i = 0; i < 2; i++)
                    {
                        fieldcoord[0] += direction[0];
                        fieldcoord[1] += direction[1];
                        fieldcoord[2] += direction[2];

                        Attack(CurrentHero.COORD, fieldcoord);
                    }
                    fieldcoord[0] = CurrentHero.COORD[0] + direction[0];
                    fieldcoord[1] = CurrentHero.COORD[1] + direction[1];
                    fieldcoord[2] = CurrentHero.COORD[2] + direction[2];

                    foreach (var n in Hex)
                    {
                        string coord3 = n.Name;
                        coord3 = coord3.Replace('m', '-');
                        int[] fieldcoord3 = Coordinata(coord3);

                        string f2 = "b" + fieldcoord3[0].ToString() + fieldcoord3[1].ToString() + fieldcoord3[2].ToString();
                        f2 = f2.Replace('-', 'm');

                        if (Dist(CurrentHero.COORD, fieldcoord3) == 1 && Dist(fieldcoord, fieldcoord3) == 1)
                        {
                            direction = new int[3] { fieldcoord3[0] - CurrentHero.COORD[0], fieldcoord3[1] - CurrentHero.COORD[1], fieldcoord3[2] - CurrentHero.COORD[2] };

                            fieldcoord3[0] = CurrentHero.COORD[0];
                            fieldcoord3[1] = CurrentHero.COORD[1];
                            fieldcoord3[2] = CurrentHero.COORD[2];
                            for (int i = 0; i < 2; i++)
                            {
                                fieldcoord3[0] += direction[0];
                                fieldcoord3[1] += direction[1];
                                fieldcoord3[2] += direction[2];

                                Attack(CurrentHero.COORD, fieldcoord3);
                            }
                        }
                    }

                    PlaySound("Archer_zalp.wav");
                    CurrentHero.HODB = 0;
                    RefreshAP();
                }
            }//+++
            else if (Mode == "Лечение ран" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                else
                    CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                int i = 0;

                foreach (var n in Hex)
                {
                    string c = n.Name;
                    c = c.Replace('m', '-');
                    int[] sk = new int[3];
                    sk = Coordinata(c);

                    if (Dist(CurrentHero.COORD, sk) == 1)
                    {
                        Hero h = heroes.Find(x => x.COORD[0] == sk[0] && x.COORD[1] == sk[1] && x.COORD[2] == sk[2]);
                        if (h == null)
                            i++;
                    }
                }

                CurrentHero.HP += i * 4;
                HPplus(CurrentHero, i * 4);
                listBox1.Items.Add(CurrentHero.NAME + " восстанавливает " + i * 4 + " хп!");

                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;
                PlaySound("Archer_hil.wav");
                RefreshAP();
                RefreshHP();
            }//+++

            //Shaman +
            else if (Mode == "Дух огня" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {

                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                else
                    CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                CurrentHero.STATUS.POWER = true;
                listBox1.Items.Add(CurrentHero.NAME + " теперь наносит на 30% больше урона!");
                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;
                RefreshAP();
                StatusView();
                PlaySound("Shaman_ogon.wav");

            }//+++
            else if (Mode == "Дух ветра" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                PlaySound("Shaman_veter.wav");
                Hero c = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                if (replace == null && Dist(CurrentHero.COORD, fieldcoord) <= 2 && Dist(CurrentHero.COORD, fieldcoord) > 0)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null)
                    {
                        if (hAttacked.STATUS.GEOSHIELD[0] != 1)
                        {
                            replace = hAttacked;
                            ViewAction();
                            return;
                        }
                    }
                }
                else if (replace != null && Dist(replace.COORD, fieldcoord) <= 1)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked == null)
                    {
                        Hero h = heroes.Find(x => x.INITIATIV == replace.INITIATIV);
                        Hero hCheck = spiritofwind.Find(x => x.INITIATIV == replace.INITIATIV);
                        if (h != null && hCheck == null)
                        {
                            CurrentHero.HODM += 1;
                            Step(h.COORD, fieldcoord);
                            SpiritOfWind++;
                            spiritofwind.Add(h);
                            CheckPassive();
                            button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = false;

                            if (SpiritOfWind >= 3)
                            {
                                if (CurrentHero.NAME == "Metamorph")
                                    CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                                else
                                    CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                                if (CurrentHero.HODM > 0)
                                    CurrentHero.HODM -= 1;
                                else
                                    CurrentHero.HODB -= 1;

                                listBox1.Items.Add(h.NAME + " свдинут силой ветра!");
                                button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = true;
                                spiritofwind.Clear();
                                SpiritOfWind = 0;
                                replace = null;
                                ZoneAbilities();
                                RefreshAP();
                                Refresh();
                                Mode = "...";
                            }
                            else
                            {
                                replace = null;
                                ZoneAbilities();
                                listBox1.Items.Add(h.NAME + " свдинут силой ветра!");
                                ViewAction();
                                return;
                            }
                        }
                        else
                        {
                            replace = null;
                            ViewAction();
                            return;
                        }
                    }
                }
                else if (c == null)
                {
                    replace = null;
                    ViewAction();
                    return;
                }
                else if (c.INITIATIV == CurrentHero.INITIATIV)
                {
                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                    else
                        CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                    if (CurrentHero.HODM > 0)
                        CurrentHero.HODM -= 1;
                    else
                        CurrentHero.HODB -= 1;

                    button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = true;
                    spiritofwind.Clear();
                    SpiritOfWind = 0;
                    replace = null;
                    RefreshAP();
                    Refresh();
                    Mode = "...";
                }
                else
                {
                    replace = null;
                    ViewAction();
                    return;
                }
            }//+++
            else if (Mode == "Дух воды" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {

                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                else
                    CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                int[] dmg = new int[2] { CurrentHero.DMG, CurrentHero.STATUS.DMG };
                CurrentHero.DMG = CurrentHero.STATUS.DMG = 7;
                int raz = CurrentHero.RAZBROS;
                CurrentHero.RAZBROS = 2;
                CurrentHero.STATUS.DMGPASSIVE = 0;
                CurrentHero.STATUS.ARMORPIERCING = true;

                foreach (var n in heroes)
                {
                    if (Dist(CurrentHero.COORD, n.COORD) == 1 && CurrentHero.TEAM != n.TEAM)
                    {
                        CurrentHero.HODB += 1;
                        Thread.Sleep(7);
                        Attack(CurrentHero.COORD, n.COORD);
                    }
                    else if (Dist(CurrentHero.COORD, n.COORD) <= 1 && CurrentHero.TEAM == n.TEAM)
                    {
                        n.HP += 16;
                        HPplus(n, 16);
                        listBox1.Items.Add(n.NAME + " восстанавливает 16 хп!");
                    }
                }

                CurrentHero.DMG = dmg[0];
                CurrentHero.STATUS.DMG = dmg[1];
                CurrentHero.STATUS.ARMORPIERCING = false;
                CurrentHero.RAZBROS = raz;

                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;

                CheckPassive();
                RefreshAP();
                RefreshHP();
                PlaySound("Shaman_voda.wav");
            }//+++

            //Mage +
            else if (Mode == "Магический выброс" && CurrentHero.HODB > 0)
            {
                foreach (var n in heroes)
                {
                    if (n.TEAM != CurrentHero.TEAM && Dist(CurrentHero.COORD, n.COORD) <= 2)
                    {
                        int[] dmg = new int[2] { CurrentHero.DMG, CurrentHero.STATUS.DMG };
                        CurrentHero.STATUS.DMG = 0;
                        CurrentHero.DMG = Convert.ToInt32(Convert.ToDouble(Math.Log10(CurrentHero.MANA) * 40) - 5);
                        int raz = CurrentHero.RAZBROS;
                        CurrentHero.RAZBROS = 0;
                        CurrentHero.STATUS.ARMORPIERCING = true;

                        CurrentHero.HODB += 1;

                        Attack(CurrentHero.COORD, n.COORD);

                        CurrentHero.DMG = dmg[0];
                        CurrentHero.STATUS.DMG = dmg[1];
                        CurrentHero.STATUS.ARMORPIERCING = false;
                        CurrentHero.RAZBROS = raz;
                    }
                    else if (n.TEAM == CurrentHero.TEAM && Dist(CurrentHero.COORD, n.COORD) <= 2)
                    {
                        int heal = Convert.ToInt32(Convert.ToDouble(Math.Log10(CurrentHero.MANA) * 40) - 5);
                        n.HP += heal;
                        HPplus(n, heal);
                    }

                    CurrentHero.HODB -= 1;
                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                    else
                        CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                    PlaySound("Mage_vybros.wav");
                }
                CurrentHero.MANA = 0;
                RefreshAP();
                RefreshHP();
            }//+++
            else if (Mode == "Иллюзия" && CurrentHero.HODB > 0)
            {
                Hero h = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
                if (h == null && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero st = heroes.Find(x => x.NAME == "Illusion" && x.TEAM == CurrentHero.TEAM);
                    st.HP = st.MAXHP;
                    st.COORD = fieldcoord;

                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                    else
                        CurrentHero.NOWKD[1] = CurrentHero.KD[1];


                    foreach (var n in heroes)
                    {
                        if (Dist(fieldcoord, n.COORD) == 1 && n.TEAM != CurrentHero.TEAM)
                        {
                            n.STATUS.ANGRY = true;
                        }
                    }
                    CurrentHero.HODB -= 1;

                    st.STATUS.ANGRY1 = new int[] { 1, 9999 };

                    StatusView();
                    CheckPassive();
                    RefreshAP();
                    RefreshHP();
                    Refresh();
                    PlaySound("Mage_illusion.wav");
                }
            }//+++
            else if (Mode == "Магический щит" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                        else
                            CurrentHero.NOWKD[2] = CurrentHero.KD[2];
                        hAttacked.STATUS.MAGICSHIELD = new int[2] { 1, heroes.Count + 1 };

                        ClearDebaff(hAttacked.INITIATIV);

                        if (CurrentHero.HODM > 0)
                            CurrentHero.HODM -= 1;
                        else
                            CurrentHero.HODB -= 1;

                        listBox1.Items.Add(hAttacked.NAME + " получает магический щит!");
                        StatusView();
                        RefreshAP();
                        PlaySound("Mage_shield.wav");
                    }
                }
            }//+++

            //Thief +
            else if (Mode == "Кульбит" && CurrentHero.HODB > 0 && CurrentHero.STATUS.ROOTS == false)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 1)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                        Attack(CurrentHero.COORD, fieldcoord);

                        if (hAttacked.STATUS.GEOSHIELD[0] != 1)
                        {
                            int[] c = new int[3];
                            c = CurrentHero.COORD;
                            CurrentHero.COORD = hAttacked.COORD;
                            hAttacked.COORD = c;
                        }
                        hAttacked = heroes.Find(x => x.INITIATIV == hAttacked.INITIATIV);
                        if (hAttacked.HP < 1)
                        {
                            CurrentHero.COORD = fieldcoord;
                            hAttacked.COORD = new int[] { 5, 5, 5 };
                        }
                        CheckPassive();
                    }
                }
                RefreshHP();
                Refresh();
                ZoneAbilities();
                PlaySound("Thief_kulbit.wav");
            }//+++
            else if (Mode == "Отравленный клинок" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 1)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                        if (hAttacked.STATUS.MAGICSHIELD[0] != 1)
                            hAttacked.STATUS.POISON = true;

                        Attack(CurrentHero.COORD, fieldcoord);
                        CheckPassive();
                        RefreshAP();
                        StatusView();
                        PlaySound("Thief_porch.wav");
                    }
                }
            }//+++
            else if (Mode == "Дымовая бомба" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0) && CurrentHero.STATUS.ROOTS == false)
            {
                CheckPassive();
                if (CurrentHero.NAME == "Metamorph")
                {
                    CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                    SmokeMet = true;
                    foreach (var n in Hex)
                    {
                        string coord3 = n.Name;
                        coord3 = coord3.Replace('m', '-');
                        int[] fieldcoord3 = Coordinata(coord3);

                        if (Dist(CurrentHero.COORD, fieldcoord3) <= 1)
                        {
                            smokeMet.Add(n);
                        }
                    }
                }
                else
                {
                    CurrentHero.NOWKD[2] = CurrentHero.KD[2];
                    Smoke = true;
                    foreach (var n in Hex)
                    {
                        string coord3 = n.Name;
                        coord3 = coord3.Replace('m', '-');
                        int[] fieldcoord3 = Coordinata(coord3);

                        if (Dist(CurrentHero.COORD, fieldcoord3) <= 1)
                        {
                            smoke.Add(n);
                        }
                    }
                }

                foreach (var n in directions)
                {
                    fieldcoord[0] = n[0] + CurrentHero.COORD[0];
                    fieldcoord[1] = n[1] + CurrentHero.COORD[1];
                    fieldcoord[2] = n[2] + CurrentHero.COORD[2];

                    Hero h = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2] && x.TEAM != CurrentHero.TEAM);
                    if (h != null && h.TEAM != CurrentHero.TEAM && h.STATUS.MAGICSHIELD[0] != 1)
                    {
                        h.STATUS.STUN = true;
                        listBox1.Items.Add(h.NAME + " в смятении!");
                    }
                }
                PictureBox pb = frames.Find(y => y.Tag.ToString() == CurrentHero.INITIATIV.ToString());
                PictureBox pb2 = framesHP.Find(y => y.Tag.ToString() == CurrentHero.INITIATIV.ToString());
                if (pb != null)
                {
                    pb.Visible = false;
                    pb2.Visible = false;
                }


                CurrentHero.COORD = new int[] { 7, 7, 7 };
                PlaySound("Thief_smoke.wav");
                button6.PerformClick();
            }//+++

            //Pyromant +
            else if (Mode == "Ожоги" && CurrentHero.HODB > 0)
            {
                Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                if (hAttacked != null)
                {
                    if (hAttacked.TEAM != CurrentHero.TEAM && Dist(CurrentHero.COORD, hAttacked.COORD) <= 2)
                    {
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                        int[] dmg = new int[2] { CurrentHero.DMG, CurrentHero.STATUS.DMG };
                        CurrentHero.STATUS.DMG = 0;
                        CurrentHero.DMG = Convert.ToInt32(hAttacked.MAXHP * 0.15);
                        int raz = CurrentHero.RAZBROS;
                        CurrentHero.RAZBROS = 0;
                        CurrentHero.STATUS.ARMORPIERCING = true;

                        Attack(CurrentHero.COORD, hAttacked.COORD);
                        if (hAttacked.STATUS.MAGICSHIELD[0] != 1)
                            hAttacked.STATUS.BURN = new double[] { 1, Convert.ToDouble(hAttacked.HP) / Convert.ToDouble(hAttacked.MAXHP) };
                        listBox1.Items.Add(hAttacked.NAME + " получает ожоги!");

                        CurrentHero.DMG = dmg[0];
                        CurrentHero.STATUS.DMG = dmg[1];
                        CurrentHero.STATUS.ARMORPIERCING = false;
                        CurrentHero.RAZBROS = raz;
                        RefreshAP();
                        RefreshHP();
                        StatusView();
                        PlaySound("Pyromant_ozhog.wav");
                    }
                }

            }//+++
            else if (Mode == "Воспламенение" && CurrentHero.HODB > 0)
            {
                if (CurrentHero.COORD[0] == fieldcoord[0] || CurrentHero.COORD[1] == fieldcoord[1] || CurrentHero.COORD[2] == fieldcoord[2] && Dist(CurrentHero.COORD, fieldcoord) <= 2 && Dist(CurrentHero.COORD, fieldcoord) > 0)
                {

                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                    else
                        CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                    int hod = CurrentHero.HODB;
                    int[] direction = new int[3] { fieldcoord[0] - CurrentHero.COORD[0], fieldcoord[1] - CurrentHero.COORD[1], fieldcoord[2] - CurrentHero.COORD[2] };
                    for (int i = 0; i < 3; i++)
                    {
                        if (Math.Abs(direction[i]) > 1)
                            direction[i] = direction[i] / Math.Abs(direction[i]);
                    }

                    fieldcoord[0] = CurrentHero.COORD[0] + direction[0];
                    fieldcoord[1] = CurrentHero.COORD[1] + direction[1];
                    fieldcoord[2] = CurrentHero.COORD[2] + direction[2];
                    for (int i = 0; i < 2; i++)
                    {
                        Attack(CurrentHero.COORD, fieldcoord);
                        fieldcoord[0] += direction[0];
                        fieldcoord[1] += direction[1];
                        fieldcoord[2] += direction[2];
                    }
                    fieldcoord[0] = CurrentHero.COORD[0] + direction[0];
                    fieldcoord[1] = CurrentHero.COORD[1] + direction[1];
                    fieldcoord[2] = CurrentHero.COORD[2] + direction[2];

                    foreach (var n in Hex)
                    {
                        string coord3 = n.Name;
                        coord3 = coord3.Replace('m', '-');
                        int[] fieldcoord3 = Coordinata(coord3);

                        if (Dist(CurrentHero.COORD, fieldcoord3) == 1 && Dist(fieldcoord, fieldcoord3) == 1)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                Attack(CurrentHero.COORD, fieldcoord3);
                                fieldcoord3[0] += direction[0];
                                fieldcoord3[1] += direction[1];
                                fieldcoord3[2] += direction[2];
                            }
                        }
                    }
                    CurrentHero.HODB = hod - 1;

                    RefreshAP();
                    PlaySound("Pyromant_aoe.wav");
                }
            }
            else if (Mode == "Адское клеймо" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                        else
                            CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                        if (hAttacked.STATUS.MAGICSHIELD[0] != 1)
                            hAttacked.STATUS.HELLSIGN = new int[2] { 1, heroes.Count };

                        if (CurrentHero.HODM > 0)
                            CurrentHero.HODM -= 1;
                        else
                            CurrentHero.HODB -= 1;

                        listBox1.Items.Add("На " + hAttacked.NAME + " наложено адское клеймо!");
                        StatusView();
                        RefreshAP();
                        PlaySound("Pyromant_prokl.wav");
                    }
                }
            }//+++

            //Golem +
            else if (Mode == "Активная броня" && CurrentHero.HODB > 0)
            {

                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                else
                    CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                CheckPassive();

                int[] dmg = new int[2] { CurrentHero.DMG, CurrentHero.STATUS.DMG };
                CurrentHero.STATUS.DMG = 0;
                CurrentHero.DMG = 6 + (CurrentHero.ARMOR + CurrentHero.STATUS.ARMOR + CurrentHero.STATUS.ARMORPASSIVE) * 3;
                int raz = CurrentHero.RAZBROS;
                CurrentHero.RAZBROS = 0;

                int hG = CurrentHero.HODB;

                foreach (var n in directions)
                {
                    fieldcoord[0] = n[0] + CurrentHero.COORD[0];
                    fieldcoord[1] = n[1] + CurrentHero.COORD[1];
                    fieldcoord[2] = n[2] + CurrentHero.COORD[2];

                    Thread.Sleep(7);
                    Attack(CurrentHero.COORD, fieldcoord);
                }

                CurrentHero.DMG = dmg[0];
                CurrentHero.STATUS.DMG = dmg[1];
                CurrentHero.RAZBROS = raz;
                CurrentHero.HODB = hG - 1;

                if (CurrentHero.ARMOR > 0)
                    CurrentHero.ARMOR -= 1;
                RefreshAP();
                PlaySound("Golem_aktiv.wav");
            }//+++
            else if (Mode == "Кровь камня" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                        else
                            CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                        hAttacked.STATUS.STONEBLOOD = new int[] { 1, heroes.Count * 2 };

                        if (CurrentHero.HODM > 0)
                            CurrentHero.HODM -= 1;
                        else
                            CurrentHero.HODB -= 1;

                        CheckPassive();
                        RefreshAP();
                        StatusView();
                        PlaySound("Golem_krov.wav");
                    }
                }
            }//+++
            else if (Mode == "Апперкот" && CurrentHero.HODB > 0)
            {
                Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
                if (hAttacked != null && replace == null && Dist(CurrentHero.COORD, fieldcoord) == 1)
                {
                    if (hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        replace = hAttacked;
                        ViewAction();
                        return;
                    }
                    else
                        replace = null;
                }
                else if (hAttacked == null && replace != null && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero h = heroes.Find(y => y.INITIATIV == replace.INITIATIV);

                    Attack(CurrentHero.COORD, h.COORD);

                    if (h.STATUS.GEOSHIELD[0] != 1)
                        h.COORD = fieldcoord;

                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                    else
                        CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                    replace = null;

                    Refresh();
                    ZoneAbilities();
                    PlaySound("Golem_punch.wav");
                }
                else
                {
                    replace = null;
                }

            }//+++

            //Angel +
            else if (Mode == "Небесный кулак" && CurrentHero.HODB > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                        else
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];

                        if (hAttacked.STATUS.MAGICSHIELD[0] != 1)
                        {
                            hAttacked.STATUS.STUN = true;
                            listBox1.Items.Add(hAttacked.NAME + " оглушен!");
                        }

                        Attack(CurrentHero.COORD, hAttacked.COORD);


                        CheckPassive();
                    }
                    StatusView();
                    RefreshAP();
                    PlaySound("Angel_stun.wav");
                }
            }//+++
            else if (Mode == "Прорыв" && ((CurrentHero.HODB > 0 && CurrentHero.HODM > 0) || CurrentHero.HODB > 1) && CurrentHero.STATUS.ROOTS == false)
            {
                Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
                if (hAttacked != null && replace == null)
                {
                    if (hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        replace = hAttacked;
                        ViewAction();
                        return;
                    }
                    else
                        replace = null;
                }
                else if (hAttacked == null && replace != null && Dist(replace.COORD, fieldcoord) == 1)
                {
                    Hero h = heroes.Find(y => y.INITIATIV == replace.INITIATIV);
                    CurrentHero.COORD = fieldcoord;
                    if(CurrentHero.STATUS.INJECT[0] == 1)
                    {
                        Hero her = heroes.Find(y => y.NAME == "Spirit" && y.TEAM == CurrentHero.TEAM);
                        if (her != null)
                            InjectCoord = fieldcoord;
                        else
                            InjectCoordMet = fieldcoord;
                    }
                    ZoneAbilities();
                    CheckPassive();

                    Attack(CurrentHero.COORD, h.COORD);

                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                    else
                        CurrentHero.NOWKD[1] = CurrentHero.KD[1];

                    if (CurrentHero.HODM > 0)
                        CurrentHero.HODM -= 1;
                    else
                        CurrentHero.HODB -= 1;
                    replace = null;
                    Refresh();
                    PlaySound("Angel_blitz.wav");

                }
                else
                {
                    replace = null;
                }

            }//+++
            else if (Mode == "Ангел хранитель" && ((CurrentHero.HODB > 0 && CurrentHero.HODM > 0) || CurrentHero.HODB > 1) && CurrentHero.STATUS.ROOTS == false)
            {
                Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);
                if (hAttacked != null && replace == null)
                {
                    if (hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        replace = hAttacked;
                        ViewAction();
                        return;
                    }
                    else
                        replace = null;
                }
                else if (hAttacked == null && replace != null && Dist(replace.COORD, fieldcoord) == 1)
                {
                    Hero h = heroes.Find(y => y.INITIATIV == replace.INITIATIV);
                    CurrentHero.COORD = fieldcoord;
                    ZoneAbilities();
                    CheckPassive();
                    if (CurrentHero.STATUS.INJECT[0] == 1)
                    {
                        Hero her = heroes.Find(y => y.NAME == "Spirit" && y.TEAM == CurrentHero.TEAM);
                        if (her != null)
                            InjectCoord = fieldcoord;
                        else
                            InjectCoordMet = fieldcoord;
                    }
                    Random rnd = new Random();
                    int heal = rnd.Next(25, 31);

                    h.HP += heal;
                    listBox1.Items.Add(h.NAME + " восстанавливает " + heal + " хп!");
                    HPplus(h, heal);

                    if (CurrentHero.NAME == "Metamorph")
                        CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                    else
                        CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                    replace = null;

                    CurrentHero.HODB -= 1;
                    if (CurrentHero.HODM > 0)
                        CurrentHero.HODM -= 1;
                    else
                        CurrentHero.HODB -= 1;

                    Refresh();
                    RefreshHP();
                    PlaySound("Angel_hil.wav");
                }
                else
                {
                    replace = null;
                }

            }//+++

            //Spirit
            else if (Mode == "Внедрение" && (CurrentHero.HODB > 0))
            {
                if (Dist(CurrentHero.COORD, fieldcoord) > 0 && Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.Target(fieldcoord) == true);
                    if (hAttacked != null && hAttacked.TEAM == CurrentHero.TEAM)
                    {
                        if (CurrentHero.NAME == "Metamorph")
                        {
                            CurrentHero.NOWKD[2] = heroAbilities.KD[0];
                            InjectCoordMet = hAttacked.COORD;
                            InjMet = true;
                        }
                        else
                        {
                            CurrentHero.NOWKD[0] = CurrentHero.KD[0];
                            InjectCoord = hAttacked.COORD;
                            Inj = true;
                         }
                        
                        hAttacked.STATUS.INJECT = new int[2] { 1, heroes.Count + 1 };
                        CurrentHero.COORD = new int[3] { 5, 5, 5 };

                        PictureBox pb = frames.Find(y => y.Tag.ToString() == CurrentHero.INITIATIV.ToString());
                        PictureBox pb2 = framesHP.Find(y => y.Tag.ToString() == CurrentHero.INITIATIV.ToString());
                        if (pb != null)
                        {
                            pb.Visible = false;
                            pb2.Visible = false;
                        }

                        CurrentHero.HODB -= 1;
                        
                        CheckPassive();
                        Refresh();
                        StatusView();
                        button6.PerformClick();
                        PlaySound("Spirit_inject.wav");
                    }
                }                
            }//+++
            else if (Mode == "Бесплотность" && (CurrentHero.HODB > 0 || CurrentHero.HODM > 0))
            {
                if (CurrentHero.NAME == "Metamorph")
                    CurrentHero.NOWKD[2] = heroAbilities.KD[1];
                else
                {
                    CurrentHero.NOWKD[1] = CurrentHero.KD[1];
                }

                CurrentHero.STATUS.UNFLESH = new int[2] { 1, heroes.Count + 1 };

                if (CurrentHero.HODM > 0)
                    CurrentHero.HODM -= 1;
                else
                    CurrentHero.HODB -= 1;

                CheckPassive();
                RefreshAP();
                StatusView();
                PlaySound("Spirit_unflesh.wav");
            }//+++
            else if (Mode == "Онемение" && CurrentHero.HODB > 0 && CurrentHero.HODM > 0)
            {
                if (Dist(CurrentHero.COORD, fieldcoord) <= 2)
                {
                    Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                    if (hAttacked != null && hAttacked.TEAM != CurrentHero.TEAM)
                    {
                        CheckPassive();
                        if (CurrentHero.NAME == "Metamorph")
                            CurrentHero.NOWKD[2] = heroAbilities.KD[2];
                        else
                            CurrentHero.NOWKD[2] = CurrentHero.KD[2];

                        if (hAttacked.STATUS.MAGICSHIELD[0] != 1)
                        {                            
                            hAttacked.STATUS.CURSE = true;
                            hAttacked.STATUS.SILENCE = true;
                            hAttacked.STATUS.ROOTS = true;
                            listBox1.Items.Add("На " + hAttacked.NAME + " наложены: немота, слабость и оцепенение!");
                        }
                        int dmg = CurrentHero.DMG;

                        CurrentHero.STATUS.ARMORPIERCING = true;
                        CurrentHero.DMG = 20;                        
                        Attack(CurrentHero.COORD, hAttacked.COORD);
                        CurrentHero.DMG = dmg;
                        CurrentHero.STATUS.ARMORPIERCING = false;

                        CheckPassive();
                        CurrentHero.HODM -= 1;
                    }
                    StatusView();
                    RefreshAP();
                    PlaySound("Spirit_onim.wav");
                }
            }//+++

            else if (Mode == "Появление")
            {
                Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                if (hAttacked == null)
                {
                    button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = true;
                    CurrentHero.COORD = fieldcoord;

                    if (CurrentHero.NAME == "Thief")
                    {
                        foreach (var n in smoke)
                        {
                            n.FlatAppearance.BorderColor = Color.Black;
                            n.FlatAppearance.BorderSize = 1;
                        }

                        Smoke = false;
                        smoke.Clear();
                    }
                    else if (CurrentHero.NAME == "Metamorph")
                    {
                        foreach (var n in smokeMet)
                        {
                            n.FlatAppearance.BorderColor = Color.Black;
                            n.FlatAppearance.BorderSize = 1;
                        }

                        SmokeMet = false;
                        smokeMet.Clear();
                    }
                    foreach (var n in skverna)
                    {
                        n.FlatAppearance.BorderColor = Color.Green;
                        n.FlatAppearance.BorderSize = 5;
                    }

                    RefreshAP();
                    Refresh();
                    ZoneAbilities();
                }
                else
                    return;
            }
            else if (Mode == "Выселение")
            {
                Hero hAttacked = heroes.Find(x => x.COORD[0] == fieldcoord[0] && x.COORD[1] == fieldcoord[1] && x.COORD[2] == fieldcoord[2]);

                if (hAttacked == null)
                {
                    button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = true;
                    CurrentHero.COORD = fieldcoord;

                    if (CurrentHero.NAME == "Spirit")
                    {
                        foreach (var n in inj)
                        {
                            n.FlatAppearance.BorderColor = Color.Black;
                            n.FlatAppearance.BorderSize = 1;
                        }

                        Inj = false;
                        inj.Clear();
                    }
                    else if (CurrentHero.NAME == "Metamorph")
                    {
                        foreach (var n in injMet)
                        {
                            n.FlatAppearance.BorderColor = Color.Black;
                            n.FlatAppearance.BorderSize = 1;
                        }

                        InjMet = false;
                        inj.Clear();
                    }
                    foreach (var n in skverna)
                    {
                        n.FlatAppearance.BorderColor = Color.Green;
                        n.FlatAppearance.BorderSize = 5;
                    }

                    Refresh();
                    ZoneAbilities();
                }
                else
                    return;
            }
            if (CurrentHero.STATUS.MAGICSHIELD[0] == 1)
            {
                ClearDebaff(CurrentHero.INITIATIV);
            }

            if (Mode != "Дух ветра" && ((CurrentHero.NAME != "Metamorph" && SmokeMet != true) || (CurrentHero.NAME != "Thief" && Smoke != true)))
                Mode = "...";
            ModeView();
            ViewAction();
        }

        private void ViewAction()
        {
            foreach (var n in Hex)
            {
                n.FlatAppearance.BorderSize = 1;
                n.FlatAppearance.BorderColor = Color.Black;
            }
            if (skvernaTurn != 0)
            {
                foreach (var n in skverna)
                {
                    n.FlatAppearance.BorderSize = 5;
                    n.FlatAppearance.BorderColor = Color.Brown;
                }
            }

            int border = 5;
            if (Mode == "Атака" || Mode == "Дублирование" || Mode == "Шинковка" || Mode == "Казнь" || Mode == "Двойное сечение" || Mode == "Провокация" || Mode == "Кровопускание" || Mode == "Кульбит" || Mode == "Отравленный клинок" || Mode == "Дымовая бомба" || Mode == "Активная броня")
            {
                foreach (var n in heroes)
                {
                    string name = "b";
                    name += n.COORD[0].ToString();
                    name += n.COORD[1].ToString();
                    name += n.COORD[2].ToString();
                    name = name.Replace("-", "m");

                    Button b = Hex.Find(x => x.Name == name);
                    if (b != null && Dist(CurrentHero.COORD, n.COORD) <= CurrentHero.RANGE && CurrentHero.TEAM != n.TEAM)
                    {
                        b.FlatAppearance.BorderSize = border;
                        b.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Взрыв стужи" || Mode == "Пригвоздить" || Mode == "Метка снайпера" || Mode == "Вакуум" || Mode == "Каррозия" || Mode == "Проклятие" || Mode == "Скоротечность" || Mode == "Ожоги" || Mode == "Адское клеймо" || Mode == "Онемение")
            {
                foreach (var n in heroes)
                {
                    string name = "b";
                    name += n.COORD[0].ToString();
                    name += n.COORD[1].ToString();
                    name += n.COORD[2].ToString();
                    name = name.Replace("-", "m");

                    Button b = Hex.Find(x => x.Name == name);
                    if (b != null && Dist(CurrentHero.COORD, n.COORD) <= 2 && CurrentHero.TEAM != n.TEAM)
                    {
                        b.FlatAppearance.BorderSize = border;
                        b.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Шиловидный болт" || Mode == "Наскок" || Mode == "Крюк" || Mode == "Каменный кулак" || Mode == "Выпад")
            {
                int y = 0;
                if (Mode == "Шиловидный болт")
                    y = 6;
                else
                    y = 2;
                foreach (var n in directions)
                {
                    int[] coord = new int[3] { CurrentHero.COORD[0], CurrentHero.COORD[1], CurrentHero.COORD[2] };
                    for (int i = 0; i < y; i++)
                    {
                        coord[0] += n[0];
                        coord[1] += n[1];
                        coord[2] += n[2];

                        string name = "b";
                        name += coord[0].ToString();
                        name += coord[1].ToString();
                        name += coord[2].ToString();
                        name = name.Replace("-", "m");

                        Button b = Hex.Find(x => x.Name == name);
                        if (b != null)
                        {
                            b.FlatAppearance.BorderSize = border;
                            b.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
            }
            else if (Mode == "Цепная молния")
            {
                foreach (var n in heroes)
                {
                    string name = "b";
                    name += n.COORD[0].ToString();
                    name += n.COORD[1].ToString();
                    name += n.COORD[2].ToString();
                    name = name.Replace("-", "m");

                    Button b = Hex.Find(x => x.Name == name);
                    if (b != null)
                    {
                        b.FlatAppearance.BorderSize = border;
                        b.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Рокировка" || Mode == "Магический выброс" || Mode == "Очищение" || Mode == "Дух воды")
            {
                int y = 0;
                if (Mode == "Дух воды")
                    y = 1;
                else
                    y = 2;
                foreach (var n in heroes)
                {
                    string name = "b";
                    name += n.COORD[0].ToString();
                    name += n.COORD[1].ToString();
                    name += n.COORD[2].ToString();
                    name = name.Replace("-", "m");

                    Button b = Hex.Find(x => x.Name == name);
                    if (b != null && Dist(CurrentHero.COORD, n.COORD) <= y)
                    {
                        b.FlatAppearance.BorderSize = border;
                        b.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Исцеление" || Mode == "Воодушевление" || Mode == "Протекторат" || Mode == "Клятва верности" || Mode == "Благословение" || Mode == "Милость Господа" || Mode == "Геопанцирь" || Mode == "Криостазис" || Mode == "Магический щит" || Mode == "Кровь камня" || Mode == "Внедрение")
            {
                foreach (var n in heroes)
                {
                    string name = "b";
                    name += n.COORD[0].ToString();
                    name += n.COORD[1].ToString();
                    name += n.COORD[2].ToString();
                    name = name.Replace("-", "m");

                    Button b = Hex.Find(x => x.Name == name);
                    if (b != null && Dist(CurrentHero.COORD, n.COORD) <= 2 && CurrentHero.TEAM == n.TEAM)
                    {
                        b.FlatAppearance.BorderSize = border;
                        b.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Удар щупальцем" || Mode == "Кара Божья" || Mode == "Небесный кулак")
            {
                foreach (var n in heroes)
                {
                    string name = "b";
                    name += n.COORD[0].ToString();
                    name += n.COORD[1].ToString();
                    name += n.COORD[2].ToString();
                    name = name.Replace("-", "m");

                    Button b = Hex.Find(x => x.Name == name);
                    if (b != null && Dist(CurrentHero.COORD, n.COORD) <= 2 && CurrentHero.TEAM != n.TEAM)
                    {
                        b.FlatAppearance.BorderSize = border;
                        b.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Иссушение")
            {
                foreach (var n in heroes)
                {
                    string name = "b";
                    name += n.COORD[0].ToString();
                    name += n.COORD[1].ToString();
                    name += n.COORD[2].ToString();
                    name = name.Replace("-", "m");

                    Button b = Hex.Find(x => x.Name == name);
                    if (b != null && Dist(CurrentHero.COORD, n.COORD) <= 1 && CurrentHero.TEAM != n.TEAM)
                    {
                        b.FlatAppearance.BorderSize = border;
                        b.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Оборона" || Mode == "Вихрь топоров" || Mode == "Запугивание")
            {
                int y = 1;

                foreach (var n in directions)
                {
                    int[] coord = new int[3] { CurrentHero.COORD[0], CurrentHero.COORD[1], CurrentHero.COORD[2] };
                    for (int i = 0; i < y; i++)
                    {
                        coord[0] += n[0];
                        coord[1] += n[1];
                        coord[2] += n[2];

                        string name = "b";
                        name += coord[0].ToString();
                        name += coord[1].ToString();
                        name += coord[2].ToString();
                        name = name.Replace("-", "m");

                        Button b = Hex.Find(x => x.Name == name);
                        if (b != null)
                        {
                            b.FlatAppearance.BorderSize = border;
                            b.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
            }
            else if (Mode == "Обнуление")
            {
                foreach (var n in heroes)
                {
                    string name = "b";
                    name += n.COORD[0].ToString();
                    name += n.COORD[1].ToString();
                    name += n.COORD[2].ToString();
                    name = name.Replace("-", "m");

                    Button b = Hex.Find(x => x.Name == name);
                    if (b != null && Dist(CurrentHero.COORD, n.COORD) == 1 && CurrentHero.TEAM == n.TEAM)
                    {
                        b.FlatAppearance.BorderSize = border;
                        b.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Сделка с дьяволом")
            {
                foreach (var n in heroes)
                {
                    string name = "b";
                    name += n.COORD[0].ToString();
                    name += n.COORD[1].ToString();
                    name += n.COORD[2].ToString();
                    name = name.Replace("-", "m");

                    Button b = Hex.Find(x => x.Name == name);
                    if (b != null && Dist(CurrentHero.COORD, n.COORD) == 1)
                    {
                        b.FlatAppearance.BorderSize = border;
                        b.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Столоктит" || Mode == "Иллюзия" || Mode == "Крылья" || Mode == "Поднятие трупа" || Mode == "Движение")
            {
                int y = 0;
                if (Mode == "Поднятие трупа" || Mode == "Движение")
                    y = 1;
                else
                    y = 2;

                foreach (var n in Hex)
                {
                    int[] coord = new int[3];
                    coord = Coordinata(n.Name.Replace("m", "-"));
                    Hero h = heroes.Find(x => x.COORD[0] == coord[0] && x.COORD[1] == coord[1] && x.COORD[2] == coord[2]);
                    if (Dist(CurrentHero.COORD, coord) <= y && h == null)
                    {
                        n.FlatAppearance.BorderSize = border;
                        n.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "Жертвоприношение" || Mode == "Марш" || Mode == "Приказ")
            {
                foreach (var n in heroes)
                {
                    string name = "b";
                    name += n.COORD[0].ToString();
                    name += n.COORD[1].ToString();
                    name += n.COORD[2].ToString();
                    name = name.Replace("-", "m");

                    Button b = Hex.Find(x => x.Name == name);
                    if (b != null && Dist(CurrentHero.COORD, n.COORD) <= 2 && Dist(CurrentHero.COORD, n.COORD) >= 1 && CurrentHero.TEAM == n.TEAM)
                    {
                        b.FlatAppearance.BorderSize = border;
                        b.FlatAppearance.BorderColor = Color.DarkGreen;
                    }
                }
            }
            else if (Mode == "В яблочко!")
            {
                foreach (var n in directions)
                {
                    int[] coord = new int[3] { CurrentHero.COORD[0], CurrentHero.COORD[1], CurrentHero.COORD[2] };
                    coord[0] += n[0];
                    coord[1] += n[1];
                    coord[2] += n[2];
                    for (int i = 0; i < 2; i++)
                    {
                        coord[0] += n[0];
                        coord[1] += n[1];
                        coord[2] += n[2];

                        string name = "b";
                        name += coord[0].ToString();
                        name += coord[1].ToString();
                        name += coord[2].ToString();
                        name = name.Replace("-", "m");

                        Button b = Hex.Find(x => x.Name == name);
                        if (b != null)
                        {
                            b.FlatAppearance.BorderSize = border;
                            b.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
            }
            else if (Mode == "Прорыв")
            {
                if (replace == null)
                {
                    foreach (var n in heroes)
                    {
                        string name = "b";
                        name += n.COORD[0].ToString();
                        name += n.COORD[1].ToString();
                        name += n.COORD[2].ToString();
                        name = name.Replace("-", "m");

                        Button b = Hex.Find(x => x.Name == name);
                        if (b != null && CurrentHero.TEAM != n.TEAM)
                        {
                            b.FlatAppearance.BorderSize = border;
                            b.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
                else
                {
                    foreach (var n in Hex)
                    {
                        int[] coord = new int[3];
                        coord = Coordinata(n.Name.Replace("m", "-"));
                        Hero h = heroes.Find(x => x.COORD[0] == coord[0] && x.COORD[1] == coord[1] && x.COORD[2] == coord[2]);
                        if (Dist(replace.COORD, coord) <= 1 && h == null)
                        {
                            n.FlatAppearance.BorderSize = border;
                            n.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
            }
            else if (Mode == "Ангел хранитель")
            {
                if (replace == null)
                {
                    foreach (var n in heroes)
                    {
                        string name = "b";
                        name += n.COORD[0].ToString();
                        name += n.COORD[1].ToString();
                        name += n.COORD[2].ToString();
                        name = name.Replace("-", "m");

                        Button b = Hex.Find(x => x.Name == name);
                        if (b != null && CurrentHero.TEAM == n.TEAM && CurrentHero.NAME != n.NAME)
                        {
                            b.FlatAppearance.BorderSize = border;
                            b.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
                else
                {
                    foreach (var n in Hex)
                    {
                        int[] coord = new int[3];
                        coord = Coordinata(n.Name.Replace("m", "-"));
                        Hero h = heroes.Find(x => x.COORD[0] == coord[0] && x.COORD[1] == coord[1] && x.COORD[2] == coord[2]);
                        if (Dist(replace.COORD, coord) <= 1 && h == null)
                        {
                            n.FlatAppearance.BorderSize = border;
                            n.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
            }
            else if (Mode == "Дух ветра")
            {
                if (replace == null)
                {
                    foreach (var n in heroes)
                    {
                        string name = "b";
                        name += n.COORD[0].ToString();
                        name += n.COORD[1].ToString();
                        name += n.COORD[2].ToString();
                        name = name.Replace("-", "m");

                        Button b = Hex.Find(x => x.Name == name);
                        Hero h = spiritofwind.Find(x => x.INITIATIV == n.INITIATIV);
                        if (b != null && CurrentHero.NAME != n.NAME && h == null && Dist(CurrentHero.COORD, n.COORD) <= 2)
                        {
                            b.FlatAppearance.BorderSize = border;
                            b.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
                else
                {
                    foreach (var n in Hex)
                    {
                        int[] coord = new int[3];
                        coord = Coordinata(n.Name.Replace("m", "-"));
                        Hero h = heroes.Find(x => x.COORD[0] == coord[0] && x.COORD[1] == coord[1] && x.COORD[2] == coord[2]);
                        if (Dist(replace.COORD, coord) <= 1 && h == null)
                        {
                            n.FlatAppearance.BorderSize = border;
                            n.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
            }
            else if (Mode == "Апперкот")
            {
                if (replace == null)
                {
                    foreach (var n in heroes)
                    {
                        string name = "b";
                        name += n.COORD[0].ToString();
                        name += n.COORD[1].ToString();
                        name += n.COORD[2].ToString();
                        name = name.Replace("-", "m");

                        Button b = Hex.Find(x => x.Name == name);
                        if (b != null && CurrentHero.TEAM != n.TEAM && Dist(CurrentHero.COORD, n.COORD) == 1)
                        {
                            b.FlatAppearance.BorderSize = border;
                            b.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
                else
                {
                    foreach (var n in Hex)
                    {
                        int[] coord = new int[3];
                        coord = Coordinata(n.Name.Replace("m", "-"));
                        Hero h = heroes.Find(x => x.COORD[0] == coord[0] && x.COORD[1] == coord[1] && x.COORD[2] == coord[2]);
                        if (Dist(CurrentHero.COORD, coord) <= 2 && h == null)
                        {
                            n.FlatAppearance.BorderSize = border;
                            n.FlatAppearance.BorderColor = Color.DarkGreen;
                        }
                    }
                }
            }
            else if (Mode == "Возмездие" || Mode == "Могильный холод" || Mode == "Временная петля" || Mode == "Лечение ран" || Mode == "Дух огня" || Mode == "Бесплотность")
            {
                string name = "b";
                name += CurrentHero.COORD[0].ToString();
                name += CurrentHero.COORD[1].ToString();
                name += CurrentHero.COORD[2].ToString();
                name = name.Replace("-", "m");

                Button b = Hex.Find(x => x.Name == name);
                if (b != null && CurrentHero.TEAM == "B")
                {
                    b.FlatAppearance.BorderSize = border;
                    b.FlatAppearance.BorderColor = Color.DarkBlue;
                }
                else
                {
                    b.FlatAppearance.BorderSize = border;
                    b.FlatAppearance.BorderColor = Color.DarkRed;
                }
            }

            Description();

        }
        private void Description()
        {
            if (Mode == "...")
            {
                ActionDescription.Text = "Ваши действия";
            }
            /*else if (Mode == "Атака" || Mode == "Иссушение" || Mode == "Удар щупальцем" || Mode == "Кара Божья" || Mode == "Небесный кулак" || Mode == "Благословение" || Mode == "Милость Господа" || Mode == "Геопанцирь" || Mode == "Криостазис" || Mode == "Магический щит" || Mode == "Кровь камня" || Mode == "Исцеление" || Mode == "Цепная молния" || Mode == "Каррозия" || Mode == "Проклятие" || Mode == "Скоротечность" || Mode == "Ожоги" || Mode == "Адское клеймо" || Mode == "Финал" || Mode == "Пригвоздить" || Mode == "Метка снайпера" || Mode == "Наскок" || Mode == "Крюк" || Mode == "Каменный кулак" || Mode == "Выпад" || Mode == "Шинковка" || Mode == "Казнь" || Mode == "Двойное сечение" || Mode == "Кровопускание" || Mode == "Кульбит" || Mode == "Отравленный клинок")
            {
                ActionDescription.Text = "Выберите цель";
            }*/
            else if (Mode == "Движение" || Mode == "Столоктит" || Mode == "Иллюзия" || Mode == "Крылья" || Mode == "Поднятие трупа")
            {
                ActionDescription.Text = "Выберите свободную клетку";
            }
            else if (Mode == "Провокация" || Mode == "Возмездие" || Mode == "Могильный холод" || Mode == "Временная петля" || Mode == "Лечение ран" || Mode == "Дух огня" || Mode == "Марш" || Mode == "Приказ" || Mode == "Оборона" || Mode == "Вихрь топоров" || Mode == "Запугивание" || Mode == "Триумф" || Mode == "Воодушевление" || Mode == "Протекторат" || Mode == "Магический выброс" || Mode == "Очищение" || Mode == "Дух воды" || Mode == "Вакуум" || Mode == "Взрыв стужи" || Mode == "Дымовая бомба" || Mode == "Активная броня")
            {
                ActionDescription.Text = "Нажмите, чтобы применить способность";
            }
            else if (Mode == "Залп" || Mode == "Шиловидный болт" || Mode == "Воспламенение")
            {
                ActionDescription.Text = "Выберите направление";
            }
            else if (Mode == "Рокировка")
            {
                if (replace == null)
                    ActionDescription.Text = "Выберите цель №1";
                else
                    ActionDescription.Text = "Выберите цель №2";
            }
            else if (Mode == "Дух ветра")
            {
                if (replace == null)
                    ActionDescription.Text = "Выберите цель (" + SpiritOfWind.ToString() + "/3)";
                else
                    ActionDescription.Text = "Выберите свободную клетку (" + SpiritOfWind.ToString() + "/3)";
            }
            else if (Mode == "Прорыв" || Mode == "Ангел хранитель" || Mode == "Апперкот")
            {
                if (replace == null)
                    ActionDescription.Text = "Выберите цель";
                else
                    ActionDescription.Text = "Выберите свободную клетку";
            }
            else if (Mode == "Скверна")
            {
                ActionDescription.Text = "Выберите две соседние клетки";
            }
            else
            {
                ActionDescription.Text = "Выберите цель";
            }

            ActionDescription.Location = new Point(952 - ActionDescription.Width / 2, 779);
        }
        private void ClearDebaff(int init)
        {
            Hero hAttacked = heroes.Find(x => x.INITIATIV == init);

            if (hAttacked != null)
            {
                hAttacked.STATUS.METKA = new int[2] { 0, 0 };
                hAttacked.STATUS.ROOTS = false;
                hAttacked.STATUS.SILENCE = false;
                hAttacked.STATUS.STUN = false;
                hAttacked.STATUS.ANGRY = false;
                hAttacked.STATUS.CORROSIVE = new int[] { 0, 0 };
                hAttacked.STATUS.CURSE = false;
                hAttacked.STATUS.SOULBURN = new int[] { 0, 0 };
                hAttacked.STATUS.WEEKNESS = false;
                hAttacked.STATUS.BLEED = new int[] { 0, 0 };
                hAttacked.STATUS.POISON = false;
                hAttacked.STATUS.BURN = new double[] { 0, 1 };
                hAttacked.STATUS.HELLSIGN = new int[] { 0, 0 };

                StatusView();
            }
        }

        private void Attack(int[] entitypos, int[] attackpos)
        {
            //MessageBox.Show("Бабах!"); 
            bool possible = true;
            Hero h = new Hero();
            try
            {
                h = heroes.Find(x => x.Target(attackpos) == true);
            }
            catch
            {
                possible = false;
            }

            if (h == null)
                possible = false;
            else if (CurrentHero.TEAM == heroes.Find(x => x.Target(attackpos) == true).TEAM)
                possible = false;

            if (possible == true)
            {
                PlaySoundAttack();
                CheckPassive();
                Hero hAttacked = heroes.Find(x => x.Target(attackpos) == true);
                //HitView(hAttacked.COORD, true);
                Random rnd = new Random();
                int extraarmor = 0;
                GuardianPassive(hAttacked);
                TemporariumPassive = false;
                if (hAttacked.STATUS.ARMORED == true || hAttacked.STATUS.ARMORED2 == true)
                    extraarmor = 3;
                if (hAttacked.NAME == "Knight" && Dist(hAttacked.COORD, entitypos) > 1)
                    hAttacked.STATUS.ARMOR += 3;
                if (CurrentHero.NAME == "Bard")
                    CurrentHero.MANA += 1;

                int damage = 0;
                if (CurrentHero.STATUS.ARMORPIERCING == true || hAttacked.STATUS.CORROSIVE[0] == 1 || hAttacked.STATUS.SOULBURN[0] == 1)
                    damage = rnd.Next(CurrentHero.DMG + CurrentHero.STATUS.DMGPASSIVE + CurrentHero.STATUS.DMG - CurrentHero.RAZBROS, CurrentHero.DMG + CurrentHero.STATUS.DMGPASSIVE + CurrentHero.STATUS.DMG + CurrentHero.RAZBROS + 1);
                else
                    damage = rnd.Next(CurrentHero.DMG + CurrentHero.STATUS.DMGPASSIVE + CurrentHero.STATUS.DMG - CurrentHero.RAZBROS, CurrentHero.DMG + CurrentHero.STATUS.DMGPASSIVE + CurrentHero.STATUS.DMG + CurrentHero.RAZBROS + 1) - hAttacked.ARMOR - extraarmor - hAttacked.STATUS.ARMOR - hAttacked.STATUS.ARMORPASSIVE - hAttacked.STATUS.BLESS[3];

                if (damage < 0)
                    damage = 0;
                CurrentHero.HODB -= 1;
                RefreshAP();
                if (hAttacked.STATUS.METKA[0] == 1)
                    damage += 8;
                if (CurrentHero.NAME == "Cultist" && (hAttacked.STATUS.METKA[0] == 1 || hAttacked.STATUS.ANGRY == true || hAttacked.STATUS.BLEED[0] == 1 || hAttacked.STATUS.CORROSIVE[0] == 1 || hAttacked.STATUS.CURSE == true || hAttacked.STATUS.ROOTS == true || hAttacked.STATUS.SILENCE == true || hAttacked.STATUS.SOULBURN[0] == 1 || hAttacked.STATUS.STUN == true || hAttacked.STATUS.WEEKNESS == true || hAttacked.STATUS.POISON == true || hAttacked.STATUS.HELLSIGN[0] == 1 || hAttacked.STATUS.BURN[0] == 1))
                    damage += 10;

                if (CurrentHero.STATUS.POWER == true)
                {
                    damage = Convert.ToInt32(Convert.ToDouble(damage * 1.3));
                }
                if (CurrentHero.STATUS.CURSE == true || CurrentHero.STATUS.POISON == true)
                {
                    damage = Convert.ToInt32(Convert.ToDouble(damage * 0.6));
                }
                if (hAttacked.NAME == "Cryomant" && Dist(hAttacked.COORD, entitypos) == 1)
                    damage = Convert.ToInt32(Convert.ToDouble(damage * 0.8));

                if (hAttacked.NAME == "Knight" && Dist(hAttacked.COORD, entitypos) > 1)
                    hAttacked.STATUS.ARMOR -= 3;

                if (CurrentHero.ABILITIES[0] == "Инстинкт убийцы")
                {
                    int dop = (hAttacked.MAXHP - hAttacked.HP) / 15;
                    damage += dop;
                }
                if (CurrentHero.STATUS.BURN[0] == 1)
                {
                    damage = Convert.ToInt32(Convert.ToDouble(damage) * CurrentHero.STATUS.BURN[1]);
                }

                if (hAttacked.STATUS.UNFLESH[0] == 1)
                    damage = damage/2;

                if (hAttacked.STATUS.CRYOSTASYS[0] == 1)
                    damage = 0;

                if (hAttacked.STATUS.PROTECTORED == false)
                {
                    if (damage > 0)
                        hAttacked.STATUS.REGENERATION = false;
                    hAttacked.HP -= damage;

                    listBox1.Items.Add(CurrentHero.NAME + " атакует!");
                    listBox1.Items.Add(h.NAME + " теряет " + damage + " ХП!");

                    if (CurrentHero.ABILITIES[0] == "Вампиризм")
                    {
                        int y = Convert.ToInt32(damage * 0.35);
                        CurrentHero.HP += y;
                        HPplus(CurrentHero, y);
                        listBox1.Items.Add(CurrentHero.NAME + " восстанавливает " + y + " хп");
                    }

                    if (hAttacked.STATUS.DEATHBREATH[0] == 1)
                    {
                        int d = damage / 2;
                        if (d > 0)
                            CurrentHero.STATUS.REGENERATION = false;
                        CurrentHero.HP -= d;
                        RefreshHP();
                        HPminus(CurrentHero, d);
                        listBox1.Items.Add(CurrentHero.NAME + " теряет " + d + " хп от могильного холода!");
                        if (CurrentHero.HP < 1 && (CurrentHero.NAME != "Angel" || AngelPassive == true))
                        {
                            CurrentHero.COORD = new int[] { 5, 5, 5 };
                            button6.PerformClick();
                            CurrentHero.STATUS.DMG = 0;
                            if (CurrentHero.STATUS.BLESS[0] == 1)
                                CurrentHero.STATUS.DMG += CurrentHero.STATUS.BLESS[2];
                        }
                        else
                            AngelPassivka();
                    }
                    //MessageBox.Show("Нанесено " + damage + " урона!");
                    if (hAttacked.HP < 1 && (hAttacked.NAME != "Angel" || AngelPassive == true))
                    {
                        AngryDead(hAttacked);
                        Hero m = heroes.Find(x => x.NAME == hAttacked.NAME && x.INITIATIV == hAttacked.INITIATIV);
                        m.COORD = new int[] { 5, 5, 5 };
                        //heroes.RemoveAll(x => x.NAME == hAttacked.NAME);
                        listBox1.Items.Add(h.NAME + " погибает!!! ");
                        Refresh();
                        if (CurrentHero.NAME == "Butcher" && hAttacked.NAME != "Stoloktit" && hAttacked.NAME != "Zombie")
                        {
                            ButcherPassive = true;
                            CheckPassive();
                        }
                    }
                    else if (hAttacked.HP < 1 && hAttacked.NAME == "Angel" && AngelPassive == false)
                    {
                        AngelPassivka();
                        if (CurrentHero.NAME == "Butcher" && hAttacked.NAME != "Stoloktit" && hAttacked.NAME != "Zombie")
                        {
                            ButcherPassive = true;
                            CheckPassive();
                        }
                    }
                }
                else
                {
                    int dmg = Convert.ToInt32(Convert.ToDouble(damage / 2));
                    hAttacked.HP -= dmg;
                    Hero hero = heroes.Find(x => x.STATUS.PROTECT[0] == 1 && x.TEAM != CurrentHero.TEAM);

                    if (hAttacked.STATUS.DEATHBREATH[0] == 1)
                    {
                        int d = dmg / 2;
                        if (d > 0)
                            CurrentHero.STATUS.REGENERATION = false;
                        CurrentHero.HP -= d;
                        RefreshHP();
                        HPminus(CurrentHero, d);
                        listBox1.Items.Add(CurrentHero.NAME + " теряет " + d + " хп от могильного холода!");
                        if (CurrentHero.HP < 1 && (CurrentHero.NAME != "Angel" || AngelPassive == true))
                        {
                            CurrentHero.COORD = new int[] { 5, 5, 5 };
                            button6.PerformClick();
                            if (CurrentHero.STATUS.BLESS[0] == 1)
                                CurrentHero.STATUS.DMG += CurrentHero.STATUS.BLESS[2];
                        }
                        else
                        {
                            AngelPassivka();
                        }
                    }

                    if (CurrentHero.ABILITIES[0] == "Вампиризм")
                    {
                        int y = dmg / 2;
                        CurrentHero.HP += y;
                        HPplus(CurrentHero, y);
                        listBox1.Items.Add(CurrentHero.NAME + " восстанавливает " + y + " хп");
                    }

                    if (hero != null)
                    {
                        hero.HP -= dmg;
                        if (dmg > 0)
                            hero.STATUS.REGENERATION = false;
                        listBox1.Items.Add(hero.NAME + " теряет " + dmg + " ХП!");
                        //MessageBox.Show("Нанесено " + damage + " урона!");

                        if (hero.HP < 1)
                        {
                            AngryDead(hero);
                            Hero m = heroes.Find(x => x.NAME == hero.NAME && x.INITIATIV == hero.INITIATIV);
                            m.COORD = new int[] { 5, 5, 5 };
                            //heroes.RemoveAll(x => x.NAME == hAttacked.NAME);
                            listBox1.Items.Add(hero.NAME + " погибает!!! ");
                            m.STATUS.PROTECT[0] = 0;
                            ZoneAbilities();
                            Refresh();
                        }
                    }
                    listBox1.Items.Add(CurrentHero.NAME + " атакует!");
                    listBox1.Items.Add(h.NAME + " теряет " + dmg + " ХП!");
                    if (hAttacked.HP < 1 && (hAttacked.NAME != "Angel" || AngelPassive == true))
                    {
                        AngryDead(hAttacked);
                        Hero m = heroes.Find(x => x.NAME == hAttacked.NAME && x.INITIATIV == hAttacked.INITIATIV);
                        m.COORD = new int[] { 5, 5, 5 };
                        //heroes.RemoveAll(x => x.NAME == hAttacked.NAME);
                        listBox1.Items.Add(h.NAME + " погибает!!! ");
                        Refresh();
                        if (CurrentHero.NAME == "Butcher" && hAttacked.NAME != "Stoloktit" && hAttacked.NAME != "Zombie")
                        {
                            ButcherPassive = true;
                            CheckPassive();
                        }
                    }
                    else if (hAttacked.HP < 1 && hAttacked.NAME == "Angel" && AngelPassive == false)
                    {
                        AngelPassivka();
                        if (CurrentHero.NAME == "Butcher" && hAttacked.NAME != "Stoloktit" && hAttacked.NAME != "Zombie")
                        {
                            ButcherPassive = true;
                            CheckPassive();
                        }
                    }
                }

                RefreshHP();
                if (Mode == "Шинковка" && countAttack < 4)
                {
                    if (hAttacked.STATUS.PROTECTORED == false)
                        dmgCount += damage;
                    else
                    {
                        dmgCount += damage / 2;
                        dmgProtect += damage / 2;
                    }
                    countAttack++;
                }
                else if (Mode == "Двойное сечение" && countAttack < 3)
                {
                    if (hAttacked.STATUS.PROTECTORED == false)
                        dmgCount += damage;
                    else
                    {
                        dmgCount += damage / 2;
                        dmgProtect += damage / 2;
                    }
                    countAttack++;
                }



                if (Mode == "Двойное сечение" && countAttack == 3)
                {
                    if (hAttacked.STATUS.PROTECTORED == false)
                    {
                        HPminus(hAttacked, dmgCount);
                    }
                    else
                    {
                        HPminus(hAttacked, dmgCount / 2);
                        Hero hero = heroes.Find(x => x.STATUS.PROTECT[0] == 1 && x.TEAM != CurrentHero.TEAM);
                        if (hero != null)
                            HPminus(hero, dmgCount / 2);
                    }
                    countAttack = 1;
                    dmgCount = 0;
                }
                else if (Mode == "Шинковка" && countAttack == 4)
                {
                    if (hAttacked.STATUS.PROTECTORED == false)
                    {
                        HPminus(hAttacked, dmgCount);
                    }
                    else
                    {
                        HPminus(hAttacked, dmgCount / 2);
                        Hero hero = heroes.Find(x => x.STATUS.PROTECT[0] == 1 && x.TEAM != CurrentHero.TEAM);
                        if (hero != null)
                            HPminus(hero, dmgCount / 2);
                    }
                    countAttack = 1;
                    dmgCount = 0;
                }
                else if (Mode != "Шинковка" && Mode != "Двойное сечение")
                {
                    if (hAttacked.STATUS.PROTECTORED == false)
                    {
                        HPminus(hAttacked, damage);
                    }
                    else
                    {
                        HPminus(hAttacked, damage / 2);
                        Hero hero = heroes.Find(x => x.STATUS.PROTECT[0] == 1 && x.TEAM != CurrentHero.TEAM);
                        if (hero != null)
                            HPminus(hero, damage / 2);
                    }
                    countAttack = 1;
                    dmgCount = 0;
                }
                StatusView();
                if(hAttacked.NAME == "Spirit" || hAttacked.STATUS.INJECT[0] == 1)
                {
                    CurrentHero.MAXHP -= 10;
                    if (CurrentHero.HP > CurrentHero.MAXHP)
                        CurrentHero.HP = CurrentHero.MAXHP;

                    if (CurrentHero.HP < 1 && (CurrentHero.NAME != "Angel" || AngelPassive == true))
                    {
                        AngryDead(CurrentHero);
                        //Hero m = heroes.Find(x => x.NAME == CurrentHero.NAME && x.INITIATIV == CurrentHero.INITIATIV);
                        CurrentHero.COORD = new int[] { 5, 5, 5 };
                        //heroes.RemoveAll(x => x.NAME == hAttacked.NAME);
                        listBox1.Items.Add(CurrentHero.NAME + " погибает от бессилия!!! ");
                        Refresh();                        
                    }
                    RefreshHP();
                }
            }
        }
        private void AngelPassivka()
        {
            Hero h = heroes.Find(x => x.NAME == "Angel");

            if (h != null && AngelPassive == false)
            {
                if (h.HP < 1)
                {
                    AngryDead(h);
                    ClearDebaff(h.INITIATIV);
                    h.STATUS.ARMORED = false;
                    h.STATUS.POWER = false;
                    if (h.STATUS.BLESS[0] == 1)
                    {
                        h.STATUS.DMG -= h.STATUS.BLESS[2];
                        //h.STATUS.ARMOR -= h.STATUS.BLESS[3];
                        h.STATUS.BLESS = new int[] { 0, 0, 0, 0 };
                    }
                    h.STATUS.DEATHBREATH = new int[] { 0, 0 };
                    if (h.STATUS.GEOSHIELD[0] == 1)
                    {
                        h.STATUS.ARMOR -= 7;
                        h.STATUS.GEOSHIELD = new int[] { 0, 0 };
                    }
                    h.STATUS.PROTECT = new int[] { 0, 0 };
                    h.STATUS.REGENERATION = false;
                    if (h.STATUS.ENCOURAGE[0] == 1)
                    {
                        h.STATUS.DMG -= h.STATUS.ENCOURAGE[1];
                        h.STATUS.ENCOURAGE = new int[] { 0, 0, 0 };
                    }
                    h.STATUS.BOOST = false;
                    h.STATUS.MAGICSHIELD = new int[] { 0, 0 };
                    h.STATUS.STONEBLOOD = new int[] { 0, 0 };
                    AngelPassive = true;
                    h.HP = h.MAXHP / 2;
                    StatusView();
                    RefreshHP();
                    Refresh();
                    listBox1.Items.Add(h.NAME + " возрождается!");

                    if (CurrentHero.NAME == "Butcher")
                    {
                        ButcherPassive = true;
                        CheckPassive();

                    }
                }

            }
        }
        private int Dist(int[] x0, int[] y0)
        {
            int[] x = new int[3];
            x[0] = x0[0];
            x[1] = x0[1];
            x[2] = x0[2];

            int[] y = new int[3];
            y[0] = y0[0];
            y[1] = y0[1];
            y[2] = y0[2];

            x[0] -= y[0];
            x[1] -= y[1];
            x[2] -= y[2];

            int d = 0;
            for (int i = 0; i < 3; i++)
            {
                if (Math.Abs(x[i]) >= d)
                    d = Math.Abs(x[i]);
            }
            return d;
        }

        private int[] Coordinata(string b)
        {
            string bbb = b;
            bbb = bbb.Remove(0, 1);
            int[] Coord = new int[3];
            int g = 0;
            string sostav = "";

            for (int i = 0; i < bbb.Length; i++)
            {
                if (bbb[i] != '-')
                {
                    sostav += bbb[i];
                    Coord[g] = Convert.ToInt32(sostav);
                    sostav = "";
                    g++;
                }
                else
                    sostav += '-';

            }

            return Coord;
        }

        private void ZoneAbilities()
        {
            Hero hB = heroes.Find(x => x.STATUS.ARMORED == true && x.TEAM == "B");
            if (hB == null)
            {
                foreach (var n in heroes)
                {
                    if (n.TEAM == "B")
                        n.STATUS.ARMORED2 = false;
                }
                StatusView();
            }

            Hero hR = heroes.Find(x => x.STATUS.ARMORED == true && x.TEAM == "R");
            if (hB == null)
            {
                foreach (var n in heroes)
                {
                    if (n.TEAM == "R")
                        n.STATUS.ARMORED2 = false;
                }
                StatusView();
            }
            foreach (var n in heroes)
                n.STATUS.ARMORED2 = n.STATUS.PROTECTORED = false;

            foreach (var n in heroes)
            {
                if (n.STATUS.PROTECT[0] == 1)
                {
                    foreach (var m in heroes)
                    {
                        if (Dist(m.COORD, n.COORD) <= 2 && m.TEAM == n.TEAM && m.NAME != n.NAME)
                            m.STATUS.PROTECTORED = true;
                        else if (m.TEAM == n.TEAM && m.NAME != n.NAME)
                            m.STATUS.PROTECTORED = false;
                    }
                    StatusView();
                }
                if (n.STATUS.ARMORED == true)
                {
                    foreach (var m in heroes)
                    {
                        if (Dist(m.COORD, n.COORD) <= 1 && m.TEAM == n.TEAM && m.NAME != n.NAME)
                            m.STATUS.ARMORED2 = true;
                        else if (m.TEAM == n.TEAM && m.NAME != n.NAME)
                            m.STATUS.ARMORED2 = false;
                    }
                    StatusView();
                }

            }

            foreach (var n in heroes)
            {
                if (n.STATUS.ANGRY1[0] == 1 && n.NAME == "Illusion")
                {
                    foreach (var m in heroes)
                    {
                        if (Dist(n.COORD, m.COORD) == 1 && n.TEAM != m.TEAM && m.STATUS.MAGICSHIELD[0] != 1)
                        {
                            m.STATUS.ANGRY = true;
                            StatusView();
                            RefreshAP();
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mode = "Движение";
            ModeView();
            ViewAction();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Mode = "Атака";
            ModeView();
            ViewAction();
        }

        // 1 ABILKA
        private void button3_Click_1(object sender, EventArgs e)
        {
            Mode = CurrentHero.ABILITIES[1];
            ModeView();
            ViewAction();
        }

        private void RefreshAP()
        {
            if (CurrentHero.HODB < 1 || CurrentHero.STATUS.WEEKNESS == true)
                button2.Enabled = false;
            else
                button2.Enabled = true;

            if ((CurrentHero.HODM < 1 && CurrentHero.HODB < 1) || CurrentHero.STATUS.ROOTS == true)
                button1.Enabled = false;
            else
                button1.Enabled = true;

            int haveAP = CurrentHero.HODB * 2;
            if (haveAP < 0)
                haveAP = 0;
            if (CurrentHero.HODM > 0)
                haveAP += 1;

            if (CurrentHero.NEEDAP[0] > haveAP || CurrentHero.NOWKD[0] > 0 || (CurrentHero.STATUS.WEEKNESS == true && CurrentHero.ATTACKABILITIES[0] == true))
                button3.Enabled = false;
            else
                button3.Enabled = true;

            if (CurrentHero.NEEDAP[1] > haveAP || CurrentHero.NOWKD[1] > 0 || (CurrentHero.STATUS.WEEKNESS == true && CurrentHero.ATTACKABILITIES[1] == true))
                button4.Enabled = false;
            else
                button4.Enabled = true;

            if (CurrentHero.NEEDAP[2] > haveAP || CurrentHero.NOWKD[2] > 0 || (CurrentHero.STATUS.WEEKNESS == true && CurrentHero.ATTACKABILITIES[2] == true))
                button5.Enabled = false;
            else
                button5.Enabled = true;

            if (CurrentHero.ABILITIES[2] == "Наскок" && CurrentHero.STATUS.ROOTS == true)
                button4.Enabled = false;
            if (CurrentHero.ABILITIES[1] == "Крылья" && CurrentHero.STATUS.ROOTS == true)
                button3.Enabled = false;
            if (CurrentHero.ABILITIES[1] == "Кульбит" && CurrentHero.STATUS.ROOTS == true)
                button3.Enabled = false;
            if (CurrentHero.ABILITIES[3] == "Дымовая бомба" && CurrentHero.STATUS.ROOTS == true)
                button5.Enabled = false;
            if (CurrentHero.ABILITIES[2] == "Прорыв" && CurrentHero.STATUS.ROOTS == true)
                button4.Enabled = false;
            if (CurrentHero.ABILITIES[3] == "Ангел хранитель" && CurrentHero.STATUS.ROOTS == true)
                button5.Enabled = false;


            panel7.Controls.Clear();
            if (CurrentHero.HODB > 0)
            {
                PictureBox pb = new PictureBox();
                if(CurrentHero.HODB > 1)
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("BAP2");
                else
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("BAP");
                pb.Size = new Size(48, 48);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                panel7.Controls.Add(pb);
                pb.Location = new Point(0, 0);
            }
            if (CurrentHero.HODM > 0)
            {
                for (int i = 0; i < CurrentHero.HODM; i++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("MAP");
                    pb.Size = new Size(48, 48);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    panel7.Controls.Add(pb);
                    pb.Location = new Point(0, (1 + i) * 48);
                }
            }

            if (CurrentHero.STATUS.SILENCE == true)
            {
                button3.Enabled = button4.Enabled = button5.Enabled = false;
            }
            if (CurrentHero.STATUS.ANGRY == true)
            {
                button3.Enabled = button4.Enabled = button5.Enabled = button1.Enabled = false;
                foreach (var n in Hex)
                {
                    string s = n.Name.Replace('m', '-');
                    int[] coord = new int[3];
                    coord = Coordinata(s);

                    Hero h = heroes.Find(x => x.COORD[0] == coord[0] && x.COORD[1] == coord[1] && x.COORD[2] == coord[2]);
                    if (h != null)
                    {
                        if (Dist(h.COORD, coord) <= CurrentHero.RANGE && h.STATUS.ANGRY1[0] != 1)
                            n.Enabled = false;
                    }
                }
            }
            else
            {
                foreach (var n in Hex)
                {
                    n.Enabled = true;
                }
            }

            if (CurrentHero.NOWKD[0] <= 0)
                labelAbKd1.Visible = false;
            else
            {
                labelAbKd1.Visible = true;
                labelAbKd1.Text = CurrentHero.NOWKD[0].ToString() + "⧖";
            }
            if (CurrentHero.NOWKD[1] <= 0)
                labelAbKd2.Visible = false;
            else
            {
                labelAbKd2.Visible = true;
                labelAbKd2.Text = CurrentHero.NOWKD[1].ToString() + "⧖";
            }
            if (CurrentHero.NOWKD[2] <= 0)
                labelAbKd3.Visible = false;
            else
            {
                labelAbKd3.Visible = true;
                labelAbKd3.Text = CurrentHero.NOWKD[2].ToString() + "⧖";
            }

            RefreshHP();
        }

        private void AngryDead(Hero hAngry)
        {
            if (hAngry.STATUS.ANGRY1[0] == 1 && hAngry.HP < 1)
            {
                hAngry.STATUS.ANGRY1[0] = 0;
                foreach (var n in heroes)
                {
                    if (Dist(n.COORD, hAngry.COORD) == 1 && n.TEAM != hAngry.TEAM && n.STATUS.ANGRY == true)
                        n.STATUS.ANGRY = false;
                }
            }
        }
        private void AngryMove(Hero hAngry)
        {
            if (hAngry.STATUS.ANGRY1[0] == 1)
            {
                foreach (var n in heroes)
                {
                    if (Dist(n.COORD, hAngry.COORD) != 1 && n.TEAM != hAngry.TEAM && n.STATUS.ANGRY == true)
                        n.STATUS.ANGRY = false;
                }
            }
        }
        private void PyromantPassive()
        {
            Hero h = heroes.Find(x => x.NAME == "Pyromant" && x.HP > 0);
            if (h != null && CurrentHero.NAME != "Pyromant")
            {
                if (Dist(h.COORD, CurrentHero.COORD) == 1)
                {
                    Hero hcur = CurrentHero;
                    CurrentHero = new Hero("Языки пламени", h.TEAM, 1, 9, 0, 0);
                    CurrentHero.COORD = h.COORD;
                    CurrentHero.STATUS.ARMORPIERCING = true;

                    Attack(CurrentHero.COORD, hcur.COORD);
                    CurrentHero = hcur;
                    RefreshAP();
                }
            }
        }
        private void RefreshHP()
        {
            double hp = (Convert.ToDouble(CurrentHero.HP) / Convert.ToDouble(CurrentHero.MAXHP)) * 300;
            pictureBox1.Size = new Size(Convert.ToInt32(hp), 32);
            labelHP.Text = Convert.ToString(CurrentHero.HP);

            foreach (var n in heroes)
            {
                if (n.HP > n.MAXHP)
                    n.HP = n.MAXHP;

                if (n.NAME == labelHname1.Text)
                {
                    if (n.HP < 1)
                        n.HP = 0;
                    hp = (Convert.ToDouble(n.HP) / Convert.ToDouble(n.MAXHP)) * 300;
                    pictureBoxHero1.Size = new Size(Convert.ToInt32(hp), 32);
                    labelHero1.Text = Convert.ToString(n.HP);
                }
                if (n.NAME == labelHname2.Text)
                {
                    if (n.HP < 1)
                        n.HP = 0;
                    hp = (Convert.ToDouble(n.HP) / Convert.ToDouble(n.MAXHP)) * 300;
                    pictureBoxHero2.Size = new Size(Convert.ToInt32(hp), 32);
                    labelHero2.Text = Convert.ToString(n.HP);
                }
                if (n.NAME == labelHname3.Text)
                {
                    if (n.HP < 1)
                        n.HP = 0;
                    hp = (Convert.ToDouble(n.HP) / Convert.ToDouble(n.MAXHP)) * 300;
                    pictureBoxHero3.Size = new Size(Convert.ToInt32(hp), 32);
                    labelHero3.Text = Convert.ToString(n.HP);
                }
                if (n.NAME == labelHname4.Text)
                {
                    if (n.HP < 1)
                        n.HP = 0;
                    hp = (Convert.ToDouble(n.HP) / Convert.ToDouble(n.MAXHP)) * 300;
                    pictureBoxHero4.Size = new Size(Convert.ToInt32(hp), 32);
                    labelHero4.Text = Convert.ToString(n.HP);
                }
                if (n.NAME == labelHname5.Text)
                {
                    if (n.HP < 1)
                        n.HP = 0;
                    hp = (Convert.ToDouble(n.HP) / Convert.ToDouble(n.MAXHP)) * 300;
                    pictureBoxHero5.Size = new Size(Convert.ToInt32(hp), 32);
                    labelHero5.Text = Convert.ToString(n.HP);
                }
                if (n.NAME == labelHname6.Text)
                {
                    if (n.HP < 1)
                        n.HP = 0;
                    hp = (Convert.ToDouble(n.HP) / Convert.ToDouble(n.MAXHP)) * 300;
                    pictureBoxHero6.Size = new Size(Convert.ToInt32(hp), 32);
                    labelHero6.Text = Convert.ToString(n.HP);
                }
            }
            if (SkipFirstTurn[1] == true)
                HPMicro();

        }
        private void HPMicro()
        {
            foreach (var n in Hex)
            {
                string c = n.Name;
                c = c.Replace('m', '-');
                int[] fc = Coordinata(c);
                Hero h = heroes.Find(x => x.COORD[0] == fc[0] && x.COORD[1] == fc[1] && x.COORD[2] == fc[2]);
                if (h != null)
                {
                    PictureBox pb = frames.Find(y => y.Tag.ToString() == h.INITIATIV.ToString());
                    if (pb == null)
                    {
                        PictureBox pbHP = new PictureBox();
                        pbHP.Name = h.NAME + h.INITIATIV.ToString();
                        Form1.ActiveForm.Controls.Add(pbHP);
                        pbHP.Size = new Size(90, 7);
                        pbHP.Image = (Image)Properties.Resources.ResourceManager.GetObject("HPmicro" + h.TEAM);
                        pbHP.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbHP.Location = new Point(n.Location.X, n.Location.Y - 2);
                        pbHP.Tag = h.INITIATIV.ToString();
                        pbHP.BringToFront();
                        frames.Add(pbHP);
                    }
                    else
                    {
                        pb.Location = new Point(n.Location.X, n.Location.Y - 2);
                        pb.Visible = true;
                    }

                    PictureBox pb2 = framesHP.Find(y => y.Tag.ToString() == h.INITIATIV.ToString());
                    if (pb2 == null)
                    {
                        PictureBox pbHP = new PictureBox();
                        pbHP.Name = h.NAME + h.INITIATIV.ToString();
                        Form1.ActiveForm.Controls.Add(pbHP);
                        double hp = (Convert.ToDouble(h.HP) / Convert.ToDouble(h.MAXHP)) * 88;
                        pbHP.Size = new Size(Convert.ToInt32(hp), 5);
                        pbHP.Image = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + h.TEAM);
                        pbHP.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbHP.Location = new Point(n.Location.X + 1, n.Location.Y - 1);
                        pbHP.Tag = h.INITIATIV.ToString();
                        pbHP.BringToFront();
                        framesHP.Add(pbHP);
                    }
                    else
                    {
                        double hp = (Convert.ToDouble(h.HP) / Convert.ToDouble(h.MAXHP)) * 88;
                        pb2.Size = new Size(Convert.ToInt32(hp), 5);
                        pb2.Location = new Point(n.Location.X + 1, n.Location.Y - 1);
                        pb2.Visible = true;
                    }
                }
            }

            foreach (var n in heroes)
            {
                if (n.HP < 1)
                {
                    PictureBox pb = frames.Find(y => y.Tag.ToString() == n.INITIATIV.ToString());
                    PictureBox pb2 = framesHP.Find(y => y.Tag.ToString() == n.INITIATIV.ToString());

                    if (pb != null)
                    {
                        pb.Visible = false;
                        pb2.Visible = false;
                    }
                }
            }
        }
        private void PicturesHero()
        {
            int r, b;
            r = b = 0;
            foreach (var n in heroes)
            {
                if (n.TEAM == "B" && b == 0)
                {
                    string na = n.NAME;
                    na = na.Replace(' ', '_');
                    pictureBoxH1.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
                    pictureBoxH1.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    pictureBoxHero1.Image = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    labelHname1.ForeColor = Color.Blue;
                    labelHname1.Text = n.NAME;
                    labelHero1.Text = Convert.ToString(n.MAXHP);
                    panel1.Tag = n.NAME;
                    panels.Add(panel1);
                    b++;
                }
                else if (n.TEAM == "B" && b == 1)
                {
                    string na = n.NAME;
                    na = na.Replace(' ', '_');
                    pictureBoxH2.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
                    pictureBoxH2.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    pictureBoxHero2.Image = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    labelHname2.ForeColor = Color.Blue;
                    labelHname2.Text = n.NAME;
                    panel2.Tag = n.NAME;
                    panels.Add(panel2);
                    b++;
                }
                else if (n.TEAM == "B" && b == 2)
                {
                    string na = n.NAME;
                    na = na.Replace(' ', '_');
                    pictureBoxH3.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
                    pictureBoxH3.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    pictureBoxHero3.Image = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    labelHname3.ForeColor = Color.Blue;
                    labelHname3.Text = n.NAME;
                    panel3.Tag = n.NAME;
                    panels.Add(panel3);
                    b++;
                }
                else if (n.TEAM == "R" && r == 0)
                {
                    string na = n.NAME;
                    na = na.Replace(' ', '_');
                    pictureBoxH4.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
                    pictureBoxH4.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    pictureBoxHero4.Image = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    labelHname4.ForeColor = Color.Red;
                    labelHname4.Text = n.NAME;
                    panel4.Tag = n.NAME;
                    panels.Add(panel4);
                    r++;
                }
                else if (n.TEAM == "R" && r == 1)
                {
                    string na = n.NAME;
                    na = na.Replace(' ', '_');
                    pictureBoxH5.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
                    pictureBoxH5.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    pictureBoxHero5.Image = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    labelHname5.ForeColor = Color.Red;
                    labelHname5.Text = n.NAME;
                    panel5.Tag = n.NAME;
                    panels.Add(panel5);
                    r++;
                }
                else if (n.TEAM == "R" && r == 2)
                {
                    string na = n.NAME;
                    na = na.Replace(' ', '_');
                    pictureBoxH6.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
                    pictureBoxH6.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    pictureBoxHero6.Image = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + n.TEAM);
                    labelHname6.ForeColor = Color.Red;
                    labelHname6.Text = n.NAME;
                    panel6.Tag = n.NAME;
                    panels.Add(panel6);
                    r++;
                }

            }
        }
        private void SkvernaRelease()
        {
            foreach (var n in skverna)
            {
                n.FlatAppearance.BorderColor = Color.Black;
                n.FlatAppearance.BorderSize = 1;

                string coord2 = n.Name;
                coord2 = coord2.Replace('m', '-');
                int[] fieldcoord2 = Coordinata(coord2);

                Hero h = heroes.Find(x => x.COORD[0] == fieldcoord2[0] && x.COORD[1] == fieldcoord2[1] && x.COORD[2] == fieldcoord2[2]);
                string team = heroes.Find(x => x.NAME == "Cultist").TEAM;
                if (h != null)
                {
                    if (h.TEAM != team)
                    {
                        Hero hcur = CurrentHero;
                        CurrentHero = new Hero("Скверна", hcur.TEAM, 1, 36, 0, 0);
                        CurrentHero.COORD = h.COORD;
                        CurrentHero.STATUS.ARMORPIERCING = true;

                        Attack(CurrentHero.COORD, h.COORD);
                        CurrentHero = hcur;
                        RefreshAP();
                    }
                }
            }
            skvernaTurn = 0;
            skverna.Clear();
            StatusView();

        }


        private void button6_Click(object sender, EventArgs e)
        {
            Mode = "...";
            ModeView();
            CurrentHero.HODB = 1;
            CurrentHero.HODM = 1;

            CurrentHero.NOWKD[0] -= 1;
            CurrentHero.NOWKD[1] -= 1;
            CurrentHero.NOWKD[2] -= 1;

            CurrentHero.STATUS.ROOTS = false;
            CurrentHero.STATUS.SILENCE = false;
            CurrentHero.STATUS.REVENGE = false;
            CurrentHero.STATUS.STUN = false;
            CurrentHero.STATUS.POWER = false;
            CurrentHero.STATUS.ANGRY = false;
            CurrentHero.STATUS.CURSE = false;
            CurrentHero.STATUS.WEEKNESS = false;
            CurrentHero.STATUS.BOOST = false;
            CurrentHero.STATUS.BURN = new double[] { 0, 1 };
            CurrentHero.STATUS.DMG = 0;

            CrossbowmanPassive = new bool[2] { false, false };
            if (CurrentHero.STATUS.REGENERATION == true && CurrentHero.HP > 0)
            {
                CurrentHero.HP += 15;
                HPplus(CurrentHero, 15);
                RefreshHP();
            }
            else if (CurrentHero.NAME == "Metamorph" && CurrentHero.STATUS.REGENERATION == false)
                CurrentHero.STATUS.REGENERATION = true;


            if (CurrentHero.NAME == "Priest" && CurrentHero.HP > 0)
            {
                foreach (var n in heroes)
                {
                    if (n.TEAM == CurrentHero.TEAM && Dist(n.COORD, CurrentHero.COORD) <= 1)
                    {
                        n.HP += 8;
                        HPplus(n, 8);
                    }
                }
            }
            if (CurrentHero.NAME == "Warlock" && CurrentHero.HP > 0)
            {
                Hero uuu = new Hero("Warlock", CurrentHero.TEAM, 25, 8, 0, 0);
                uuu.COORD = CurrentHero.COORD;
                CurrentHero = uuu;
                CurrentHero.STATUS.ARMORPIERCING = true;
                foreach (var n in heroes)
                {
                    if (n.TEAM != CurrentHero.TEAM && Dist(n.COORD, CurrentHero.COORD) <= 1)
                    {
                        Attack(CurrentHero.COORD, n.COORD);
                    }
                }
            }
            listBox1.Items.Clear();
            if (CurrentHero.STATUS.BLEED[0] == 1 && CurrentHero.HP > 0)
            {
                CurrentHero.HP -= 10;
                if (CurrentHero.HP < 1)
                {
                    RefreshHP();
                    listBox1.Items.Add(CurrentHero.NAME + " погибает от кровотечения!");
                    if (CurrentHero.NAME == "Angel" && AngelPassive == false)
                        AngelPassivka();
                    else
                        CurrentHero.COORD = new int[] { -5, -5, -5 };
                    Refresh();
                }
            }
            if (CurrentHero.STATUS.POISON == true && CurrentHero.HP > 0)
            {
                CurrentHero.HP -= 10;
                if (CurrentHero.HP < 1)
                {
                    RefreshHP();
                    listBox1.Items.Add(CurrentHero.NAME + " погибает от яда!");
                    if (CurrentHero.NAME == "Angel" && AngelPassive == false)
                        AngelPassivka();
                    else
                        CurrentHero.COORD = new int[] { -5, -5, -5 };
                    Refresh();
                }
                CurrentHero.STATUS.POISON = false;
            }

            turn++;
            if (turn > heroes.Count)
            {
                turn = 1;
                if (SkipFirstTurn[0] == true)
                    SkipFirstTurn[1] = true;
                else
                    SkipFirstTurn[0] = true;
            }

            /*while (heroes.Find(x => x.INITIATIV == turn).HP <= 0 || heroes.Find(x => x.INITIATIV == turn).NAME == "Stoloktit" || heroes.Find(x => x.INITIATIV == turn).STATUS.CRYOSTASYS[0] == 1)
            {
                turn++;
                RefreshStatus();
                if (turn > heroes.Count)
                    turn = 1;
            }   */

            CurrentHero = heroes.Find(x => x.INITIATIV == turn);
            if (CurrentHero.HP > 0)
                CheckWin();
            button3.Text = CurrentHero.ABILITIES[1];
            button4.Text = CurrentHero.ABILITIES[2];
            button5.Text = CurrentHero.ABILITIES[3];
            TemporariumPassive = true;

            label2.Text = CurrentHero.NAME;
            label2.Location = new Point(287 - label2.Width / 2, label2.Location.Y);
            CurrentHero.STATUS.DMGPASSIVE = 0;
            if (CurrentHero.STATUS.STUN == true)
                CurrentHero.HODM -= 1;
            if (CurrentHero.STATUS.BLESS[0] == -1)
            {
                CurrentHero.STATUS.BLESS[0] = 0;
                CurrentHero.STATUS.ARMOR -= CurrentHero.STATUS.BLESS[3];
            }
            if (CurrentHero.STATUS.SOULBURN[0] == 1)
                CurrentHero.HODB -= 1;
            if (CurrentHero.STATUS.BOOST == true)
                CurrentHero.HODM += 1;
            if (CurrentHero.STATUS.INJECT[0] == 1)
                CurrentHero.HODB += 1;

            if (CurrentHero.NAME == "Zombie")
            {
                CurrentHero.HODM -= 1;
                initView -= 1;
            }

            SandsOfTime();
            if (skvernaTurn > 0)
            {
                skvernaTurn++;
                if (skvernaTurn >= heroes.Count + 1)
                {
                    SkvernaRelease();
                }
            }

            string na = CurrentHero.NAME;
            na = na.Replace(' ', '_');
            pictureBox6.Image = (Image)Properties.Resources.ResourceManager.GetObject(na);
            pictureBox6.BackgroundImage = pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar" + CurrentHero.TEAM);
            if (CurrentHero.TEAM == "B")
            {
                label2.ForeColor = Color.Blue;
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject("HealthbarB");
            }
            else
            {
                label2.ForeColor = Color.Red;
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject("HealthbarR");
            }

            if (CurrentHero.STATUS.STONEBLOOD[0] == 1 && CurrentHero.HP > 0)
            {
                int heal = 6;
                heal += (CurrentHero.ARMOR + CurrentHero.STATUS.ARMOR + CurrentHero.STATUS.ARMORPASSIVE) * 2;
                CurrentHero.HP += heal;
                HPplus(CurrentHero, heal);
                RefreshHP();
                listBox1.Items.Add(CurrentHero.NAME + " восстанавливает " + heal + " хп!");
            }

            if (CurrentHero.NAME == "Aramusha" && CurrentHero.HP > 0 && CurrentHero.STATUS.CRYOSTASYS[0] != 1 && SkipFirstTurn[1] == true)
                AramushaPassive = "";
            if (CurrentHero.NAME == "Mage" && CurrentHero.HP > 0)
                CurrentHero.MANA += 1;
            if (CurrentHero.NAME == "Stoloktit" || CurrentHero.NAME == "Illusion")
                initView -= 1;

            GuradPassive = true;
            CheckPassive();
            RefreshStatus();
            RefreshAP();
            Refresh();
            InitView();
            ViewAction();
            ZoneAbilities();
            SetSkillsPictures();

            if (CurrentHero.HP <= 0 || CurrentHero.NAME == "Stoloktit" || CurrentHero.NAME == "Illusion" || CurrentHero.STATUS.CRYOSTASYS[0] == 1)
            {
                button6.PerformClick();
            }
            if (CurrentHero.HP > 0 && ((Smoke == true && CurrentHero.NAME == "Thief") || (SmokeMet == true && CurrentHero.NAME == "Metamorph")))
            {
                Mode = "Появление";
                ModeView();

                foreach (var n in Hex)
                {
                    n.Enabled = false;
                }
                if (CurrentHero.NAME == "Thief")
                {
                    foreach (var n in smoke)
                    {
                        n.Enabled = true;
                        n.FlatAppearance.BorderSize = 3;
                        if (CurrentHero.TEAM == "R")
                            n.FlatAppearance.BorderColor = Color.Red;
                        else
                            n.FlatAppearance.BorderColor = Color.Blue;
                    }
                }
                else if (CurrentHero.NAME == "Metamorph")
                {
                    foreach (var n in smokeMet)
                    {
                        n.Enabled = true;
                        n.FlatAppearance.BorderSize = 3;
                        if (CurrentHero.TEAM == "R")
                            n.FlatAppearance.BorderColor = Color.Red;
                        else
                            n.FlatAppearance.BorderColor = Color.Blue;
                    }
                }

                button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = false;
            }

            if (CurrentHero.HP > 0 && ((Inj == true && CurrentHero.NAME == "Spirit") || (InjMet == true && CurrentHero.NAME == "Metamorph")))
            {
                Mode = "Выселение";
                ModeView();

                foreach (var n in Hex)
                {
                    n.Enabled = false;
                }

                if (CurrentHero.NAME == "Spirit")
                {
                    foreach (var n in Hex)
                    {
                        string coord3 = n.Name;
                        coord3 = coord3.Replace('m', '-');
                        int[] fieldcoord3 = Coordinata(coord3);

                        if (Dist(InjectCoord, fieldcoord3) <= 2)
                        {
                            inj.Add(n);
                        }
                    }
                }
                else if (CurrentHero.NAME == "Metamorph")
                {
                    foreach (var n in Hex)
                    {
                        string coord3 = n.Name;
                        coord3 = coord3.Replace('m', '-');
                        int[] fieldcoord3 = Coordinata(coord3);

                        if (Dist(InjectCoordMet, fieldcoord3) <= 2)
                        {
                            inj.Add(n);
                        }
                    }
                }

                if (CurrentHero.NAME == "Spirit" || CurrentHero.NAME == "Metamorph")
                {
                    foreach (var n in inj)
                    {
                        n.Enabled = true;
                        n.FlatAppearance.BorderSize = 3;
                        if (CurrentHero.TEAM == "R")
                            n.FlatAppearance.BorderColor = Color.Red;
                        else
                            n.FlatAppearance.BorderColor = Color.Blue;
                    }
                }               

                button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = false;
            }

        }

        private void SetSkillsPictures()
        {
            if (CurrentHero.NAME != "Zombie" && CurrentHero.NAME != "Stoloktit" && CurrentHero.NAME != "Illusion")
            {
                button3.Image = Image.FromFile(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SkillPict\\" + CurrentHero.ABILITIES[1] + ".png"));
                button4.Image = Image.FromFile(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SkillPict\\" + CurrentHero.ABILITIES[2] + ".png"));
                button5.Image = Image.FromFile(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SkillPict\\" + CurrentHero.ABILITIES[3] + ".png"));
            }
            else
            {
                button3.Image = null;
                button4.Image = null;
                button5.Image = null;
            }
        }

        // 2 ABILKA
        private void button4_Click(object sender, EventArgs e)
        {
            Mode = CurrentHero.ABILITIES[2];
            ModeView();
            ViewAction();
        }

        // 3 ABILKA
        private void button5_Click(object sender, EventArgs e)
        {
            Mode = CurrentHero.ABILITIES[3];
            ModeView();
            ViewAction();
        }

        private void pictureBoxHero1_Click(object sender, EventArgs e)
        {

        }
        private async void HPminus(Hero h, int d)
        {
            pbHP++;
            PictureBox pb = new PictureBox();
            pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("HPminus");
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Size = new Size(Convert.ToInt32(Convert.ToDouble(d) / Convert.ToDouble(h.MAXHP) * 300), 32);
            pb.Name = "pbHP" + pbHP.ToString();
            Form1.ActiveForm.Controls.Add(pb);
            if (h.NAME == labelHname1.Text)
            {
                pb.Location = new Point(pictureBoxHero1.Location.X + pictureBoxHero1.Width, pictureBoxHero1.Location.Y);
            }
            if (h.NAME == labelHname2.Text)
            {
                pb.Location = new Point(pictureBoxHero2.Location.X + pictureBoxHero2.Width, pictureBoxHero2.Location.Y);
            }
            if (h.NAME == labelHname3.Text)
            {
                pb.Location = new Point(pictureBoxHero3.Location.X + pictureBoxHero3.Width, pictureBoxHero3.Location.Y);
            }
            if (h.NAME == labelHname4.Text)
            {
                pb.Location = new Point(pictureBoxHero4.Location.X + pictureBoxHero4.Width, pictureBoxHero4.Location.Y);
            }
            if (h.NAME == labelHname5.Text)
            {
                pb.Location = new Point(pictureBoxHero5.Location.X + pictureBoxHero5.Width, pictureBoxHero5.Location.Y);
            }
            if (h.NAME == labelHname6.Text)
            {
                pb.Location = new Point(pictureBoxHero6.Location.X + pictureBoxHero6.Width, pictureBoxHero6.Location.Y);
            }
            if (pb.Location.X != 0)
            {
                pb.BringToFront();
                int s = pb.Width;
                await Task.Delay(500);
                if (s != 0)
                {
                    for (int i = 0; i < 51; i++)
                    {
                        pb.Width = s - Convert.ToInt32(s * Convert.ToDouble(s * i) / Convert.ToDouble(s * 50));
                        await Task.Delay(1);
                    }
                }
            }
            else
                pb.Visible = false;

            Form1.ActiveForm.Controls.Remove(pb);
        }
        private async void HPplus(Hero h, int d)
        {
            pbHP++;
            PictureBox pb = new PictureBox();
            pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Healthbar");
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Size = new Size(Convert.ToInt32(Convert.ToDouble(d) / Convert.ToDouble(h.MAXHP) * 300), 32);

            pb.Name = "pbHP" + pbHP.ToString();
            Form1.ActiveForm.Controls.Add(pb);
            if (h.NAME == labelHname1.Text)
            {
                pb.Location = new Point(pictureBoxHero1.Location.X + pictureBoxHero1.Width + 1, pictureBoxHero1.Location.Y);
                if (pb.Width + pictureBoxHero1.Width > 300)
                    pb.Width = 300 - pictureBoxHero1.Width;
            }
            if (h.NAME == labelHname2.Text)
            {
                pb.Location = new Point(pictureBoxHero2.Location.X + pictureBoxHero2.Width + 1, pictureBoxHero2.Location.Y);
                if (pb.Width + pictureBoxHero2.Width > 300)
                    pb.Width = 300 - pictureBoxHero2.Width;
            }
            if (h.NAME == labelHname3.Text)
            {
                pb.Location = new Point(pictureBoxHero3.Location.X + pictureBoxHero3.Width + 1, pictureBoxHero3.Location.Y);
                if (pb.Width + pictureBoxHero3.Width > 300)
                    pb.Width = 300 - pictureBoxHero3.Width;
            }
            if (h.NAME == labelHname4.Text)
            {
                pb.Location = new Point(pictureBoxHero4.Location.X + pictureBoxHero4.Width + 1, pictureBoxHero4.Location.Y);
                if (pb.Width + pictureBoxHero4.Width > 300)
                    pb.Width = 300 - pictureBoxHero4.Width;
            }
            if (h.NAME == labelHname5.Text)
            {
                pb.Location = new Point(pictureBoxHero5.Location.X + pictureBoxHero5.Width + 1, pictureBoxHero5.Location.Y);
                if (pb.Width + pictureBoxHero5.Width > 300)
                    pb.Width = 300 - pictureBoxHero5.Width;
            }
            if (h.NAME == labelHname6.Text)
            {
                pb.Location = new Point(pictureBoxHero6.Location.X + pictureBoxHero6.Width + 1, pictureBoxHero6.Location.Y);
                if (pb.Width + pictureBoxHero6.Width > 300)
                    pb.Width = 300 - pictureBoxHero6.Width;
            }
            if (pb.Location.X != 0)
            {
                pb.BringToFront();
                int s = pb.Width;
                int ix = pb.Location.X;
                await Task.Delay(500);

                if (pb.Width > 0)
                {
                    for (int i = 0; i < 51; i++)
                    {
                        pb.Width = s - Convert.ToInt32(s * Convert.ToDouble(s * i) / Convert.ToDouble(s * 50));
                        pb.Location = new Point(ix - pb.Width + s, pb.Location.Y);
                        await Task.Delay(1);
                    }
                }
            }
            else
                pb.Visible = false;


            Form1.ActiveForm.Controls.Remove(pb);
        }
        private void RefreshStatus()
        {
            foreach (var n in heroes)
            {
                if (n.STATUS.METKA[1] > 0)
                {
                    n.STATUS.METKA[1] -= 1;
                    if (n.STATUS.METKA[1] < 1)
                        n.STATUS.METKA[0] = 0;
                }
                if (n.STATUS.PROTECT[1] > 0)
                {
                    n.STATUS.PROTECT[1] -= 1;
                    if (n.STATUS.PROTECT[1] < 1)
                    {
                        n.STATUS.PROTECT[0] = 0;
                        foreach (var m in heroes)
                        {
                            m.STATUS.PROTECTORED = false;
                        }
                    }
                }
                if (n.STATUS.ANGRY1[1] > 0)
                {
                    n.STATUS.ANGRY1[1] -= 1;
                    if (n.STATUS.ANGRY1[1] < 1)
                        n.STATUS.ANGRY1[0] = 0;
                }
                if (n.STATUS.BLESS[1] > 0)
                {
                    n.STATUS.BLESS[1] -= 1;
                    if (n.STATUS.BLESS[1] < 1)
                    {
                        n.STATUS.BLESS = new int[4] { 0, 0, 0, 0 };
                        //n.STATUS.ARMOR -= n.STATUS.BLESS[3];
                    }
                }
                if (n.STATUS.GEOSHIELD[1] > 0)
                {
                    n.STATUS.GEOSHIELD[1] -= 1;
                    if (n.STATUS.GEOSHIELD[1] < 1)
                    {
                        n.STATUS.GEOSHIELD[0] = -1;
                        n.STATUS.ARMOR -= 7;
                    }
                }
                if (n.STATUS.SOULBURN[1] > 0)
                {
                    n.STATUS.SOULBURN[1] -= 1;
                    if (n.STATUS.SOULBURN[1] < 1)
                        n.STATUS.SOULBURN[0] = -1;
                }
                if (n.STATUS.CORROSIVE[1] > 0)
                {
                    n.STATUS.CORROSIVE[1] -= 1;
                    if (n.STATUS.CORROSIVE[1] < 1)
                        n.STATUS.CORROSIVE[0] = -1;
                }
                if (n.STATUS.DEATHBREATH[1] > 0)
                {
                    n.STATUS.DEATHBREATH[1] -= 1;
                    if (n.STATUS.DEATHBREATH[1] < 1)
                        n.STATUS.DEATHBREATH[0] = -1;
                }
                if (n.STATUS.ENCOURAGE[2] > 0)
                {
                    n.STATUS.ENCOURAGE[2] -= 1;
                    if (n.STATUS.ENCOURAGE[2] < 1)
                    {
                        n.STATUS.ENCOURAGE[0] = -1;
                    }
                }
                if (n.STATUS.CRYOSTASYS[1] > 0)
                {
                    n.STATUS.CRYOSTASYS[1] -= 1;
                    if (n.STATUS.CRYOSTASYS[1] < 1)
                    {
                        n.STATUS.METKA = new int[2] { 0, 0 };
                        n.STATUS.ROOTS = false;
                        n.STATUS.SILENCE = false;
                        n.STATUS.STUN = false;
                        n.STATUS.ANGRY = false;
                        n.STATUS.CORROSIVE = new int[] { 0, 0 };
                        n.STATUS.CURSE = false;
                        n.STATUS.WEEKNESS = false;
                        n.STATUS.SOULBURN = new int[] { 0, 0 };
                        n.STATUS.CRYOSTASYS = new int[] { 0, 0 };

                        n.HP += 40;
                        HPplus(n, 40);
                        RefreshHP();
                    }
                }
                if (n.STATUS.BLEED[0] > 0)
                {
                    n.STATUS.BLEED[1] -= 1;
                    if (n.STATUS.BLEED[1] < 1)
                    {
                        n.STATUS.BLEED[0] = -1;
                    }
                }
                if (n.STATUS.MAGICSHIELD[0] > 0)
                {
                    n.STATUS.MAGICSHIELD[1] -= 1;
                    if (n.STATUS.MAGICSHIELD[1] < 1)
                    {
                        n.STATUS.MAGICSHIELD[0] = -1;
                    }
                }
                if (n.STATUS.HELLSIGN[0] > 0)
                {
                    n.STATUS.HELLSIGN[1] -= 1;
                    if (n.STATUS.HELLSIGN[1] < 1)
                    {
                        n.STATUS.HELLSIGN[0] = -1;
                        Hero hcur = CurrentHero;
                        CurrentHero = new Hero("Адское клеймо", hcur.TEAM, 1, 15, 0, 0);
                        CurrentHero.COORD = n.COORD;
                        CurrentHero.STATUS.ARMORPIERCING = true;
                        foreach (var m in heroes)
                        {
                            if (Dist(CurrentHero.COORD, m.COORD) <= 1 && m.TEAM != CurrentHero.TEAM)
                            {
                                Attack(CurrentHero.COORD, m.COORD);
                            }
                        }
                        CurrentHero = hcur;
                        RefreshAP();
                    }
                }
                if (n.STATUS.STONEBLOOD[0] > 0)
                {
                    n.STATUS.STONEBLOOD[1] -= 1;
                    if (n.STATUS.STONEBLOOD[1] < 1)
                    {
                        n.STATUS.STONEBLOOD[0] = -1;
                    }
                }
                if (n.STATUS.INJECT[0] > 0)
                {
                    n.STATUS.INJECT[1] -= 1;
                    if (n.STATUS.INJECT[1] < 1)
                    {
                        n.STATUS.INJECT[0] = -1;
                    }
                }
                if (n.STATUS.UNFLESH[0] > 0)
                {
                    n.STATUS.UNFLESH[1] -= 1;
                    if (n.STATUS.UNFLESH[1] < 1)
                    {
                        n.STATUS.UNFLESH[0] = -1;
                    }
                }
            }

            StatusView();
        }
        private void StatusView()
        {
            foreach (var n in heroes)
            {
                Panel p = panels.Find(x => x.Tag.ToString() == n.NAME);

                if (p == null)
                    continue;
                p.Controls.Clear();
                int s = 0;
                if (n.STATUS.METKA[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Metka");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Атаки по персонажу наносят +8 урона");
                }
                if (n.STATUS.POISON == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Poison");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "-40% урона, -10 хп в конце хода");
                }
                if (n.STATUS.CRYOSTASYS[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Cryostasys");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Персонаж неуязвим");
                }
                if (n.STATUS.PROTECT[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Protect");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Персонаж принимает 50% урона полученного союзниками на себя");
                }
                if (n.STATUS.BURN[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Burn");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Урон снижен на % недостающего хп");
                }
                if (n.STATUS.SILENCE == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Silence");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Персонаж не может использовать способности");
                }
                if (n.STATUS.ROOTS == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Roots");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Персонаж не может передвигаться");
                }
                if (n.STATUS.PROTECTORED == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Protectored");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Перенаправляет 50% полученного урона союзнику");
                }
                if (n.STATUS.REVENGE == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Revenge");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Урон увеличен на +" + Convert.ToInt32(Convert.ToDouble((CurrentHero.MAXHP - CurrentHero.HP) / 15 * 2)));
                }
                if (n.STATUS.STUN == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Stun");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Персонаж лишается дополнительного действия");
                }
                if (n.STATUS.ARMORED == true || n.STATUS.ARMORED2 == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Armored");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "+3 брони");
                }
                if (n.STATUS.POWER == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Power");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Урон увеличен на 30%");
                }
                if (n.STATUS.ANGRY == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Angry");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Персонаж спровоцирован");
                }
                if (n.STATUS.BLESS[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Bless");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "+" + n.STATUS.BLESS[2] + " к урону и +" + n.STATUS.BLESS[3] + " к броне");
                }
                if (n.STATUS.GEOSHIELD[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Geoshield");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "+7 к броне. Персонажа не могут переместить");
                }
                if (n.STATUS.CORROSIVE[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Corrosive");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Персонаж лишается брони");
                }
                if (n.STATUS.SOULBURN[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Soulburn");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Персонаж лишается брони и основного действия");
                }
                if (n.STATUS.DEATHBREATH[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Deathbreath");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Отражает 50% полученного урона атакующему");
                }
                if (n.STATUS.CURSE == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Curse");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "-40% урона");
                }
                if (n.STATUS.WEEKNESS == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Weekness");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Персонаж не может атаковать и использовать атакующие способности");
                }
                if (n.STATUS.REGENERATION == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Regeneration");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "+15 хп в начале хода");
                }
                if (n.STATUS.BOOST == true)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Boost");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "+1 дополнительное действие");
                }
                if (n.STATUS.ENCOURAGE[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Encourage");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "+" + n.STATUS.ENCOURAGE[1] + " к урону");
                }
                if (n.STATUS.BLEED[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Bleed");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "-10 хп в конце хода");
                }
                if (n.STATUS.MAGICSHIELD[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("MagicShield");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Неуязвимость к негативным эффектам");
                }
                if (n.STATUS.HELLSIGN[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("HellSign");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Персонаж скоро взорвется!");
                }
                if (n.STATUS.STONEBLOOD[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("StoneBlood");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Восстанавливает " + (CurrentHero.ARMOR + CurrentHero.STATUS.ARMOR + CurrentHero.STATUS.ARMORPASSIVE) * 2 + " хп в начале хода");
                }
                if (n.STATUS.INJECT[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Inject");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "В Вас вселился дух и наполняет своей силой.");
                }
                if (n.STATUS.UNFLESH[0] == 1)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = (Image)Properties.Resources.ResourceManager.GetObject("Unflesh");
                    pb.Size = new Size(50, 50);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Controls.Add(pb);
                    pb.Location = new Point(50 * s + 1, 0);
                    s++;

                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(pb, "Игнорируйте 50% урона.");
                }
            }
        }
        private bool Thunder(List<Hero> list, string name)
        {
            bool result = true;
            foreach (var n in list)
            {
                if (n.NAME == name)
                    result = false;
            }
            return result;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.W)
                button1.PerformClick();
            else if (e.KeyValue == (char)Keys.E)
                button2.PerformClick();
            else if (e.KeyValue == (char)Keys.Space)
                button6.PerformClick();
            else if (e.KeyValue == (char)Keys.D1)
                button3.PerformClick();
            else if (e.KeyValue == (char)Keys.D2)
                button4.PerformClick();
            else if (e.KeyValue == (char)Keys.D3)
                button5.PerformClick();
            else if (e.KeyValue == (char)Keys.OemMinus)
                MinusVolume.PerformClick();
            else if (e.KeyValue == (char)Keys.Oemplus)
                PlusVolume.PerformClick();

        }

        private void PlusVolume_Click(object sender, EventArgs e)
        {
            if (volume < 10)
            {
                volume += 1;
                Player.Volume = Convert.ToDouble(volume) / 10;
            }
            Volume.Text = Convert.ToString(volume * 10) + "%";
        }

        private void MinusVolume_Click(object sender, EventArgs e)
        {
            if (volume > 0)
            {
                volume -= 1;
                Player.Volume = Convert.ToDouble(volume) / 10;
            }
            Volume.Text = Convert.ToString(volume * 10) + "%";
            //WriteWinRate("R");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Thread.Sleep(500);
            Application.Exit();
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            composition++;
            if (composition > 5)
                composition = 1;

            for (int i = 0; i < 100; i++)
            {
                Player.Volume = Convert.ToDouble(volume) / 10 - (Convert.ToDouble(i) * (Convert.ToDouble(volume) / 1000));
                await Task.Delay(10);
            }
            Player.Stop();
            Player.Volume = Convert.ToDouble(volume) / 10;
            PlayMusic();
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            composition--;
            if (composition < 1)
                composition = 5;

            for (int i = 0; i < 100; i++)
            {
                Player.Volume = Convert.ToDouble(volume) / 10 - (Convert.ToDouble(i) * (Convert.ToDouble(volume) / 1000));
                await Task.Delay(10);
            }
            Player.Stop();
            Player.Volume = Convert.ToDouble(volume) / 10;
            PlayMusic();
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

            while (pb.Location.Y < 320)
            {
                pb.Location = new Point(0, pb.Location.Y - 6);
                pb2.Location = new Point(0, pb2.Location.Y + 6);
                await Task.Delay(1);
            }
            while (pb.Location.Y < 470)
            {
                pb.Location = new Point(0, pb.Location.Y - 4);
                pb2.Location = new Point(0, pb2.Location.Y + 4);
                await Task.Delay(1);
            }
            while (pb.Location.Y <= 1)
            {
                pb.Location = new Point(0, pb.Location.Y - 2);
                pb2.Location = new Point(0, pb2.Location.Y + 2);
                await Task.Delay(1);
            }

            if (pb.Location.Y < 0)
            {
                ActiveForm.Controls.Remove(pb);
                ActiveForm.Controls.Remove(pb2);
                return;
            }
        }

        private void ClearEffects(Hero n, int team)
        {
            if (team == 0)
            {
                int heal = 0;

                if (n.STATUS.METKA[0] == 1)
                    heal += 8;
                if (n.STATUS.ROOTS == true)
                    heal += 8;
                if (n.STATUS.SILENCE == true)
                    heal += 8;
                if (n.STATUS.STUN == true)
                    heal += 8;
                if (n.STATUS.ANGRY == true)
                    heal += 8;
                if (n.STATUS.CORROSIVE[0] == 1)
                    heal += 8;
                if (n.STATUS.CURSE == true)
                    heal += 8;
                if (n.STATUS.SOULBURN[0] == 1)
                    heal += 8;
                if (n.STATUS.METKA[0] == 1)
                    heal += 8;
                if (n.STATUS.WEEKNESS == true)
                    heal += 8;
                if (n.STATUS.BLEED[0] == 1)
                    heal += 8;
                if (n.STATUS.POISON == true)
                    heal += 8;
                if (n.STATUS.BURN[0] == 1)
                    heal += 8;
                if (n.STATUS.HELLSIGN[0] == 1)
                    heal += 8;

                n.HP += heal;
                HPplus(n, heal);
            }
            else
            {
                int heal = 0;
                if (n.STATUS.ARMORED == true)
                    heal += 8;
                if (n.STATUS.POWER == true)
                    heal += 8;
                if (n.STATUS.BLESS[0] == 1)
                    heal += 8;
                if (n.STATUS.DEATHBREATH[0] == 1)
                    heal += 8;
                if (n.STATUS.GEOSHIELD[0] == 1)
                    heal += 8;
                if (n.STATUS.PROTECT[0] == 1)
                    heal += 8;
                if (n.STATUS.REGENERATION == true)
                    heal += 8;
                if (n.STATUS.ENCOURAGE[0] == 1)
                    heal += 8;
                if (n.STATUS.BOOST == true)
                    heal += 8;
                if (n.STATUS.MAGICSHIELD[0] == 1)
                    heal += 8;
                if (n.STATUS.STONEBLOOD[0] == 1)
                    heal += 8;
                if (n.STATUS.CRYOSTASYS[0] == 1)
                    heal += 8;
                if (n.STATUS.INJECT[0] == 1)
                    heal += 8;
                if (n.STATUS.UNFLESH[0] == 1)
                    heal += 8;
                n.HP -= heal;
                RefreshHP();
                HPminus(n, heal);
            }
        }

        private void SkillDescription(int idskill)
        {
            SkillName.Text = CurrentHero.ABILITIES[idskill];
            SkillDesc.Text = CurrentHero.ABILITIESINFO[idskill];
            try
            {
                SkillPicture.Load(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "skillspictures\\" + CurrentHero.ABILITIES[idskill] + ".png"));
            }
            catch
            {
                SkillPicture.Load(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "skillspictures\\Макет.png"));
            }
            if (CurrentHero.NEEDAP[idskill - 1] == 1)
                SkillCost.Image = (Image)Properties.Resources.ResourceManager.GetObject("MAP");
            else if (CurrentHero.NEEDAP[idskill - 1] == 2)
                SkillCost.Image = (Image)Properties.Resources.ResourceManager.GetObject("BAP");
            else if (CurrentHero.NEEDAP[idskill - 1] == 3)
                SkillCost.Image = (Image)Properties.Resources.ResourceManager.GetObject("BMAP");
            else
                SkillCost.Image = (Image)Properties.Resources.ResourceManager.GetObject("XAP");
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            SkillDescription(1);
            skillPanel.Visible = true;
            skillPanel.Location = new Point(616, 507);
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            SkillDescription(2);
            skillPanel.Visible = true;
            skillPanel.Location = new Point(817, 507);
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            SkillDescription(3);
            skillPanel.Visible = true;
            skillPanel.Location = new Point(1023, 507);
        }

        private void HideSkill(object sender, EventArgs e)
        {
            skillPanel.Visible = false;
        }

        private void WriteWinRate(string winteam)
        {
            try
            {
                using (ExcelHelper helper = new ExcelHelper())
                {
                    if (helper.Open(path: Path.Combine(Environment.CurrentDirectory, "WinRate.xlsx")))
                    {
                        foreach (var n in heroes)
                        {
                            if (n.TEAM == winteam)
                                helper.Set(n.NAME, "C");
                            else
                                helper.Set(n.NAME, "D");
                        }
                        helper.Save();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void HitView(int[] coord ,bool hit)
        {
            string s = coord[0].ToString() + coord[1].ToString() + coord[2].ToString();
            s = "b" + s.Replace('-', 'm');
            Button b = Hex.Find(x => x.Name == s);
            if (b != null)
            {
                if (hit)
                    b.FlatAppearance.BorderColor = Color.Red;
                else
                    b.FlatAppearance.BorderColor = Color.Green;

                b.FlatAppearance.BorderSize = 8;
                await Task.Delay(30);
                b.FlatAppearance.BorderSize = 7;
                await Task.Delay(30);
                b.FlatAppearance.BorderSize = 6;
                await Task.Delay(30);
                b.FlatAppearance.BorderSize = 5;
                await Task.Delay(30);
                b.FlatAppearance.BorderSize = 4;
                await Task.Delay(30);
                b.FlatAppearance.BorderSize = 3;
                await Task.Delay(30);
                b.FlatAppearance.BorderSize = 2;
                await Task.Delay(30);
                b.FlatAppearance.BorderSize = 1;
                await Task.Delay(30);
                b.FlatAppearance.BorderSize = 0;
                Refresh();
            }
        }
    }
}
