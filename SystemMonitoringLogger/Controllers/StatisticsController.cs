using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemMonitoringLogger.Data;
using SystemMonitoringLogger.Entities;
using SystemMonitoringLogger.Services;
using Microsoft.EntityFrameworkCore;

namespace SystemMonitoringLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly SystemMonitoringLoggerContext _context;

        public StatisticsController(SystemMonitoringLoggerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{deviceName}")]
        public async Task<Measurement[]> GetStatisticsForDevice(string deviceName, int pageIndex = 0, int pageSize = 100)
        {
            return await _context.Measurements
                .Include(m => m.SystemInfo).ThenInclude(s => s.Cpu)
                .Include(m => m.SystemInfo).ThenInclude(s => s.Ram)
                .Where(m => m.SystemInfo.Name == deviceName).Skip(pageIndex * pageSize).Take(pageSize)
                .ToArrayAsync();
        }



    }
}