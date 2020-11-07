using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitoringLogger.Entities
{
    [FirestoreData]
    public class Cpu
    {
        public int Id { get; set; }
        [FirestoreProperty]
        public int Baseclock { get; set; }
        [FirestoreProperty]
        public int Currentclock { get; set; }
        [FirestoreProperty]
        public int Utilisation { get; set; }
    }
}
