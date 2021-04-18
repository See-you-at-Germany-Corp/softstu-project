using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public enum Transaction_type
    {
        borrow, give, lated_give
    }
    public class Transaction
    {
        [Key]
        public int uuid { get; set; }
        [Required]

        public int user_id { get; set; }
        
        [Required]
        public int item_id { get; set; }

        public Transaction_type transactions_type { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]

        public DateTime created { get; set; }
        /////////////////////////////////////////////////////////////////////////

        public virtual User user { get; set; }

        public virtual Item item { get; set; }
    }
}