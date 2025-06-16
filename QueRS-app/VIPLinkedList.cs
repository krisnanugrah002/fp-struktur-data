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

        public void InsertAfter(int position, PatientNode newNode)
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

        public PatientNode RemoveFirst() //setter jika pasien sudah di tangani maka akan dikosongkan, dan head akan lanjut ke node berikutnya
        {
            if (head == null) return null;
            PatientNode temp = head;
            head = head.Next;
            return temp;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public void PrintVIPs()
        {
            var patients = new List<PatientNode>();
            var temp = head;
            while (temp != null)
            {
                patients.Add(temp);
                temp = temp.Next;
            }
            foreach (var p in patients.OrderBy(x => x.QueueNumber))
            {
                Console.WriteLine($"{p.QueueNumber} - {p.Name} (VIP)");
            }
        }

        public bool RemoveById(string id, out PatientNode removed) // untuk fitur skip jika pasien belum hadir
        {
            removed = null; // disini removed masih null (Kosong)
            PatientNode prev = null, curr = head; // 
            while (curr != null) // jika masih ada antrian sleanjutnya (bukan antrian terakhir
            {
                if (curr.QueueNumber == id)
                {
                    removed = curr; //removed diisi dengan curr yang mana adalah ID pasien yang ingin di skip
                    if (prev == null) head = curr.Next;
                    else prev.Next = curr.Next;
                    return true;
                }
                prev = curr;
                curr = curr.Next;
            }
            return false;
        }

        public void Requeue(PatientNode node, int afterCount)
        {
            InsertAfter(afterCount, node);
        }
    }

}
