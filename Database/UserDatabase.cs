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

        public static async Task<List<User>> GetByIDAsync(int userID)
        {
            using (var db = new SoftwareStudioContext())
            {
                string queryString = $"SELECT * FROM users WHERE uuid = {userID}";
                var user = await db.users.FromSqlRaw(queryString).ToListAsync();

                return user;
            }
        }

        public static int Register(User user)
        {
            var db = new SoftwareStudioContext();
            
            /// hash password.
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.password);

            user.password = hashedPassword;
            db.users.Add(user);
            db.SaveChanges();

            return 0;
        }

        public async static Task<int> LoginAsync(string username, string password)
        {
            var db = new SoftwareStudioContext();

            /// find in db user with username.
            User user = await db.users.FirstOrDefaultAsync(user => user.username == username);

            if (user == null) return -1;
            else
            {
                /// verify incoming password with in db password.
                bool result = BCrypt.Net.BCrypt.Verify(password, user.password);

                if (result) return user.uuid; 
                else return -1;
            } 
        }
    }
}

/*
    * unitest
    string value1 = await UserDatabase.Login("61011422", "12345678") + "";
    string value2 = await UserDatabase.Login("61011444", "1234567") + "";

    * admin
    User user1 = new User(User_role.admin, "61010914", "12345678", "ลัทธพล", "แพ่งสภา", 61010914, "วิศวกรรมศาสตร์", "วิศวกรรมคอมพิวเตอร์", "61010914@kmitl.ac.th");
    UserDatabase.Register(user1);
    User user2 = new User(User_role.admin, "61010968", "12345678", "วิธวินท์", "เมืองน้อย", 61010968, "วิศวกรรมศาสตร์", "วิศวกรรมคอมพิวเตอร์", "61010968@kmitl.ac.th");
    UserDatabase.Register(user2);
    User user3 = new User(User_role.admin, "61011405", "12345678", "พรรษา", "บุญทวีกุลสวัสดิ์", 61011405, "วิศวกรรมศาสตร์", "วิศวกรรมคอมพิวเตอร์", "61011405@kmitl.ac.th");
    UserDatabase.Register(user3);
    User user4 = new User(User_role.admin, "61011422", "12345678", "วีรวิทย์", "ศิรกุลวัฒน์", 61011422, "วิศวกรรมศาสตร์", "วิศวกรรมคอมพิวเตอร์", "61011422@kmitl.ac.th");
    UserDatabase.Register(user4);
    User user5 = new User(User_role.admin, "61011433", "12345678", "เสฎฐวุฒิ", "ทิพย์กรรภิรมย์", 61011433, "วิศวกรรมศาสตร์", "วิศวกรรมคอมพิวเตอร์", "61011433@kmitl.ac.th");
    UserDatabase.Register(user5);
*/