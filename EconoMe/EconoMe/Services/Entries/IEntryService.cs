using EconoMe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EconoMe.Services.Entries
{
    public interface IEntryService
    {
        Task<List<Category>> GetCategories();
        Task<bool> SaveEntry(Models.Entry entry);
        Task<List<Entry>> GetMyEntries();
    }
}
