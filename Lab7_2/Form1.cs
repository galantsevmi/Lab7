using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7_2
{
    public partial class Form1 : Form
    {
        int red = 125, green = 125, blue = 125;
        string toolTipText;
        ToolTip toolTip1 = new ToolTip();

        private void ChangeColor()
        {
            pictureBox1.BackColor = Color.FromArgb(red, green, blue);
        }
        private string GetColorString()
        {
           return "#" + red.ToString("X").PadLeft(2, '0') + green.ToString("X").PadLeft(2, '0') + blue.ToString("X").PadLeft(2, '0');

        }
        public Form1()
        {
            InitializeComponent();
            ChangeColor();
            tableLayoutPanel1.Width = this.ClientSize.Width - 20;
            tableLayoutPanel1.Height = this.ClientSize.Height - 20;
            tableLayoutPanel1.Location = new Point(10, 10);
        }
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTipText = GetColorString();
            toolTip1.SetToolTip(this.pictureBox1, toolTipText);
        }
        private void trackBarGreen_Scroll(object sender, EventArgs e)
        {
            green = trackBarGreen.Value;
            ChangeColor();
            Clipboard.SetText(GetColorString());
        }

        private void trackBarBlue_Scroll(object sender, EventArgs e)
        {
            blue = trackBarBlue.Value;
            ChangeColor();
            Clipboard.SetText(GetColorString());
        }

        private void trackBarRed_Scroll(object sender, EventArgs e)
        {
            red = trackBarRed.Value;
            ChangeColor();
            Clipboard.SetText(GetColorString());
        }
    }
}
