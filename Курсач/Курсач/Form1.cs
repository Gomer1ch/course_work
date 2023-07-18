using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсач
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pen = new Pen(Color.Black,3);
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(bmp);
            timer1.Interval = 1;
            N = hScrollBar1.Value;
            label2.Text = "(Меньше = быстрее)";
        }
        Bitmap bmp;
        Graphics graph;
        Pen pen;
        int count = 0, count2 = 0;
        Point p1 = new Point();
        Point p2 = new Point();
        Point p3 = new Point();
        Point p4 = new Point();
        Point p5 = new Point();
        void DrawTriangle()
        {
            graph.DrawLine(pen, p1, p2);
            graph.DrawLine(pen, p2, p3);
            graph.DrawLine(pen, p3, p1);
            pictureBox1.Image = bmp;
        }
        void DrawLINIYA()
        {
            graph.DrawLine(pen, p4, p5);
            CentLine = new Point((p4.X + p5.X) / 2, (p4.Y + p5.Y) / 2);
            graph.DrawRectangle(pen, CentLine.X - 2, CentLine.Y - 2, 5, 5); //центральная точка на линии
            pictureBox1.Image = bmp;
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int CX = e.Location.X;
                int CY = e.Location.Y;
                count++;
                if (count == 1) { p1.X = CX; p1.Y = CY; }
                if (count == 2) { p2.X = CX; p2.Y = CY; }
                if (count == 3) { p3.X = CX; p3.Y = CY; count = 0; graph.Clear(pictureBox1.BackColor); DrawTriangle();}
            }
            if (e.Button == MouseButtons.Right)
            {
                int CX = e.Location.X;
                int CY = e.Location.Y;
                count2++;
                if (count2 == 1) { p4.X = CX; p4.Y = CY; }
                if (count2 == 2) { p5.X = CX; p5.Y = CY; count2 = 0; graph.Clear(pictureBox1.BackColor); DrawLINIYA(); DrawTriangle();}
            }
        }
        double rP1, rP2, rP3, angleP1, angleP2, angleP3;

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            N = hScrollBar1.Value;
            label1.Text = "Скорость = " + hScrollBar1.Value;
        }

        Point CentLine;
        int N; //Кол-во отрисовок
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            hScrollBar1.Enabled = false;
            CentLine = new Point((p4.X + p5.X) / 2, (p4.Y + p5.Y) / 2);
            rP1 = Math.Sqrt(Math.Pow(p1.X - CentLine.X, 2) + Math.Pow(p1.Y - CentLine.Y, 2));
            rP2 = Math.Sqrt(Math.Pow(p2.X - CentLine.X, 2) + Math.Pow(p2.Y - CentLine.Y, 2));
            rP3 = Math.Sqrt(Math.Pow(p3.X - CentLine.X, 2) + Math.Pow(p3.Y - CentLine.Y, 2));
            angleP1 = Math.Atan2(-p1.Y + CentLine.Y, p1.X - CentLine.X);
            angleP2 = Math.Atan2(-p2.Y + CentLine.Y, p2.X - CentLine.X);
            angleP3 = Math.Atan2(-p3.Y + CentLine.Y, p3.X - CentLine.X);
            //label1.Text = "" + angleP1;
            timer1.Start();
            
        }
        int T = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            graph.Clear(pictureBox1.BackColor);
            DrawLINIYA();
            T++;
            angleP1 += Math.PI / N;
            angleP2 += Math.PI / N;
            angleP3 += Math.PI / N;
            p1.X = Convert.ToInt32(CentLine.X + rP1 * Math.Cos(angleP1));
            p2.X = Convert.ToInt32(CentLine.X + rP2 * Math.Cos(angleP2));
            p3.X = Convert.ToInt32(CentLine.X + rP3 * Math.Cos(angleP3));
            p1.Y = Convert.ToInt32(CentLine.Y - rP1 * Math.Sin(angleP1));
            p2.Y = Convert.ToInt32(CentLine.Y - rP2 * Math.Sin(angleP2));
            p3.Y = Convert.ToInt32(CentLine.Y - rP3 * Math.Sin(angleP3));
            DrawTriangle();
            if (T >= N) { timer1.Stop(); T = 0; button1.Enabled = true; hScrollBar1.Enabled = true; }
        }
    }
}
