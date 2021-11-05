using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RectanglesWindowsForms
{
    public partial class Form1 : Form
    {
        Point point;
        List<Rectangle> rectangles = new List<Rectangle>();
        public static int SerialNumber { get; set; } = 0;
        public Form1()
        {
            InitializeComponent();
        }

        // сохраняет координаты первой точки
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            point = e.Location;
        }
        // создает на основе координат прямоугольник
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Rectangle rect = new Rectangle(point, e.Location, SerialNumber++);
                if (rect.Height > 10 && rect.Width > 10)
                {
                    Label label = new Label();
                    label.Location = rect.P1;
                    label.BackColor = rect.color;
                    label.Size = new Size(rect.Height, rect.Width);
                    label.Text = rect.MyNumber.ToString();
                    label.Visible = true;
                    label.AutoSize = false;
                    label.MouseDoubleClick += new MouseEventHandler(this.label_DoubleClick);
                    label.MouseClick += new MouseEventHandler(this.label_Click);
                    this.Controls.Add(label);
                    this.rectangles.Add(rect);
                }
                else
                {
                    MessageBox.Show("Размер прямоугольника должен быть больше 10x10 px.", "Условие", MessageBoxButtons.OK);
                }
            }
            
        }
        // 2 левых клика для удаления прямоугольника
        private void label_DoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Controls.Remove((sender as Label));
            }
        }
        // правый клик для информации в шапке
        private void label_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Text = rectangles[Convert.ToInt32((sender as Label).Text)].ToString();
            }
        }

    }
    class Rectangle
    {
        public Point P1 { get; set; } // точка 1
        public Point P2 { get; set; } // точка 2 
        public int Height { get { return P1.X > P2.X ? P1.X - P2.X : P2.X - P1.X; } } // высота
        public int Width  { get { return P1.Y > P2.Y ? P1.Y - P2.Y : P2.Y - P1.Y; } } // ширина
        public Color color { get; set; } // цвет
        public int MyNumber { get; set; } // номер

        //конструктор
        public Rectangle(Point p1, Point p2, int number)
        {
            Random random = new Random();
            if(p1.X < p2.X && p1.Y < p2.Y)
            {
                this.P1 = p1;
                this.P2 = p2;
            }
            else if (p1.X > p2.X && p1.Y < p2.Y) 
            {
                this.P1 = new Point(p2.X, p1.Y); 
                this.P2 = new Point(p1.X, p2.Y); 
            }
            else if(p1.X < p2.X && p1.Y > p2.Y)
            {
                this.P1 = new Point(p1.X, p2.Y);
                this.P2 = new Point(p2.X, p1.Y);
            }
            else if (p1.X > p2.X && p1.Y > p2.Y)
            {
                this.P1 = new Point(p2.X, p2.Y);
                this.P2 = new Point(p1.X, p1.Y);
            }
            this.MyNumber = number;
            color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        } 

        // площадь и координаты в ToString()
        public override string ToString()
        {
            return $"[Площадь: {Width * Height} px.][Координаты: X = {P1.X} | Y = {P1.Y}]";
        }
    }
}
