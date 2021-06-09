using System;

namespace WebApplication5.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phones { get; set; }
        public string Diagnosis { get; set; }
    }
}