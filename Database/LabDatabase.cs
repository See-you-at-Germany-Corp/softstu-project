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
            using (var db = new SoftwareStudioContext())
            {
                var lab = await db.laboratories.FromSqlRaw("SELECT * FROM laboratories").ToListAsync();

                return lab;
            }
        }
        public static Laboratory GetLabByID(int labID)
        {
            using (var db = new SoftwareStudioContext())
            {
                var lab = db.laboratories.Find(labID);

                return lab;
            }
        }

        public async static Task<IList<LabListModel>> GetLabList()
        {
            IList<LabListModel> labLists = new List<LabListModel>();

            using (var db = new SoftwareStudioContext())
            {
                var labs = await GetAllLab();
                var allItems = await LabItemDatabase.GetAllItem();
                var currentItems = await LabItemDatabase.GetCurrentItem();

                for (int i = 0; i < 5; i++)
                {
                    Laboratory lab = labs[i];
                    labLists.Add(new LabListModel() { id = lab.uuid, name = lab.name, current_tool = currentItems[i], total_tool = allItems[i] });
                }
            }

            return labLists;
        }
    }
}