using NSubstitute;
using Publisher.Application;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;

namespace Publisher.Test
{
    public class BookFacadeTests
    {
        [Fact]
        public void AddManyBooks_Fails_InvalidListOfBookDtos()
        {
            //arrange
            var BookDtos = new List<AddBookDto> 
            {
                new AddBookDto {AuthorId = 20, Title = "Test"},
                new AddBookDto {AuthorId = 25, Title = "Test Two"}
            };

            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IBookFacade bookFacade = new BookFacade(null, authorRepository);

            //act
            var result = Assert.Throws<Exception>(() => bookFacade.AddManyBooks(BookDtos.ToArray()));

            //assert
            Assert.Equal("Author Id does not match author in database", result.Message);
        }
        //[Fact]
        //public void AddManyBooks_Succeeds_ValidDtos()
        //{
        //    //arrange
        //    var BookDtos = new List<AddBookDto>
        //    {
        //        new AddBookDto {AuthorId = 1, Title = "Test"},
        //        new AddBookDto {AuthorId = 2, Title = "Test Two"}
        //    };

        //    IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

        //    IBookRepository bookRepository = Substitute.For<IBookRepository>();

        //    IBookFacade bookFacade = new BookFacade(bookRepository, authorRepository);

        //    var expected = true;
        //    //act
        //    var result = bookFacade.AddManyBooks(BookDtos.ToArray());

        //    //assert
        //    Assert.Equal(expected, result);
        //}
    }
}
