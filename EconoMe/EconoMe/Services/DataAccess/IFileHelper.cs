using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.Service.DataAccess
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
