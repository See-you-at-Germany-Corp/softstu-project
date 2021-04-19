using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class UserDatabase
    {
        public UserDatabase() { }

        public static async Task<List<User>> GetUserByID(int userID)
        {
            using (var db = new SoftwareStudioContext())
            {
                string queryString = $"SELECT * FROM users WHERE uuid = {userID}";
                var user = await db.users.FromSqlRaw(queryString).ToListAsync();

                return user;
            }
        }
    }
}