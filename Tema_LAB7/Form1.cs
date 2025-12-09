using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_LAB7
{
    public partial class Form1 : Form
    {
        // Generator aleator pentru pozițiile cercurilor "pufoase"
        private Random rand = new Random();

        public Form1()
        {
            InitializeComponent();

            // Dimensiune aproximativ 500x500
            this.Width = 500;
            this.Height = 500;

            // Evenimentul de desen
            this.Paint += PaintScene;
        }

        private void PaintScene(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // fundal albastru (cer)
            g.Clear(Color.SkyBlue);

            // centru al ferestrei
            float cx = ClientSize.Width / 2f;
            float cy = ClientSize.Height / 2f;

            // UN SINGUR NOR, rotund, în centru
            DrawFractalCloud(g, cx, cy, 90, 4);
        }

        /// <summary>
        /// Desenează un nor "pufoșel" folosind cercuri și recursivitate.
        /// </summary>
        /// 
        private void DrawFractalCloud(Graphics g, float x, float y, float radius, int depth)
        {
            if (depth == 0 || radius < 2f)
                return;

            using (Brush brush = new SolidBrush(Color.FromArgb(210, 255, 255, 255)))
            {
                g.FillEllipse(brush, x - radius, y - radius, radius * 2, radius * 2);
            }

            int children = 4; // 3–4 e ok

            for (int i = 0; i < children; i++)
            {
                float angle = (float)(rand.NextDouble() * 2 * Math.PI);

                // mai aproape de centru, ca să fie mai rotund
                float distance = radius * (0.25f + (float)rand.NextDouble() * 0.25f); // 0.25R–0.5R

                // copil puțin mai mare => nor mai plin
                float radiusFactor = 0.60f + (float)rand.NextDouble() * 0.20f; // 0.6R–0.8R

                float newX = x + (float)Math.Cos(angle) * distance;
                float newY = y + (float)Math.Sin(angle) * distance;

                DrawFractalCloud(g, newX, newY, radius * radiusFactor, depth - 1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
