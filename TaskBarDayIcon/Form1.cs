using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskBardDayIcon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            var img = DrawTextImage(DateTime.Now.Day.ToString());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 900000;
            timer1.Enabled = true;
            UpdateIcon();
        }
        private void UpdateIcon()
        {
            Bitmap bmp = new Bitmap(DrawTextImage(DateTime.Now.Day.ToString()), new Size(32, 32));
            var hicon = bmp.GetHicon();
            Icon ico = Icon.FromHandle(hicon);
            this.Icon = ico;
        }
        private Image DrawTextImage(String text)
        {
            Font font = Control.DefaultFont;
            Color textColor = Color.Black;
            Color backColor = Color.White;
            Size minSize = Size.Empty;
            return DrawTextImage(text, font, textColor, backColor, Size.Empty);
        }
        private Image DrawTextImage(String text, Font font, Color textColor, Color backColor, Size minSize)
        {
            SizeF textSize;
            using (Image img = new Bitmap(1, 1))
            {
                using (Graphics drawing = Graphics.FromImage(img))
                {
                    textSize = drawing.MeasureString(text, font);
                    if (!minSize.IsEmpty)
                    {
                        textSize.Width = textSize.Width > minSize.Width ? textSize.Width : minSize.Width;
                        textSize.Height = textSize.Height > minSize.Height ? textSize.Height : minSize.Height;
                    }
                }
            }
            Image retImg = new Bitmap((int)textSize.Width, (int)textSize.Height);
            using (var drawing = Graphics.FromImage(retImg))
            {
                drawing.Clear(backColor);
                using (Brush textBrush = new SolidBrush(textColor))
                {
                    drawing.DrawString(text, font, textBrush, 0, 0);
                    drawing.Save();
                }
            }
            return retImg;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateIcon();
        }
    }
}
