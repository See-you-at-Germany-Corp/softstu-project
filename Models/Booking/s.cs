using System;

namespace softstu_project.Models
{
    public class ItemTimeSlot
    {
        public int am { get; set; }
        public int pm { get; set; }
        public DateTime date { get; set; }

        public ItemTimeSlot()
        {

        }

        public ItemTimeSlot(int am, int pm, DateTime date)
        {
            this.am = am;
            this.pm = pm;
            this.date = date;
        }
    }
}