﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6Santa.Abstraction
{
    public abstract class Toy: Label
    {
        public Toy()
        {
            AutoSize = false;
            Width = 50;
            Height = Width;
            Top = 100;
            Paint += Toy_Paint;
        }

        private void Toy_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }
        protected abstract void DrawImage(Graphics g);

        public void MoveToy()
        {
            Left += 1;
        }
    }


}
