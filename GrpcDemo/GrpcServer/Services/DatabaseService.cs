using Microsoft.Data.Sqlite;

namespace GrpcServer.Services
{
    public sealed class DatabaseService
    {
        private static readonly Lazy<DatabaseService> _instance =
            new Lazy<DatabaseService>(() => new DatabaseService());

        public static DatabaseService Instance => _instance.Value;

        private bool disposed = false;

        //private

        private DatabaseService()
        {
            string connectionString = "Data Source=./Database/GrpcDemoDataBase.db";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~DatabaseService()
        {
            Dispose();
        }
    }
}