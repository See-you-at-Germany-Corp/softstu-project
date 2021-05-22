using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public enum ItemTypes
    {
        None,
        Keyboard,
        Mouse,
        Monitor,
        Ups,
        Speaker,
    }

    public class Item
    {
        [Key]
        public int uuid { get; set; }
        [Required]

        public string name { get; set; }

        public int type { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }

        public Item()
        {

        }

        public Item(string name, ItemTypes itemType)
        {
            this.name = name;
            this.created = DateTime.Now;
            this.updated = DateTime.Now;
            this.type = (int)itemType;
        }

        public static string getName(ItemTypes itemType)
        {
            List<string> itemNames = new List<string>() { "none", "Keyboard", "Mouse", "Monitor", "Ups", "Speaker" };

            return itemNames[(int)itemType];
        }

        public static string getName(int itemType)
        {
            List<string> itemNames = new List<string>() { "none", "Keyboard", "Mouse", "Monitor", "Ups", "Speaker" };

            return itemNames[itemType];
        }
    }
}