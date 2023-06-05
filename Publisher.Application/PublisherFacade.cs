using Microsoft.EntityFrameworkCore;
using PublisherData;
using Publisher.Domain.Entities;
using Publisher.Shared.Dtos;
using Publisher.Infrastructure;

namespace Publisher.Application
{
    public class PublisherFacade : IPublisherFacade
    {
        private readonly IAuthorRepository authorRepository;

        public PublisherFacade(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public ApiResponseDto<bool> AddAuthor(AddAuthorDto authorDto)
        {
            if (authorDto.FirstName == null && authorDto.LastName == null)
            {
                return new ApiResponseDto<bool>("FirstName and LastName required");
            }

            var author = new Author
            {
                FirstName = authorDto.FirstName, 
                LastName = authorDto.LastName
            };

            var result = authorRepository.AddAuthor(author);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> AddAuthorWithBook(AddAuthorWithBookDto authorWithBookDto)
        {
            if (authorWithBookDto.FirstName == null && authorWithBookDto.LastName == null ||
                authorWithBookDto.Title == null && authorWithBookDto.PublishDate == DateTime.MinValue)
            {
                return new ApiResponseDto<bool>("Author and Book is required");
            }

            var author = new Author
            {
                FirstName = authorWithBookDto.FirstName,
                LastName = authorWithBookDto.LastName
            };

            author.Books.Add(new Book
            {
                Title = authorWithBookDto.Title,

                PublishDate = authorWithBookDto.PublishDate
            });

            var result = authorRepository.AddAuthor(author);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> AddManyAuthors(params AddAuthorDto[] authorDtos)
        {
            var authors = new List<Author>();

            foreach (var authorDto in authorDtos)
            {
                if (authorDto.FirstName == null && authorDto.LastName == null)
                {
                    return new ApiResponseDto<bool>("FirstName and LastName of author or authors required");
                }

                var author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName
                };

                authors.Add(author);
            }

            var result = authorRepository.AddManyAuthors(authors.ToArray());

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> AddNewBookToExistingAuthor(string authorsLastName, AddAuthorBookDto bookDto)
        {
            if (string.IsNullOrWhiteSpace(authorsLastName))
            {
                return new ApiResponseDto<bool>("Author's lastname is required");
            }

            if (bookDto.Title == null && bookDto.Title == null) 
            {
                return new ApiResponseDto<bool>("Valid book info required");
            }

            var author = authorRepository.GetAuthorByLastName(authorsLastName);

            author.Books.Add(
                new Book 
                { 
                    Title = bookDto.Title, 
                    PublishDate = new DateTime(2012, 1, 1) 
                });

            var result = authorRepository.AddAuthor(author);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> CoordinatedRetrieveAndUpdateAuthor(int authorId, string originalName, string updatedName)
        {
            if (authorId <= 0)
            {
                return new ApiResponseDto<bool>("Insert valid Id");
            }

            if (string.IsNullOrWhiteSpace(originalName))
            {
                return new ApiResponseDto<bool>("Insert name you would like to change");
            }

            if (string.IsNullOrWhiteSpace(updatedName))
            {
                return new ApiResponseDto<bool>("New name is required");
            }

            var author = authorRepository.FindAnAuthorById(authorId);

            if (author?.FirstName == originalName) 
            {
                author.FirstName = updatedName;
                               
                authorRepository.SaveAnAuthor(author);
            }

            return new ApiResponseDto<bool>("Author name updated");
        }

        public ApiResponseDto<bool> DeleteAuthor(int authorId)
        {
            if (authorId <= 0)
            {
                return new ApiResponseDto<bool>("Insert valid Id");
            }

            var result = authorRepository.DeleteAnAuthor(authorId);
            
            if (!result)
            {
                return new ApiResponseDto<bool>("Author not found in Database");
            }

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> EagerLoadBooksWithAuthors()
        {
            var result = authorRepository.EagerLoadBooksWithAuthors();

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<List<AuthorDto>> GetAuthors()
        {
            var authors = authorRepository.GetAuthors();

            var authorDtos = new List<AuthorDto>();

            foreach(var author in authors)
            {
                var authorDto = new AuthorDto { AuthorName = author.FirstName + " " + author.LastName };

                authorDtos.Add(authorDto);
            }

            return new ApiResponseDto<List<AuthorDto>>(authorDtos);
        }

        public ApiResponseDto<AuthorDto> GetAuthorByName(string firstName)
        {
            var author = authorRepository.GetAuthorByName(firstName);

            if (author == null)
            {
                throw new Exception("Author not found");
            }

            var authorDto = new AuthorDto
            {
                AuthorName = author.FirstName + " " + author.LastName
            };

            return new ApiResponseDto<AuthorDto>(authorDto);
        }

        public ApiResponseDto<List<AuthorDto>> GetAuthorsByRecentBook(int publishedOnAndAfter)
        {
            var authors = authorRepository.GetAuthorsByRecentBook(publishedOnAndAfter);

            var authorDtos = new List<AuthorDto>();

            foreach (var author in authors)
            {
                var authorDto = new AuthorDto { AuthorName = author.FirstName + " " + author.LastName };

                authorDtos.Add(authorDto);
            }

            return new ApiResponseDto<List<AuthorDto>>(authorDtos);
        }

        public ApiResponseDto<AuthorDto> GetAuthorById(int authorId)
        {
            var author = authorRepository.FindAnAuthorById(authorId);

            var authorDto = new AuthorDto 
            {
                AuthorName = author.FirstName + " " + author.LastName
            };

            return new ApiResponseDto<AuthorDto>(authorDto);
        }

        public ApiResponseDto<List<AuthorWithBooksDto>> GetAuthorsWithBooks()
        {
            var authors = authorRepository.GetAuthorsWithBooks();

            var authorDtos = new List<AuthorWithBooksDto>();

            foreach (var author in authors)
            {
                var authorDto = new AuthorWithBooksDto {FirstName = author.FirstName, LastName = author.LastName};

                foreach (var book in author.Books)
                {
                    var bookDto = new BookDto { Title = book.Title };

                    authorDto.Books.Add(bookDto);
                }

                authorDtos.Add(authorDto);
            }

            return new ApiResponseDto<List<AuthorWithBooksDto>>(authorDtos);
        }
        
        public ApiResponseDto<bool> InsertAuthor(AddAuthorDto authorDto)
        {
            if (authorDto.FirstName == null && authorDto.LastName == null)
            {
                return new ApiResponseDto<bool>("FirstName and LastName required");
            }

            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName
            };

            var result = authorRepository.InsertAuthor(author);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> SetBookBasePrice(int authorId, int bookId, decimal price)
        {
            var author = authorRepository.GetAuthorByIdWithBooks(authorId);

            author.Books[bookId - 1].BasePrice = price;

            var result = authorRepository.UpdateAuthorBook(author, bookId -1);

            return new ApiResponseDto<bool>(result);
        }

        public void InsertMultipleAuthors()
        {
            authorRepository.InsertMultipleAuthors();
        }

        public ApiResponseDto<bool> QueryAggregate(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return new ApiResponseDto<bool>("LastName required for query");
            }

            var result = authorRepository.QueryAggregate(lastName);

            return new ApiResponseDto<bool>(result);
        }

        public void QueryFilters()
        {
            var name = "Julie";

            var filters = "L%";

            var result = authorRepository.QueryFilters(name, filters);
        }

        public ApiResponseDto<bool> RetrieveAndUpdateAuthor(string name, string newName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new ApiResponseDto<bool>("Insert name you would like to change");
            }

            if (string.IsNullOrWhiteSpace(newName))
            {
                return new ApiResponseDto<bool>("New name is required");
            }

            var author = authorRepository.GetAuthorByName(name);

            if (author == null)
            {
                return new ApiResponseDto<bool>("Author not found");
            }

            author.FirstName = newName;

            var result = authorRepository.AddAuthor(author);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> RetrieveAndUpdateMultipleAuthorsLastNames(string lastName, string updatedLastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return new ApiResponseDto<bool>("Insert name you would like to change");
            }

            if (string.IsNullOrWhiteSpace(updatedLastName))
            {
                return new ApiResponseDto<bool>("New name is required");
            }

            var authors = authorRepository.RetrieveAuthorsByLastName(lastName);

            var authorList = new List<Author>();

            foreach (var author in authors)
            {
                author.LastName = updatedLastName;

                authorList.Add(author);
            }

            var result = authorRepository.AddManyAuthors(authorList.ToArray());

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> SkipAndTakeAuthors(int groupSize)
        {
            if (groupSize <= 0)
            {
                return new ApiResponseDto<bool>("A Group size larger than 0 is needed");
            }

            var result = authorRepository.SkipAndTakeAuthors(groupSize);

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<bool> SortAuthorsDecendingOrder()
        {
            var result = authorRepository.SortAuthors();

            return new ApiResponseDto<bool>(result);
        }

        public ApiResponseDto<List<AuthorDto>> GetAuthorsWithSqlRaw()
        {
            var authors = authorRepository.SimpleRawSql();

            var authorDtos = new List<AuthorDto>();

            foreach (var author in authors)
            {
                var authorDto = new AuthorDto { AuthorName = author.FirstName + " " + author.LastName };

                authorDtos.Add(authorDto);
            }

            return new ApiResponseDto<List<AuthorDto>>(authorDtos);
        }
    }
}