using Lab9Startup.Models;
using Lab9Startup.Services;
using Microsoft.AspNetCore.Components;

namespace Lab9Startup.Components.Pages
{
    public partial class Home : ComponentBase
    {
        // used to navigate back to the Home page
        [Inject] NavigationManager NavigationManager { get; set; }

        // book database accessor
        private BookDbAccessor bookDbAccessor = new BookDbAccessor();

        public List<Book> books = new List<Book>(); 

        protected override void OnInitialized()
        {
            // Initialize the database and create the books table
            bookDbAccessor.InitializeDatabase();

            // Get the list of books from the database
            // Uncomment once the GetBooks method is implemented in the BookDbAccessor class
            books = bookDbAccessor.GetBooks();
        }

        private void EditBook(Book book)
        {
            // Navigate to the EditBook page
            NavigationManager.NavigateTo($"/editbook/{book.BookId}");
        }

        private void ViewBookDetails(Book book)
        {
            // Navigate to the BookDetails page
            NavigationManager.NavigateTo($"/bookdetails/{book.BookId}");
        }

        private void DeleteBook(Book book)
        {
            books.Remove(book);
            bookDbAccessor.DeleteBook(book.BookId);
        }
    }
}
