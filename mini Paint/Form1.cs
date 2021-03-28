using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mini_Paint
{
    public partial class Form1 : Form
    {
        public Color FigureColor { get; set; }
        IFactory FigureFactory { get; set; }
        public Form1()
        {
            InitializeComponent();
            List<string> figures = new List<string> { "Triangle", "Circle", "Rectangle" };
            guna2ComboBox1.Items.AddRange(figures.ToArray());
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = guna2ComboBox1.SelectedItem.ToString();
            if (item == "Triangle")
            {
                FigureFactory = new TriangleFactory();
            }
            else if (item == "Rectangle")
            {
                FigureFactory = new RectangleFactory();
            }
            else if (item == "Circle")
            {
                FigureFactory = new CircleFactory();
            }
        }
        List<IFigure> Figures = new List<IFigure>();
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (FigureFactory.GetFigure() is Rectangle rec)
            {
                rec.Color = FigureColor;
                rec.Point = e.Location;
                rec.size = new Size(int.Parse(widthTxtBx.Text), int.Parse(HeightTxtbx.Text));
                rec.isFilled = true;

                Figures.Add(rec);
            }
            this.Refresh();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(FigureColor, 3);
            SolidBrush brush = new SolidBrush(FigureColor);
            using (var a = e.Graphics)
            {
                foreach (var item in Figures)
                {
                    if (item is Rectangle rec)
                    {
                        if (rec.isFilled)
                        {
                            a.FillRectangle(brush, rec.Point.X, rec.Point.Y, rec.size.Width, rec.size.Height);
                        }
                        else
                        {

                            a.DrawRectangle(pen, rec.Point.X, rec.Point.Y, rec.size.Width, rec.size.Height);

                        }
                    }

                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            var result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FigureColor = colorDialog.Color;
            }
        }
    }
    interface IFigure
    {
        Point Point { get; set; }
        Size size { get; set; }
        Color Color { get; set; }
        bool isFilled { get; set; }
    }
    class Rectangle : IFigure
    {
        public Point Point { get; set; }
        public Size size { get; set; }
        public Color Color { get; set; }
        public bool isFilled { get; set; }
    }
    class Circle : IFigure
    {
        public Point Point { get; set; }
        public Size size { get; set; }
        public Color Color { get; set; }
        public bool isFilled { get; set; }
    }
    class Triangle : IFigure
    {
        public Point Point { get; set; }
        public Size size { get; set; }
        public Color Color { get; set; }
        public bool isFilled { get; set; }
    }
    interface IFactory
    {
        IFigure GetFigure();

    }
    class CircleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new Circle();
        }
    }
    class TriangleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new Triangle();
        }
    }
    class RectangleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new Rectangle();
        }
    }

}
