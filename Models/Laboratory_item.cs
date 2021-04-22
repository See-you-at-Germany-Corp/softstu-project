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

        public Laboratory_item()
        {

        }

        public Laboratory_item(int laboratory_id, int item_id)
        {
            this.laboratory_id = laboratory_id;
            this.item_id = item_id;
            this.created = DateTime.Now;
            this.updated = DateTime.Now;
        }
    }
}