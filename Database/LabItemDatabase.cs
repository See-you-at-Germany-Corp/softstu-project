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

        public static async Task<IList<int>> GetAllItem()
        {
            IList<int> allItems = new List<int>();
            using (var db = new SoftwareStudioContext())
            {
                string queryString = $"SELECT * FROM laboratory_items";
                var items = await db.laboratory_items.FromSqlRaw(queryString).ToListAsync();

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
        }

        public static async Task<List<Laboratory_item>> GetAllItemByLabID(int labID)
        {
            using (var db = new SoftwareStudioContext())
            {
                string queryString = $"SELECT * FROM laboratory_items WHERE laboratory_id = {labID}";
                var items = await db.laboratory_items.FromSqlRaw(queryString).ToListAsync();

                return items;
            }
        }
 
        public static async Task<IList<int>> GetCurrentItem()
        {
            IList<int> currentItems = new List<int>();
            using (var db = new SoftwareStudioContext())
            {
                string queryString = $"SELECT laboratory_id, items.uuid, item_id, items.updated, items.created FROM laboratory_items LEFT JOIN items ON laboratory_items.item_id = items.uuid WHERE items.is_available = true";
                var items = await db.laboratory_items.FromSqlRaw(queryString).ToListAsync();

                int labIDCounter = 1;
                int itemCounter = 0;
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].laboratory_id == labIDCounter)
                        itemCounter++;
                    if (items[i].laboratory_id > labIDCounter)
                    {
                        labIDCounter++;
                        currentItems.Add(itemCounter);
                        itemCounter = 1;
                    }
                }

                currentItems.Add(itemCounter);

                return currentItems;
            }
        }

        public static async Task<List<Laboratory_item>> GetCurrentItemByLabID(int labID)
        {
            using (var db = new SoftwareStudioContext())
            {
                string queryString = $"SELECT laboratory_id, items.uuid, item_id, items.updated, items.created FROM laboratory_items LEFT JOIN items ON laboratory_items.item_id = items.uuid WHERE laboratory_items.laboratory_id = {labID} AND items.is_available = true";
                var items = await db.laboratory_items.FromSqlRaw(queryString).ToListAsync();

                return items;
            }
        }
    }
}