using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemMonitoringLogger.Entities
{
    [FirestoreData]
    public class Measurement
    {
        public string Id { get; set; }
        [FirestoreProperty]
        public SystemInfo SystemInfo { get; set; }

        [DataType(DataType.DateTime)]
        [Timestamp]
        public DateTime Timestamp { get; set; }
    }
}
