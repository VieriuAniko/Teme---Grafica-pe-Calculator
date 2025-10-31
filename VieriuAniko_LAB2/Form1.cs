using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VieriuAniko_LAB2
{
    public partial class Form1 : Form
    {
        private NumericUpDown numericLaturi;
        private NumericUpDown numericRaza;
        private Button btnDraw;
        private Label lblInfo;
        private PictureBox pic;

        private int sides = 5;
        private float radius = 100f;

        public Form1()
        {
            Text = "Poligon regulat inscris in cerc";
            Width = 900;
            Height = 600;
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;

            // Controale stanga
            var left = new Panel { Dock = DockStyle.Left, Width = 320, Padding = new Padding(16) };
            Controls.Add(left);

            var lblN = new Label { Text = "Numar laturi (n): ", AutoSize = true, Top = 10, Left = 10 };
            numericLaturi = new NumericUpDown { Minimum = 3, Maximum = 60, Value = 5, Left = 10, Top = 32, Width = 120 };
            var lblR = new Label { Text = "Raza (r): ", AutoSize = true, Top = 70, Left = 10 };
            numericRaza = new NumericUpDown { Minimum = 10, Maximum = 1000, Value = 100, Left = 10, Top = 92, Width = 120 };

            btnDraw = new Button { Text = "Deseneaza", Left = 10, Top = 135, Width = 180, Height = 32 };
            btnDraw.Click += (_, __) =>
            {
                sides = (int)numericLaturi.Value;
                radius = (float)numericRaza.Value;
                lblInfo.Text = $"Desenez poligon regulat cu {sides} laturi si raza {radius}";
                pic.Invalidate();
            };

            lblInfo = new Label { Text = "—", AutoSize = true, Left = 10, Top = 180, ForeColor = Color.DimGray };

            left.Controls.AddRange(new Control[] { lblN, numericLaturi, lblR, numericRaza, btnDraw, lblInfo });

            // Zona de desen
            pic = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            pic.Paint += Pic_Paint;
            Controls.Add(pic);
            //InitializeComponent();
        }

        private void Pic_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // centru in mijlocul pictureBox-ului
            float cx = pic.ClientSize.Width / 2f;
            float cy = pic.ClientSize.Height / 2f;

            // asigurare ca raza incape
            float maxR = 0.9f * Math.Min(pic.ClientSize.Width, pic.ClientSize.Height) / 2f;
            float R = Math.Min(radius, maxR);

            // setam un unghi de start
            double theta0 = 0.0;

            // calculam varfurile
            var pts = Enumerable.Range(0, sides)
                .Select(k =>
                {
                    double t = theta0 + 2.0 * Math.PI * k / sides;
                    float x = cx + R * (float)Math.Cos(t);
                    float y = cy - R * (float)Math.Sin(t); // minus la sin, pt ca Y e invers pe ecran
                    return new PointF(x, y);
                })
                .ToArray();

            // cercul ghid
            using (var penCircle = new Pen(Color.Gainsboro, 2f))
                g.DrawEllipse(penCircle, cx - R, cy - R, 2 * R, 2 * R);

            // poligonul
            using (var penPoly = new Pen(Color.MediumOrchid, 2f))
                g.DrawPolygon(penPoly, pts);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
