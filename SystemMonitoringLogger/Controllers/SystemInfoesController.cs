using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemMonitoringLogger.Data;
using SystemMonitoringLogger.Entities;

namespace SystemMonitoringLogger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemInfoesController : ControllerBase
    {
        private readonly SystemMonitoringLoggerContext _context;

        public SystemInfoesController(SystemMonitoringLoggerContext context)
        {
            _context = context;
        }

        // GET: api/SystemInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemInfo>>> GetSystemInfo()
        {
            return await _context.SystemInfo.ToListAsync();
        }

        // GET: api/SystemInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemInfo>> GetSystemInfo(string id)
        {
            var systemInfo = await _context.SystemInfo.FindAsync(id);

            if (systemInfo == null)
            {
                return NotFound();
            }

            return systemInfo;
        }

        // PUT: api/SystemInfoes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSystemInfo(string id, SystemInfo systemInfo)
        {
            if (id != systemInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(systemInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SystemInfoes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SystemInfo>> PostSystemInfo(SystemInfo systemInfo)
        {
            _context.SystemInfo.Add(systemInfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SystemInfoExists(systemInfo.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSystemInfo", new { id = systemInfo.Id }, systemInfo);
        }

        // DELETE: api/SystemInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SystemInfo>> DeleteSystemInfo(string id)
        {
            var systemInfo = await _context.SystemInfo.FindAsync(id);
            if (systemInfo == null)
            {
                return NotFound();
            }

            _context.SystemInfo.Remove(systemInfo);
            await _context.SaveChangesAsync();

            return systemInfo;
        }

        private bool SystemInfoExists(string id)
        {
            return _context.SystemInfo.Any(e => e.Id == id);
        }
    }
}
