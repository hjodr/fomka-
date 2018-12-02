using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace fomka_web.DAL
{
    public class MigrationManager : IDisposable
    {
        private List<string> _appliedMigrations = new List<string>();
        public MigrationManager(DbContext dbContext, string relativePath)
        {
            _dbContext = dbContext;
            _relativePath = relativePath;
            _registeredMigrationsFile = $"{relativePath}\\migrationHistory.json";
            if (File.Exists(_registeredMigrationsFile))
                _appliedMigrations = JsonConvert.DeserializeObject<IEnumerable<string>>(File.ReadAllText(_registeredMigrationsFile)).ToList();
        }

        public void RunMigration(string migrationName)
        {
            try
            {
                string filePath = $"{_relativePath}\\{migrationName}.sql";
                if (!File.Exists(filePath))
                    throw new Exception("Migration not found");

                if (_appliedMigrations.Any(m => m == migrationName))
                    return;

                _dbContext.Database.ExecuteSqlCommand(File.ReadAllText(filePath));
                _appliedMigrations.Add(migrationName);
            }
            catch(Exception ex)
            { }
        }

        public void SaveHistory()
        {
            File.WriteAllText(_registeredMigrationsFile, JsonConvert.SerializeObject(_appliedMigrations));
        }

        private readonly DbContext _dbContext;
        private readonly string _relativePath;
        private readonly string _registeredMigrationsFile = String.Empty;


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    SaveHistory();
                    _dbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}