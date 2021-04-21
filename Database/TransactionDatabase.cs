using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class TransactionDatabase
    {
        public TransactionDatabase() { }

        public static void AddTransaction(Transaction transaction)
        {
            var db = new SoftwareStudioContext();

            db.transactions.Add(transaction);
            db.SaveChanges();
        }

        public static async Task<List<Transaction>> GetAllTransaction()
        {
            var db = new SoftwareStudioContext();

            string queryString = $"SELECT * FROM transactions";
            var transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetTransactionByDate(DateTime date)
        {
            var db = new SoftwareStudioContext();

            string queryString = $"SELECT * FROM transactions WHERE book_date = '{date}'";
            var transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetTransactionByLabID(int labID)
        {
            var db = new SoftwareStudioContext();

            List<string> reqList = new List<string>{
                    "transactions.uuid",
                    "book_date",
                    "transactions.created",
                    "transactions.item_id",
                    "time_id",
                    "transaction_type",
                    "transactions.user_id"
                };
            var reqString = db.ListToString(reqList);
            string queryString = $"SELECT {reqString} FROM transactions LEFT JOIN laboratory_items ON laboratory_id = {labID} WHERE transactions.item_id = laboratory_items.item_id; ";
            var transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetTransactionByLabIDAndDate(int labID, DateTime date)
        {
            var db = new SoftwareStudioContext();

            List<string> reqList = new List<string>{
                    "transactions.uuid",
                    "book_date",
                    "transactions.created",
                    "transactions.item_id",
                    "time_id",
                    "transaction_type",
                    "transactions.user_id"
                };
            var reqString = db.ListToString(reqList);
            string queryString = $"SELECT {reqString} FROM transactions LEFT JOIN laboratory_items ON laboratory_id = {labID} WHERE transactions.item_id = laboratory_items.item_id AND book_date = '{date}'; ";
            var transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetTransactionByUserID(int userID)
        {
            var db = new SoftwareStudioContext();

            string queryString = $"SELECT * FROM transactions WHERE user_id = {userID}";
            var transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }
    }
}