using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NSubstitute;
using Publisher.Application;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;

namespace Publisher.Test
{
    public class PublisherFacadeTests
    {
        [Fact]
        public void AddAuthor_Fails_WithInvalidAddAuthorDto()
        {
            //arrange
            var authorDto = new AddAuthorDto();

            IPublisherFacade publisherFacade = new PublisherFacade(null);

            //act
            var result = Assert.Throws<Exception>(() => publisherFacade.AddAuthor(authorDto));

            //assert
            Assert.Equal("FirstName and LastName required", result.Message);
        }

        [Fact]
        public void AddAuthor_Succeeds_WithValidAddAuthorDto()
        {
            //arrange
            var authorDto = new AddAuthorDto 
            {
                FirstName = "Test",
                LastName = "TestLastName",
            };

            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            var expected = true;
            //act

            var result = publisherFacade.AddAuthor(authorDto);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void AddAuthorWithBook_Fails_WithInvalidAddAuthorDto()
        {
            //arrange
            var authorDto = new AddAuthorDto();

            var bookDto = new AddBookDto {PublishDate = DateTime.Now, Title = "Test" };   

            IPublisherFacade publisherFacade = new PublisherFacade(null);

            //act
            var result = Assert.Throws<Exception>(() => publisherFacade.AddAuthorWithBook(authorDto, bookDto));

            //assert
            Assert.Equal("Author and Book is required", result.Message);
        }

        [Fact]
        public void AddAuthorWithBook_Fails_WithInvalidAddBookDto()
        {
            //arrange
            var authorDto = new AddAuthorDto
            {
                FirstName = "Test",
                LastName = "TestLastName",
            };

            var bookDto = new AddBookDto();

            IPublisherFacade publisherFacade = new PublisherFacade(null);

            //act
            var result = Assert.Throws<Exception>(() => publisherFacade.AddAuthorWithBook(authorDto, bookDto));

            //assert
            Assert.Equal("Author and Book is required", result.Message);

        }

        [Fact]
        public void AddAuthorWithBook_Succeeds_WithValidAddDtos()
        {
            //arrange
            var authorDto = new AddAuthorDto
            {
                FirstName = "Test",
                LastName = "TestLastName",
            };

            var bookDto = new AddBookDto { PublishDate = DateTime.Now, Title = "Test" };

            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            var expected = true;
            //act

            var result = publisherFacade.AddAuthorWithBook(authorDto, bookDto);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeleteAuthor_Fails_InvalidId()
        {
            //arrange
            var id = -1;

            IPublisherFacade publisherFacade = new PublisherFacade(null);

            //act
            var result = Assert.Throws<Exception>(() => publisherFacade.DeleteAuthor(id));

            //assert
            Assert.Equal("Insert valid Id", result.Message);
        }

        [Fact]
        public void DeleteAuthor_Fails_AuthorWithIdNotFound()
        {
            //arrange
            var id = 1;

            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            //act
            var result = Assert.Throws<Exception>(() => publisherFacade.DeleteAuthor(id));

            //assert
            Assert.Equal("Author not found in Database", result.Message);
        }

        [Fact]
        public void DeleteAuthor_Succeeds_AuthorFound()
        {
            //arrange
            var id = 1;

            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            var expected = true;
            //act
            var result = publisherFacade.DeleteAuthor(id);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAuthors_Succeeds()
        {
            //arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            var expected = true;

            //act
            var result = publisherFacade.GetAuthors();

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetAuthorsWithBooks_Succeeds()
        {
            //arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            var expected = true;

            //act
            var result = publisherFacade.GetAuthorsWithBooks();

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void InsertAuthor_Fails_WithInvalidAddAuthorDto()
        {
            //arrange
            var authorDto = new AddAuthorDto();

            IPublisherFacade publisherFacade = new PublisherFacade(null);

            //act
            var result = Assert.Throws<Exception>(() => publisherFacade.InsertAuthor(authorDto));

            //assert
            Assert.Equal("FirstName and LastName required", result.Message);
        }

        [Fact]
        public void InsertAuthor_Succeeds_WithValidAddAuthorDto()
        {
            //arrange
            var authorDto = new AddAuthorDto
            {
                FirstName = "Test",
                LastName = "TestLastName",
            };

            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            var expected = true;
            //act

            var result = publisherFacade.InsertAuthor(authorDto);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void QueryAggregate_Fails_WhiteSpaceString()
        {
            //arrange
            IPublisherFacade publisherFacade = new PublisherFacade(null);

            var name = "   ";

            var expected = "LastName required for query";
            //act
            var result = Assert.Throws<Exception>(() => publisherFacade.QueryAggregate(name));

            //assert
            Assert.Equal(expected, result.Message);
        }

        [Fact]
        public void QueryAggregate_Succeeds_ValidName()
        {
            //arrange
            var name = "name";

            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            var expected = true;
            //act
            var result = publisherFacade.QueryAggregate(name);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void RetrieveAndUpdateAuthor_Fails_EmptyString()
        {
            //arrange
            IPublisherFacade publisherFacade = new PublisherFacade(null);

            var name = string.Empty;

            var newName = "Test";

            var expected = "Insert name you would like to change";
            //act

            var result = Assert.Throws<Exception>(() => publisherFacade.RetrieveAndUpdateAuthor(name, newName));

            //assert
            Assert.Equal(expected, result.Message);
        }

        [Fact]
        public void RetrieveAndUpdateAuthor_Fails_UpdatedNameIsWhiteSpace()
        {
            //arrange
            IPublisherFacade publisherFacade = new PublisherFacade(null);

            var name = "Test" ;

            var newName = string.Empty;

            var expected = "New name is required";
            //act

            var result = Assert.Throws<Exception>(() => publisherFacade.RetrieveAndUpdateAuthor(name, newName));

            //assert
            Assert.Equal(expected, result.Message);
        }

        [Fact]
        public void RetrieveAndUpdateAuthor_Succeeds_ValidNameAndNameAndNewName()
        {
            //arrange
            var name = "name";

            var newName = "Test";

            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            var expected = true;
            //act
            var result = publisherFacade.RetrieveAndUpdateAuthor(name, newName);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SkipAndTakeAuthors_Fails_NegativeIntGiven()
        {
            //arrange
            var groupSize = -1;

            IPublisherFacade publisherFacade = new PublisherFacade(null);

            //act
            var result = Assert.Throws<Exception>(() => publisherFacade.SkipAndTakeAuthors(groupSize));

            //assert
            Assert.Equal("A Group size larger than 0 is needed", result.Message);
        }
        
        [Fact]
        public void SkipAndTakeAuthors_Fails_ZeroGiven()
        {
            //arrange
            var groupSize = 0;

            IPublisherFacade publisherFacade = new PublisherFacade(null);

            //act
            var result = Assert.Throws<Exception>(() => publisherFacade.SkipAndTakeAuthors(groupSize));

            //assert
            Assert.Equal("A Group size larger than 0 is needed", result.Message);
        }

        [Fact]
        public void SkipAndTakeAuthors_Succeeds_ValidGroupSizeGive()
        {
            //arrange
            var groupSize = 2;

            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            var expected = true;

            //act
            var result = publisherFacade.SkipAndTakeAuthors(groupSize);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SortAuthorsDecendingOrder_Succeeds()
        {
            //arrange
            IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

            IPublisherFacade publisherFacade = new PublisherFacade(authorRepository);

            var expected = true;

            //act
            var result = publisherFacade.SortAuthorsDecendingOrder();

            //assert
            Assert.Equal(expected, result);
        }
    }
}