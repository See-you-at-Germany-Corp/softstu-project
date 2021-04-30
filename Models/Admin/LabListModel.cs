using System;

namespace softstu_project.Models
{
    public class LabListModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int total_tool { get; set; }

        public LabListModel()
        {
        }

        public LabListModel(int id, string name, int total_tool)
        {
            this.id = id;
            this.name = name;
            this.total_tool = total_tool;
        }
    }
}