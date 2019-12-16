using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapGenerator
{
    public partial class Form1 : Form
    {
        MapGridHelper GridMap;
        Point MousePoint;
        Image SelectedImage;

        public Form1()
        {
            InitializeComponent();

            typeof(Control).InvokeMember("DoubleBuffered",
                 System.Reflection.BindingFlags.SetProperty |
                 System.Reflection.BindingFlags.Instance |
                 System.Reflection.BindingFlags.NonPublic
                 , null, MapGridView, new object[] { true });

            this.Controls.Add(MapGridView);

            GridMap = new MapGridHelper(ref MapGridView, Convert.ToInt32(Height.Text),
                Convert.ToInt32(Width.Text));
        }

        private void GenerateMap_Click(object sender, EventArgs e)
        {

        }

        private void RefreshOption_Click(object sender, EventArgs e)
        {
            GridMap.Refresh(Convert.ToInt32(Height.Text),
                Convert.ToInt32(Width.Text));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox1.Image;
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox20.Image;
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox21.Image;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox2.Image;

        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox19.Image;

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox11.Image;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox3.Image;

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox18.Image;

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox10.Image;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox4.Image;

        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox17.Image;

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox12.Image;

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox5.Image;

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox16.Image;

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox9.Image;

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox13.Image;

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox15.Image;

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox6.Image;

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox7.Image;

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox14.Image;

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox8.Image;

        }

        private void Shutdown_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MousePoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (MousePoint.X - e.X),
                    this.Top - (MousePoint.Y - e.Y));
            }
        }

        private void Map_MouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (SelectedImage == null)
                    return;

                GridMap.FillCellImage(e.ColumnIndex, e.RowIndex, SelectedImage);
            }
            else
            {
                GridMap.RemoveCell(e.ColumnIndex, e.RowIndex);
            }
        }

        private void Map_MouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (SelectedImage == null)
                    return;

                GridMap.FillCellImage(e.ColumnIndex, e.RowIndex, SelectedImage);
            }
            else if((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                GridMap.RemoveCell(e.ColumnIndex, e.RowIndex);
            }
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox22.Image;
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox23.Image;
        }
    }
}
