using Lab9Startup.Models;
using Lab9Startup.Services;
using Microsoft.AspNetCore.Components;

namespace Lab9Startup.Components.Pages
{
    public partial class AddBook : ComponentBase
    {
        private Book book;
        private bool isSaved = false;

        // book database accessor
        private BookDbAccessor bookDbAccessor = new BookDbAccessor();

        // used to navigate back to the Home page
        [Inject] NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string BookId { get; set; }

        protected override void OnInitialized()
        {
            book = new Book
            {
                BookId = Guid.NewGuid().ToString(),
            };
        }
        private async Task SaveBook()
        {
            // Add the book to the list of books
            // Uncomment once the AddBook method is implemented in the BookDbAccessor class
            bookDbAccessor.AddBook(book);

            // Navigate back to the Home page
            isSaved = true;
            await Task.Delay(1000);
            NavigationManager.NavigateTo("/");
        }
    }
}
