using EconoMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EconoMe.Services.Entries
{
    public class EntryService : IEntryService
    {
        public async Task<Category> GetCategoryByName(string name)
        {
            var result = new Category();

            await Task.Run(() => 
            {
                result = new Category() { Id = 1, ImageSource = "", Name = "Car" };
            });

            return result;
        }

        public async Task<List<string>> GetCategories()
        {
            var categories = new List<Category>();
            await Task.Run(() =>
            {
                categories = new List<Category>()
                {
                    new Category() { Id = 1, ImageSource = "", Name = "Car" },
                    new Category() { Id = 2, ImageSource = "", Name = "House" },
                    new Category() { Id = 3, ImageSource = "", Name = "Food" },
                };
            });

            return categories.Select(x => x.Name).ToList();
        }

        public async Task<List<Entry>> GetMyEntries()
        {
            var result = new List<Entry>();
            await Task.Run(() =>
            {
                result = new List<Entry>()
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
            });

            return result;
        }

        public async Task<Totals> GetTotals()
        {
            var result = new Totals();

            await Task.Run(() => 
            {
                result = new Totals()
                {
                    Income = 1000,
                    Expense = 500
                };
            });

            return result;
        }

        public async Task<bool> SaveEntry(Models.Entry entry)
        {
            var result = false;
            await Task.Run(() => { result = true; });

            return result;
        }
    }
}
