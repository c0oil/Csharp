using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Test.DbConnection.Smo
{
    public interface ISmoTasks
    {
        IEnumerable<string> SqlServers { get; }
        List<string> GetDatabases(SqlConnectionStringBuilder connectionString);
        List<DatabaseTable> GetTables(SqlConnectionStringBuilder connectionString);
    }

    public class AsyncSmoTasks : SmoTasks
    {
        public Task<IEnumerable<string>> SqlServersAsync
        {
            get { return Task<IEnumerable<string>>.Factory.StartNew(() => SqlServers); }
        }

        public Task<List<string>> GetDatabasesAsync(SqlConnectionStringBuilder connectionString)
        {
            return Task<List<string>>.Factory.StartNew(() => GetDatabases(connectionString));
        }

        public Task<List<DatabaseTable>> GetTablesAsync(SqlConnectionStringBuilder connectionString)
        {
            return Task<List<DatabaseTable>>.Factory.StartNew(() => GetTables(connectionString));
        }
    }

    public class SmoTasks : ISmoTasks
    {
        public IEnumerable<string> SqlServers
        {
            get
            {
                return SmoApplication
                    .EnumAvailableSqlServers()
                    .AsEnumerable()
                    .Select(r => r["Name"].ToString());
            }
        }

        public List<string> GetDatabases(SqlConnectionStringBuilder connectionString)
        {
            var databases = new List<string>();

            using (var conn = new SqlConnection(connectionString.ConnectionString))
            {
                try
                {
                    conn.Open();
                    var serverConnection = new ServerConnection(conn);
                    var server = new Server(serverConnection);
                    databases.AddRange(from Database database in server.Databases select database.Name);
                }
                catch (SqlException)
                {
                    return new List<string>();
                }
            }

            return databases;
        }

        public List<DatabaseTable> GetTables(SqlConnectionStringBuilder connectionString)
        {
            using (var conn = new SqlConnection(connectionString.ConnectionString))
            {
                try
                {
                    conn.Open();
                    var serverConnection = new ServerConnection(conn);
                    var server = new Server(serverConnection);
                    return server.Databases[connectionString.InitialCatalog].Tables.Cast<Microsoft.SqlServer.Management.Smo.Table>().Select(t => new DatabaseTable
                        {
                            Name = t.Name,
                            RowCount = t.RowCount
                        }).ToList();
                }
                catch (SqlException)
                {
                    return new List<DatabaseTable>();
                }
            }
        }
    }
}
