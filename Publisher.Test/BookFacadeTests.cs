using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Publisher.Application;
using Publisher.Domain.Entities;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;
using PublisherData;

namespace Publisher.Test
{
    public class BookFacadeTests
    {
        [Fact]
        public void AddManyBooks_Fails_InvalidListOfBookDtos()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<PubContext>();

            builder.UseInMemoryDatabase("AddManyBooks_Fails_InvalidListOfBookDtos");

            using (var context = new PubContext(builder.Options))
            {
                IAuthorRepository authorRepository = new AuthorRepository(context);

                IBookFacade bookFacade = new BookFacade(null, authorRepository);

                var BookDtos = new List<AddBookDto>
                {
                    new AddBookDto {AuthorId = 20, Title = "Test"},
                    new AddBookDto {AuthorId = 25, Title = "Test Two"}
                };

                var expected = new ApiResponseDto<bool>("Author Id does not match author in database");

                //act
                var result = bookFacade.AddManyBooks(BookDtos.ToArray());

                //assert
                Assert.Equal(expected.ErrorMessage, result.ErrorMessage);
            };
        }

        [Fact]
        public void AddManyBooks_Succeeds_ValidDtos()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<PubContext>();

            builder.UseInMemoryDatabase("AddManyBooks_Succeeds_ValidDtos");

            using (var context = new PubContext(builder.Options))
            {
                var author1 = new Author { FirstName = "Test", LastName = "author" };

                var author2 = new Author { FirstName = "Also", LastName = "Test" };

                context.Authors.AddRange(author1, author2);

                context.SaveChanges();

                var BookDtos = new List<AddBookDto>
                {
                    new AddBookDto {AuthorId = 1, Title = "Test"},
                    new AddBookDto {AuthorId = 2, Title = "Test Two"}
                };

                IAuthorRepository authorRepository = new AuthorRepository(context);

                IBookRepository bookRepository = new BookRepository(context);

                IBookFacade bookFacade = new BookFacade(bookRepository, authorRepository);

                var expected = new ApiResponseDto<bool>(true);

                //act
                var result = bookFacade.AddManyBooks(BookDtos.ToArray());

                //assert
                Assert.Equal(expected.Value, result.Value);
            } 
            
        }
    }
}
