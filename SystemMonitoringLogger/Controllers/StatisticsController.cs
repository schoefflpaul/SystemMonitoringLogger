using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemMonitoringLogger.Data;
using SystemMonitoringLogger.Services;

namespace SystemMonitoringLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly SystemMonitoringLoggerContext _context;
        private readonly IMqttService _mqttService;

        public StatisticsController(SystemMonitoringLoggerContext context, IMqttService mqttService)
        {
            _context = context;
            _mqttService = mqttService;
        }
    }
}