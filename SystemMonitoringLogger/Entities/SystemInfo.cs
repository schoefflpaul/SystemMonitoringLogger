using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemMonitoringLogger.Entities
{
    public class SystemInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Cpu Cpu { get; set; }
        public Ram Ram { get; set; }
    }
}
