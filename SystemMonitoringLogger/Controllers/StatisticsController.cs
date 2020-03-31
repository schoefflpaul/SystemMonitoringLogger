﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemMonitoringLogger.Data;
using SystemMonitoringLogger.Entities;
using SystemMonitoringLogger.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        public async Task<MeasurementDto[]> GetStatisticsForDevice(string deviceName, int pageIndex = 0, int pageSize = 100)
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
        [Route("{deviceName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<MeasurementDto[]> GetStatisticsForDeviceInTimeFrame(string deviceName, DateTime from, DateTime to ,int pageIndex = 0, int pageSize = 100)
        {
            return await _context.Measurements
                .Include(m => m.SystemInfo).ThenInclude(s => s.Cpu)
                .Include(m => m.SystemInfo).ThenInclude(s => s.Ram)
                .Where(m => m.SystemInfo.Name == deviceName && m.Timestamp >= from && m.Timestamp <= to)
                .OrderByDescending(m => m.Timestamp)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(m => new MeasurementDto(m))
                .ToArrayAsync();
        }
    }
}