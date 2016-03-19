using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example1
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            pictureBox1.Image = bmp;
        }

        Queue<Point> q = new Queue<Point>();

        private void button1_Click(object sender, EventArgs e)
        {
            g.DrawRectangle(new Pen(Color.Red), 0, 0, 250, 250);
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //F1(e.Location);
            F2(e.Location);
        }

        private void F2(Point location)
        {
            SimplePaint.MapFill m = new SimplePaint.MapFill();
            m.Fill(g, location, Color.Blue, ref bmp);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
        }

        private void F1(Point pp)
        {
            q.Enqueue(pp);

            Color origin = bmp.GetPixel(pp.X, pp.Y);

            while (q.Count > 0)
            {
                Point p = q.Dequeue();
                Step(p.X + 1, p.Y, origin, Color.Blue);
                Step(p.X - 1, p.Y, origin, Color.Blue);
                Step(p.X, p.Y + 1, origin, Color.Blue);
                Step(p.X, p.Y - 1, origin, Color.Blue);
            }
        }

        private void Step(int x, int y, Color origin, Color fillColor)
        {
            if (x < 0 || y < 0) return;
            if (x > pictureBox1.Width || y > pictureBox1.Height) return;
            if (!colorsAreSame(bmp.GetPixel(x, y), origin)) return;
            bmp.SetPixel(x, y, fillColor);
            q.Enqueue(new Point(x, y));

            pictureBox1.Refresh();
        }

        private bool colorsAreSame(Color locationColor, Color origin)
        {
            if (locationColor.A == origin.A)
                if (locationColor.R == origin.R)
                    if (locationColor.G == origin.G)
                        if (locationColor.B == origin.B)
                            return true;
            return false;
        }
    }
}
