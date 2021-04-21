using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class LabDatabase
    {
        public LabDatabase() { }

        public static async Task<List<Laboratory>> GetAllLab()
        {
            var db = new SoftwareStudioContext();
            var lab = await db.laboratories.FromSqlRaw("SELECT * FROM laboratories").ToListAsync();

            return lab;
        }
        public static Laboratory GetLabByID(int labID)
        {
            var db = new SoftwareStudioContext();
            var lab = db.laboratories.Find(labID);

            return lab;
        }

        public async static Task<IList<LabListModel>> GetLabList()
        {
            IList<LabListModel> labLists = new List<LabListModel>();

            var db = new SoftwareStudioContext();
            var labs = await GetAllLab();
            var allItems = await LabItemDatabase.GetAllItem();

            for (int i = 0; i < 5; i++)
            {
                Laboratory lab = labs[i];
                labLists.Add(new LabListModel() { id = lab.uuid, name = lab.name, total_tool = allItems[i] });
            }

            return labLists;
        }
    }
}