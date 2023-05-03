using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebApi.model;

namespace MyWebApi
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Interview>? Interviews { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Candidate>? Candidates { get; set; }
        public DbSet<JobOffer>? JobOffers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=interviews.db");
        }
    }
}