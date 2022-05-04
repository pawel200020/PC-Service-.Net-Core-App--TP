﻿using System;
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
        private RepairDAO repairDAO;
        public RepairsController(AuthDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
            repairDAO = new RepairDAO("DataSource=Data\\app.db");
        }

        // GET: Repairs
        [Authorize(Roles = "Admin")]
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
            var deliveriesDAO = new DelivertiesDAO("DataSource=Data\\app.db");
            ViewBag.Deliveries = deliveriesDAO.GetDeliveryMethods();
            return View();
        }

        // POST: Repairs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,SerialNumber,Condition,Description,Delivery,Date,ImageFile,Status,Email,Report,PartsUsed,Labour")] Repair repair)
        {


            if (ModelState.IsValid)
            {
                if (repair.ImageFile != null)
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
                }


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

            var repair = await _context.Repair.FindAsync(id);
            if (repair == null)
            {
                return NotFound();
            }

            var deliveriesDAO = new DelivertiesDAO("DataSource=Data\\app.db");
            ViewBag.Deliveries = deliveriesDAO.GetDeliveryMethods();
            return View(repair);
        }

        // POST: Repairs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,SerialNumber,Condition,Description,Delivery,Date,Status,Email,Report,PartsUsed,Labour")] Repair repair)
        {
            bool wasMesaage = false;
            if (id != repair.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    PartDAO partDAO = new PartDAO("DataSource=Data\\app.db");
                    var oldItem = repairDAO.GetRepair(repair.Id);
                    List<string> NonSufficientParts = new List<string>();
                    if (oldItem != null)
                    {
                        repair.ImageName = oldItem.ImageName;
                    }
                    oldItem = repairDAO.GetRepairPartsUsed(repair.Id);

                    if (oldItem != null)
                    {
                        var partsToChangeInDB = new Dictionary<string, int>();
                        foreach (KeyValuePair<string, int> entry in repair.PartsUsedList)
                        {
                            if (oldItem.PartsUsedList.ContainsKey(entry.Key))
                            {
                                if (entry.Value - oldItem.PartsUsedList[entry.Key] != 0)
                                    partsToChangeInDB.Add(entry.Key, entry.Value - oldItem.PartsUsedList[entry.Key]);
                            }
                            else
                            {
                                partsToChangeInDB.Add(entry.Key, entry.Value);
                            }

                        }
                        foreach (KeyValuePair<string, int> entry in oldItem.PartsUsedList)
                        {
                            if (!repair.PartsUsedList.ContainsKey(entry.Key))
                            {
                                partsToChangeInDB.Add(entry.Key, -entry.Value);
                            }
                        }
                        NonSufficientParts = partDAO.isEnoughPartInDatabase(partsToChangeInDB);
                        if (NonSufficientParts.Count == 0)
                        {
                            foreach (KeyValuePair<string, int> entry in partsToChangeInDB)
                            {
                                partDAO.DecrementPart(entry.Key, entry.Value);
                            }               
                        }
                        else
                        {
                            repair.PartsUsed = oldItem.PartsUsed;
                            repair.PartsUsed = "";
                            TempData["error"] = "there is not enough parts: ";
                            foreach (var item in NonSufficientParts)
                            {
                                TempData["error"] += item + ", ";
                            }
                            wasMesaage = true;
                        }
                    }
                    else
                    {
                        NonSufficientParts = partDAO.isEnoughPartInDatabase(repair.PartsUsedList);
                        if (NonSufficientParts.Count == 0)
                        {
                            foreach (KeyValuePair<string, int> entry in repair.PartsUsedList)
                            {
                                partDAO.DecrementPart(entry.Key, entry.Value);
                            }
                        }
                        else
                        {
                            repair.PartsUsed = "";
                            TempData["error"] = "there is not enough parts: ";
                            foreach (var item in NonSufficientParts)
                            {
                                TempData["error"] += item + ", ";
                            }
                            wasMesaage= true;
                        }
                    }
                    if (!wasMesaage)
                    {
                        TempData["pass"] = "Item has been successfully modified";
                    }
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
