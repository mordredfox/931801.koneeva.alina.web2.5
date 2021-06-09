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
    public class PatientsController : Controller
    {
        private HospitalContext db;
        public PatientsController(HospitalContext context)
        {
            db = context;
        }
        //С помощью метода db.Hospitals.ToListAsnc() мы будем получать объекты из бд, 
        //создавать из них список и передавать в представление.
        public async Task<IActionResult> Index()
        {
            return View(await db.Patients.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Patient pat)
        {
            //при помощи метода db.Phones.Add() для данных из объекта phone 
            //формируется sql-выражение INSERT
            db.Patients.Add(pat);
            //метод db.SaveChangesAsync() добавляет данные в БД
            await db.SaveChangesAsync();
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Patient pat = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
                if (pat != null)
                    return View(pat);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Patient pat = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
                if (pat != null)
                    return View(pat);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Patient pat)
        {
            db.Patients.Update(pat);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Patient pat = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
                if (pat != null)
                    return View(pat);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Patient pat = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
                if (pat != null)
                {
                    db.Patients.Remove(pat);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
