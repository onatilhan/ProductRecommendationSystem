using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalMarket
{
    class UrunCifti
    {
        public string urunAdi1, urunAdi2;
        public int sayi = 0;
    }
    class OnerilenUrunler
    {
        public string urunAdi;
        public int sayi = 1;
    }
    class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            int ikililer = 0;
            string[] urunler = { "Ekmek", "Simit", "Peynir", "Tereyağı", "Zeytin", "Çay", "Makarna", "Bal", "Reçel", "Yumurta" };
            string[] alinanlar = new String[2];
            List<UrunCifti> urunCiftleri = new List<UrunCifti>();
            string[][] musteriler = new string[5][];

            for (int i = 0; i < 5; i++)
            {
                int urunSayisi = random.Next(1, 6);
                musteriler[i] = new string[urunSayisi];
                musteriler[i] = musteriSepetiOlustur(urunler, urunSayisi, random, i);
                Array.Sort(musteriler[i]);
                ikililer += ((urunSayisi) * (urunSayisi - 1) / 2);
            }

            urunCiftleri.AddRange(urunCiftiKombinasyonlariOlustur(musteriler, ikililer));
            kombinasyonuArtır(urunCiftleri);
            urunCiftleri = urunCiftleri.OrderByDescending(x => x.sayi).ThenBy(x => x.urunAdi1).ToList();

            for (int h = 0; h < 5; h++)
            {
                Console.Write("Müşteri {0}: ", (h + 1));
                for (int s = 0; s < musteriler[h].Length; s++)
                {
                    Console.Write(" " + musteriler[h][s]);
                }
                Console.WriteLine();
                Console.ReadKey();
            }
            urunCiftleriniEkranaListele(urunCiftleri);

            Console.WriteLine("\nİlk ürünü giriniz:");
            alinanlar[0] = Console.ReadLine();
            do
            {
                Console.WriteLine("İkinci ürünü giriniz:");
                alinanlar[1] = Console.ReadLine();
            } while (alinanlar[0].ToUpper().Equals(alinanlar[1].ToUpper()));

            Array.Sort(alinanlar);

            ürünÖnerisiYap(alinanlar[0], alinanlar[1], musteriler);

            Console.ReadKey();
        }

        static string[] musteriSepetiOlustur(string[] urunler, int sepetBoyutu, Random random, int musteriNo)
        {
            string[] urunlerKopya = new string[urunler.Length];
            Array.Copy(urunler, urunlerKopya, urunler.Length);

            int maxValue = urunlerKopya.Length;
            string[][] musteriSepeti = new string[5][];
            musteriSepeti[musteriNo] = new string[sepetBoyutu];

            for (int i = 0; i < sepetBoyutu; i++)
            {
                int randomNumber = random.Next(0, maxValue);
                musteriSepeti[musteriNo][i] = urunlerKopya[randomNumber];

                for (int j = randomNumber; j < urunlerKopya.Length - 1; j++)
                {
                    urunlerKopya[j] = urunlerKopya[j + 1];
                }
                maxValue--;
            }
            return musteriSepeti[musteriNo];
        }

        static UrunCifti[] urunCiftiKombinasyonlariOlustur(string[][] musteriSepeti, int boyut)
        {
            UrunCifti[] urunCiftiDizisi = new UrunCifti[boyut];

            int k = 0;

            for (int y = 0; y < 5; y++)
            {
                for (int i = 0; i < musteriSepeti[y].Length; i++)
                {
                    for (int j = i + 1; j < musteriSepeti[y].Length; j++)
                    {
                        UrunCifti urunCifti = new UrunCifti();
                        urunCifti.urunAdi1 = musteriSepeti[y][i];
                        urunCifti.urunAdi2 = musteriSepeti[y][j];
                        urunCifti.sayi += 1;
                        urunCiftiDizisi[k] = urunCifti;
                        k++;
                    }
                }
            }
            return urunCiftiDizisi;
        }

        public static void urunCiftleriniEkranaListele(List<UrunCifti> urunCiftleri)
        {
            Console.WriteLine("\n-------ÜRÜN ÇİFTLERİ-------");
            Console.Write("Urun 1  -  Urun 2  -  Sayi\n");
            Console.WriteLine("---------------------------");
            foreach (UrunCifti uc in urunCiftleri)
            {
                Console.Write("{0,-10} {1,-11} {2,-10}\n", uc.urunAdi1, uc.urunAdi2, uc.sayi);
            }
            Console.WriteLine("---------------------------");
        }

        public static List<UrunCifti> kombinasyonuArtır(List<UrunCifti> urunCiftleri)
        {
            for (int m = 0; m < urunCiftleri.Count - 1; m++)
            {
                for (int z = m + 1; z < (urunCiftleri.Count); z++)
                {
                    if ((urunCiftleri[m].urunAdi1.Equals(urunCiftleri[z].urunAdi1)) && (urunCiftleri[m].urunAdi2.Equals(urunCiftleri[z].urunAdi2)))
                    {
                        urunCiftleri[m].sayi++;
                        urunCiftleri.RemoveAt(z);
                    }
                }
            }
            return urunCiftleri;
        }

        public static void ürünÖnerisiYap(string alinan1, string alinan2, string[][] müşteriSepetleri)
        {
            int ikiliBulma = 0;
            List<OnerilenUrunler> öneriler = new List<OnerilenUrunler>();

            for (int h = 0; h < 5; h++)
            {
                for (int s = 0; s < müşteriSepetleri[h].Length; s++)
                {
                    if (müşteriSepetleri[h][s].ToUpper().Equals(alinan1.ToUpper()))
                    {
                        for (int k = s + 1; k < müşteriSepetleri[h].Length; k++)
                        {
                            if (müşteriSepetleri[h][k].ToUpper().Equals(alinan2.ToUpper()))
                            {
                                ikiliBulma++;
                                for (int t = 0; t < müşteriSepetleri[h].Length; t++)
                                {
                                    if ((t != s) && t != k)
                                    {
                                        OnerilenUrunler önerilenUrun = new OnerilenUrunler();
                                        önerilenUrun.urunAdi = müşteriSepetleri[h][t];
                                        öneriler.Add(önerilenUrun);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int m = 0; m < öneriler.Count - 1; m++)
            {
                for (int z = m + 1; z < (öneriler.Count); z++)
                {
                    if (öneriler[m].urunAdi.Equals(öneriler[z].urunAdi))
                    {
                        öneriler[m].sayi++;
                        öneriler.RemoveAt(z);
                    }
                }
            }
            Console.WriteLine();

            if ((öneriler.Count) == 0)
            {
                Console.WriteLine("ÖNERİLEN ÜRÜN YOKTUR!");
            }
            else
            {
                Console.WriteLine("ÖNERİLEN ÜRÜNLER VE GÜVEN YÜZDELERİ");
                Console.WriteLine("-----------------------------------");
                foreach (OnerilenUrunler ürün in öneriler)
                {
                    Console.Write("         {0,-10} {1:00.0} \n", ürün.urunAdi, Convert.ToDouble(ürün.sayi * 100) / (ikiliBulma));
                }
            }
        }
    }
}