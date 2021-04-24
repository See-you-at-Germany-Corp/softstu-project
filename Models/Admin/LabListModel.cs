using System;
using System.Collections.Generic;

namespace softstu_project.Models
{
    public class LabListModel
    {
        public int id { get; set; }

        public string name { get; set; }
        
        public int total_tool { get; set; }

        public int current_tool_am { get; set;}

        public int current_tool_pm { get; set;}

        public LabListModel()
        {
        }

        public LabListModel(int id, string name, int total_tool, int current_tool_am, int current_tool_pm)
        {
            this.id = id;
            this.name = name;
            this.total_tool = total_tool;
            this.current_tool_am = current_tool_am;
            this.current_tool_pm = current_tool_pm;
        }
    }
}