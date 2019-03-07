using ExamenSergioRomeoGNB.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenSergioRomeoGNB.Repositories
{
    public class RatesRepository : GnbContext, IRepository<Rate>
    {
        private readonly Logger Log;

        public RatesRepository(DbContextOptions<GnbContext> options, LogFactory Factory) : base(options)
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
                Log.Error(DbUpdateEx, "Error at updating Rate.");
            }
            catch (DbUpdateException DbUpdateEx)
            {
                Log.Error(DbUpdateEx, "Error at updating Rate.");
            }
            catch (Exception Ex)
            {
                Log.Error(Ex, "Error at updating Rate.");
            }

            return 0;
        }

        public async Task CommitAsync()
        {
            await this.SaveChangesAsync();
        }

        public int Create(Rate Entity)
        {
            var Result = this.Rates.Add(Entity);

            return Result.Entity.Id;
        }

        public int CreateMultiple(IEnumerable<Rate> Entities)
        {
            if (Entities == null)
            {
                Entities = new List<Rate>();
            }

            Entities.ToList().ForEach(r => this.Rates.Add(r));

            var insertedRecords = this.Commit();
            return insertedRecords;
        }

        public bool DeleteAll()
        {
            this.Rates.Clear();

            return true;
        }

        public bool Delete(Rate Entity)
        {
            this.Rates.Remove(Entity);

            return true;
        }

        // HACK: Utilizando IQueryable
        public IQueryable<Rate> GetAll()
        {
            return this.Rates.AsQueryable();
        }

        public IQueryable<Rate> GetAllByField(string field, string value)
        {
            IQueryable<Rate> all = this.GetAll();
            return all.Where(x => x.GetType().GetProperty(field).GetValue(x).Equals(value));
        }

        public Rate GetSingle(int ID)
        {
            return this.Rates.Find(ID);
        }

        public Task<Rate> GetSingleAsync(int ID)
        {
            return this.Rates.FindAsync(ID);
        }

        public bool Update(Rate Entity)
        {
            this.Rates.Update(Entity);

            return true;
        }
    }
}