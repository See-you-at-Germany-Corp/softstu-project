using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public class TransactionItem
    {
        [Key]
        public int uuid { get; set; }

        public string name { get; set; }

        public int time_id { get; set; }

        public int item_id { get; set; }

        public int type { get; set; }

        public DateTime book_date { get; set; }

        public override string ToString()
        {
            return $"name: {name}, type: {type}, bookdate: {book_date}";
        }
    }
}