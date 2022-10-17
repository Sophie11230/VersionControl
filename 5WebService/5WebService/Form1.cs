using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5WebService
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            ExchangeRates();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ExchangeRates()
        {
            var mnbService = new ServiceReference1.MNBArfolyamServiceSoapClient();

            var request = new ServiceReference1.GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            //richTextBox1.Text = result;
        }
    }
}
