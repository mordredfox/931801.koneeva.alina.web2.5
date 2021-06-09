using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class LabsController : Controller
    {
        private HospitalContext db;
        public LabsController(HospitalContext context)
        {
            db = context;
        }
        //С помощью метода db.Hospitals.ToListAsnc() мы будем получать объекты из бд, 
        //создавать из них список и передавать в представление.
        public async Task<IActionResult> Index()
        {
            return View(await db.Labs.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Lab lab)
        {
            //при помощи метода db.Phones.Add() для данных из объекта phone 
            //формируется sql-выражение INSERT
            db.Labs.Add(lab);
            //метод db.SaveChangesAsync() добавляет данные в БД
            await db.SaveChangesAsync();
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Lab lab = await db.Labs.FirstOrDefaultAsync(p => p.Id == id);
                if (lab != null)
                    return View(lab);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Lab lab = await db.Labs.FirstOrDefaultAsync(p => p.Id == id);
                if (lab != null)
                    return View(lab);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Lab lab)
        {
            db.Labs.Update(lab);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Lab lab = await db.Labs.FirstOrDefaultAsync(p => p.Id == id);
                if (lab != null)
                    return View(lab);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Lab lab = await db.Labs.FirstOrDefaultAsync(p => p.Id == id);
                if (lab != null)
                {
                    db.Labs.Remove(lab);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
