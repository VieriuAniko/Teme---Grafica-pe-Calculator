using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_LAB5
{
    public partial class Form1 : Form
    {
        // Imaginea originala (incarcata de utilizator)
        private Bitmap _originalImage = null;

        // Fereastra de clipping (dreptunghiul vizibil)
        private Rectangle _clipWindow = new Rectangle(100, 80, 300, 200);

        // Variabile pentru drag & drop al ferestrei de clipping
        private bool _isDragging = false;
        private Point _dragStart; // punctul unde a inceput drag-ul (in coordonate mouse-ului)
        private Point _initialClipPos; // pozitia initiala a ferestrei la inceputul drag-ului

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Selecteza o imagine";
                ofd.Filter = "Imagini|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Eliberam imaginea veche (daca exista)
                    if (_originalImage != null)
                    {
                        _originalImage.Dispose();
                    }

                    _originalImage = new Bitmap(ofd.FileName);

                    // Ajustam fereastra de clipping sa fie rezonabila
                    _clipWindow = new Rectangle(100, 80, 300, 200);

                    Invalidate(); // redesenam fereastra
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Fundal alb
            g.Clear(Color.White);

            // Desenam imaginea decupata doar in interiorul ferestrei de clipping
            if (_originalImage != null)
            {
                // Salvam regiunea initiala de clipping
                Region oldClip = g.Clip;

                // Setam regiunea de clipping la dreptunghiul nostru
                g.SetClip(_clipWindow);

                // Desenam imaginea; aici o desenez de la (0,0) in susul formularului,
                // dar se poate schimba daca vrei o alta pozitie.
                g.DrawImage(_originalImage, new Rectangle(0, 50, _originalImage.Width, _originalImage.Height));
                
                // Revenim la regiunea originala de clipping
                g.Clip = oldClip;
            }

            // Desenam conturul ferestrei de clipping peste tot
            using (Pen p = new Pen(Color.Red, 2))
            {
                g.DrawRectangle(p, _clipWindow);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Pornim drag-ul doar daca apasam in interiorul ferestrei de clipping
            if (e.Button == MouseButtons.Left && _clipWindow.Contains(e.Location))
            {
                _isDragging = true;
                _dragStart = e.Location;
                _initialClipPos = _clipWindow.Location;
            }
        }

        private void Form1_MouseMove (object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Delta de miscare a mouse-ului
                int dx = e.X - _dragStart.X;
                int dy = e.Y - _dragStart.Y;

                // Noua pozitie a ferestrei de clipping
                _clipWindow.Location = new Point(_initialClipPos.X + dx, _initialClipPos.Y + dy);

                Invalidate(); // redesenam pentru a vedea fereastra mutata
            }
        }


        private void Form1_MouseUp (object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
