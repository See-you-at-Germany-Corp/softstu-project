using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace softstu_project.Models
{
    public class UserBlacklist
    {
        [Key]
        public int uuid { get; set; }
        
        public string fname { get; set; }

        public string lname { get; set; }

        public int gender { get; set; }

        public bool is_blacklist { get; set; }

        public UserBlacklist(int uuid, string fname, string lname, int gender, bool is_blacklist)
        {
            this.uuid = uuid;
            this.fname = fname;
            this.lname = lname;
            this.gender = gender;
            this.is_blacklist = is_blacklist;
        }
    }
}