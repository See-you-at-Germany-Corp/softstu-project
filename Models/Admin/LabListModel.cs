using System;

namespace soft_stu_project.Models
{
    public class LabListModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int current_tool { get; set; }
        public int total_tool { get; set; }

        public LabListModel()
        {
        }

        public LabListModel(int id, string name, int current_tool, int total_tool)
        {
            this.id = id;
            this.name = name;
            this.current_tool = current_tool;
            this.total_tool = total_tool;
        }
    }
}