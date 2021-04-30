using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{  
    public class Log
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

        public Log()
        {

        }

        public Log(int user_id, int item_id, int transaction_type, int time_id, DateTime book_date)
        {
            this.user_id = user_id;
            this.item_id = item_id;
            this.transaction_type = transaction_type;
            this.time_id = time_id;
            this.book_date = book_date;
            this.created = DateTime.Now;
        }

        public Log(Transaction transaction)
        {
            this.user_id = transaction.user_id;
            this.item_id = transaction.item_id;
            this.transaction_type = transaction.transaction_type;
            this.time_id = transaction.time_id;
            this.book_date = transaction.book_date;
            this.created = DateTime.Now;
        }
    }
}