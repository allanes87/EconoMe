using EconoMe.DataAccess;
using System;
using System.IO;

namespace EconoMe.IOS.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string databaseName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, databaseName);
        }
    }
}