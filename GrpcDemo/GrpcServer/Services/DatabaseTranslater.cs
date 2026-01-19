namespace GrpcServer.Services
{
    public static class DatabaseTranslater
    {
        public static BookModel TranslateToBookModel(List<string> _convertContent)
        {
            return new BookModel {
                BookID = int.Parse(_convertContent[0]),
                Title = _convertContent[1],
                Author = _convertContent[2],
                Price = float.Parse(_convertContent[4].Replace('.', ',')), //Replace '.' with this ',' cause convert to float can be done
                BookStatus = (BookStatus)(int.Parse(_convertContent[6]) - 1), //-1 cause ID Starts with 1 and the Enum with 0
                BookCategory = (BookCategory)(int.Parse(_convertContent[3]) - 1), //-1 cause ID Starts with 1 and the Enum with 0
                BookInStock = int.Parse(_convertContent[5]),
            };
        }
    }
}
