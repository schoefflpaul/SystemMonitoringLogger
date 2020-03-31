using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemMonitoringLogger.Data;
using Microsoft.EntityFrameworkCore;
using SystemMonitoringLogger.Dtos;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MeasurementDto[]>> GetStatisticsForDevice(string deviceName, int pageIndex = 0, int pageSize = 100)
        {
            return await _context.Measurements
                .Include(m => m.SystemInfo).ThenInclude(s => s.Cpu)
                .Include(m => m.SystemInfo).ThenInclude(s => s.Ram)
                .Where(m => m.SystemInfo.Name == deviceName)
                .OrderByDescending(m => m.Timestamp)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(m => new MeasurementDto(m))
                .ToArrayAsync();
        }
        //DESKTOP-JPLO7L8

        [HttpGet]
        [Route("timeframe/{deviceName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MeasurementDto[]>> GetStatisticsForDeviceInTimeFrame(string deviceName, string from, string to ,int pageIndex = 0, int pageSize = 100)
        {
            DateTime fromTime, toTime;

            if (DateTime.TryParse(from, out fromTime) && DateTime.TryParse(to, out toTime))
            {
                return await _context.Measurements
                    .Include(m => m.SystemInfo).ThenInclude(s => s.Cpu)
                    .Include(m => m.SystemInfo).ThenInclude(s => s.Ram)
                    .Where(m => m.SystemInfo.Name == deviceName && m.Timestamp >= fromTime && m.Timestamp <= toTime)
                    .OrderByDescending(m => m.Timestamp)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .Select(m => new MeasurementDto(m))
                    .ToArrayAsync();

            }

            return BadRequest("Date time not valid");
        }
    }
}