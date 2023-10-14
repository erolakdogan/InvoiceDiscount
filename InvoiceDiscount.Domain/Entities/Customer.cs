using InvoiceDiscount.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDiscount.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime JoinDate { get; set; }
        public CustomerType CustomerType { get; set; } // Enum: Regular, Employee, Affiliate

    }
}
