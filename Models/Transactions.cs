using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models {
    public  enum Transaction_type {
        borrow, give, rated_give
    }
    public class Transactions {
        public int id { get; set; }
        [Required]
        public int user_id { get; set; }
        [Required]
        public int item_id { get; set; }
        public Transaction_type transactions_type  { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "0:{yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime created { get; set; }
        /////////////////////////////////////////////////////////////////////////
        public virtual Users user { get; set; }
        public virtual Items item { get; set; }
    }
}