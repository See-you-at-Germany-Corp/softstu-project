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
        public DbSet<ItemDetail> itemDetails { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<Log> logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            // => optionsBuilder.UseNpgsql("Host=ec2-54-152-185-191.compute-1.amazonaws.com;Database=d24grd5b53erkr;Username=zdsnyvscwvtbbh;Password=8fedcb67936f50cf734cc63ecaa3cd23d091a2b5809552756681c47f4d1e9a72;sslmode=Require;Trust Server Certificate=true");
            => optionsBuilder.UseNpgsql("Host=ec2-54-224-194-214.compute-1.amazonaws.com;Database=d21ibcoasub7u2;Username=qeidxsqwybysbw;Password=88de187159666fd97dd5007bb402e48101dd653cfdaf2b353dcb222876b03e62;sslmode=Require;Trust Server Certificate=true");

        public string ListToString(List<String> strList)
        {
            string newString = String.Join(", ", strList);
            return newString;
        }
    }
}