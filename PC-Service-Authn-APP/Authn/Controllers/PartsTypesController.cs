using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Authn.Data;
using Authn.Models;
using Microsoft.AspNetCore.Authorization;
using Authn.DataDAO;

namespace Authn.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PartsTypesController : Controller
    {
        private readonly AuthDbContext _context;

        public PartsTypesController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: PartsTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PartsTypes.ToListAsync());
        }

        // GET: PartsTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partsTypes = await _context.PartsTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partsTypes == null)
            {
                return NotFound();
            }

            return View(partsTypes);
        }

        // GET: PartsTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PartsTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PartsTypes partsTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partsTypes);
                await _context.SaveChangesAsync();
                TempData["pass"] = "Part type has been successfully created.";
                return RedirectToAction(nameof(Index));
            }
            return View(partsTypes);
        }

        // GET: PartsTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partsTypes = await _context.PartsTypes.FindAsync(id);
            if (partsTypes == null)
            {
                return NotFound();
            }

            return View(partsTypes);
        }

        // POST: PartsTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PartsTypes partsTypes)
        {
            if (id != partsTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var partsTypesDao = new PartTypesDAO("DataSource=Data\\app.db");
                    var partdao = new PartDAO("DataSource=Data\\app.db");
                    partdao.UpdateType(partsTypesDao.GetPartOldName(partsTypes.Id), partsTypes.Name);
                    _context.Update(partsTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartsTypesExists(partsTypes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["pass"] = "Part type has been successfully modified.";
                return RedirectToAction(nameof(Index));
            }
            return View(partsTypes);
        }

        // GET: PartsTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partsTypes = await _context.PartsTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partsTypes == null)
            {
                return NotFound();
            }

            return View(partsTypes);
        }

        // POST: PartsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partsTypes = await _context.PartsTypes.FindAsync(id);
            _context.PartsTypes.Remove(partsTypes);
            await _context.SaveChangesAsync();
            TempData["warning"] = "Item has been successfully removed.";
            return RedirectToAction(nameof(Index));
        }

        private bool PartsTypesExists(int id)
        {
            return _context.PartsTypes.Any(e => e.Id == id);
        }
    }
}
