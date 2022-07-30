﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Enforsement.Core.Model;

namespace Enforsement.Core.Servise
{

    public class DataBaseServise
    {
        readonly SQLiteAsyncConnection dataBase;

        public DataBaseServise(string _connectionString)
        {
            dataBase = new SQLiteAsyncConnection(Path.Combine(_connectionString));
            dataBase.CreateTableAsync<EnforsementClass>().Wait();
        }

        public Task<int> SaveAsync(EnforsementClass _enforsement)
        {
            try
            {
                return dataBase.InsertAsync(_enforsement);
            }
            catch
            {
                return null;
            }
        }

        public Task<int> UpdateAsync(EnforsementClass _enforsement)
        {
            try
            {
                return dataBase.UpdateAsync(_enforsement);
            }
            catch
            {
                return null;
            }
        }

        public Task<int> DeleteAsync(EnforsementClass _enforsement)
        {
            try
            {
                return dataBase.DeleteAsync(_enforsement);
            }
            catch
            {
                return null;
            }
        }

        public Task<List<EnforsementClass>> GetAllAsync()
        {
            return dataBase.Table<EnforsementClass>().ToListAsync();
        }
    }
}
