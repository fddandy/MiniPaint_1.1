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
        SolidBrush sbFill;
        Point tempPoint;
        SolidBrush solidBrush;
        private string currentFileName = "";
        //Bitmap bmpA;
        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = saveFileDialog.Filter =  "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPEG|*.jpg";
            
            myPen = new Pen(button1.BackColor, (float)numericUpDown1.Value);
            myPen.EndCap = myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            nowyToolStripMenuItem_Click(null, null);
            solidBrush = new SolidBrush(Color.Aquamarine);
            sbFill = new SolidBrush(Color.Indigo);
            
            setFormText();
        }

        private void setFormText()
        {
            if(currentFileName == "")
            {
                Text = "Nowy";
            }
            else
            {
                Text = Path.GetFileNameWithoutExtension(currentFileName);
            }
        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox.Image = new Bitmap(600, 800);
            graphics = Graphics.FromImage(pictureBox.Image);
            graphics.Clear(Color.Transparent);
            currentFileName = "";
            setFormText();
        }

        private void otworzToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                pictureBox.Image = Image.FromFile(openFileDialog.FileName);
                graphics = Graphics.FromImage(pictureBox.Image);
                currentFileName = openFileDialog.FileName;
                setFormText();
            }
        }
        //zapis obrazku
        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(saveFileDialog.FileName);
            ImageFormat imageFormat = ImageFormat.Bmp;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFileName = saveFileDialog.FileName;
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
            setFormText();
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
                if(radioButtonCurve.Checked)
                {
                    // graphics.DrawEllipse(new Pen(Color.LavenderBlush, 6), e.X, e.Y, 20, 20);
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                    pictureBox.Refresh();
                    tempPoint = e.Location;
                }
            }
            
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (radioButtonCurve.Checked)
                {
                    // graphics.DrawEllipse(new Pen(Color.LavenderBlush, 6), e.X, e.Y, 20, 20);
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                   
                   // tempPoint = e.Location; zbedny bo to jest ostatnia faza cyklu rysowania, wznosimy myszke do gory
                }
                else if(radioButtonLine.Checked)
                {
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                }
                else if(radioButtonRectangle.Checked)
                {
                   // bmpA = new Bitmap(600, 800);
                  //  Graphics g = Graphics.FromImage(bmpA);
                   if(checkBoxFillIn.Checked)
                    {

                        graphics.FillRectangle(sbFill, Math.Min(tempPoint.X, e.X),
                                                 Math.Min(tempPoint.Y, e.Y),
                                                 Math.Abs(tempPoint.X - e.X),
                                                 Math.Abs(tempPoint.Y - e.Y));
                    }
                   
                    graphics.DrawRectangle(myPen, Math.Min(tempPoint.X, e.X), 
                                                  Math.Min(tempPoint.Y, e.Y), 
                                                  Math.Abs(tempPoint.X - e.X),
                                                  Math.Abs(tempPoint.Y - e.Y));
                }
                else if(radioButtonElipse.Checked)
                {
                    if (checkBoxFillIn.Checked)
                    {
                        graphics.FillEllipse(sbFill, Math.Min(tempPoint.X, e.X),
                                                Math.Min(tempPoint.Y, e.Y),
                                                Math.Abs(tempPoint.X - e.X),
                                                Math.Abs(tempPoint.Y - e.Y));
                    }
                   
                    graphics.DrawEllipse(myPen, Math.Min(tempPoint.X, e.X),
                                                 Math.Min(tempPoint.Y, e.Y),
                                                 Math.Abs(tempPoint.X - e.X),
                                                 Math.Abs(tempPoint.Y - e.Y));
                }

                    pictureBox.Refresh();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            myPen.Width = (float) numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog.Color;
                myPen.Color = colorDialog.Color;
            }
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFileName == "")
            {
                zapiszJakoToolStripMenuItem_Click(null, null);
            }
            else
            {
                pictureBox.Image.Save(currentFileName);
            }
            setFormText();
        }

       
        private void buttonFillIn_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonFillIn.BackColor = colorDialog.Color;
                sbFill.Color = colorDialog.Color;
            }
        }
    }
}
