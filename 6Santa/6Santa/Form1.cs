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
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
            panel1.Width = ClientSize.Width;
           
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
            if (maxPos >=1000)
            {
                var last = _toys[0];
                _toys.Remove(last);
                panel1.Controls.Remove(last);
            }
        }
    }
}
