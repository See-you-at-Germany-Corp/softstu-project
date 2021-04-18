using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public class Item
    {
        [Key]
        public int uuid { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]

        public string name { get; set; }

        public Boolean is_available { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]

        public DateTime created { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]

        public DateTime updated { get; set; }
        /////////////////////////////////////////////////////////////////////////

        public virtual Laboratory laboratory { get; set; }
    }
}