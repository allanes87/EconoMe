using EconoMe.Models.Enums;
using System;

namespace EconoMe.Models
{
    public class Entry : DataBaseEntity
    {
        public DateTime Date { get; set; }
        public Category Category { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public EntryType EntryType { get; set; }
    }
}
