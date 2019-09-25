using EconoMe.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.UnitTest.Stubs
{
    public static class EntriesStub
    {
        public static List<Entry> GetMyEntriesWithTwoExpenses()
        {
            return new List<Entry>()
            {
                new Entry()
                {
                    Id = 1,
                    Category = new Category() {Id = 1, Name = "Car" },
                    Date = DateTime.Now,
                    Description = "Parking",
                    EntryType = Models.Enums.EntryType.Outcome,
                    Amount = 200
                },
                new Entry()
                {
                    Id = 2,
                    Category = new Category() {Id = 2, Name = "Food" },
                    Date = DateTime.Now,
                    Description = "Supermarket",
                    EntryType = Models.Enums.EntryType.Outcome,
                    Amount = 300
                },
                new Entry()
                {
                    Id = 3,
                    Category = new Category() {Id = 3, Name = "Salary" },
                    Date = DateTime.Now,
                    Description = "Salary",
                    EntryType = Models.Enums.EntryType.Income,
                    Amount = 1000
                },
            };
        }
    }
}
