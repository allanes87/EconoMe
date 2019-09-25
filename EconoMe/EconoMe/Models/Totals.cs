using System;
using System.Collections.Generic;
using System.Text;

namespace EconoMe.Models
{
    public class Totals
    {
        public double Income { get; set; }
        public double Expense { get; set; }
        public double Balance => Income - Expense;
    }
}
