using ExamenSergioRomeoGNB.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ExamenSergioRomeoGNB.Repositories
{
    public class TransactionsRepository : GnbContext, IRepository<Transaction>
    {
        private readonly Logger Log;

        public TransactionsRepository(DbContextOptions<GnbContext> options, LogFactory Factory) : base(options)
        {
            Log = Factory.GetCurrentClassLogger();
        }

        public int Commit()
        {
            try
            {
                return SaveChanges();
            }
            catch (DbUpdateConcurrencyException DbUpdateEx)
            {
                Log.Error(DbUpdateEx, "Error at updating Transaction.");
            }
            catch (DbUpdateException DbUpdateEx)
            {
                Log.Error(DbUpdateEx, "Error at updating Transaction.");
            }
            catch (Exception Ex)
            {
                Log.Error(Ex, "Error at updating Transaction.");
            }

            return 0;
        }

        public async Task CommitAsync()
        {
            await this.SaveChangesAsync();
        }

        public int Create(Transaction Entity)
        {
            var Result = this.Transactions.Add(Entity);

            return Result.Entity.Id;
        }

        public int CreateMultiple(IEnumerable<Transaction> Entities)
        {
            if (Entities == null)
            {
                Entities = new List<Transaction>();
            }

            Entities.ToList().ForEach(r => this.Transactions.Add(r));

            var insertedRecords = this.Commit();
            return insertedRecords;
        }

        public bool DeleteAll()
        {
            this.Transactions.Clear();
            return true;
        }

        public bool Delete(Transaction Entity)
        {
            this.Transactions.Remove(Entity);

            return true;
        }

        // HACK: Utilizando IQueryable
        public IQueryable<Transaction> GetAll()
        {
            return this.Transactions.AsQueryable();
        }

        public IQueryable<Transaction> GetAllByField(string field, string value)
        {
            PropertyInfo prop = typeof(Transaction).GetProperty(field);
            IQueryable<Transaction> allByField = this.Transactions.Where(x => prop.GetValue(x).Equals(value));
            return allByField;
        }

        public Transaction GetSingle(int ID)
        {
            return this.Transactions.Find(ID);
        }

        public Task<Transaction> GetSingleAsync(int ID)
        {
            return this.Transactions.FindAsync(ID);
        }

        public bool Update(Transaction Entity)
        {
            this.Transactions.Update(Entity);

            return true;
        }
    }
}