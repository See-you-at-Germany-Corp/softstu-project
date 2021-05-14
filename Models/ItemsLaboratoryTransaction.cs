using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public class ItemsLaboratoryTransaction
    {
        [Key]
        public int uuid { get; set; }

        public string name { get; set; }

        public int time_id { get; set; }

        public int laboratory_id { get; set; }

        public int type { get; set; }

        public override string ToString()
        {
            return $@"{this.uuid}, {this.name}, {this.time_id}, {this.laboratory_id}, {this.type}";
        }
    }
}