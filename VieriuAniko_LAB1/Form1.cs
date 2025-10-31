using System;
using System.Drawing;
using System.Windows.Forms;

namespace VieriuAniko_LAB1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Leg evenimentul Paint de metoda de desenare
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Cele 3 puncte ale triunghiului
            Point p1 = new Point(80, 220);
            Point p2 = new Point(250, 90);
            Point p3 = new Point(330, 210);

            // Conectarea punctelor intre ele prin linii de diferite culori si grosimi
            g.DrawLine(new Pen(Color.Red, 2), p1, p2);
            g.DrawLine(new Pen(Color.Green, 3), p2, p3);
            g.DrawLine(new Pen(Color.Blue, 4), p3, p1);

            // Afisarea punctelor
            g.FillEllipse(Brushes.Black, p1.X - 3, p1.Y - 3, 6, 6);
            g.FillEllipse(Brushes.Black, p2.X - 3, p2.Y - 3, 6, 6);
            g.FillEllipse(Brushes.Black, p3.X - 3, p3.Y - 3, 6, 6);
        }
    }
}
