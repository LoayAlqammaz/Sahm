using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sahm.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public virtual User User { get; set; }
    }

}