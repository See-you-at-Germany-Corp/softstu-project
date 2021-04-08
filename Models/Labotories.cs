using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models {
    public class Labotaries {
        public int id { get; set; }
        [Required]
        [StringLength(50, MinimumLength=1)]
        public string name { get; set; }
        [StringLength(200, MinimumLength=0)]
        public string description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime created { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime updated { get; set; }
        /////////////////////////////////////////////////////////////////////////
        public virtual ICollection<Labotary_item> labotary_Items { get; set; }
    }
}