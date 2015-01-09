using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Data.SQLite.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Data.SQLite;

namespace CRS.Sync.Watcher.DLL
{
    public class SqliteDataContext : DataContext
    {
        public SqliteDataContext(string connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
        }
        public SqliteDataContext(IDbConnection connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
        }
        public SqliteDataContext(string connectionString) :
            base(new SQLiteConnection(connectionString))
        {
        }
        public SqliteDataContext(IDbConnection connection) :
            base(connection)
        {
        }
    }
}
