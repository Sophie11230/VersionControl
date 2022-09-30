using Microsoft.Win32;
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
using UserMaintence.Entities;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace UserMaintence
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName;

            button1.Text = Resource1.Add;
            button2.Text = Resource1.Write;
            button3.Text = Resource1.Delete;

            listBox1.DataSource = users;
            listBox1.DisplayMember = "FullName";
            listBox1.ValueMember = "ID";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = textBox1.Text,

            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Vesszővel tagolt szöveg (*csv)|*.csv";
            sfd.DefaultExt = ".csv";
            sfd.AddExtension = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8); //megadott fájlnéven ment és felülírja az eddigi fájlt
                foreach (var u in users)
                {
                    sw.WriteLine($"{u.ID},{u.FullName}");
                    //writeline esetén mindent egy sorba ír és a végén entert ad, write() esetén minden egy sorba enter nélkül
                }
                sw.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var selected = (User)listBox1.SelectedItem;
            users.Remove(selected);
        }
    }
}
