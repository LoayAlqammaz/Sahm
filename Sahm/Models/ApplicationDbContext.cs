using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Sahm.Models
{

        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext() : base("DefaultConnection") { }

            public DbSet<User> Users { get; set; }
            public DbSet<Payment> Payments { get; set; }
            public DbSet<CarRequest> CarRequests { get; set; }
            public DbSet<Evaluation> Evaluations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Payment>().HasKey(p => p.PaymentId);
            modelBuilder.Entity<CarRequest>().HasKey(c => c.RequestId);
            modelBuilder.Entity<Evaluation>().HasKey(e => e.EvaluationId);

            base.OnModelCreating(modelBuilder);
        }
    }

}