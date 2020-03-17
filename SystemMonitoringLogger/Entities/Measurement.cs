using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemMonitoringLogger.Entities
{
    public class Measurement
    {
        public int Id { get; set; }
        public SystemInfo SystemInfo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }
    }
}
