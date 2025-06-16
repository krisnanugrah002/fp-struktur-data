using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueRS_app
{
    public class QueueManager
    {
        private RegulerQueue regulerQueue = new RegulerQueue();
        private VIPLinkedList vipList = new VIPLinkedList();
        private int regularCounter = 0;
        private int vipCounter = 0;
        private const int maxVipPerDay = 10;
        private int vipToday = 0;
        private int vipCallsInARow = 0;
        private PatientNode currentPatient = null;

        public void Register(string name, string type)
        {
            if (type == "V" && vipToday >= maxVipPerDay)
            {
                Console.WriteLine("Kuota pasien VIP hari ini penuh.");
                return;
            }

            string queueNumber = "";
            switch (type)
            {
                case "V":
                    vipCounter++;
                    vipToday++;
                    queueNumber = "V-" + vipCounter.ToString("D3");
                    type = "VIP";
                    break;

                case "R":
                    regularCounter++;
                    queueNumber = "R-" + regularCounter.ToString("D3");
                    type = "Reguler";
                    break;

                default:
                    Console.WriteLine("Tipe pasien tidak valid. Gunakan 'R' untuk Reguler atau 'V' untuk VIP.");
                    return;
            }

            PatientNode newNode = new PatientNode
            {
                Name = name,
                Type = type,
                QueueNumber = queueNumber,
                SkipCount = 0
            };

            if (type == "Reguler") regulerQueue.Enqueue(newNode);
            else vipList.InsertAfter(2, newNode);

            Console.WriteLine($"Pasien {type} {name} terdaftar dengan nomor antrian {queueNumber}");
        }

        public void CallNext()
        {
            if (currentPatient != null)
            {
                Console.WriteLine($"Pasien {currentPatient.QueueNumber} - {currentPatient.Name} selesai dilayani.");
                currentPatient = null;
            }

            if (vipCallsInARow < 2 && !vipList.IsEmpty())
            {
                currentPatient = vipList.RemoveFirst();
                Console.WriteLine($"Memanggil: {currentPatient.QueueNumber} - {currentPatient.Name} (VIP)");
                vipCallsInARow++;
            }
            else if (!regulerQueue.IsEmpty())
            {
                currentPatient = regulerQueue.Dequeue();
                Console.WriteLine($"Memanggil: {currentPatient.QueueNumber} - {currentPatient.Name} (Reguler)");
                vipCallsInARow = 0;
            }
            else if (!vipList.IsEmpty())
            {
                currentPatient = vipList.RemoveFirst();
                Console.WriteLine($"Memanggil: {currentPatient.QueueNumber} - {currentPatient.Name} (VIP)");
                vipCallsInARow = 1;
            }
            else
            {
                Console.WriteLine("Tidak ada pasien dalam antrian.");
            }
        }

        public void SkipCurrent()
        {
            if (currentPatient == null)
            {
                Console.WriteLine("Tidak ada pasien yang sedang dalam sesi pemanggilan.");
                return;
            }

            currentPatient.SkipCount++;
            Console.WriteLine($"Pasien {currentPatient.QueueNumber} ditandai tidak hadir (Skip ke-{currentPatient.SkipCount})");

            if (currentPatient.Type == "Reguler")
                regulerQueue.Requeue(currentPatient, 2);
            else
                vipList.Requeue(currentPatient, 2);

            currentPatient = null;
        }

        public void ShowQueue()
        {
            Console.WriteLine("\n======= DAFTAR ANTRIAN =======");
            Console.WriteLine("\nPasien VIP:");
            vipList.PrintVIPs();
            Console.WriteLine("\nPasien Reguler:");
            regulerQueue.PrintQueue();
            Console.WriteLine("==============================\n");
        }
    }


}
