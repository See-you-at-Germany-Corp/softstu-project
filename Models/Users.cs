using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models {
    public enum Role {
        user, admin, super_admin
    }
    public class Users{
        public int id { get; set; }
        [StringLength(50)]
        [Required]
        public string name { get; set; }
        public Role role { get; set; }
        [Required]
        [StringLength(20)]
        public string username { get; set; }
        [Required]
        [StringLength(100)]
        public string password { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime created { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime updated { get; set; }
    }
}