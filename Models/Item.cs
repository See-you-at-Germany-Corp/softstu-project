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

        public string name { get; set; }
  
        public DateTime created { get; set; }

        public DateTime updated { get; set; } 

        public Item()
        {

        }

        public Item(string name)
        {
            this.name = name;
            this.created = DateTime.Now;
            this.updated = DateTime.Now;
        } 
    }
}