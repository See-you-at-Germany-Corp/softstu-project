using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class ItemDatabase
    {
        public ItemDatabase() { }

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
                "laboratory_items.laboratory_id",
            };
            string reqStr = db.ListToString(reqList);
            string queryString = $"SELECT {reqStr} FROM items LEFT JOIN laboratory_items ON laboratory_id = {labID} WHERE laboratory_items.item_id = items.uuid";
            List<ItemDetail> items = await db.itemDetails.FromSqlRaw(queryString).ToListAsync();

            return items;
        }
    }
}