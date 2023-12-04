using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleArena
{
    public class Hero
    {
        string Name;
        string Team;

        int hp, maxHP, dmg, razbros, armor, range, hodB, hodM, initiativ;
        int[] coord = new int[3];
        string[] abilities = new string[4];
        string[] abilitiesinfo = new string[4];
        int[] kd = new int[3];
        int[] nowkd = new int[3];
        int[] needap = new int[3];
        int mana = 0;
        bool[] attackabilities = new bool[3];

        Status status = new Status();

        public Hero(string N, string T, int mh, int d, int r, int a)
        {
            Name = N;
            Team = T;
            hp = mh;
            maxHP = mh;
            dmg = d;
            razbros = r;
            armor = a;
            range = hodB = hodM = 1;
            abilities = new string[] { "", "", "", "" };
        }

        public Hero()
        {
            
        
        }

        public bool Target(int[] crd)
        {
            if (coord[0] == crd[0] && coord[1] == crd[1] && coord[2] == crd[2])
                return true;
            else
                return false;
        }

        public int[] COORD
        {
            get { return coord; }
            set { coord = value; }
        }
        public int[] KD
        {
            get { return kd; }
            set { kd = value; }
        }
        public int[] NOWKD
        {
            get { return nowkd; }
            set { nowkd = value; }
        }
        public int[] NEEDAP
        {
            get { return needap; }
            set { needap = value; }
        }
        public bool[] ATTACKABILITIES
        {
            get { return attackabilities; }
            set { attackabilities = value; }
        }
        public string[] ABILITIES
        {
            get { return abilities; }
            set { abilities = value; }
        }
        public string[] ABILITIESINFO
        {
            get { return abilitiesinfo; }
            set { abilitiesinfo = value; }
        }
        public string NAME
        {
            get { return Name; }
            set { Name = value; }
        }
        public string TEAM
        {
            get { return Team; }
            set { Team = value; }
        }
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        public int MANA
        {
            get { return mana; }
            set { mana = value; }
        }
        public int RANGE
        {
            get { return range; }
            set { range = value; }
        }
        public int MAXHP
        {
            get { return maxHP; }
            set { maxHP = value; }
        }
        public int DMG
        {
            get { return dmg; }
            set { dmg = value; }
        }
        public int RAZBROS
        {
            get { return razbros; }
            set { razbros = value; }
        }
        public int ARMOR
        {
            get { return armor; }
            set { armor = value; }
        }
        public int HODB
        {
            get { return hodB; }
            set { hodB = value; }
        }
        public int HODM
        {
            get { return hodM; }
            set { hodM = value; }
        }
        public int INITIATIV
        {
            get { return initiativ; }
            set { initiativ = value; }
        }
        public Status STATUS
        {
            get { return status; }
            set { status = value; }
        }
    }
}
