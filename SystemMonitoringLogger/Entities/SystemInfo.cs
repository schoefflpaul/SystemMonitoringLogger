using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemMonitoringLogger.Entities
{
    public class SystemInfo
    {
        [Key]
        public string Id { get; set; }
        public Cpu Cpu { get; set; }
        public Ram Ram { get; set; }
    }
}
