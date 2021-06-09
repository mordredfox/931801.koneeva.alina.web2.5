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
    public class HospitalsController : Controller
    {
        private HospitalContext db;
        public HospitalsController(HospitalContext context)
        {
            db = context;
        }
        //С помощью метода db.Hospitals.ToListAsnc() мы будем получать объекты из бд, 
        //создавать из них список и передавать в представление.
        public async Task<IActionResult> Index()
        {
            return View(await db.Hospitals.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Hospital hosp)
        {
            //при помощи метода db.Phones.Add() для данных из объекта phone 
            //формируется sql-выражение INSERT
            db.Hospitals.Add(hosp);
            //метод db.SaveChangesAsync() добавляет данные в БД
            await db.SaveChangesAsync();
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Hospital hospital = await db.Hospitals.FirstOrDefaultAsync(p => p.Id == id);
                if (hospital != null)
                    return View(hospital);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Hospital hospital = await db.Hospitals.FirstOrDefaultAsync(p => p.Id == id);
                if (hospital != null)
                    return View(hospital);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Hospital hospital)
        {
            db.Hospitals.Update(hospital);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Hospital hospital = await db.Hospitals.FirstOrDefaultAsync(p => p.Id == id);
                if (hospital != null)
                    return View(hospital);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Hospital hospital = await db.Hospitals.FirstOrDefaultAsync(p => p.Id == id);
                if (hospital != null)
                {
                    db.Hospitals.Remove(hospital);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
