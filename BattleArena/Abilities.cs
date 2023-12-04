using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleArena
{
    public partial class Abilities : Form
    {
        public Abilities()
        {
            InitializeComponent();
        }

        private void Abilities_Load(object sender, EventArgs e)
        {
            button3.Text = Form1.heroAbilities.ABILITIES[1];
            button4.Text = Form1.heroAbilities.ABILITIES[2];
            button5.Text = Form1.heroAbilities.ABILITIES[3];
            Form1.abilitieChoose = 5;
            
            if (Form1.abilitiesMode == "Обнуление")
            {
                if (Form1.heroAbilities.NOWKD[0] <= 0)
                    button3.Enabled = false;
                if (Form1.heroAbilities.NOWKD[1] <= 0)
                    button4.Enabled = false;
                if (Form1.heroAbilities.NOWKD[2] <= 0)
                    button5.Enabled = false;
            }
            else if (Form1.abilitiesMode == "Дублирование")
            {
                int haveAP = Form1.hMetamorph.HODB * 2;
                if (Form1.hMetamorph.HODM > 0)
                    haveAP += 1;

                if (Form1.heroAbilities.NEEDAP[0] > haveAP  || (Form1.hMetamorph.STATUS.WEEKNESS == true && Form1.heroAbilities.ATTACKABILITIES[0] == true))
                    button3.Enabled = false;
                else
                    button3.Enabled = true;

                if (Form1.heroAbilities.NEEDAP[1] > haveAP || (Form1.hMetamorph.STATUS.WEEKNESS == true && Form1.heroAbilities.ATTACKABILITIES[1] == true))
                    button4.Enabled = false;
                else
                    button4.Enabled = true;

                if (Form1.heroAbilities.NEEDAP[2] > haveAP  || (Form1.hMetamorph.STATUS.WEEKNESS == true && Form1.heroAbilities.ATTACKABILITIES[2] == true) || Form1.heroAbilities.ABILITIES[3] == "Скверна")
                    button5.Enabled = false;
                else
                    button5.Enabled = true;


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.abilitieChoose = 0;
            Form.ActiveForm.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1.abilitieChoose = 1;
            Form.ActiveForm.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1.abilitieChoose = 2;
            Form.ActiveForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.abilitieChoose = 5;
            Form.ActiveForm.Close();
        }
    }
}
