using System;

namespace softstu_project.Models
{
    public class BookingFormModel
    {
        public int lab_id { get; set; }
        public int user_id { get; set; }
        public int time_id { get; set; }
        public int quantity { get; set; }
        public int item_type { get; set; }
        public DateTime book_date { get; set; }

        public BookingFormModel()
        {
        }

        public BookingFormModel(int lab_id, int user_id, int time_id, int quantity, int item_type, DateTime book_date)
        {
            this.lab_id = lab_id;
            this.user_id = user_id;
            this.time_id = time_id;
            this.quantity = quantity;
            this.item_type = item_type;
            this.book_date = book_date;
        }
    }
}
