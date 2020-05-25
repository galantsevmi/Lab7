using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7
{
    public partial class Form1 : Form
    {
        // измерение расстояния между двумя точками
        private double MeasureDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
        // масштабирование кнопки
        int buttonScale = 15;
        // scaleX - отношение расстояния до ближайшего края по X к общей длине формы
        // scaleY - отношение расстояния до ближайшего края по Y к общей ширине формы
        double scaleX, scaleY;
        // sideX - если точка слева середины формы по X, то false, иначе true
        // sideY - если точка сверху середины формы по Y, то false, иначе true
        bool sideX, sideY;
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(700, 500);
            button1.Width = button1.Height = (this.ClientSize.Width + this.ClientSize.Height) / buttonScale;
            // установка кнопка посередине формы
            int x = (this.ClientSize.Width - button1.Width) / 2;
            int y = (this.ClientSize.Height - button1.Height) / 2;
            button1.Location = new Point(x, y);
            scaleX = (double)button1.Location.X / this.ClientSize.Width;
            sideX = false;
            scaleY = (double)button1.Location.Y / this.ClientSize.Height;
            sideY = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // dButtonX - кратчайшее расстояние по X от мыши до кнопки
            // mButtonX - расстояние по X от мыши до середины ближайшей стороны кнопки или до ближайшего угла
            // c Y аналогично
            int dButtonX, dButtonY, mButtonX, mButtonY;
            // значения переменных зависят от того, по какую сторону от краёв кнопки находится мышь
            if (e.X < button1.Location.X)
            {
                mButtonX = dButtonX = button1.Location.X;
            }
            else if (e.X > button1.Location.X + button1.Width)
            {
                mButtonX = dButtonX = button1.Location.X + button1.Width;
            }
            else
            {
                dButtonX = e.X;
                mButtonX = button1.Location.X + button1.Width / 2;
            }

            if (e.Y < button1.Location.Y)
            {
                mButtonY = dButtonY = button1.Location.Y;
            }
            else if (e.Y > button1.Location.Y + button1.Height)
            {
                mButtonY = dButtonY = button1.Location.Y + button1.Height;
            }
            else
            {
                dButtonY = e.Y;
                mButtonY = button1.Location.Y + button1.Height / 2;
            }

            double distance = MeasureDistance(e.X, e.Y, dButtonX, dButtonY);
            // если мышь находится на достаточно малом расстоянии до кнопки
            if (distance < (button1.Width + button1.Height) / 5)
            {
                // движение относительно середины ближайшей стороны или ближайшего угла
                int newX = button1.Location.X + (mButtonX - e.X) / 2;
                int newY = button1.Location.Y + (mButtonY - e.Y) / 2;
                if (newX < 0)
                {
                    newX = 0;
                }
                else if (newX + button1.Width > this.ClientSize.Width)
                {
                    newX = this.ClientSize.Width - button1.Width;
                }

                if (newY < 0)
                {
                    newY = 0;
                }
                else if (newY + button1.Height > this.ClientSize.Height)
                {
                    newY = this.ClientSize.Height - button1.Height;
                }

                button1.Location = new Point(newX, newY);

                // Высчитываются расположение и пропорции для изменения размера окна
                // centerButtonX - координата середины кнопки по X
                int centerButtonX = button1.Location.X + button1.Width / 2;
                // centerButtonY - координата середины кнопки по Y
                int centerButtonY = button1.Location.Y + button1.Height / 2;
                // centerFormX - координата середины формы по X
                int centerFormX = this.ClientSize.Width / 2;
                // centerFormY - координата середины формы по Y
                int centerFormY = this.ClientSize.Height / 2;
                // значения переменных зависят от того, к какому краю кнопка ближе
                if (centerButtonX <= centerFormX)
                {
                    scaleX = (double)button1.Location.X / this.ClientSize.Width;
                    sideX = false;
                }
                else
                {
                    scaleX = (double)(this.ClientSize.Width - button1.Location.X - button1.Width) / this.ClientSize.Width;
                    sideX = true;
                }
                if (centerButtonY <= centerFormY)
                {
                    scaleY = (double)button1.Location.Y / this.ClientSize.Height;
                    sideY = false;
                }
                else
                {
                    scaleY = (double)(this.ClientSize.Height - button1.Location.Y - button1.Height) / this.ClientSize.Height;
                    sideY = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Поздравляем! Вы смогли нажать на кнопку!", "Победа");
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                // x и y - новые координаты формы
                // newWidth и newHeight - новые длина и ширина формы
                int x, y, newWidth, newHeight;
                newWidth = (this.ClientSize.Width + this.ClientSize.Height) / buttonScale;
                newHeight = (this.ClientSize.Width + this.ClientSize.Height) / buttonScale;
                // если в правой части формы
                if (sideX)
                {
                    // без изменения размера кнопки
                    x = this.ClientSize.Width - (int)(scaleX * this.ClientSize.Width) - button1.Width;
                    // с изменением размера кнопки
                    x += (newWidth - button1.Width);
                }
                // если в левой части формы
                else
                {
                    x = (int)(scaleX * this.ClientSize.Width);
                }
                // если в нижней части формы
                if (sideY)
                {
                    y = this.ClientSize.Height - (int)(scaleY * this.ClientSize.Height) - button1.Height;
                    y += (newWidth - button1.Width);
                }
                // если в верхней части формы
                else
                {
                    y = (int)(scaleY * this.ClientSize.Height);
                }
                // проверка на выход за границы
                if (x < 0)
                {
                    x = 0;
                }
                else if (x + newWidth > this.ClientSize.Width)
                {
                    x = this.ClientSize.Width - newWidth;
                }

                if (y < 0)
                {
                    y = 0;
                }
                else if (y + newWidth > this.ClientSize.Height)
                {
                    y = this.ClientSize.Height - newHeight;
                }
                button1.Width = newWidth;
                button1.Height = newHeight;
                button1.Location = new Point(x, y);
            }
        }

    }
}
