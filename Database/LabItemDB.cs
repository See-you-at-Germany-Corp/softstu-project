using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class LabItemDB
    {
        public LabItemDB() { }

        public static async Task<IList<Laboratory_item>> GetAllAsync()
        {
            var db = new SoftwareStudioContext();
            string queryString = $"SELECT * FROM laboratory_items";
            List<Laboratory_item> items = await db.laboratory_items.FromSqlRaw(queryString).ToListAsync();

            return items;
        }

        public static async Task<List<int>> GetAllQuantityAsync()
        {
            List<int> allItems = new List<int>();
            var db = new SoftwareStudioContext();
            string queryString = $"SELECT * FROM laboratory_items";
            List<Laboratory_item> items = await db.laboratory_items.FromSqlRaw(queryString).ToListAsync();
            for (int i = 0; i < items.Count; i++)
            {
                int labid = items[i].laboratory_id;
                while(allItems.Count < labid) {
                    allItems.Add(0);
                }
                allItems[labid-1] = allItems[labid-1] + 1;
            }
            return allItems;
        }

        public static async Task<List<int>> GetCurrentQuantityByLabIDAsync(int labID)
        {
            var db = new SoftwareStudioContext();

            int allQuantity = (await GetAllByLabIDAsync(labID)).Count;
            int allItemsAM = allQuantity;
            int allItemsPM = allQuantity;

            List<Transaction> transactions = await TransactionDB.GetByLabIDAndDateAsync(labID, DateTime.Now);

            for (int j = 0; j < transactions.Count; j++)
            {
                switch (transactions[j].time_id)
                {
                    case (int)Time_id_type.none:
                        allItemsAM++;
                        allItemsPM++;
                        break;
                    case (int)Time_id_type.AM:
                        allItemsAM--;
                        break;
                    case (int)Time_id_type.PM:
                        allItemsPM++;
                        break;
                    case (int)Time_id_type.Day:
                        allItemsAM--;
                        allItemsPM--;
                        break;
                }
            }

            return new List<int> { allItemsAM, allItemsPM };
        }

        public static async Task<List<Laboratory_item>> GetAllByLabIDAsync(int labID)
        {
            var db = new SoftwareStudioContext();
            string queryString = $"SELECT * FROM laboratory_items WHERE laboratory_id = {labID}";
            List<Laboratory_item> items = await db.laboratory_items.FromSqlRaw(queryString).ToListAsync();

            return items;
        }

        public static async Task<List<List<int>>> GetCurrentQuantityByDateAsync()
        {
            var db = new SoftwareStudioContext();

            List<int> allQuantity = await GetAllQuantityAsync();
            List<int> allItemsAM = allQuantity;
            List<int> allItemsPM = allQuantity;

            for (int i = 0; i < 5; i++)
            {
                List<Transaction> transactions = await TransactionDB.GetByLabIDAndDateAsync(i + 1, DateTime.Now);

                for (int j = 0; j < transactions.Count; j++)
                {
                    switch (transactions[j].time_id)
                    {
                        case (int)Time_id_type.none:
                            allItemsAM[i]++;
                            allItemsPM[i]++;
                            break;
                        case (int)Time_id_type.AM:
                            allItemsAM[i]--;
                            break;
                        case (int)Time_id_type.PM:
                            allItemsPM[i]++;
                            break;
                        case (int)Time_id_type.Day:
                            allItemsAM[i]--;
                            allItemsPM[i]--;
                            break;
                    }
                }
            }

            return new List<List<int>> { allItemsAM, allItemsPM };
        }

        public static async Task<List<List<int>>> GetCurrentQuantityByDateAsync(DateTime date)
        {
            var db = new SoftwareStudioContext();

            List<int> allItemsAM = await GetAllQuantityAsync();
            List<int> allItemsPM = await GetAllQuantityAsync();

            for (int i = 0; i < 5; i++)
            {
                List<Transaction> transactions = await TransactionDB.GetByLabIDAndDateAsync(i + 1, date);

                for (int j = 0; j < transactions.Count; j++)
                {
                    switch (transactions[j].time_id)
                    {
                        case (int)Time_id_type.none:
                            allItemsAM[i]++;
                            allItemsPM[i]++;
                            break;
                        case (int)Time_id_type.AM:
                            allItemsAM[i]--;
                            break;
                        case (int)Time_id_type.PM:
                            allItemsPM[i]++;
                            break;
                        case (int)Time_id_type.Day:
                            allItemsAM[i]--;
                            allItemsPM[i]--;
                            break;
                    }
                }
            }

            return new List<List<int>> { allItemsAM, allItemsPM };
        }

        public static async Task<int> GetLabItemCountByLabIDAndType(int labID, int itemType)
        {
            var db = new SoftwareStudioContext();

            string queryString = $@"
                SELECT laboratory_items.uuid
                FROM laboratory_items
                LEFT JOIN items ON laboratory_items.item_id = items.uuid
                WHERE laboratory_items.laboratory_id = {labID} AND items.type = {itemType}
            ";

            int labItemCount = await db.laboratory_items.FromSqlRaw(queryString).CountAsync();

            return labItemCount;
        }

        public static void AddItem(int labID, int itemID)
        {
            var db = new SoftwareStudioContext();
            Laboratory_item labItem = new Laboratory_item(labID, itemID);

            if (labItem != null)
            {
                db.laboratory_items.Add(labItem);
                db.SaveChanges();
            }
        }

        public static async void RemoveItem(int itemID)
        {
            var db = new SoftwareStudioContext();
            Laboratory_item labItem = await db.laboratory_items.FirstOrDefaultAsync(labItem => labItem.item_id == itemID);

            if (labItem != null)
            {
                db.Remove(labItem);
                db.SaveChanges();
            }
        }
    }
}