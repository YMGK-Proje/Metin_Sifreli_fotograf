using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
// mail ile iletişim için yazılan kütüphane

namespace proje_ekip
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        // maile random sayı gönderme
        Random rstgl = new Random();
        int kod;

        private void button1_Click(object sender, EventArgs e)
        {
            // 4 basamaklı random kod
            kod = rstgl.Next(1000, 9999);
            
            // mail işlemleri
            MailMessage ilet = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("kardelenaktoprakk@hotmail.com", "13031415.KA");
            client.Host = "smtp.live.com";
            client.Port = 587;
            client.EnableSsl = true;
             
            // mail dpğrulama işlemleri
            ilet.To.Add(textBox1.Text);
            ilet.From = new MailAddress("kardelenaktoprakk@hotmail.com", "Giriş Doğrulama Kodu");
            ilet.Subject = "Şifreleme Uygulaması Güvenlik Kodu";
            ilet.Body = kod.ToString();

            client.Send(ilet);
            // onay mesajı
            MessageBox.Show("Güvenlik Kodu Mailinize Gönderildi.");
        }

        private void button2_Click_1(object sender, EventArgs e)
            // doğrulama
        {
            if (textBox2.Text == kod.ToString())
            {
                MessageBox.Show("Güvenlik kodu doğrulandı.");
            }
            else
            {
                MessageBox.Show("Güvenlik kodu yanlış!");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sifreleme f1 = new sifreleme();
            f1.Show();
            this.Hide();
        }
    }
}
