using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore; 
using Npgsql;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class SoftwareStudioContext : DbContext
    {   
        public DbSet<User> users { get; set; }
        public DbSet<Laboratory> laboratories { get; set; }
        public DbSet<Laboratory_item> laboratory_items { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<Transaction> transactions { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=ec2-3-217-219-146.compute-1.amazonaws.com;Database=dad5f4jh36a3qs;Username=kuxcqkumnhxint;Password=12cc898a3f88dc5addb495a6bcb7d23adcfce841482fc9c90af37772cc91af33;sslmode=Require;Trust Server Certificate=true");
    }
}