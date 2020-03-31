using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoringLogger.Entities;

namespace SystemMonitoringLogger.Dtos
{
    public class MeasurementDto
    {
        public SystemInfoDto SystemInfo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        public MeasurementDto(Measurement measurement)
        {
            SystemInfo = new SystemInfoDto(measurement.SystemInfo);
            Timestamp = measurement.Timestamp;
        }
    }
}
