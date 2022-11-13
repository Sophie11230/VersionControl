using _6Santa.Abstraction;
using _6Santa.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6Santa
{
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();

        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
                DisplayNext();
            }
        }
        public Toy _nextToy;

        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
            panel1.Width = ClientSize.Width;
            buttonColor.BackColor = Color.Fuchsia;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            panel1.Controls.Add(toy);

        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            int maxPos = 0;
            foreach (var b in _toys)
            {
                b.MoveToy();
                if (b.Left > maxPos) maxPos = b.Left;
            }
            if (maxPos >= 1000)
            {
                var last = _toys[0];
                _toys.Remove(last);
                panel1.Controls.Remove(last);
            }
        }

        private void buttonCar_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void buttonBall_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory
            {
                BallColor = buttonColor.BackColor
            };
            
        }
        private void DisplayNext()
        {
            if (_nextToy != null) panel1.Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Left = label1.Left;
            _nextToy.Top = label1.Top +10;
            panel1.Controls.Add(_nextToy);
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var ColorPicker = new ColorDialog();
            ColorPicker.Color = buttonColor.BackColor;
            if (ColorPicker.ShowDialog() != DialogResult.OK) return;
            button.BackColor = ColorPicker.Color;

        }
    }
}
