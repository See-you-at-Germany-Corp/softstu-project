using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public enum User_role
    {
        user, admin
    }
    public class User
    {
        [Key]
        public int uuid { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [Required]
      
        public User_role role_id { get; set; }
        [Required]

        public string username { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8)]

        public string password { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]

        public string fname { get; set; }

        public string lname { get; set; }
      
        public int student_id { get; set; }

        public string faculty { get; set; }

        public string department { get; set; }

        public string email { get; set; }
        
        public int gender { get; set; }

        public DateTime created { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]

        public DateTime updated { get; set; }

        public User()
        {
        }

        public User(User_role role_id, string username, string password, string fname, string lname, int student_id, string facaulty, string department, string email)
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
            this.created = DateTime.Now;
            this.updated = DateTime.Now;
        }

        /////////////////////////////////////////////////////////////////////////
        // public virtual ICollection<Transactions> transactions { get; set; }
    }
} 