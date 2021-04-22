using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class LabItemDatabase
    {
        public LabItemDatabase() { }

        public static async Task<IList<Laboratory_item>> GetAllAsync()
        {
            var db = new SoftwareStudioContext();
            string queryString = $"SELECT * FROM laboratory_items";
            List<Laboratory_item> items = await db.laboratory_items.FromSqlRaw(queryString).ToListAsync();

            return items;
        }

        public static async Task<IList<int>> GetAllQuantityAsync()
        {
            IList<int> allItems = new List<int>();
            var db = new SoftwareStudioContext();
            string queryString = $"SELECT * FROM laboratory_items";
            List<Laboratory_item> items = await db.laboratory_items.FromSqlRaw(queryString).ToListAsync();

            int labIDCounter = 1;
            int itemCounter = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].laboratory_id == labIDCounter)
                    itemCounter++;
                if (items[i].laboratory_id > labIDCounter)
                {
                    labIDCounter++;
                    allItems.Add(itemCounter);
                    itemCounter = 1;
                }
            }

            allItems.Add(itemCounter);

            return allItems;
        }

        public static async Task<List<Laboratory_item>> GetAllByLabIDAsync(int labID)
        {
            var db = new SoftwareStudioContext();
            string queryString = $"SELECT * FROM laboratory_items WHERE laboratory_id = {labID}";
            List<Laboratory_item> items = await db.laboratory_items.FromSqlRaw(queryString).ToListAsync();

            return items;
        }

        public static void AddItemToLab(int labID, int itemID)
        {
            var db = new SoftwareStudioContext();
            Laboratory_item item = new Laboratory_item(labID, itemID);

            if (item != null) 
            {
                db.Add(item);
                db.SaveChanges();
            }
        }

        public static void RemoveItemFromLab(Laboratory_item item)
        {
            var db = new SoftwareStudioContext();

            if (item != null)
            {
                db.Remove(item);
                db.SaveChanges();
            }
        }
    }
}