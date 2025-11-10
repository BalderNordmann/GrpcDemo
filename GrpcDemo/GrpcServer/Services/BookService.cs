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

            //there i can put my Datbase shit
            if (request.BookID == 1)
            {
                output.Title = "Deine Mom";
                output.Author = "Dein Dad";
            }
            else if (request.BookID == 2)
            {
                output.Title = "Jane stinkt";
                output.Author = "Doe Fotze";
            }
            else
            {
                output.Title = "Greg leckt";
                output.Author = "Thomas Riecht";
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
                    Title = "Anne Schrank",
                    Author = "Deine Mam",
                    Price = 32.2f,
                    IsAvailable = true,
                    BookCategory = BookCategory.Classics,
                },
                new BookModel()
                {
                    BookID = 1,
                    Title = "Anne Schrank",
                    Author = "Deine Mam",
                    Price = 21.99f,
                    IsAvailable = true,
                    BookCategory = BookCategory.Classics,
                },
                new BookModel()
                {
                   BookID = 2,
                    Title = "Anne Schrank",
                    Author = "Deine Mam",
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