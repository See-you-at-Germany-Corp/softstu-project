using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public class UserRegister
    {
        public int role_id { get; set; }
        [Required]

        public string username { get; set; }
        [Required]

        public string password { get; set; }
        [DataType(DataType.Date)]

        public string fname { get; set; }

        public string lname { get; set; }

        public int student_id { get; set; }

        public string faculty { get; set; }

        public string department { get; set; }

        public string email { get; set; }

        public int gender { get; set; }

        public UserRegister()
        {
        }

        public UserRegister(int role_id, string username, string password, string fname, string lname, int student_id, string facaulty, string department, string email, int gender)
        {
            this.role_id = role_id;
            this.username = username;
            this.password = password;
            this.fname = fname;
            this.lname = lname;
            this.student_id = student_id;
            this.faculty = facaulty;
            this.department = department;
            this.email = email;
            this.gender = gender;
        }
    }
}