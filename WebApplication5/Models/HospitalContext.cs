using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Models
{
    public class HospitalContext : DbContext
    {
        //DbSet - коллекция объектов, которая сопоставляется с определенной таблицей в БД
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Lab> Labs { get; set; }
   
        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options)
        {
            Database.EnsureCreated(); //создается БД
        }
    }
}
