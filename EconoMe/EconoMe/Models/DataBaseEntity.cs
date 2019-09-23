using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.Models
{
    public class DataBaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
