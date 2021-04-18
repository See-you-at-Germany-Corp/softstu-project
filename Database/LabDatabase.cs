using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ConsoleApp.PostgreSQL;
using softstu_project.Models;
using Npgsql;

namespace ConsoleApp.PostgreSQL
{
    public class LabDatabase : Controller {
        public LabDatabase() { }

        public static async Task<List<Laboratory>> GetAllLab() {
            using (var db = new SoftwareStudioContext())
            {
                var lab = await db.laboratories.FromSqlRaw("SELECT * FROM laboratories").ToListAsync();

                return lab;
            }
        }
        public static Laboratory GetLabByID(int labID) {
            using (var db = new SoftwareStudioContext())
            {
                var lab = db.laboratories.Find(labID);

                return lab;
            }
        }
    }
}