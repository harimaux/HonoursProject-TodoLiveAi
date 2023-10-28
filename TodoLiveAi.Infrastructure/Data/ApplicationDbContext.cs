using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoLiveAi.Core.DbModels;
using TodoLiveAi.Core;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace TodoLiveAi.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<TaskDB>? TaskDB { get; set; }
        public DbSet<TaskPriorityDB>? TaskPriorityDB { get; set; }
        public DbSet<TestingDB> TestingDB { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(
        //            optionsBuilder.Options.GetExtension<SqlServerOptionsExtension>()?.ConnectionString ?? throw new InvalidOperationException(),
        //            sqlServerOptions => sqlServerOptions.MigrationsAssembly("TodoLiveAi.Infrastructure"));
        //    }
        //}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationship between Posts and Users
            modelBuilder.Entity<TaskDB>()
                .HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            //.OnDelete(DeleteBehavior.Cascade);
        }


    }
}
