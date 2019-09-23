using EconoMe.Models;
using EconoMe.Service.DataAccess;
using EconoMe.Services.LogService;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EconoMe.Services.DataAccess
{
    public class DataBaseService : IDataBaseService
    {
        private readonly ILoggerService _loggerService;
        private readonly IFileHelper _fileHepler;

        private const string DatabaseName = "Econome.db3";
        private const int DatabaseCurrentVersion = 1;
        private readonly string DatabasePath;
        public SQLiteAsyncConnection Connection;

        public DataBaseService(ILoggerService loggerService,
            IFileHelper fileHepler)
        {
            _loggerService = loggerService;
            _fileHepler = fileHepler;
            DatabasePath = _fileHepler.GetLocalFilePath(DatabaseName);
        }

        #region Initialize

        public async Task Initialize()
        {
            Connection = GetDatabase(DatabasePath);

            bool dataBaseExists = File.Exists(DatabasePath);
            if (!dataBaseExists)
            {
                await CreateTables();
            }
            else
            {
                await CheckDatabaseVersion();
            }
        }

        private SQLiteAsyncConnection GetDatabase(string dataBasePath)
        {
            try
            {
                return new SQLiteAsyncConnection(dataBasePath);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                throw ex;
            }
        }

        private async Task CheckDatabaseVersion()
        {
            //Check DB version
            if (!await TableExists(nameof(LocalSettings)))
            {
                await ReCreateDatabase();
            }
            else
            {
                var localSetting = await GetLocalSettingsAsync();
                if (!IsCurrentDatabaseVersion(localSetting))
                {
                    await ReCreateDatabase();
                }
            }
        }

        private bool IsCurrentDatabaseVersion(LocalSettings localSetting)
        {
            try
            {
                return (localSetting.DatabaseVersion == DatabaseCurrentVersion);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return false;
            }
        }

        private async Task ReCreateDatabase()
        {
            await Connection.DropTableAsync<Category>();

            await CreateTables();
        }

        private async Task CreateTables()
        {
            try
            {
                await Connection.CreateTableAsync<Category>();
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                throw ex;
            }

            await Seed();
        }

        private async Task Seed()
        {
            await GenerateLCategories();
        }

        #endregion

        #region LocalSettings

        private async Task GenerateLocalSettings()
        {
            LocalSettings settings = new LocalSettings()
            {
                DatabaseVersion = DatabaseCurrentVersion
            };

            await Connection.UpdateAsync(settings);
        }

        public async Task<LocalSettings> GetLocalSettingsAsync()
        {
            var localSettings = await Connection.Table<LocalSettings>().ToListAsync();
            return localSettings.FirstOrDefault();
        }

        #endregion

        #region Categories

        private async Task GenerateLCategories()
        {
            var categoryCar = new Category() { Name = "Car" };
            var categoryHouse = new Category() { Name = "House" };
            var categoryFood = new Category() { Name = "Food" };
            var categoryHobbies = new Category() { Name = "Hobbies" };

            await Connection.UpdateAllAsync(
                new List<Category>
                {
                    categoryCar,
                    categoryHouse,
                    categoryFood,
                    categoryHobbies
                }
            );
        }

        public async Task<List<Category>> GetCategories()
        {
            return await Connection.Table<Category>().ToListAsync();
        }

        #endregion

        #region Helper

        private async Task<bool> TableExists(string tableName)
        {
            try
            {
                if (Connection != null)
                {
                    var result = await Connection.ExecuteScalarAsync<string>(
                    $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}'");

                    return (result != null && result == tableName);
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return false;
            }
        }

        #endregion

    }
}
