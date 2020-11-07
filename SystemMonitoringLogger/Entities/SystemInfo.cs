using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemMonitoringLogger.Entities
{
    [FirestoreData]
    public class SystemInfo
    {
        public int Id { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public Cpu Cpu { get; set; }
        [FirestoreProperty]
        public Ram Ram { get; set; }
    }
}
