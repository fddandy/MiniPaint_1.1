using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPaint_1._0
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Pen myPen;
        Point tempPoint;
        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = saveFileDialog.Filter =  "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPEG|*.jpg";
            
            myPen = new Pen(Color.Purple, 8);
            myPen.EndCap = myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void otworzToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                pictureBox.Image = Image.FromFile(openFileDialog.FileName);
                graphics = Graphics.FromImage(pictureBox.Image);
            }
        }
        //zapis obrazku
        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(saveFileDialog.FileName);
            ImageFormat imageFormat = ImageFormat.Bmp;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (extension)
                {

                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;
                    case ".jpg":
                        imageFormat = ImageFormat.Jpeg;
                        break;


                }


               pictureBox.Image.Save(saveFileDialog.FileName, imageFormat);
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button ==MouseButtons.Left)
            {
                tempPoint = e.Location;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // graphics.DrawEllipse(new Pen(Color.LavenderBlush, 6), e.X, e.Y, 20, 20);
                graphics.DrawLine(myPen, tempPoint, e.Location);
                pictureBox.Refresh();
            }
            tempPoint = e.Location;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
