using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public class Laboratory_item
    {
        [Key]
        public int uuid { get; set; }
        [Required]
        
        public int laboratory_id { get; set; }
        [Required]

        public int item_id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        
        public DateTime created { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]


        public DateTime updated { get; set; }
        /////////////////////////////////////////////////////////////////////////

        public virtual Laboratory laboratory { get; set; }

        public virtual Item item { get; set; }
    }
}