using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemMonitoringLogger.Data;
using Microsoft.EntityFrameworkCore;
using SystemMonitoringLogger.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using SystemMonitoringLogger.DataAccess;
using System.Collections.Generic;
using SystemMonitoringLogger.Entities;

namespace SystemMonitoringLogger.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowMyOrigin")]
    //[Authorize]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly SystemMonitoringDataAccessLayer accessLayerContext = new SystemMonitoringDataAccessLayer();

        [HttpGet("{id}")]
        public Task<Measurement> Get(string id)
        {
            return accessLayerContext.GetMeasurementData(id);
        }

        [HttpPost]
        public void Post([FromForm] Measurement measurement)
        {
            accessLayerContext.AddMeasurement(measurement);
        }

        [HttpPut]
        public void Put([FromForm]Measurement measurement)
        {
            accessLayerContext.UpdateMeasurement(measurement);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            accessLayerContext.DeleteMeasurement(id);

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MeasurementDto[]>> GetStatisticsForDevice(string deviceName, int pageIndex = 0, int pageSize = 100)
        {
            var measurementsDb = await accessLayerContext.GetAllMeasurements();

            return measurementsDb
                .Where(m => m.SystemInfo.Name == deviceName)
                .OrderByDescending(m => m.Timestamp)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(m => new MeasurementDto(m))
                .ToArray();
        }

    }
}