using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Authn.Data;
using Authn.Models;
using Authn.DataDAO;
using Microsoft.AspNetCore.Authorization;

namespace Authn.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class PartsController : Controller
    {
        private readonly AuthDbContext _context;

        public PartsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: Parts
        public async Task<IActionResult> Index(string searchString, string searchType)
        {
            var parts = await _context.Part.ToListAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                parts = parts.Where(s => s.Name.Equals(searchString)).ToList();
            }
            if (!string.IsNullOrEmpty(searchType))
            {
                parts = parts.Where(s => s.Type.Equals(searchType)).ToList();
            }
            var partsDAO = new PartTypesDAO("DataSource=Data\\app.db");
            ViewBag.Parts = partsDAO.GetPartTypes();
            ViewBag.Selection = searchType;
            ViewBag.SearchStr = searchString;
            return View(parts);
        }

        // GET: Parts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Part
                .FirstOrDefaultAsync(m => m.Id == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        // GET: Parts/Create
        public IActionResult Create()
        {
            var partsDAO = new PartTypesDAO("DataSource=Data\\app.db");
            ViewBag.Parts = partsDAO.GetPartTypes();
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Name,Description,Price,Quantity")] Part part)
        {
            if (ModelState.IsValid)
            {
                _context.Add(part);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Part has been successfully created.";
                return RedirectToAction(nameof(Index));
            }
            return View(part);
        }

        // GET: Parts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var partTypessDAO = new PartTypesDAO("DataSource=Data\\app.db");
            ViewBag.Parts = partTypessDAO.GetPartTypes();
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Part.FindAsync(id);
            if (part == null)
            {
                return NotFound();
            }
            
            return View(part);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Name,Description,Price,Quantity")] Part part)
        {
            if (id != part.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var partDao = new PartDAO("DataSource=Data\\app.db");
                    var reparirDao = new RepairDAO("DataSource=Data\\app.db");
                    var oldName = partDao.GetPartName(part.Id);
                    if(oldName != part.Name) {
                        reparirDao.UpdatePart(oldName, part.Name);
                    }
                    _context.Update(part);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartExists(part.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["pass"] = "Part has been successfully modified.";
                return RedirectToAction(nameof(Index));
            }
            
            return View(part);
        }

        // GET: Parts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var part = await _context.Part
                .FirstOrDefaultAsync(m => m.Id == id);
            if (part == null)
            {
                return NotFound();
            }

            return View(part);
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part = await _context.Part.FindAsync(id);
            _context.Part.Remove(part);
            await _context.SaveChangesAsync();
            TempData["warning"] = "Part has been successfully removed.";
            return RedirectToAction(nameof(Index));
        }

        private bool PartExists(int id)
        {
            return _context.Part.Any(e => e.Id == id);
        }
    }
}
