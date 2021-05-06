using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using softstu_project.Models;

namespace ConsoleApp.PostgreSQL
{
    public class TransactionDB
    {
        public TransactionDB() { }

        public static async Task<List<Transaction>> GetAsync(int transactionID)
        {
            var db = new SoftwareStudioContext();

            string queryString = $"SELECT * FROM transactions WHERE uuid = {transactionID}";
            List<Transaction> transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetAllAsync()
        {
            var db = new SoftwareStudioContext();

            string queryString = $"SELECT * FROM transactions";
            List<Transaction> transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetByDateAsync(DateTime date)
        {
            var db = new SoftwareStudioContext();

            string queryString = $"SELECT * FROM transactions WHERE book_date = '{date}'";
            List<Transaction> transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetByLabIDAsync(int labID)
        {
            var db = new SoftwareStudioContext();

            List<string> reqList = new List<string>{
                    "transactions.uuid",
                    "transactions.item_id",
                    "transactions.user_id",
                    "time_id",
                    "book_date",
                    "transaction_type",
                    "transactions.created",
                };
            var reqString = db.ListToString(reqList);
            string queryString = $"SELECT {reqString} FROM transactions LEFT JOIN laboratory_items ON laboratory_id = {labID} WHERE transactions.item_id = laboratory_items.item_id; ";
            List<Transaction> transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static async Task<List<Transaction>> GetByLabIDAndDateAsync(int labID, DateTime date)
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

        public static async Task<List<Transaction>> GetByUserIDAsync(int userID)
        {
            var db = new SoftwareStudioContext();

            string queryString = $"SELECT * FROM transactions WHERE user_id = {userID}";
            List<Transaction> transactions = await db.transactions.FromSqlRaw(queryString).ToListAsync();

            return transactions;
        }

        public static void Add(Transaction transaction)
        {
            var db = new SoftwareStudioContext();

            DateTime datetime_now = DateTime.Now;
            int hour;
            if (transaction.time_id == (int)Time_id_type.pm)
            {
                hour = 12;
            }
            else
            {
                hour = 8;
            }
            DateTime book_date = new DateTime(transaction.book_date.Year,
                                              transaction.book_date.Month,
                                              transaction.book_date.Day,
                                              hour, 0, 0);
            int result = DateTime.Compare(datetime_now, book_date);
            if (result <= 0)
            {
                db.transactions.Add(transaction);
                db.logs.Add(new Log(transaction));
                db.SaveChanges();
            }
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

        public static void Cancel(Transaction transaction)
        {
            var db = new SoftwareStudioContext();

            if (transaction != null)
            {
                db.transactions.Remove(transaction);
                transaction.transaction_type = (int)Transaction_type.cancel;
                db.logs.Add(new Log(transaction));
                db.SaveChanges();
            }
        }
    }
}

/*
    * unittest
    new Transaction(11, 1, 0, 1, DateTime.Now)

    TransactionDB.Add(new Transaction(6, 1, 0, 1, DateTime.Now));
    TransactionDB.Add(new Transaction(6, 4, 0, 1, DateTime.Now.AddDays(1)));
    TransactionDB.Add(new Transaction(6, 5, 0, 1, DateTime.Now.AddDays(5)));
    TransactionDB.Add(new Transaction(6, 13, 0, 1, DateTime.Now.AddDays(2)));
    TransactionDB.Add(new Transaction(6, 19, 0, 1, DateTime.Now.AddDays(8)));
    TransactionDB.Add(new Transaction(6, 25, 0, 1, DateTime.Now.AddDays(4)));
    TransactionDB.Add(new Transaction(6, 1, 0, 1, DateTime.Now.AddDays(1)));
*/