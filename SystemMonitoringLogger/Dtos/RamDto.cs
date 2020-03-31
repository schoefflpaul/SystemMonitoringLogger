using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemMonitoringLogger.Dtos
{
    public class RamDto
    {
        public double Used { get; set; }
        public double Max { get; set; }

        public RamDto(double used, double max)
        {
            Used = used;
            Max = max;
        }
    }
}
