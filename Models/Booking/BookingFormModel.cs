using System;

namespace softstu_project.Models
{
    public class BookingFormModel
    {
        public int time_am { get; set; }
        public int time_pm { get; set; }
        public int quantity { get; set; }
        public int item_type { get; set; }
        public DateTime book_date { get; set; }

        public BookingFormModel()
        {
        }

        public BookingFormModel(int time_am, int time_pm, int quantity, int item_type, DateTime book_date)
        {
            this.time_am = time_am;
            this.time_pm = time_pm;
            this.quantity = quantity;
            this.item_type = item_type;
            this.book_date = book_date;
        }
    }
}
