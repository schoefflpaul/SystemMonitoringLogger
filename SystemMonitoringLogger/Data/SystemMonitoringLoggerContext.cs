using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SystemMonitoringLogger.Entities;

namespace SystemMonitoringLogger.Data
{
    public class SystemMonitoringLoggerContext : DbContext
    {
        public SystemMonitoringLoggerContext (DbContextOptions<SystemMonitoringLoggerContext> options)
            : base(options)
        {
        }

        public DbSet<Measurement> Measurements { get; set; }
    }
}
