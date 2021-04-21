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

        public static async Task<List<Laboratory>> GetAll()
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

        public async static Task<IList<LabListModel>> GetList()
        {
            IList<LabListModel> labLists = new List<LabListModel>();

            var db = new SoftwareStudioContext();
            List<Laboratory> labs = await GetAll();
            var allItems = await LabItemDatabase.GetAllQuantity();

            for (int i = 0; i < 5; i++)
            {
                Laboratory lab = labs[i];
                labLists.Add(new LabListModel() { id = lab.uuid, name = lab.name, total_tool = allItems[i] });
            }

            return labLists;
        }
    }
}