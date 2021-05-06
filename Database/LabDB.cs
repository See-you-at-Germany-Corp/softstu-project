using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class LabDB
    {
        public LabDB() { }

        public static async Task<List<Laboratory>> GetAllAsync()
        {
            var db = new SoftwareStudioContext();
            List<Laboratory> labs = await db.laboratories.FromSqlRaw("SELECT * FROM laboratories").ToListAsync();

            return labs;
        }

        public static Laboratory GetByID(int labID)
        {
            var db = new SoftwareStudioContext();
            Laboratory lab = db.laboratories.Find(labID);

            return lab;
        }

        public async static Task<IList<LabListModel>> GetListAsync()
        {
            IList<LabListModel> labLists = new List<LabListModel>();

            var db = new SoftwareStudioContext();
            List<Laboratory> labs = await GetAllAsync();
            var allItems = await LabItemDB.GetAllQuantityAsync();

            for (int i = 0; i < 5; i++)
            {
                Laboratory lab = labs[i];
                labLists.Add(new LabListModel() { id = lab.uuid, name = lab.name, total_tool = allItems[i] });
            }

            return labLists;
        }
    }
}