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
        Raspberry_Pi_3,
        Macbook_M1,
        Lan_Cable,
        Arduino_Uno,
        Arduino_Nano,
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

        public static string getName(int itemType)
        {
            List<string> itemNames = new List<string> { "None", "Keyboard", "Mouse", "Monitor", "Ups", "Speaker", "Raspberry Pi 3", "Macbook M1", "Lan cable", "Arduino Uno", "Arduino Nano" };

            return itemNames[itemType];
        }
    }
}