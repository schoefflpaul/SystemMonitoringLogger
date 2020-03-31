using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemMonitoringLogger.Dtos
{
    public class CpuDto
    {
        public int Baseclock { get; set; }
        public int Currentclock { get; set; }
        public int Utilisation { get; set; }

        public CpuDto(int baseClock, int currentClock, int utilisation)
        {
            Baseclock = baseClock;
            Currentclock = currentClock;
            Utilisation = utilisation;
        }

    }
}
