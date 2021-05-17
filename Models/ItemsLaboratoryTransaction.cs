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

        public Boolean time_am { get; set; }

        public Boolean time_pm { get; set; }

        public int laboratory_id { get; set; }

        public int type { get; set; }
    }
}