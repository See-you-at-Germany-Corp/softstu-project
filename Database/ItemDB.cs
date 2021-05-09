using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class ItemDB
    {
        public ItemDB() { }

        public static async Task<List<Item>> GetAllAsync()
        {
            var db = new SoftwareStudioContext();
            string queryString = $"SELECT * FROM items;";
            List<Item> items = await db.items.FromSqlRaw(queryString).ToListAsync();

            return items;
        }

        public static async Task<List<Item>> GetByIDAsync(int itemID)
        {
            var db = new SoftwareStudioContext();
            string queryString = $"SELECT * FROM items WHERE uuid = {itemID}";
            List<Item> items = await db.items.FromSqlRaw(queryString).ToListAsync();

            return items;
        }

        public static async Task<List<ItemDetail>> GetAllDetailAsync()
        {
            var db = new SoftwareStudioContext();
            List<string> reqList = new List<string>{
                "items.uuid",
                "items.name",
                "items.type",
                "laboratory_items.laboratory_id",
            };
            string reqStr = db.ListToString(reqList);
            string queryString = $"SELECT {reqStr} FROM items LEFT JOIN laboratory_items ON laboratory_items.item_id = items.uuid";
            List<ItemDetail> items = await db.itemDetails.FromSqlRaw(queryString).ToListAsync();

            return items;
        }

        public static async Task<List<ItemDetail>> GetDetailByIDAsync(int itemID)
        {
            var db = new SoftwareStudioContext();
            List<string> reqList = new List<string>{
                "items.uuid",
                "items.name",
                "items.type",
                "laboratory_items.laboratory_id",
            };
            string reqStr = db.ListToString(reqList);
            string queryString = $"SELECT {reqStr} FROM items LEFT JOIN laboratory_items ON items.uuid = {itemID} WHERE laboratory_items.item_id = items.uuid";
            List<ItemDetail> items = await db.itemDetails.FromSqlRaw(queryString).ToListAsync();

            return items;
        }

        public static async Task<List<ItemDetail>> GetAllDetailByLabIDAsync(int labID)
        {
            var db = new SoftwareStudioContext();
            List<string> reqList = new List<string>{
                "items.uuid",
                "items.name",
                "items.type",
                "laboratory_items.laboratory_id",
            };
            string reqStr = db.ListToString(reqList);
            string queryString = $"SELECT {reqStr} FROM items LEFT JOIN laboratory_items ON laboratory_id = {labID} WHERE laboratory_items.item_id = items.uuid";
            List<ItemDetail> items = await db.itemDetails.FromSqlRaw(queryString).ToListAsync();

            return items;
        }

        public static async Task<List<int>> GetAllQuantityByLabIDAsync(int labID)
        {
            List<ItemDetail> itemDetails = await ItemDB.GetAllDetailByLabIDAsync(labID);
            List<int> itemSet = new List<int>();
            List<int> itemQuantity = new List<int>();

            itemDetails.ForEach(item =>
            {
                if (!itemSet.Contains(item.type))
                {
                    itemSet.Add(item.type);
                    itemQuantity.Add(0);
                }

                int index = itemSet.IndexOf(item.type);
                itemQuantity[index]++;
            });

            return itemQuantity;
        }

        public static async Task<List<int>> GetItemSetByLabIDAsync(int labID)
        {
            List<ItemDetail> itemDetails = await ItemDB.GetAllDetailByLabIDAsync(labID);
            List<int> itemSet = new List<int>();

            itemDetails.ForEach(item =>
            {
                if (!itemSet.Contains(item.type)) itemSet.Add(item.type);
            });

            return itemSet;
        }

        public static int Add(Item item)
        {
            var db = new SoftwareStudioContext();

            if (item != null)
            {
                var change = db.items.Add(item);
                db.SaveChanges();

                return change.Entity.uuid;
            }
            else return -1;
        }

        public static void Remove(Item item)
        {
            var db = new SoftwareStudioContext();

            if (item != null)
            {
                db.Remove(item);
                db.SaveChanges();
            }
        }

        public static async Task<List<List<AvailableItemsModel>>> GetAvailableItems(DateTime booking_datetime)
        {
            var db = new SoftwareStudioContext();
            
            List<AvailableItemsModel> items_am = await db.available_items.FromSqlRaw(getAvailableItemsQueryString((int)Time_id_type.am, booking_datetime)).ToListAsync();
            List<AvailableItemsModel> items_pm = await db.available_items.FromSqlRaw(getAvailableItemsQueryString((int)Time_id_type.pm, booking_datetime)).ToListAsync();

            return new List<List<AvailableItemsModel>> { items_am, items_pm };
        }

        public static string getAvailableItemsQueryString(int time_id, DateTime datetime){
            string x = $@"SELECT items.uuid, items.name, items.type, laboratory_items.laboratory_id, 
                            COALESCE(transactions.time_id, {time_id}) as time_id FROM items
                                INNER JOIN laboratory_items ON 
                                    laboratory_items.item_id = items.uuid 
                                LEFT JOIN transactions ON 
                                    transactions.item_id != items.uuid 
                                    AND transactions.book_date = '{datetime.ToString("yyyy-MM-dd")}'
                                WHERE items.uuid NOT IN (
                                    SELECT transactions.item_id FROM transactions WHERE 
                                        transactions.book_date = '{datetime.ToString("yyyy-MM-dd")}'
                                        AND transactions.time_id = {time_id})
                            ORDER BY items.uuid";
            return x;
        }
    }
}

/*
    * unittest
    * Add
    int itemID = ItemDatabase.Add(new Item("keyboard_7"));
    LabItemDatabase.AddItem(1, itemID);
    
    * Remove
    item
    ItemDatabase.Remove(item);
    LabItemDatabase.RemoveItem(item.itemID);
*/ 