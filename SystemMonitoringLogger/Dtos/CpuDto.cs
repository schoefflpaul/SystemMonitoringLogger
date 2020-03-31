using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoringLogger.Entities;

namespace SystemMonitoringLogger.Dtos
{
    public class CpuDto
    {
        public int Baseclock { get; set; }
        public int Currentclock { get; set; }
        public int Utilisation { get; set; }

        public CpuDto(Cpu cpu)
        {
            Baseclock = cpu.Baseclock;
            Currentclock = cpu.Currentclock;
            Utilisation = cpu.Utilisation;
        }

    }
}
