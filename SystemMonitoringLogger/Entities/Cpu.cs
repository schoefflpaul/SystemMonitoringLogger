using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitoringLogger.Entities
{
    public class Cpu
    {
        public int Id { get; set; }
        public int Baseclock { get; set; }
        public int Currentclock { get; set; }
        public int Utilisation { get; set; }
    }
}
