using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class BlackDB
    {
        public BlackDB() { }

        public static async Task<List<UserBlacklist>> GetBlacklistAsync()
        {
            var db = new SoftwareStudioContext();
            string queryString = $@"select users.uuid, users.fname, users.lname, users.gender,
    (case
        when blacklist.user_id is NULL then False
        else True
    end) as is_blacklist 
from users 
LEFT JOIN blacklist on users.uuid = blacklist.user_id
WHERE users.uuid not in (6, 7, 8, 9 ,10)
order by is_blacklist;";
            List<UserBlacklist> userBlacklists = await db.userBlacklists.FromSqlRaw(queryString).ToListAsync();

            return userBlacklists;
        }
    }
}