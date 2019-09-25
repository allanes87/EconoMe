using EconoMe.Models;
using EconoMe.Services.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EconoMe.Services.Entries
{
    public class EntryService : IEntryService
    {
        private readonly IDataBaseService _databaseService;

        public EntryService(IDataBaseService databaseService)
        {
            _databaseService = databaseService;
        }

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
            return await _databaseService.GetMyEntries();
        }

        public async Task<Totals> GetTotals()
        {
            var result = new Totals();
            var myEntries = await GetMyEntries();

            var income = myEntries.
                Where(x => x.EntryType == Models.Enums.EntryType.Income)
                .Sum(e => e.Amount);

            var expense = myEntries.
                Where(x => x.EntryType == Models.Enums.EntryType.Outcome)
                .Sum(e => e.Amount);

            result = new Totals()
            {
                Income = income,
                Expense = expense
            };

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
