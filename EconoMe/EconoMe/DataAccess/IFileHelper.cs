using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.DataAccess
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
