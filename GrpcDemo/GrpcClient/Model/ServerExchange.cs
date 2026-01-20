using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcClient.Model
{
    public sealed class ServerExchange
    {
        private static readonly Lazy<ServerExchange> _instance =
            new Lazy<ServerExchange>(() => new ServerExchange());

        public static ServerExchange Instance => _instance.Value;

        private GrpcChannel channel;
        private BookService.BookServiceClient client;

        private ServerExchange()
        {
            channel = GrpcChannel.ForAddress("https://localhost:7072");
            client = new BookService.BookServiceClient(channel);
        }

        public void Init()
        {

        }

        public void Dispose()
        {
            channel.Dispose();
            GC.SuppressFinalize(this);
        }

        ~ServerExchange()
        {
            Dispose();
        }

        public async Task<List<BookModel>> AsyncGetAllBooksRequest()
        {
            List<BookModel> bookModels = new List<BookModel>();
            using (var call = client.GetAllBooks(new GetAllBooksRequest()))
            {
                while (await call.ResponseStream.MoveNext(new CancellationToken()))
                {
                    var currentBook = call.ResponseStream.Current;
                    bookModels.Add(currentBook);
                }
            }

            return bookModels;
        }

        public async Task<BookModel> GetBookByIDRequest(int _id)
        {
            BookLookupModel bookLookupModel = new BookLookupModel() { BookID = _id};
            BookModel bookModel;

            using (var call = client.GetBookInfoAsync(bookLookupModel))
            {
                bookModel = await call.ResponseAsync;
            }

            return bookModel;
        }

        public async Task UpdateBookByID(BookLookupModelUpdate _lookupModelUpdate)
        {
            await client.UpdateBookInfoAsync(_lookupModelUpdate);
        }
    }
}