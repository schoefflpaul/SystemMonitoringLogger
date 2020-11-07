using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoringLogger.Entities;

namespace SystemMonitoringLogger.DataAccess
{
    public class SystemMonitoringDataAccessLayer
    {
        string projectId;
        FirestoreDb fireStoreDb;
        public SystemMonitoringDataAccessLayer()
        {
            string filepath = "D:\\Schule\\NVS Lackinger\\SystemMonitoringLogger\\systemmonitoring-995cc-c533194931d4.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            projectId = "systemmonitoring-995cc";
            fireStoreDb = FirestoreDb.Create(projectId);
        }
        public async Task<IEnumerable<Measurement>> GetAllMeasurements()
        {
            try
            {
                Query measurementQuery = fireStoreDb.Collection("measurement");
                QuerySnapshot measurementQuerySnapshot = await measurementQuery.GetSnapshotAsync();
                List<Measurement> measurements = new List<Measurement>();

                foreach (DocumentSnapshot documentSnapshot in measurementQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> measurement = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(measurement);
                        Measurement newmeasurement = JsonConvert.DeserializeObject<Measurement>(json);
                        newmeasurement.Id = documentSnapshot.Id;
                        newmeasurement.Timestamp = documentSnapshot.CreateTime.Value.ToDateTime();
                        measurements.Add(newmeasurement);
                    }
                }

                List<Measurement> sortedMeasurementList = measurements;
                return sortedMeasurementList;
            }
            catch
            {
                throw;
            }
        }


        public async void AddMeasurement(Measurement measurement)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("measurement");
                await colRef.AddAsync(measurement);
            }
            catch
            {
                throw;
            }
        }
        public async void UpdateMeasurement(Measurement measurement)
        {
            try
            {
                DocumentReference measurementRef = fireStoreDb.Collection("measurement").Document(measurement.Id.ToString());
                await measurementRef.SetAsync(measurement, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Measurement> GetMeasurementData(string id)
        {
            try
            {
                DocumentReference docRef = fireStoreDb.Collection("measurement").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    Measurement measurement = snapshot.ConvertTo<Measurement>();
                    measurement.Id = snapshot.Id;
                    measurement.Timestamp = snapshot.CreateTime.Value.ToDateTime();
                    return measurement;
                }
                else
                {
                    return new Measurement();
                }
            }
            catch
            {
                throw;
            }
        }
        public async void DeleteMeasurement(string id)
        {
            try
            {
                DocumentReference measurementRef = fireStoreDb.Collection("measurement").Document(id);
                await measurementRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<SystemInfo>> GetSystemInfosData()
        {
            try
            {
                Query systemInfoQuery = fireStoreDb.Collection("systeminfo");
                QuerySnapshot systemInfoQuerySnapshot = await systemInfoQuery.GetSnapshotAsync();
                List<SystemInfo> lstSystemInfo = new List<SystemInfo>();

                foreach (DocumentSnapshot documentSnapshot in systemInfoQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> systemInfo = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(systemInfo);
                        SystemInfo newSystemInfo = JsonConvert.DeserializeObject<SystemInfo>(json);
                        lstSystemInfo.Add(newSystemInfo);
                    }
                }
                return lstSystemInfo;
            }
            catch
            {
                throw;
            }
        }
    }
}
