using Grpc.Core;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace GrpcServer.Services
{
    public class BookStoreService : BookService.BookServiceBase
    {
        private readonly ILogger<BookStoreService> logger;
        public BookStoreService(ILogger<BookStoreService> _logger)
        {
            logger = _logger;
        }

        public override Task<BookModel> GetBookInfo(BookLookupModel request, ServerCallContext context)
        {
            BookModel output = new BookModel();

            //there i can put it in my Datbase
            if (request.BookID == 1)
            {
                output.Title = "The best way";
                output.Author = "Joy Gorden";
            }
            else if (request.BookID == 2)
            {
                output.Title = "Never ending";
                output.Author = "Doe Jorden";
            }
            else
            {
                output.Title = "The best time";
                output.Author = "Thomas Alleson";
            }

            return Task.FromResult(output);
        }

        public override async Task GetAllBooks(GetAllBooksRequest request, IServerStreamWriter<BookModel> responseStream, ServerCallContext context)
        {
            List<BookModel> books = new List<BookModel>()
            {
                new BookModel()
                {
                    BookID = 0,
                    Title = "Annes next journey",
                    Author = "Doe Jorden",
                    Price = 32.2f,
                    IsAvailable = true,
                    BookCategory = BookCategory.Classics,
                },
                new BookModel()
                {
                    BookID = 1,
                    Title = "Annes next journey",
                    Author = "Doe Jorden",
                    Price = 21.99f,
                    IsAvailable = true,
                    BookCategory = BookCategory.Classics,
                },
                new BookModel()
                {
                   BookID = 2,
                    Title = "Annes next journey",
                    Author = "Doe Jorden",
                    Price = 19.99f,
                    IsAvailable = true,
                    BookCategory = BookCategory.Classics,
                },
            };

            foreach (var book in books)
            {
                await responseStream.WriteAsync(book);
            }
        }
    }
}