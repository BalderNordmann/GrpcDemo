using Grpc.Net.Client;
using GrpcServer;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GrpcClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Task.Run(AsyncHelloTask);
            //Task.Run(AsyncDataTask);
            Task.Run(AsyncMultiDataTask);
        }

        private async Task AsyncHelloTask()
        {
            var input = new HelloRequest { Name = "Max" };

            var channel = GrpcChannel.ForAddress("https://localhost:7072");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(input);

            Dispatcher.Invoke(() => GetMessage(reply.Message));
        }

        private async Task AsyncDataTask()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7072");
            var client = new BookService.BookServiceClient(channel);

            var clientRequesed = new BookLookupModel { BookID = 3 };

            var reply = await client.GetBookInfoAsync(clientRequesed);

            Dispatcher.Invoke(() => GetMessage($"Firstname: {reply.Title}, Lastname: {reply.Author}"));
        }

        private async Task AsyncMultiDataTask()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7072");
            var client = new BookService.BookServiceClient(channel);

            string outputString = string.Empty;
            using (var call = client.GetAllBooks(new GetAllBooksRequest()))
            {
                while (await call.ResponseStream.MoveNext(new CancellationToken()))
                {
                    var currentBook = call.ResponseStream.Current;
                    outputString += $"BookID: {currentBook.BookID}, Title: {currentBook.Title}, Author: {currentBook.Author}, price: {currentBook.Price}, isAvailable {currentBook.IsAvailable}, bookCategory {currentBook.BookCategory}\n";
                }
            }

            Dispatcher.Invoke(() => GetMessage(outputString));
        }

        public void GetMessage(string _message)
        {
            L_msg.Content = _message;
        }
    }
}