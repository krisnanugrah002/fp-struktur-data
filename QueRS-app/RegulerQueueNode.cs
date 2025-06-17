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

        public PatientNode Dequeue() 
        {
            if (head == null) return null; // kalau antrian di awal kosong
            PatientNode result = head.Data; // set data ke head atau ke awal
            head = head.Next;
            if (head == null) tail = null; // kalau head kosong, dan next juga kosong
            return result; // return antrian terkahir
        }

        public bool IsEmpty() => head == null;

        public PatientNode Peek() => head?.Data;

        public void PrintQueue() // bubble sort
        {
            var patients = new List<PatientNode>();
            var temp = head;
            while (temp != null)
            {
                patients.Add(temp.Data);
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
                Console.WriteLine($"{p.QueueNumber} - {p.Name} (Reguler)");
            }
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
