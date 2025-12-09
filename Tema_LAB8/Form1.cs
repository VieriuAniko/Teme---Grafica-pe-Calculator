using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_LAB8
{
    public partial class Form1 : Form
    {
        private Random rand = new Random();

        public Form1()
        {
            InitializeComponent();

            // Evenimente
            this.Load += Form1_Load;
            trackBarPoints.Scroll += trackBarPoints_Scroll;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Setam textul label-ului la incarcare
            labelPoints.Text = $"Puncte: {trackBarPoints.Value}";
            DrawFern();
        }

        private void trackBarPoints_Scroll(object sender, EventArgs e)
        {
            labelPoints.Text = $"Puncte: {trackBarPoints.Value}";
            DrawFern();
        }

        private void DrawFern()
        {
            int width = pictureBoxFern.Width;
            int height = pictureBoxFern.Height;

            if (width <= 0 || height <= 0)
                return;

            int n = trackBarPoints.Value;

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);
            }

            // Coordonate pentru Barnsley Fern
            double x = 0.0;
            double y = 0.0;

            // Dimensiunile aproximative ale ferigii
            // x ~ [-2.5, 2.5], y ~ [0, 10]
            double scaleX = width / 6.0;   // puțin mai lat decât intervalul [-2.5, 2.5]
            double scaleY = height / 10.0; // înălțime 0..10

            for (int i = 0; i < n; i++)
            {
                double r = rand.NextDouble();
                double x1, y1;

                // Cele 4 transformări din Barnsley Fern
                if (r < 0.01)
                {
                    // T1
                    x1 = 0.0;
                    y1 = 0.16 * y;
                }
                else if (r < 0.86)
                {
                    // T2
                    x1 = 0.85 * x + 0.04 * y;
                    y1 = -0.04 * x + 0.85 * y + 1.6;
                }
                else if (r < 0.93)
                {
                    // T3
                    x1 = 0.20 * x - 0.26 * y;
                    y1 = 0.23 * x + 0.22 * y + 1.6;
                }
                else
                {
                    // T4
                    x1 = -0.15 * x + 0.28 * y;
                    y1 = 0.26 * x + 0.24 * y + 0.44;
                }

                x = x1;
                y = y1;

                // Convertim din coordonate matematice la pixeli
                int px = (int)(width / 2 + x * scaleX);
                int py = (int)(height - y * scaleY); // inversăm axa Y

                if (px >= 0 && px < width && py >= 0 && py < height)
                {
                    Color c = GetGradientColor(i, n);
                    bmp.SetPixel(px, py, c);
                }
            }

            pictureBoxFern.Image = bmp;
        }

        // Gradient simplu de la DarkGreen la LightGreen
        private Color GetGradientColor(int index, int total)
        {
            if (total <= 1)
                return Color.LightGreen;

            double t = (double)index / (total - 1);

            Color start = Color.DarkGreen;
            Color end = Color.LightGreen;

            int r = (int)(start.R + t * (end.R - start.R));
            int g = (int)(start.G + t * (end.G - start.G));
            int b = (int)(start.B + t * (end.B - start.B));

            // Asigurăm limitele în 0..255
            r = Math.Max(0, Math.Min(255, r));
            g = Math.Max(0, Math.Min(255, g));
            b = Math.Max(0, Math.Min(255, b));

            return Color.FromArgb(r, g, b);
        }

        private void labelPoints_Click(object sender, EventArgs e)
        {

        }
    }
}
