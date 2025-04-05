using System.Reflection.Metadata;
using Dapper;
using Lab9Startup.Models;
using MySqlConnector;

namespace Lab9Startup.Services
{
    public class BookDbAccessor
    {
        protected MySqlConnection connection;

        public BookDbAccessor()
        {
            // get environemnt variable
            //string dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            //string dbUser = Environment.GetEnvironmentVariable("DB_USER");
            //string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            string dbHost = "localhost";
            string dbUser = "root";
            string dbPassword = "password";

            var builder = new MySqlConnectionStringBuilder
            {
                Server = dbHost,
                UserID = dbUser,
                Password = dbPassword,
                Database = "library", // Use maria db to create a database called library
            };

            connection = new MySqlConnection(builder.ConnectionString);
        }

        /// <summary>
        /// Initialize the database and create the books table
        /// </summary>
        public void InitializeDatabase()
        {
            connection.Open();

            var sql = @"CREATE TABLE IF NOT EXISTS books (
                BookId VARCHAR(36) PRIMARY KEY,
                Title VARCHAR(255) NOT NULL,
                Author VARCHAR(255) NOT NULL,
                Description TEXT,
                Category VARCHAR(255)
            )";

            connection.Execute(sql);

            connection.Close();
        }

        /// <summary>
        /// Implement the AddBook method to add a book to the database
        /// </summary>
        /// <param name="book"></param>
        public void AddBook(Book book)
        {
            connection.Open();

            string addBook = "INSERT INTO books (BookId, Title, Author, Description, Category) VALUES" +
                $"('{book.BookId}','{book.Title}','{book.Author}','{book.Description}','{book.Category}')";
            MySqlCommand cmd = new MySqlCommand(addBook, connection);
            int execAddBook = cmd.ExecuteNonQuery();

            connection.Close();
        }

        /// <summary>
        /// Implement the GetBooks method to get all books from the database
        /// </summary>
        /// <returns></returns>
        public List<Book> GetBooks()
        {
            List<Book> bookList = new List<Book>();

            connection.Open();
            string readBooks = "SELECT * FROM books";
            MySqlCommand bookReader = new MySqlCommand(readBooks, connection);

            using (MySqlDataReader reader = bookReader.ExecuteReader())
            {
                while (reader.Read())
                {
                    string bookId = reader.GetString(0);
                    string title = reader.GetString(1);
                    string author = reader.GetString(2);
                    string desc = reader.GetString(3);
                    string category = reader.GetString(4);

                    Book book = new Book();

                    book.BookId = bookId;
                    book.Title = title;
                    book.Author = author;
                    book.Description = desc;
                    book.Category = category;
                    bookList.Add(book);
                }
            }
            connection.Close();
            return bookList;
        }

        /// <summary>
        /// Implement the GetBook method to get a book from the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public Book GetBook(string bookId)
        {
            string getBook = $"SELECT * FROM books WHERE BookId='{bookId}'";

            connection.Open();

            Book book = new Book();
            MySqlCommand getBookReader = new MySqlCommand(getBook, connection);

            using (MySqlDataReader reader = getBookReader.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    string title = reader.GetString(1);
                    string author = reader.GetString(2);
                    string desc = reader.GetString(3);
                    string category = reader.GetString(4);

                    book.BookId = id;
                    book.Title = title;
                    book.Author = author;
                    book.Description = desc;
                    book.Category = category;
                    
                }
            }
            connection.Close();
            return book;
        }

        /// <summary>
        /// Implement the UpdateBook method to update a book in the database
        /// </summary>
        /// <param name="book"></param>
        public void UpdateBook(Book book)
        {
            connection.Open();
            string updateBookQuery = $"UPDATE books SET Title='{book.Title}',Author='{book.Author}',Description='{book.Description}',Category='{book.Category}' WHERE BookId='{book.BookId}'";
            MySqlCommand updateBook = new MySqlCommand(updateBookQuery, connection);
            int execute = updateBook.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Implement the DeleteBook method to delete a book from the database
        /// </summary>
        /// <param name="bookId"></param>
        public void DeleteBook(string bookId)
        {
            connection.Open();
            string deleteBookQuery = $"DELETE FROM books WHERE BookId='{bookId}'";
            MySqlCommand deleteBook = new MySqlCommand(deleteBookQuery, connection);
            int execute = deleteBook.ExecuteNonQuery();
            connection.Close();
        }
    }
}
