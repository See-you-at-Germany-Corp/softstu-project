using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public class Laboratory
    {
        [Key]
        public int uuid { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]

        public string name { get; set; }
        [StringLength(200, MinimumLength = 0)]

        public string description { get; set; }

        public DateTime created { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        
        public DateTime updated { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        ///////////////////////////////////////////////////////////////////////

        public virtual ICollection<Laboratory_item> laboratory_Items { get; set; }

        public Laboratory() {
            
        }

        public Laboratory(int uuid, string name, string description, DateTime created, DateTime updated) {
            this.uuid = uuid;
            this.name = name;
            this.description = description;
            this.created = created;
            this.updated = updated;
        }
    }
}