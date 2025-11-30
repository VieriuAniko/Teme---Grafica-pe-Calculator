using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_LAB4
{
    public partial class Form1 : Form
    {
        private Bitmap _canvas;
        private List<Point> _polygon;
        private Color _fillColor = Color.LightBlue;
        private Color _outlineColor = Color.Black;
        private Color _backgroundColor = Color.White;

        public Form1()
        {
            InitializeComponent();

            // Setting form size
            this.Width = 800;
            this.Height = 600;

            // Creating bitmap to draw on 
            _canvas = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            // Example polygon
            _polygon = new List<Point>
            {
                new Point(100, 100),
                new Point(300, 80),
                new Point(350, 200),
                new Point(250, 250),
                new Point(150, 220)
            };

            // Redraw when form loaded or resized
            this.Load += Form1_Load;
            this.Resize += Form1_Resize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Redraw();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.ClientSize.Width <= 0 || this.ClientSize.Height <= 0)
                return;

            _canvas = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            Redraw();
        }

        private void Redraw()
        {
            using (Graphics g = Graphics.FromImage(_canvas))
            {
                g.Clear(_backgroundColor);

                // First drawing the outline of polygon
                DrawPolygonOutline(g, _polygon, _outlineColor);

                // Then fill its interior using the scanline algorithm
                ScanlineFill(_polygon, _fillColor);
            }

            pictureBox1.Image = _canvas;
        }

        private void DrawPolygonOutline(Graphics g, List<Point> poly, Color color)
        {
            if (poly.Count < 2)
                return;

            using (Pen p = new Pen(color))
            {
                for (int i = 0; i < poly.Count; i++)
                {
                    Point a = poly[i];
                    Point b = poly[(i + 1) % poly.Count];
                    g.DrawLine(p, a, b);
                }
            }
        }

        private void ScanlineFill (List<Point> poly, Color fillColor)
        {
            if (poly == null || poly.Count < 3)
                return;

            // Finding global ymin and ymax of polygon
            int ymin = poly.Min(p => p.Y);
            int ymax = poly.Max(p => p.Y);

            // For every scanline y
            for (int y = ymin; y < ymax; y++)
            {
                List<float> intersections = new List<float>();

                // For each edge of polygon
                for (int i = 0; i < poly.Count; i++)
                {
                    Point p1 = poly[i];
                    Point p2 = poly[(i + 1) % poly.Count];

                    // Skipping horizontal edges
                    if (p1.Y == p2.Y)
                        continue;

                    // Determining ymin and ymax for this edge
                    int edgeYMin = Math.Min(p1.Y, p2.Y);
                    int edgeYMax = Math.Max(p1.Y, p2.Y);

                    // Including intersections for y in [edgeYMin, edgeYMax]
                    // -> includes lower endpoint, exclude upper
                    if (y >= edgeYMin && y < edgeYMax)
                    {
                        // Parametric form to find x of intersection:
                        // x = x1 + (y - y1) * (x2 - x1) / (y2 - y1)
                        float x = p1.X + (float)(y - p1.Y) * (p2.X - p1.X) / (float)(p2.Y - p1.Y);
                        intersections.Add(x);
                    }
                }

                // Sorting intersection points by X
                intersections.Sort();

                // Now filling between pairs of intersections
                for (int k = 0; k < intersections.Count - 1; k += 2)
                {
                    int xStart = (int)Math.Ceiling(intersections[k]);
                    int xEnd = (int)Math.Floor(intersections[k + 1]);
                    
                    for (int x = xStart; x < xEnd; x++)
                    {
                        if (x >= 0 && x < _canvas.Width && y >= 0 && y < _canvas.Height)
                        {
                            _canvas.SetPixel(x, y, fillColor);
                        }
                    }
                }
            }
        }
    }
}
