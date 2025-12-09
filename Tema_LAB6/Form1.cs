using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_LAB6
{
    public partial class Form1 : Form
    {
        // Lista de puncte de control (P0, P1, P2, P3)
        private List<Point> controlPoints = new List<Point>();

        public Form1()
        {
            InitializeComponent();
        }

        // Eveniment de click pe fereastră - adăugăm puncte de control
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Acceptăm maxim 4 puncte
                if (controlPoints.Count < 4)
                {
                    controlPoints.Add(e.Location);
                    Invalidate(); // redesenează
                }
            }
        }

        // Butonul de resetare - șterge punctele și curba
        private void btnReset_Click(object sender, EventArgs e)
        {
            controlPoints.Clear();
            Invalidate();
        }

        // Desenăm punctele, poligonul de control și curba Bézier (dacă avem 4 puncte)
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // fundal alb
            g.Clear(Color.White);

            // dacă avem cel puțin 1 punct, desenează punctele
            foreach (var p in controlPoints)
            {
                // punct roșu mic
                g.FillEllipse(Brushes.Red, p.X - 4, p.Y - 4, 8, 8);
            }

            // dacă avem cel puțin 2 puncte, desenează poligonul de control (linii gri)
            if (controlPoints.Count >= 2)
            {
                g.DrawLines(Pens.Gray, controlPoints.ToArray());
            }

            // dacă avem exact 4 puncte, desenează curba Bézier cubică
            if (controlPoints.Count == 4)
            {
                DrawBezier(g, controlPoints.ToArray());
            }
        }

        // Funcția care trasează curba Bézier cubică unind puncte succesive
        private void DrawBezier(Graphics g, Point[] pts)
        {
            int steps = 100; // numărul de segmente
            PointF prev = BezierPoint(pts, 0f);

            for (int i = 1; i <= steps; i++)
            {
                float t = i / (float)steps;
                PointF next = BezierPoint(pts, t);

                // linie albastră între puncte succesive pe curbă
                g.DrawLine(Pens.Blue, prev, next);
                prev = next;
            }
        }

        // Calculează un punct pe curba Bézier cubică pentru un anumit t ∈ [0,1]
        private PointF BezierPoint(Point[] pts, float t)
        {
            // Formula standard:
            // B(t) = (1−t)^3 P0 + 3(1−t)^2 t P1 + 3(1−t) t^2 P2 + t^3 P3
            float oneMinusT = 1 - t;

            float x =
                (float)(Math.Pow(oneMinusT, 3) * pts[0].X +
                        3 * Math.Pow(oneMinusT, 2) * t * pts[1].X +
                        3 * oneMinusT * Math.Pow(t, 2) * pts[2].X +
                        Math.Pow(t, 3) * pts[3].X);

            float y =
                (float)(Math.Pow(oneMinusT, 3) * pts[0].Y +
                        3 * Math.Pow(oneMinusT, 2) * t * pts[1].Y +
                        3 * oneMinusT * Math.Pow(t, 2) * pts[2].Y +
                        Math.Pow(t, 3) * pts[3].Y);

            return new PointF(x, y);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
