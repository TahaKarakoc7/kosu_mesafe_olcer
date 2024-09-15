namespace kosu_mesafe_olcer_Muhammed_Taha_Karakoc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Bölüm sayısını kullanıcıdan al
            int bolumSayisi = BolumSayisiniAl();

            // Her bir bölüm için süre ve mesafe bilgilerini tutacak listeler
            List<int> bolumSureleri = new List<int>();
            List<double> bolumMesafeleri = new List<double>();

            // Toplam süre ve mesafe hesaplamaları için değişkenler
            int toplamSureDakika = 0;
            double toplamMesafe = 0;

            // Kullanıcıdan her bölüm için süre ve mesafe bilgilerini al
            for (int i = 0; i < bolumSayisi; i++)
            {
                Console.WriteLine($"\nBölüm {i + 1}:");
                int bolumDakika = KosuSuresiAl();

                double bolumMesafe = 0;

                // Eğer kullanıcı hem saat hem de dakika için 0 girdiyse, adım boyu ve adım sayısını sorma
                if (bolumDakika == 0)
                {
                    Console.WriteLine("Bu bölümde koşmadınız.");
                }
                else
                {
                    double adimBoyu = AdimBoyuAl();
                    int adimSayisi = AdimSayisiAl();
                    bolumMesafe = HesaplaMesafe(adimBoyu, adimSayisi, bolumDakika);
                }

                // Bilgileri listelere ekle
                bolumSureleri.Add(bolumDakika);
                bolumMesafeleri.Add(bolumMesafe);

                // Her bir bölümdeki süre ve mesafe bilgileri toplam süre ve mesafeye ekle
                toplamSureDakika += bolumDakika;
                toplamMesafe += bolumMesafe;
            }

            // Sonuçları yazdır
            SonuclariYazdir(bolumSureleri, bolumMesafeleri, toplamSureDakika, toplamMesafe);
        }

        // Bölüm sayısını kullanıcıdan alma metodu
        static int BolumSayisiniAl()
        {
            int bolumSayisi;
            bool gecerliMi;
            do
            {
                Console.WriteLine("Kaç bölüm koşu yapacaksınız?");
                string girdi = Console.ReadLine();
                gecerliMi = int.TryParse(girdi, out bolumSayisi) && bolumSayisi > 0;

                if (!gecerliMi)
                    Console.WriteLine("Geçerli bir pozitif tam sayı giriniz.");

            } while (!gecerliMi);

            return bolumSayisi;
        }

        // Koşu süresi (saat ve dakika olarak) alma metodu
        static int KosuSuresiAl()
        {
            int saat, dakika;
            bool saatGecerli, dakikaGecerli;

            // Saat verisini alma
            do
            {
                Console.Write("Lütfen koşu saatinizi giriniz (0-24 arasında bir değer): ");
                saatGecerli = int.TryParse(Console.ReadLine(), out saat) && saat >= 0 && saat <= 24;

                if (!saatGecerli)
                    Console.WriteLine("Geçerli bir saat değeri giriniz.");
            } while (!saatGecerli);

            // Dakika verisini alma
            do
            {
                Console.Write("Lütfen koşu dakikanızı giriniz (0-59 arasında bir değer): ");
                dakikaGecerli = int.TryParse(Console.ReadLine(), out dakika) && dakika >= 0 && dakika < 60;

                if (!dakikaGecerli)
                    Console.WriteLine("Geçerli bir dakika değeri giriniz.");
            } while (!dakikaGecerli);

            // Eğer hem saat hem de dakika 0 ise 0 döndür
            if (saat == 0 && dakika == 0)
            {
                return 0;
            }

            // Toplam süreyi dakikaya çevirme
            return (saat * 60) + dakika;
        }

        // Adım boyunu kullanıcıdan alma metodu
        static double AdimBoyuAl()
        {
            double adimBoyu;
            bool gecerliMi;

            do
            {
                Console.WriteLine("Ortalama adım boyunuzu kaç santimetre olduğunu giriniz:");
                gecerliMi = double.TryParse(Console.ReadLine(), out adimBoyu) && adimBoyu > 0;

                if (!gecerliMi)
                    Console.WriteLine("Pozitif bir sayı giriniz.");
            } while (!gecerliMi);

            return adimBoyu / 100; // Santimetreden metreye çevirme
        }

        // Adım sayısını kullanıcıdan alma metodu
        static int AdimSayisiAl()
        {
            int adimSayisi;
            bool gecerliMi;

            do
            {
                Console.WriteLine("Bir dakikada kaç adım koştuğunuzu giriniz:");
                gecerliMi = int.TryParse(Console.ReadLine(), out adimSayisi) && adimSayisi > 0;

                if (!gecerliMi)
                    Console.WriteLine("Pozitif bir tam sayı giriniz.");
            } while (!gecerliMi);

            return adimSayisi;
        }

        // Mesafeyi hesaplama metodu
        static double HesaplaMesafe(double adimBoyu, int adimSayisi, int kosuSureDakika)
        {
            return adimBoyu * adimSayisi * kosuSureDakika;
        }

        // Sonuçları yazdırma metodu
        static void SonuclariYazdir(List<int> bolumSureleri, List<double> bolumMesafeleri, int toplamSureDakika, double toplamMesafe)
        {
            Console.WriteLine("\n-------------------------------------------------");
            for (int i = 0; i < bolumSureleri.Count; i++)
            {
                int bolumSureSaat = bolumSureleri[i] / 60;  // Bölüm süresini saat cinsine çevirir.
                int bolumKalanDakika = bolumSureleri[i] % 60;  // Bölüm süresinin saat kısmından arta kalan dakikayı hesaplar.

                if (bolumSureleri[i] == 0)
                {
                    Console.WriteLine($"Bölüm {i + 1}: Koşmadınız.");
                }
                else
                {
                    // Mesafeyi metre cinsinden kontrol et ve uygun birimle yazdır
                    double bolumMesafeKm = bolumMesafeleri[i] / 1000; // Metreden kilometreye çevirir

                    Console.WriteLine($"Bölüm {i + 1}: Koştuğunuz süre: {bolumSureSaat} saat {bolumKalanDakika} dakika");
                    if (bolumMesafeKm >= 1) // Mesafe 1 kilometreden büyükse
                    {
                        Console.WriteLine($"Bölüm {i + 1}: Koştuğunuz mesafe: {bolumMesafeKm:F2} kilometre\n");
                    }
                    else // Mesafe 1 kilometreden küçükse
                    {
                        Console.WriteLine($"Bölüm {i + 1}: Koştuğunuz mesafe: {bolumMesafeleri[i]:F2} metre\n");
                    }
                }
            }

            // Tüm bölümlerin toplam süresi ve mesafesi hesaplanıp ekrana yazdırılır.
            int toplamSureSaat = toplamSureDakika / 60;  // Toplam süreyi saat cinsine çevirir.
            int kalanDakika = toplamSureDakika % 60;  // Toplam sürenin saat kısmından arta kalan dakikayı hesaplar.

            // Toplam mesafeyi kilometre cinsinden hesapla
            double toplamMesafeKm = toplamMesafe / 1000; // Metreden kilometreye çevirir

            // Kilometre değerinin virgülle ayrılmasını sağlar
            Console.WriteLine($"Toplam koşu süresi: {toplamSureSaat} saat {kalanDakika} dakika");
            if (toplamMesafeKm >= 1) // Kilometre 1'den büyük veya eşitse, virgül ile gösterim
            {
                Console.WriteLine($"Toplam koşu mesafesi: {toplamMesafeKm:F2} kilometre");
            }
            else // Kilometre 1'den küçükse, metre cinsinden gösterim
            {
                Console.WriteLine($"Toplam koşu mesafesi: {toplamMesafe:F2} metre");
            }
        }
    }
}