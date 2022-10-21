using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace _5WebService
{
    public partial class Form1 : Form
    {
        BindingList<Entities.RateData> Rates = new BindingList<Entities.RateData>();
        BindingList<string> currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            GetCurrencies();

            RefreshData();
            comboBox1.DataSource = currencies;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private string ExchangeRates()
        {
            var mnbService = new ServiceReference1.MNBArfolyamServiceSoapClient();

            var request = new ServiceReference1.GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            return result;
            //richTextBox1.Text = result;
        }

        private void ProcessXML(string result)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                Entities.RateData r = new Entities.RateData();
                Rates.Add(r);
                r.Date = DateTime.Parse(element.GetAttribute("date"));
                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null) continue;

                r.Currency = childElement.GetAttribute("curr");

                var egyseg = decimal.Parse(childElement.GetAttribute("unit"));
                var ertek = decimal.Parse(childElement.InnerText);

                if ( (ertek/egyseg)!=0)
                {
                    r.Value = ertek / egyseg;
                }                
            }
        }
        private void MakeChart()
        {
            chartRateData.DataSource = Rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;

        }
        private void RefreshData()
        {
            Rates.Clear();
            dataGridView1.DataSource = Rates;
            var results = ExchangeRates();
            ProcessXML(results);
            MakeChart();
        }
        private void GetCurrencies()
        {
            var mnbService = new ServiceReference1.MNBArfolyamServiceSoapClient();
            var currRequest = new ServiceReference1.GetCurrenciesRequestBody();
            var currResponse = mnbService.GetCurrencies(currRequest);
            var currResult = currResponse.GetCurrenciesResult;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(currResult);
            foreach (XmlElement element in xml.DocumentElement.ChildNodes[0])
            {

                currencies.Add(element.InnerText);
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
