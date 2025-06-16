using QueRS_app;
using System;

namespace QueRS_app
{
    class Program
    {
        static void Main()
        {
            QueueManager queue = new QueueManager();
            string input;

            Console.WriteLine("\n=== SISTEM ANTRIAN RUMAH SAKIT ===");
            Console.WriteLine("Selamat datang di sistem QueRS CLI.");

            do
            {
                Console.WriteLine("\nMenu Utama:");
                Console.WriteLine("1. Daftar Pasien");
                Console.WriteLine("2. Panggil Pasien Berikutnya");
                Console.WriteLine("3. Skip Pasien (Tidak Hadir)");
                Console.WriteLine("4. Lihat Seluruh Antrian");
                Console.WriteLine("0. Keluar Aplikasi");
                Console.Write("Pilih menu (0-4): ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Nama Pasien: ");
                        string name = Console.ReadLine();
                        Console.Write("Tipe Pasien (R untuk Reguler / V untuk VIP): ");
                        string type = Console.ReadLine().ToUpper();
                        queue.Register(name, type);
                        break;
                    case "2":
                        queue.CallNext();
                        break;
                    case "3":
                        queue.SkipCurrent();
                        break;
                    case "4":
                        queue.ShowQueue();
                        break;
                    case "0":
                        Console.WriteLine("Terima kasih telah menggunakan QueRS.");
                        break;
                    default:
                        Console.WriteLine("Input tidak dikenali. Silakan pilih menu yang tersedia.");
                        break;
                }

            } while (input != "0");
        }
    }
}
