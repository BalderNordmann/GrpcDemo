using Microsoft.Data.Sqlite;

namespace GrpcServer.Services
{
    public sealed class DatabaseService
    {
        private static readonly Lazy<DatabaseService> _instance =
            new Lazy<DatabaseService>(() => new DatabaseService());

        public static DatabaseService Instance => _instance.Value;

        private SqliteConnection connection;

        private DatabaseService()
        {
            string connectionString = "Data Source=./Database/GrpcDemoDataBase.db";

            connection = new SqliteConnection(connectionString);
            connection.StateChange += connection_StateChange;
            connection.Open();
        }

        private void connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            Console.WriteLine($"Database State: {connection.State}");
        }

        public void Dispose()
        {
            connection.Dispose();
            GC.SuppressFinalize(this);
        }

        ~DatabaseService()
        {
            Dispose();
        }

        public void GetAllBooks()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Can't get Data! Connection is not Open");
            }

            string selectSql = "SELECT bi.ID, ba.AuthorName, bi.BookTitle, bc.Category, bi.BookPrice, bi.BooksInStock, bs.Status FROM " +
                "BookInventory AS bi LEFT JOIN BookAuthors AS ba ON bi.BookAuthor = ba.ID LEFT JOIN BookCategory AS bc ON bi.BookCategory = bc.ID " +
                "LEFT JOIN BookStatus AS bs ON bi.BookStatus = bs.ID;";

            using var cmd = new SqliteCommand(selectSql, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string currentOutput = string.Empty;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    currentOutput += $"{reader[i].ToString()}, ";
                }

                Console.WriteLine(currentOutput);
            }
        }

        public void UpdateBook()
        {
            string updateSql = "UPDATE Users SET Name = @name, Age = @age WHERE Id = @id";

            using var cmd = new SqliteCommand(updateSql, connection);

            cmd.Parameters.AddWithValue("@name", "Bob");
            cmd.Parameters.AddWithValue("@age", 35);
            cmd.Parameters.AddWithValue("@id", 1); // ID of the record to update

            int rowsAffected = cmd.ExecuteNonQuery();

            Console.WriteLine($"Updated {rowsAffected} row(s).");
        }
    }
}