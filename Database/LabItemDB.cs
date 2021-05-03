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
                    case (int)Time_id_type.am:
                        allItemsAM--;
                        break;
                    case (int)Time_id_type.pm:
                        allItemsPM++;
                        break;
                    case (int)Time_id_type.day:
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
                        case (int)Time_id_type.am:
                            allItemsAM[i]--;
                            break;
                        case (int)Time_id_type.pm:
                            allItemsPM[i]++;
                            break;
                        case (int)Time_id_type.day:
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
                        case (int)Time_id_type.am:
                            allItemsAM[i]--;
                            break;
                        case (int)Time_id_type.pm:
                            allItemsPM[i]++;
                            break;
                        case (int)Time_id_type.day:
                            allItemsAM[i]--;
                            allItemsPM[i]--;
                            break;
                    }
                }
            }

            return new List<List<int>> { allItemsAM, allItemsPM };
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