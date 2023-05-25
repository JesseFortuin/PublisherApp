namespace Publisher.Shared.Dtos
{
    public class AuthorWithBooksDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<BookDto> Books { get; set; } = new List<BookDto>();
    }
}
