using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models {
    public class Items {
        public int id  { get; set; }
        [Required]
        [StringLength(50)]
        public string name  { get; set; }
        public Boolean is_available { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime created  { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime updated  { get; set; }
    }
}