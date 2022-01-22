using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using WindowsFormsApp1;

namespace proje_ekip
{
    public partial class sifreleme : Form
    {
        public sifreleme()
        {
            InitializeComponent();
        }
        private Bitmap bmp = null;
        private string cikarilanMetin = string.Empty;

        private void button4_Click_1(object sender, EventArgs e)
        {
            //şifre çöz butonu
            bmp = (Bitmap)pictureBox1.Image;
            string coz = islem.Coz(bmp);
            txtMesaj.Text = "";
            txtMesaj.Text = coz;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // resim aç butonu 
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Resim Dosyaları (*.png,*.bmp,*.jpg)|*.png;*.bmp;*.jpg";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dialog.FileName);
                button3.Enabled = true;
            }
            int lsb1, lsb2;
            for (int i = 0; i < pictureBox1.Height; i++)
            {
                for (int j = 0; j < pictureBox1.Width; j++)
                {
                    lsb1 = pictureBox1.Height * pictureBox1.Width * 3;
                    lsb2 = lsb1 / 8;
                    toolStripSayi.Text = lsb2.ToString();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // kaydet butonu 
            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

            if (save_dialog.ShowDialog() == DialogResult.OK)
            {
                switch (save_dialog.FilterIndex)
                {
                    case 0:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Png);
                        }
                        break;
                    case 1:
                        {
                            bmp.Save(save_dialog.FileName, ImageFormat.Bmp);
                        }
                        break;
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // şifrele butonu 
            Console.WriteLine("...");
            bmp = (Bitmap)pictureBox1.Image;
            string yazi = txtMesaj.Text;
            bmp = islem.yaziSifrele(yazi, bmp);
            MessageBox.Show("İşlendi. Resmi Kaydetmeyi unutmayın!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}

