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
    [Authorize]
    public class RepairsController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public RepairsController(AuthDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: Repairs
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Repair.ToListAsync());
        }
        [AllowAnonymous]
        public IActionResult Search()
        {
            return View();
        }


        // GET: Repairs/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _context.Repair
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        // GET: Repairs/Create
        public IActionResult Create()
        {
            var deliveriesDAO = new DelivertiesGetToList("DataSource=Data\\app.db");
            ViewBag.Deliveries = deliveriesDAO.GetDeliveryMethods();
            return View();
        }

        // POST: Repairs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,SerialNumber,Condition,Description,Delivery,Date,ImageFile,Status,Email")] Repair repair)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(repair.ImageFile.FileName);
            string extension = Path.GetExtension(repair.ImageFile.FileName);
            repair.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "\\images\\", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await repair.ImageFile.CopyToAsync(fileStream);
            }
            if (ModelState.IsValid)
            {
                repair.Status = "New";
                _context.Add(repair);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repair);
        }

        // GET: Repairs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //TODO - get old imageName From Database
            var repair = await _context.Repair.FindAsync(id);
            if (repair == null)
            {
                return NotFound();
            }
            var deliveriesDAO = new DelivertiesGetToList("DataSource=Data\\app.db");
            ViewBag.Deliveries = deliveriesDAO.GetDeliveryMethods();
            return View(repair);
        }

        // POST: Repairs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,SerialNumber,Condition,Description,Delivery,Date,Status,Email")] Repair repair)
        {
            if (id != repair.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var repairOLD = await _context.Repair.FindAsync(repair.Id);
                    //repair.ImageName = repairOLD.ImageName;
                    _context.Update(repair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepairExists(repair.Id))
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
            return View(repair);
        }

        // GET: Repairs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _context.Repair
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        // POST: Repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repair = await _context.Repair.FindAsync(id);
            if (repair.ImageName != null)
            {
                var imagePath = Path.Combine(hostEnvironment.WebRootPath, "images", repair.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

            }
            
            _context.Repair.Remove(repair);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairExists(int id)
        {
            return _context.Repair.Any(e => e.Id == id);
        }
    }
}
