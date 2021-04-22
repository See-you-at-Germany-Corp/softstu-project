using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public enum Time_id_type
    {
        none, am, pm, day
    }

    public enum Transaction_type
    {
        borrow, give
    }
    public class Transaction
    {
        [Key]
        public int uuid { get; set; }

        public int user_id { get; set; }

        public int item_id { get; set; }

        public int time_id { get; set; }

        public int transaction_type { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]

        public DateTime book_date { get; set; }

        public DateTime created { get; set; }

        public Transaction()
        {

        }

        public Transaction(int user_id, int item_id, int transaction_type, int time_id, DateTime book_date)
        {
            this.user_id = user_id;
            this.item_id = item_id;
            this.transaction_type = transaction_type;
            this.time_id = time_id;
            this.book_date = book_date;
            this.created = DateTime.Now;
        }

        public string ToColumnString()
        {
            return $"(user_id, item_id, transaction_type, time_id, created, book_date)";
        }

        public string ToValueString()
        {
            return $"({this.user_id}, {this.item_id}, {this.transaction_type}, {this.time_id}, '{this.created}', '{this.book_date}')";
        }
    }
}