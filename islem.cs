using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace proje_ekip
{
    class islem
    {
        #region Enum
        //Enum çalışma içerisinde belirlediğim sabitlerin  tutulması 
        public enum Deger
        {
            sakla, uzunluk
        }; 
        #endregion

        #region Yazı Gizleme Kodu

        public static Bitmap yaziSifrele(string yazi, Bitmap bmp)
        {
            Deger durum = Deger.sakla;//Başlangıçta resimde karakterleri gizliyorum

            int a = 0; //Gizlenenen karakterin dizinini tutacak değişken

            int b = 0; //Tam sayıya dönüştürülmüş karakterlerin yeni değerini tutan değişken

            long pixelc = 0;//İşlenen karakterin renklerini tutan değişkenler (R,G,B)

            int sonsifir = 0;// karakterler işlendikten sonra eklenen son sıfırları tutan değişken.

            int R = 0, G = 0, B = 0;//Piksel değerini tutacak

            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);//şifrelemek istediğim piksel alınır
                    R = pixel.R - pixel.R % 2;//Alınan piksel için en anlamsız biti temizle. Soldan bir bit temizlenir.
                    G = pixel.G - pixel.G % 2;
                    B = pixel.B - pixel.B % 2;

                    for (int n = 0; n < 3; n++) //Her piksel elemanını inceleyip işlem yaptırılacak
                    {
                        if (pixelc % 8 == 0)//8 bit ilendi mi kontrolü
                        {
                            if (durum == Deger.uzunluk && sonsifir == 8)//8 bit işlenince 8 sıfır eklenir, Bu şekilde çözülürken sadece mesaj görünür
                            {
                                if ((pixelc - 1) % 3 < 2)
                                {
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));//Gelen karakterlerin new değerleri yazılır
                                }
                                return bmp;//İşlenen değer döndürülür
                            }
                            if (a >= yazi.Length)//Tüm karakterlerin gizlenip gizlenmediği kontrolü
                            {
                                durum = Deger.uzunluk;//metin sonuna işaretlemek için sıfır yaz
                            }
                            else
                            {
                                b = yazi[a++];//bir sonraki karaktere geçip işlem yap 

                                Console.WriteLine(b);
                            }
                        }

                        switch (pixelc % 3)//İşlenen karakterin renk değerine göre işlem
                        {
                            case 0:
                                {
                                    if (durum == Deger.sakla)
                                    {
                                        R += b % 2;//Karakterdeki en sağdaki bit
                                        /*Çıkarılan en önemli  bit(LSB) yerine değer konulur.*/
                                        b /= 2;

                                        Console.WriteLine(R.ToString());
                                    }
                                    break;
                                }

                            case 1:
                                {
                                    if (durum == Deger.sakla)
                                    {
                                        G += b % 2;
                                       b /= 2;

                                        Console.WriteLine(G.ToString());
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (durum == Deger.sakla)
                                    {
                                        B += b % 2;
                                        b /= 2;

                                        Console.WriteLine(B.ToString());
                                    }
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }
                                break;
                        }

                        pixelc++;
                        if (durum == Deger.uzunluk)
                        {
                            sonsifir++;
                        }
                    }

                }

            }

            return bmp;
            #endregion
        }




        #region Mesajı Çözme
        public static string Coz(Bitmap bmp)
        {
            int colorUnitIndex = 0;
            int charVal = 0;
            string cikarilanMetin = "";//Çıkarılacak metni tutan değişken
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);

                    for (int n = 0; n < 3; n++)//Her piksel için kontrol 
                    {
                        switch (colorUnitIndex % 3)
                        {
                            /*
                             * Pixel öğelerinden LSB'yi alınıyor
                             * geçerli karakterin sağına bir bit eklenir( charVal = charVal * 2)
                             * eklenen bit değiştirilir (varsayılan değer 0)
                             */
                            case 0:
                                {
                                    charVal = charVal * 2 + pixel.R % 2;//Kırmızı pikselde bulunan işlenmiş en önemli bit
                                }
                                break;
                            case 1:
                                {
                                    charVal = charVal * 2 + pixel.G % 2;
                                }
                                break;
                            case 2:
                                {
                                    charVal = charVal * 2 + pixel.B % 2;
                                }
                                break;
                        }
                        colorUnitIndex++;

                        //8 bit eklenmişse

                        if (colorUnitIndex % 8 == 0)
                        {
                            charVal = reverseBits(charVal);/*8 bite ulaşana kadar işlem yapılır*/
                            //
                            Console.WriteLine(charVal);
                            //

                            if (charVal == 0)
                            {
                                return cikarilanMetin;
                            }
                            #region Türkçe Karakter Hatasını Önlemek İçin
                            if (charVal == 94)/*Ü=220 Ç=*/
                            {
                                charVal = 350;

                            }
                            else if (charVal == 95)
                            {
                                charVal = 351;
                            }
                            else if (charVal == 48)
                            {
                                charVal = 304;
                            }
                            #endregion
                            char c = (char)charVal;

                            cikarilanMetin += c.ToString();
                        }
                    }
                }

            }

            return cikarilanMetin;
        }



        public static int reverseBits(int n)
        {
            int sonuc = 0;
            for (int i = 0; i < 8; i++)
            {
                sonuc = sonuc * 2 + n % 2;
                n /= 2;
            }
            return sonuc;
        }
    }
    #endregion
}
