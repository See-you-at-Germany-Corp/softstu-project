using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public class Black
    {
        [Key]
        public int user_id { get; set;}
        public Black(int user_id)
        {
            this.user_id = user_id;
        }
    }
}