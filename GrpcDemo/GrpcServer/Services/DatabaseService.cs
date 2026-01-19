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
            var dbPath = Path.Combine(AppContext.BaseDirectory, "Database", "GrpcDemoDataBase.db");

            string connectionString = $"Data Source={dbPath}";

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

        public List<BookModel> GetAllBooks()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Can't get Data! Connection is not Open");
                return null;
            }            

            string selectSql = "SELECT bi.ID, ba.AuthorName, bi.BookTitle, bi.BookCategory, bi.BookPrice, bi.BooksInStock, bi.BookStatus FROM " +
                "BookInventory AS bi LEFT JOIN BookAuthors AS ba ON bi.BookAuthor = ba.ID;";

            using var cmd = new SqliteCommand(selectSql, connection);
            using var reader = cmd.ExecuteReader();
            List <BookModel> books = new List<BookModel>();

            while (reader.Read())
            {
                List<string> bookContent = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader[i] == null)
                    {
                        bookContent.Add("0");
                        Console.WriteLine("Error!, no Item found;");
                        continue;
                    }

                    bookContent.Add(reader[i].ToString());
                }

                books.Add(DatabaseTranslater.TranslateToBookModel(bookContent));
            }

            return books;
        }

        public BookModel GetBookByID(int _bookID)
        {
            string selectSql = "SELECT bi.ID, ba.AuthorName, bi.BookTitle, bi.BookCategory, bi.BookPrice, bi.BooksInStock, bi.BookStatus FROM " +
                "BookInventory AS bi LEFT JOIN BookAuthors AS ba ON bi.BookAuthor = ba.ID WHERE bi.ID = @bookId;";

            using var cmd = new SqliteCommand(selectSql, connection);
            cmd.Parameters.AddWithValue("@bookId", _bookID);
            using var reader = cmd.ExecuteReader();
            List<BookModel> books = new List<BookModel>();

            while (reader.Read())
            {
                List<string> bookContent = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader[i] == null)
                    {
                        bookContent.Add("0");
                        Console.WriteLine("Error!, no Item found;");
                        continue;
                    }

                    bookContent.Add(reader[i].ToString());
                }

                books.Add(DatabaseTranslater.TranslateToBookModel(bookContent));
            }

            return books[0];
        }

        public void UpdateBook(int _booksInStock, int _bookStatus, int _bookID)
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Can't get Data! Connection is not Open");
                return;
            }

            string updateSql = "UPDATE BookInventory SET BooksInStock = @booksInStock, BookStatus = @bookStatus WHERE Id = @id";

            using var cmd = new SqliteCommand(updateSql, connection);

            cmd.Parameters.AddWithValue("@booksInStock", _booksInStock);
            cmd.Parameters.AddWithValue("@bookStatus", _bookStatus);
            cmd.Parameters.AddWithValue("@id", _bookID);

            int rowsAffected = cmd.ExecuteNonQuery();

            Console.WriteLine($"Updated {rowsAffected} row(s).");
        }
    }
}