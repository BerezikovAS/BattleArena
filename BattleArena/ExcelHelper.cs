using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace BattleArena
{
    class ExcelHelper : IDisposable
    {
        private Application _excel;
        private Workbook _workbook;

        public ExcelHelper()
        {
            _excel = new Excel.Application();
        }

        public void Dispose()
        {
            try
            {
                _workbook.Close();
                _excel.Quit();
            }
            catch { }
        }

        public bool Open(string path)
        {
            try
            {
                _workbook = _excel.Workbooks.Open(path);
                return true;
            }
            catch (Exception ex){ }
            return false;
        }

        internal void Set(string name, string win)
        {
             try
             {  
             int row = 100;
             switch (name)
             {
                 case "Aeroturg":
                     row = 2;
                     break;
                 case "Angel":
                     row = 3;
                     break;
                 case "Aramusha":
                     row = 4;
                     break;
                 case "Archer":
                     row = 5;
                     break;
                 case "Spirit":
                     row = 6;
                     break;
                 case "Berserker":
                     row = 7;
                     break;
                 case "Butcher":
                     row = 8;
                     break;
                 case "Crossbowman":
                     row = 9;
                     break;
                 case "Cryomant":
                     row = 10;
                     break;
                 case "Cultist":
                     row = 11;
                     break;
                 case "Geomant":
                     row = 12;
                     break;
                 case "Golem":
                     row = 13;
                     break;
                 case "Guardian":
                     row = 14;
                     break;
                 case "Knight":
                     row = 15;
                     break;
                 case "Mage":
                     row = 16;
                     break;
                 case "Metamorph":
                     row = 17;
                     break;
                 case "Necromancer":
                     row = 18;
                     break;
                 case "Priest":
                     row = 19;
                     break;
                 case "Pyromant":
                     row = 20;
                     break;
                 case "Shaman":
                     row = 21;
                     break;
                 case "Temporarium":
                     row = 22;
                     break;
                 case "Thief":
                     row = 23;
                     break;
                 case "Warlock":
                     row = 24;
                     break;
                 case "Witch doctor":
                     row = 25;
                     break;
             }
            
            if (row < 100)
                {
                    int a = Convert.ToInt32(this.Get(win, row)) + 1;
                    //int b = Convert.ToInt32(this.Get("G", row)) + 1;
                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, win] = a;
                    //((Excel.Worksheet)_excel.ActiveSheet).Cells[row, "G"] = b;
                    //int b = ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, "G"].Value2;
                    //int a2 = Convert.ToInt32(this.Get("C", 2));
                }

             }
             catch (Exception ex) { }
            
        }

        internal object Get(string column, int row)
        {
            try
            {
                return ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, column].Value2;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null;
        }

        internal object GetValue(string name)
        {
            try
            {
                int row = 0;
                switch (name)
                {
                    case "Aeroturg":
                        row = 2;
                        break;
                    case "Angel":
                        row = 3;
                        break;
                    case "Aramusha":
                        row = 4;
                        break;
                    case "Archer":
                        row = 5;
                        break;
                    case "Spirit":
                        row = 6;
                        break;
                    case "Berserker":
                        row = 7;
                        break;
                    case "Butcher":
                        row = 8;
                        break;
                    case "Crossbowman":
                        row = 9;
                        break;
                    case "Cryomant":
                        row = 10;
                        break;
                    case "Cultist":
                        row = 11;
                        break;
                    case "Geomant":
                        row = 12;
                        break;
                    case "Golem":
                        row = 13;
                        break;
                    case "Guardian":
                        row = 14;
                        break;
                    case "Knight":
                        row = 15;
                        break;
                    case "Mage":
                        row = 16;
                        break;
                    case "Metamorph":
                        row = 17;
                        break;
                    case "Necromancer":
                        row = 18;
                        break;
                    case "Priest":
                        row = 19;
                        break;
                    case "Pyromant":
                        row = 20;
                        break;
                    case "Shaman":
                        row = 21;
                        break;
                    case "Temporarium":
                        row = 22;
                        break;
                    case "Thief":
                        row = 23;
                        break;
                    case "Warlock":
                        row = 24;
                        break;
                    case "Witch doctor":
                        row = 25;
                        break;
                }

                return ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, "E"].Value2 * 100;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null;
        }

        public void Save()
        {
            _workbook.Save();
        }
    }
}
