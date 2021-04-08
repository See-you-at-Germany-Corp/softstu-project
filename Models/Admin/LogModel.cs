using System;

namespace soft_stu_project.Models
{
    public class LogModel
    {
        public int id { get; set; }
        public string user_name { get; set; }
        public int item_id { get; set; }
        public int transaction_type { get; set; }
        public DateTime created { get; set; }

        public LogModel()
        {
        }

        public LogModel(int id, string user_name, int item_id, int transaction_type, DateTime created)
        {
            this.id = id;
            this.user_name = user_name;
            this.item_id = item_id;
            this.transaction_type = transaction_type;
            this.created = created;
        }
    }
}