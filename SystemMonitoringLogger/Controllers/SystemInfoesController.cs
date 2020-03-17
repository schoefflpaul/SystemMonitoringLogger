using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemMonitoringLogger.Data;
using SystemMonitoringLogger.Entities;

namespace SystemMonitoringLogger.Controllers
{
    public class SystemInfoesController : Controller
    {
        private readonly SystemMonitoringLoggerContext _context;

        public SystemInfoesController(SystemMonitoringLoggerContext context)
        {
            _context = context;
        }

        // GET: SystemInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemInfo.ToListAsync());
        }

        // GET: SystemInfoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemInfo = await _context.SystemInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemInfo == null)
            {
                return NotFound();
            }

            return View(systemInfo);
        }

        // GET: SystemInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] SystemInfo systemInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemInfo);
        }

        // GET: SystemInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemInfo = await _context.SystemInfo.FindAsync(id);
            if (systemInfo == null)
            {
                return NotFound();
            }
            return View(systemInfo);
        }

        // POST: SystemInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id")] SystemInfo systemInfo)
        {
            if (id != systemInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemInfoExists(systemInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(systemInfo);
        }

        // GET: SystemInfoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemInfo = await _context.SystemInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemInfo == null)
            {
                return NotFound();
            }

            return View(systemInfo);
        }

        // POST: SystemInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var systemInfo = await _context.SystemInfo.FindAsync(id);
            _context.SystemInfo.Remove(systemInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemInfoExists(string id)
        {
            return _context.SystemInfo.Any(e => e.Id == id);
        }
    }
}
