using Google.Protobuf.WellKnownTypes;
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
            BookModel output = DatabaseService.Instance.GetBookByID(request.BookID);
            return Task.FromResult(output);
        }

        public override async Task GetAllBooks(GetAllBooksRequest request, IServerStreamWriter<BookModel> responseStream, ServerCallContext context)
        {
            foreach (var book in DatabaseService.Instance.GetAllBooks())
            {
                Console.WriteLine($"Sending BookInfo: {book.BookID}, {book.Author}, {book.Title}, {book.Price}, {book.BookInStock}, {book.BookCategory}, {book.BookStatus};");
                await responseStream.WriteAsync(book);
            }
        }

        public override Task<Empty> UpdateBookInfo(BookLookupModelUpdate request, ServerCallContext context)
        {
            DatabaseService.Instance.UpdateBook(request.BookInStock, request.BookStatus, request.BookID);
            return Task.FromResult(new Empty());
        }
    }
}