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
        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = saveFileDialog.Filter =  "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPEG|*.jpg";
        }

        private void otworzToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                pictureBox.Image = Image.FromFile(openFileDialog.FileName);
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
    }
}
