using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueRS_app
{
    public class VIPLinkedList
    {
        private PatientNode head;

        public void InsertPatient(int position, PatientNode newNode)
        {
            if (head == null || position <= 0) // kalau di kepala kosong, dan posisi antrian masih 0
            {
                newNode.Next = head; //antrian baru dijadikan head (awal)
                head = newNode;
                return;
            }

            PatientNode current = head; // set antrian sekarang (yang dipanggil) adalah angka pertama (head)
            for (int i = 1; i < position && current.Next != null; i++) // looping untuk antrian, jadi akan terus looping sampai current.Next itu kosong
            {
                current = current.Next;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public PatientNode RemoveFirst() 
        {
            if (head == null) return null; // cek apakah head itu kosong, kalau kosong ya lanjut ke reguler
            PatientNode temp = head; // set antrian sekarang ke hold
            head = head.Next; //set antrian yang akan dipanggil
            return temp; // return antrian yang sedang di hold
        }

        public void Requeue(PatientNode node, int afterCount)
        {
            InsertPatient(afterCount, node);
        }

        public void PrintVIPs() // bubble sort
        {
            var patients = new List<PatientNode>();
            var temp = head;
            while (temp != null)
            {
                patients.Add(temp);
                temp = temp.Next;
            }

            for (int i = 0; i < patients.Count - 1; i++)
            {
                for (int j = 0; j < patients.Count - i - 1; j++)
                {
                    if (string.Compare(patients[j].QueueNumber, patients[j + 1].QueueNumber) > 0)
                    {
                        var tmp = patients[j];
                        patients[j] = patients[j + 1];
                        patients[j + 1] = tmp;
                    }
                }
            }

            foreach (var p in patients)
            {
                Console.WriteLine($"{p.QueueNumber} - {p.Name} (VIP)");
            }
        }

    }

}
