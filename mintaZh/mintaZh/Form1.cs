using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace mintaZh
{
    public partial class Form1 : Form
    {
        List<mintaZh.OlympicResult.OlympicResult> results = new List<mintaZh.OlympicResult.OlympicResult>();

        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;
        public Form1()
        {
            InitializeComponent();
            LoadData("Summer_olympic_Medals.csv");
            GetYear();
            GetPos();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }
        private void LoadData(string fajlnev)
        {
            results.Clear();

            using (StreamReader sr = new StreamReader(fajlnev, Encoding.Default))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] sor = sr.ReadLine().Split(',');
                    int[] mlist = new int[3]
                    {
                        int.Parse(sor[5]), int.Parse(sor[6]), int.Parse(sor[6])
                    };
                    //mlist.Append(int.Parse(sor[5]));
                    //mlist.Append(int.Parse(sor[6]));
                    //mlist.Append(int.Parse(sor[7]));

                    mintaZh.OlympicResult.OlympicResult r = new mintaZh.OlympicResult.OlympicResult()
                    {
                        Year = int.Parse(sor[0]),
                        Country = sor[3],
                        Medals = mlist
                    };
                    results.Add(r);
                }
            }
        }
        private void GetYear()
        {
            var years = (from x in results
                        orderby x.Year ascending
                         select x.Year).Distinct();
            comboBox1.DataSource = years.ToList();
            //comboBox1.DisplayMember = "Year";
        }
        private int GetResult(mintaZh.OlympicResult.OlympicResult result)
        {
            int counter = 0;

            var sameYear = (from x in results
                           where x.Year.Equals(result.Year) && x.Country != result.Country
                           select x);

            foreach (var r in sameYear)
            {
                if (r.Medals[0] > result.Medals[0]) counter++;
                if (r.Medals[0] == result.Medals[0] && r.Medals[1] > result.Medals[1]) counter++;
                if (r.Medals[0] == result.Medals[0] && r.Medals[1] == result.Medals[1] && r.Medals[2] > result.Medals[2]) counter++;
            }

            return counter+1;
        }
        private void GetPos()
        {
            foreach (var r in results)
            {
                r.Position = GetResult(r);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateExcel();

        }

        private void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

               CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }
        private void CreateTable()
        {
            string[] headers = new string[]
            {
                "Helyezés",
                "Ország",
                "Arany",
                "Ezüst",
                "Bronz"
            };
            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }
            
            int currYear = (int)comboBox1.SelectedItem;

            var er = (from x in results
                      where x.Year.Equals(currYear)
                      orderby x.Position ascending
                      select x) ;

            int counter = 2;
            foreach (var e in er)
            {
                xlSheet.Cells[counter, 1] = e.Position;
                xlSheet.Cells[counter, 2] = e.Country;
                xlSheet.Cells[counter, 3] = e.Medals[0];
                xlSheet.Cells[counter, 4] = e.Medals[1];
                xlSheet.Cells[counter, 5] = e.Medals[2];

                counter++;
            }
            
        }
    }
}
