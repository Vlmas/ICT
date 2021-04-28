using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public enum Tool
    {
        Line,
        Rectangle,
        Circle,
        Triangle,
        Pen,
        Fill,
        Eraser
    }

    public partial class Form1 : Form
    {
        Bitmap bitmap = default;
        Graphics graphics = default;
        Pen pen = default;
        Point previousPoint = default;
        Point currentPoint = default;
        Tool currentTool;
        Color fillColor = default;

        public Form1()
        {
            InitializeComponent();
            pen = new Pen(Color.Black);
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            pictureBox1.Image = bitmap;
            graphics.Clear(Color.White);
            currentTool = Tool.Line;
            fillColor = Color.White;
        }

        public Rectangle GetMRectangle(Point prevPoint, Point curPoint)
        {
            return new Rectangle {
                X = Math.Min(prevPoint.X, curPoint.X),
                Y = Math.Min(prevPoint.Y, curPoint.Y),
                Width = Math.Abs(prevPoint.X - curPoint.X),
                Height = Math.Abs(prevPoint.Y - curPoint.Y)
            };
        }

        public void LineButtonClicked(object sender, EventArgs arg)
        {
            currentTool = Tool.Line;
        }

        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Location.ToString();
            if(e.Button == MouseButtons.Left)
            {
                switch(currentTool)
                {
                    case Tool.Pen:
                        previousPoint = currentPoint;
                        currentPoint = e.Location;
                        graphics.DrawLine(pen, previousPoint, currentPoint);
                        break;
                    case Tool.Line:
                        currentPoint = e.Location;
                        break;
                    case Tool.Rectangle:
                        currentPoint = e.Location;
                        break;
                    case Tool.Circle:
                        currentPoint = e.Location;
                        break;
                    case Tool.Triangle:
                        currentPoint = e.Location;
                        break;
                    case Tool.Eraser:
                        currentPoint = e.Location;
                        graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(e.Location, new Size(20, 20)));
                        break;
                    default:
                        break;
                }

                pictureBox1.Refresh();
            }
        }

        private void PictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            previousPoint = e.Location;
            currentPoint = e.Location;

            if(currentTool == Tool.Fill)
            {
                //bitmap = Utils.Fill(bitmap, currentPoint, bitmap.GetPixel(e.X, e.Y), Color.Blue);
                MapFill fill = new MapFill();
                fill.Fill(graphics, currentPoint, fillColor, ref bitmap);
                graphics = Graphics.FromImage(bitmap);
                pictureBox1.Image = bitmap;
                pictureBox1.Refresh();
            }
        }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;

            switch(currentTool)
            {
                case Tool.Line:
                    e.Graphics.DrawLine(pen, previousPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    e.Graphics.DrawRectangle(pen, GetMRectangle(previousPoint, currentPoint));
                    break;
                case Tool.Circle:
                    e.Graphics.DrawEllipse(pen, GetMRectangle(previousPoint, currentPoint));
                    break;
                case Tool.Triangle:
                    e.Graphics.DrawPolygon(pen, new Point[] { previousPoint, currentPoint, new Point(currentPoint.X, 50) });
                    break;
            }
        }

        private void PictureBoxMouseUp(object sender, MouseEventArgs e)
        {
            switch(currentTool)
            {
                case Tool.Line:
                    graphics.DrawLine(pen, previousPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    graphics.DrawRectangle(pen, GetMRectangle(previousPoint, currentPoint));
                    break;
                case Tool.Circle:
                    graphics.DrawEllipse(pen, GetMRectangle(previousPoint, currentPoint));
                    break;
                case Tool.Triangle:
                    graphics.DrawPolygon(pen, new Point[] { previousPoint, currentPoint, new Point(currentPoint.X, 50) });
                    break;
            }
            previousPoint = e.Location;
        }

        private void RectangleButtonClicked(object sender, EventArgs e)
        {
            currentTool = Tool.Rectangle;
        }

        private void PenButtonClicked(object sender, EventArgs e)
        {
            currentTool = Tool.Pen;
        }

        private void SaveToolStripMenuItemClicked(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void OpenToolStripMenuItemClicked(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap = (Bitmap) Bitmap.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = bitmap;
                graphics = Graphics.FromImage(bitmap);
            }
        }

        private void CircleButtonClicked(object sender, EventArgs e)
        {
            currentTool = Tool.Circle;
        }

        private void FillButtonClicked(object sender, EventArgs e)
        {
            currentTool = Tool.Fill;
        }

        private void EraserButtonClicked(object sender, EventArgs e)
        {
            currentTool = Tool.Eraser;
        }

        private void ColorPickerClicked(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog { AllowFullOpen = true };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pen.Color = dialog.Color;
                fillColor = dialog.Color;
            }
        }

        private void TriangleButtonClicked(object sender, EventArgs e)
        {
            currentTool = Tool.Triangle;
        }

        private void ApplyThicknessClicked(object sender, EventArgs e)
        {
            int newWidth;
            if(int.TryParse(textBox1.Text, out newWidth))
            {
                pen.Width = newWidth;
            }
            else
            {
                MessageBox.Show("Enter a proper number!");
            }
        }
    }
}