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

        public static async Task<List<Transaction>> GetAll()
        {
            var db = new SoftwareStudioContext();

            string queryString = $"SELECT * FROM transactions";
            List<Transaction> transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetByDate(DateTime date)
        {
            var db = new SoftwareStudioContext();

            string queryString = $"SELECT * FROM transactions WHERE book_date = '{date}'";
            List<Transaction> transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetByLabID(int labID)
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
            List<Transaction> transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetByLabIDAndDate(int labID, DateTime date)
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
            List<Transaction> transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetByUserID(int userID)
        {
            var db = new SoftwareStudioContext();

            string queryString = $"SELECT * FROM transactions WHERE user_id = {userID}";
            List<Transaction> transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static void AddTransaction(Transaction transaction)
        {
            var db = new SoftwareStudioContext();

            db.transactions.Add(transaction);
            db.logs.Add(new Log(transaction));
            db.SaveChanges();
        }

        public static void Delete(Transaction transaction)
        {
            var db = new SoftwareStudioContext();

            if (transaction != null)
            {
                db.transactions.Remove(transaction);
                transaction.transaction_type = (int)Transaction_type.give;
                db.logs.Add(new Log(transaction));
                db.SaveChanges();
            }
        }
    }
}

/*
    * unittest
    new Transaction(11, 1, 0, 1, DateTime.Now)

    TransactionDatabase.AddTransaction(new Transaction(6, 1, 0, 1, DateTime.Now));
    TransactionDatabase.AddTransaction(new Transaction(6, 4, 0, 1, DateTime.Now.AddDays(1)));
    TransactionDatabase.AddTransaction(new Transaction(6, 5, 0, 1, DateTime.Now.AddDays(5)));
    TransactionDatabase.AddTransaction(new Transaction(6, 13, 0, 1, DateTime.Now.AddDays(2)));
    TransactionDatabase.AddTransaction(new Transaction(6, 19, 0, 1, DateTime.Now.AddDays(8)));
    TransactionDatabase.AddTransaction(new Transaction(6, 25, 0, 1, DateTime.Now.AddDays(4)));
    TransactionDatabase.AddTransaction(new Transaction(6, 1, 0, 1, DateTime.Now.AddDays(1)));
*/