using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueRS_app
{
    public class PatientNode
    {
        public string Name;
        public string Type; // "R" untuk Reguler, "V" untuk VIP
        public string QueueNumber;
        public int SkipCount;
        public PatientNode Next;
    }
}
