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
        List<Ball> _balls = new List<Ball>();

        private BallFactory _factory;

        public BallFactory Factory
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
            var ball = Factory.CreateNew();            
            _balls.Add(ball);
            ball.Left = -ball.Width;
            panel1.Controls.Add(ball);
            
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            int maxPos = 0;
            foreach (var b in _balls)
            {
                b.MoveBall();
                if (b.Left > maxPos) maxPos = b.Left;
            }
            if (maxPos >=1000)
            {
                var last = _balls[0];
                _balls.Remove(last);
                panel1.Controls.Remove(last);
            }
        }
    }
}
