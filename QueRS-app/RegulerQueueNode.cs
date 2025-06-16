using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueRS_app
{
    public class RegulerQueueNode
    {
        public PatientNode Data;
        public RegulerQueueNode Next;
    }

    public class RegulerQueue
    {
        private RegulerQueueNode head;
        private RegulerQueueNode tail;

        public void Enqueue(PatientNode patient) // set antrian untuk queue reguler
        {
            RegulerQueueNode newNode = new RegulerQueueNode { Data = patient };
            if (head == null) // kalau head atau awal antrian masih kosong
                head = tail = newNode; // set head itu untuk new node
            else
            {
                tail.Next = newNode; // kalau sudah terisi head, maka antrian akan ditempatkan di next (atau tail)
                tail = newNode;
            }
        }

        public PatientNode Dequeue() // memanggil antrian
        {
            if (head == null) return null; // kalau antrian di awal kosong
            PatientNode result = head.Data; // set data ke head atau ke awal
            head = head.Next;
            if (head == null) tail = null; // kalau head kosong, dan next juga kosong
            return result; // return antrian terkahir
        }

        public bool IsEmpty() => head == null;

        public PatientNode Peek() => head?.Data;

        public void PrintQueue()
        {
            var patients = new List<PatientNode>();
            var temp = head;
            while (temp != null)
            {
                patients.Add(temp.Data);
                temp = temp.Next;
            }
            foreach (var p in patients.OrderBy(x => x.QueueNumber))
            {
                Console.WriteLine($"{p.QueueNumber} - {p.Name} (Reguler)");
            }
        }

        public bool RemoveById(string id, out PatientNode removed) // fitur skip kalau pasien belum hadir
        {
            removed = null;
            RegulerQueueNode prev = null, curr = head;
            while (curr != null) // looping selama current itu tidak kosong
            {
                if (curr.Data.QueueNumber == id) // set curr dengan ID antrian
                {
                    removed = curr.Data; // set removed dengan data dari current
                    if (prev == null) head = curr.Next; // kalau antrian pertama, maka head akan di set ke antrian selanjutnya
                    else prev.Next = curr.Next; // kalau antrian bukan pertama, maka antrian sebelumnya akan di set ke antrian selanjutnya
                    if (curr == tail) tail = prev; //kalau antrian terakhir, ga di skip
                    return true;
                }
                prev = curr;
                curr = curr.Next;
            }
            return false;
        }

        public void Requeue(PatientNode node, int afterCount)
        {
            RegulerQueueNode temp = head; //set current ke awal antrian atau head
            for (int i = 1; i < afterCount && temp?.Next != null; i++) // looping untuk antrian, jadi akan terus looping sampai current.Next itu kosong
                temp = temp.Next;

            RegulerQueueNode newNode = new RegulerQueueNode { Data = node }; 
            if (temp == null) // jika antrian kosong
            {
                newNode.Next = head; //set node itu jadi head dan tail, karena antrian kosong
                head = tail = newNode; // set head dan tail ke newNode
            }
            else // ada antrian selanjutnya
            {
                newNode.Next = temp.Next; //set antrian selanjutnya jadi antrian sekarang
                temp.Next = newNode; //menghubungkan ke antrian selanjutnya agar rantai ga putus
                if (newNode.Next == null) tail = newNode;
            }
        }
    }


}
