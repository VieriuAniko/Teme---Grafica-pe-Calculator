using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_LAB3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Text = "Translatie triunghi";
            this.Size = new Size(800, 600);
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float unit = 60f;

            base.OnPaint(e);

            Graphics g = e.Graphics;

            // 1. Varfurile triunghiului initial (in pixeli)
            PointF[] originalTriangle = new PointF[]
            {
                new PointF(200, 300),               // A
                new PointF(200 - unit, 300 + unit), // B
                new PointF(200 + unit, 300 + unit)  // C
            };

            // 2. Desenare triunghi original 
            using (Pen pOriginal = new Pen(Color.Blue, 2))
            {
                g.DrawPolygon(pOriginal, originalTriangle);
            }

            // 3. Vectorul de translatie (dx, dy) = (2, 3)
            float dx = 2f * unit;
            float dy = 3f * unit;

            // 4. Aplicare translatie folosind matricea de translatie in coordonate omogene:
            // [ 1  0  dx ]
            // [ 0  1  dy ]
            // [ 0  0  1  ]
            PointF[] translatedTriangle = new PointF[originalTriangle.Length];

            for (int i = 0; i < originalTriangle.Length; i++)
            {
                translatedTriangle[i] = ApplyTranslation(originalTriangle[i], dx, dy);
            }

            // 5. Desenare triunghi translat 
            using (Pen pTranslated = new Pen(Color.Red, 2))
            {
                g.DrawPolygon(pTranslated, translatedTriangle);
            }    

            // (Optional) Etichete pentru a vedea mai bine fiecare ce reprezinta
            using (Font f = new Font("Arial", 10))
            using (Brush b = new SolidBrush(Color.Black))
            {
                g.DrawString("Triunghi original", f, b, 50, 50);
                g.DrawString("Triunghi translat cu vectorul (2, 3)", f, b, 50, 70);
            }
        }

        /// <summary>
        ///  Aplica translatia unui punct P(x, y) dupa vectorul (dx, dy)
        ///  folosind forma matriceala in coordonate omogene
        /// </summary>
        /// 
        private PointF ApplyTranslation (PointF p, float dx, float dy)
        {
            // Vectorul omogen al punctului: [x, y, 1]
            float x = p.X;
            float y = p.Y;
            float w = 1f;

            // Matricea de translatie T: 
            // [ 1  0  dx ]
            // [ 0  1  dy ]
            // [ 0  0   1 ]
            float[,] T = new float[,]
            {
                { 1f, 0f, dx },
                { 0f, 1f, dy },
                { 0f, 0f, 1f }
            };

            // Inmultire T * [x, y, 1]^T
            float xNew = x * T[0, 0] + y * T[0, 1] + w * T[0, 2];
            float yNew = x * T[1, 0] + y * T[1, 1] + w * T[1, 2];

            return new PointF(xNew, yNew); // w ramane 1, deci nu mai trebuie impartire
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
