using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriftyTrifty.DataAccess.Models
{
    public class ExpenseBalance
    {
        public string UserName { get; set; }
        public decimal Paid { get; set; }
        public decimal Balance { get; set; }
    }
}
