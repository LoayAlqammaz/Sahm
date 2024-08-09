using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sahm.Models
{
    public class CarRequest
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public virtual User User { get; set; }
    }

}