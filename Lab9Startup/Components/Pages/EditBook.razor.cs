using Lab9Startup.Models;
using Lab9Startup.Services;
using Microsoft.AspNetCore.Components;

namespace Lab9Startup.Components.Pages
{
    public partial class EditBook : ComponentBase
    {
        private Book book = new Book();
        private bool isSaved = false;

        [Parameter]
        public string BookId { get; set; }

        // used to navigate back to the Home page
        [Inject] NavigationManager NavigationManager { get; set; }

        // book database accessor
        BookDbAccessor bookDbAccessor = new BookDbAccessor();

        protected override void OnInitialized()
        {
            // Get the book from the database using the BookId
            // Uncomment once the GetBook method is implemented in the BookDbAccessor class
            book = bookDbAccessor.GetBook(BookId);
        }

        private async Task UpdateBook()
        {
            // Update the book in the database
            // Uncomment once the UpdateBook method is implemented in the BookDbAccessor class
            bookDbAccessor.UpdateBook(book);

            // Navigate back to the Home page
            isSaved = true;

            // Wait for 1 second before navigating back to the Home page
            await Task.Delay(1000);
            NavigationManager.NavigateTo("/");
        }
    }
}
