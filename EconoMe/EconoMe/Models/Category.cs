using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.Models
{
    public class Category : DataBaseEntity
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
    }
}
