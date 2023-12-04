using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleArena
{
    public class Status
    {
        bool roots, armorpiercing, silence, revenge, stun, armored, armored2, power, angry, curse, weekness, regeneration, boost, poison;
        int dmg, dmgpassive, razbros, armorpassive, armor;
        int[] metka = new int[2];
        int[] soulburn = new int[2];
        int[] protect = new int[2];
        int[] angry1 = new int[2];
        int[] bless = new int[4];
        int[] encourage = new int[3];
        int[] geoshield = new int[2];
        int[] corrosive = new int[2];
        int[] deathbreath = new int[2];
        int[] cryostasys = new int[2];
        int[] bleed = new int[2];
        int[] magicshield = new int[2];
        int[] hellsign = new int[2];
        int[] stoneblood = new int[2];
        int[] inject = new int[2];
        int[] unflesh = new int[2];
        double[] burn = new double[2];
        bool protectored;

        public Status()
        {
            roots = armorpiercing = poison = silence = protectored = boost = revenge = stun = armored = armored2 = power = angry = regeneration = weekness = curse = false;
            dmg = razbros = armorpassive = 0;
            metka[0] = metka[1] = 0;
            protect[0] = protect[1] = 0;
            geoshield[0] = geoshield[1] = 0;
            soulburn[0] = soulburn[1] = 0;
            corrosive[0] = corrosive[1] = 0;
            deathbreath[0] = deathbreath[1] = 0;
            inject[0] = inject[1] = 0;
            unflesh[0] = unflesh[1] = 0;
        }

        public bool ROOTS
        {
            get { return roots; }
            set { roots = value; }
        }
        public bool REVENGE
        {
            get { return revenge; }
            set { revenge = value; }
        }
        public int[] CRYOSTASYS
        {
            get { return cryostasys; }
            set { cryostasys = value; }
        }
        public int[] MAGICSHIELD
        {
            get { return magicshield; }
            set { magicshield = value; }
        }
        public int[] BLEED
        {
            get { return bleed; }
            set { bleed = value; }
        }
        public int[] STONEBLOOD
        {
            get { return stoneblood; }
            set { stoneblood = value; }
        }
        public int[] HELLSIGN
        {
            get { return hellsign; }
            set { hellsign = value; }
        }
        public double[] BURN
        {
            get { return burn; }
            set { burn = value; }
        }
        public bool POISON
        {
            get { return poison; }
            set { poison = value; }
        }
        public bool BOOST
        {
            get { return boost; }
            set { boost = value; }
        }
        public int[] CORROSIVE
        {
            get { return corrosive; }
            set { corrosive = value; }
        }
        public int[] ENCOURAGE
        {
            get { return encourage; }
            set { encourage = value; }
        }
        public int[] DEATHBREATH
        {
            get { return deathbreath; }
            set { deathbreath = value; }
        }
        public bool STUN
        {
            get { return stun; }
            set { stun = value; }
        }
        public bool REGENERATION
        {
            get { return regeneration; }
            set { regeneration = value; }
        }
        public bool WEEKNESS
        {
            get { return weekness; }
            set { weekness = value; }
        }
        public bool CURSE
        {
            get { return curse; }
            set { curse = value; }
        }
        public bool POWER
        {
            get { return power; }
            set { power = value; }
        }
        public bool ANGRY
        {
            get { return angry; }
            set { angry = value; }
        }
        public bool PROTECTORED
        {
            get { return protectored; }
            set { protectored = value; }
        }
        public bool ARMORED
        {
            get { return armored; }
            set { armored = value; }
        }
        public int[] GEOSHIELD
        {
            get { return geoshield; }
            set { geoshield = value; }
        }
        public int[] SOULBURN
        {
            get { return soulburn; }
            set { soulburn = value; }
        }
        public bool ARMORED2
        {
            get { return armored2; }
            set { armored2 = value; }
        }
        public bool SILENCE
        {
            get { return silence; }
            set { silence = value; }
        }
        public bool ARMORPIERCING
        {
            get { return armorpiercing; }
            set { armorpiercing = value; }
        }
        public int DMG
        {
            get { return dmg; }
            set { dmg = value; }
        }
        public int ARMOR
        {
            get { return armor; }
            set { armor = value; }
        }
        public int DMGPASSIVE
        {
            get { return dmgpassive; }
            set { dmgpassive = value; }
        }
        public int ARMORPASSIVE
        {
            get { return armorpassive; }
            set { armorpassive = value; }
        }
        public int[] METKA
        {
            get { return metka; }
            set { metka = value; }
        }
        public int[] PROTECT
        {
            get { return protect; }
            set { protect = value; }
        }
        public int[] ANGRY1
        {
            get { return angry1; }
            set { angry1 = value; }
        }
        public int[] BLESS
        {
            get { return bless; }
            set { bless = value; }
        }
        public int[] INJECT
        {
            get { return inject; }
            set { inject = value; }
        }
        public int[] UNFLESH
        {
            get { return unflesh; }
            set { unflesh = value; }
        }
    }
}
