using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int id;
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(int id)
        {
            this.id = id;
            InitializeComponent();
        }
        sqlBaglantisi baglan = new sqlBaglantisi();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dosyaAc = new OpenFileDialog(); //OpenFileDialog sınıfından "dosyaAc" nesnesini oluşturduk.
                dosyaAc.Filter = "Resim Dosyaları|*.jpg|*.png|*.gif"; //bu kısımda açmak istediğimiz dosyaları filtreliyoruz.
                dosyaAc.ValidateNames = true;
                dosyaAc.Title = "Resim Yükle"; //Dosya açma penceresinin başlık ismini değiştirdik.
                byte[] imgx = null;
                id = GetId();
                if (dosyaAc.ShowDialog() == DialogResult.OK) //eğer kullanıcı dosya seçerse
                {
                    pictureBox1.Image = Image.FromFile(dosyaAc.FileName); //picturebox nesnesine açtığımız dosyanın yolunu ekliyoruz.
                    imgx = CreateImages(dosyaAc.FileName);
                    TxtResimYolu.Text = dosyaAc.FileName;
                    dosyaAc.Dispose(); //en son ise dosyaAc nesnesini sonlandırıyoruz.

                    SqlCommand komut = new SqlCommand("insert into gönderi(ıd, foto) " +
                            "values (@id, @p1)", baglan.Baglanti());
                    //komut.Parameters.AddWithValue("@p1", (Image)pictureBox1.Image);
                    komut.Parameters.Add("@p1", SqlDbType.Image).Value = imgx;
                    komut.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    komut.ExecuteNonQuery();
                    baglan.Baglanti().Close();
                    MessageBox.Show("Resim seçildi.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("resim seçilmedi...");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Seçim Sırasında Hata Oluştu." + hata.Message, "UYARI", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private static byte[] CreateImages(string FileName)
        {
            string DirPath = "C:\\Users\\pc-\\Desktop\\Yeni klasör";
            if (!System.IO.Directory.Exists(DirPath)) System.IO.Directory.CreateDirectory(DirPath);
            string picname = DirPath + @"\indir.jpg";

            System.Drawing.Image img = System.Drawing.Image.FromFile(picname);


            byte[] aryimg = null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                aryimg = ms.ToArray();
                ms.Close();
            }
            return aryimg;
        }

        private int GetId()
        {
            SqlCommand komut = new SqlCommand("select coalesce(ıd, -1) from gönderi order by ıd desc", baglan.Baglanti());
            object id = komut.ExecuteScalar();
            if (Convert.ToInt32(id) < 1) id = 1;

            return Convert.ToInt32(id) + 1;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2(id);
            frm2.Show();
        }

        private void TxtResimYolu_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string s = "";
            if (id > 0) s = "where ıd = " + id;

            SqlCommand komut = new SqlCommand("select foto from gönderi " + s , baglan.Baglanti());
            object oimg = komut.ExecuteScalar();

            if (oimg != null)
            {
                byte[] imgbytes = (byte[])oimg;
                System.IO.MemoryStream stimg = new System.IO.MemoryStream(imgbytes, true);
                stimg.Write(imgbytes, 0, imgbytes.Length);
                Bitmap img = new Bitmap(stimg);
                pictureBox1.Image = img;
            }
        }

        private void textResimAl_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
