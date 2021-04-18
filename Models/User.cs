using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public enum Role
    {
        user, admin, super_admin
    }
    public class User
    {
        [Key]
        public int uuid { get; set; } 
        [StringLength(50, MinimumLength = 1)]
        [Required]
 
        public string role { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]

        public string username { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8)]

        public string password { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]

        public string email { get; set; }

        public DateTime created { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]

        public DateTime updated { get; set; }

        public User()
        {
        }

        /////////////////////////////////////////////////////////////////////////
        // public virtual ICollection<Transactions> transactions { get; set; }
    }
}