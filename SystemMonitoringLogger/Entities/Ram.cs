using Google.Cloud.Firestore;

namespace SystemMonitoringLogger.Entities
{
    [FirestoreData]
    public class Ram
    {
        public int Id { get; set; }
        [FirestoreProperty]
        public double Used { get; set; }
        [FirestoreProperty]
        public double Max { get; set; }
    }
}