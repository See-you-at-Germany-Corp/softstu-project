using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class LogDatabase
    {
        public LogDatabase() { }

        public static async Task<List<Log>> GetAllAsync()
        {
            var db = new SoftwareStudioContext();
            List<Log> logs = await db.logs.FromSqlRaw("SELECT * FROM logs").ToListAsync();

            return logs;
        }

        public static void Add(Log log)
        {
            var db = new SoftwareStudioContext();
            
            if (log != null)
            {
                db.logs.Add(log);
                db.SaveChanges();
            }
        }

        public static void AddWithTransaction(Transaction transaction)
        {
            var db = new SoftwareStudioContext();
            
            if (transaction != null)
            {
                db.logs.Add(new Log(transaction));
                db.SaveChanges();
            }
        }
    }
}