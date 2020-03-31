using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMonitoringLogger.Entities;

namespace SystemMonitoringLogger.Dtos
{
    public class SystemInfoDto
    {
        public string Name { get; set; }
        public CpuDto Cpu { get; set; }
        public RamDto Ram { get; set; }
        public SystemInfoDto(SystemInfo systemInfo)
        {
            Name = systemInfo.Name;
            Cpu = new CpuDto(systemInfo.Cpu);
            Ram = new RamDto(systemInfo.Ram);
        }
    }
}
