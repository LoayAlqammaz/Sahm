using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sahm.Models
{
    public class Evaluation
    {
        public int EvaluationId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public string Emoji { get; set; } 
        public DateTime EvaluationDate { get; set; }
        public virtual User User { get; set; }
    }
}