using System;

namespace softstu_project.Models
{
    public class Book
    {
        public int user_id { get; set; }

        public int item_id { get; set; }

        public int time_id { get; set; }

        public string date { get; set; }


        public Book()
        {
        }

        public Book(int user_id, int item_id, int time_id, string date)
        {
            this.user_id = user_id;
            this.item_id = item_id;
            this.time_id = time_id;
            this.date = date;
        }
    }
}
