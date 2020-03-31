using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoringLogger.Entities;

namespace SystemMonitoringLogger.Dtos
{
    public class RamDto
    {
        public double Used { get; set; }
        public double Max { get; set; }

        public RamDto(Ram ram)
        {
            Used = ram.Used;
            Max = ram.Max;
        }
    }
}
