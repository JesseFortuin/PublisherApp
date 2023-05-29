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

        public bool AddAuthor(AddAuthorDto authorDto)
        {
            if (authorDto.FirstName == null && authorDto.LastName == null)
            {
                throw new Exception("FirstName and LastName required");
            }

            var author = new Author
            {
                FirstName = authorDto.FirstName, 
                LastName = authorDto.LastName
            };

            var result = authorRepository.AddAuthor(author);

            return true;
        }

        public bool AddAuthorWithBook(AddAuthorDto authorDto, AddAuthorBookDto bookDto)
        {
            if (authorDto.FirstName == null && authorDto.LastName == null || 
                bookDto.Title == null && bookDto.PublishDate == DateTime.MinValue)
            {
                throw new Exception("Author and Book is required");
            }

            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName
            };

            author.Books.Add(new Book
            {
                Title = bookDto.Title,

                PublishDate = bookDto.PublishDate
            });

            var result = authorRepository.AddAuthor(author);

            return true;
        }

        public bool AddManyAuthors(params AddAuthorDto[] authorDtos)
        {
            var authors = new List<Author>();

            foreach (var authorDto in authorDtos)
            {
                if (authorDto.FirstName == null && authorDto.LastName == null)
                {
                    throw new Exception("FirstName and LastName of author or authors required");
                }

                var author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName
                };

                authors.Add(author);
            }

            var result = authorRepository.AddManyAuthors(authors.ToArray());

            return result;
        }

        public bool AddNewBookToExistingAuthor(string authorsLastName, AddAuthorBookDto bookDto)
        {
            if (string.IsNullOrWhiteSpace(authorsLastName))
            {
                throw new Exception("Author's lastname is required");
            }

            if (bookDto.Title == null && bookDto.Title == null) 
            {
                throw new Exception("Valid book info required");
            }

            var result = authorRepository.AddNewBookToExistingAuthor(authorsLastName, bookDto);

            return result;
        }

        public void CoordinatedRetrieveAndUpdateAuthor(int authorId, string originalName, string updatedName)
        {
            if (authorId <= 0)
            {
                throw new Exception("Insert valid Id");
            }

            if (string.IsNullOrWhiteSpace(originalName))
            {
                throw new Exception("Insert name you would like to change");
            }

            if (string.IsNullOrWhiteSpace(updatedName))
            {
                throw new Exception("New name is required");
            }

            var author = authorRepository.FindAnAuthorById(authorId);

            if (author?.FirstName == originalName) 
            {
                author.FirstName = updatedName;
                
                authorRepository.SaveAnAuthor(author);
            }
        }

        public bool DeleteAuthor(int authorId)
        {
            if (authorId <= 0)
            {
                throw new Exception("Insert valid Id");
            }

            var result = authorRepository.DeleteAnAuthor(authorId);
            
            if (!result)
            {
                throw new Exception("Author not found in Database");
            }

            return result;        
        }

        public bool EagerLoadBooksWithAuthors()
        {
            var result = authorRepository.EagerLoadBooksWithAuthors();

            return result;
        }

        public List<AuthorDto> GetAuthors()
        {
            var authors = authorRepository.GetAuthors();

            var authorDtos = new List<AuthorDto>();

            foreach(var author in authors)
            {
                var authorDto = new AuthorDto { AuthorName = author.FirstName + " " + author.LastName };

                authorDtos.Add(authorDto);
            }

            return authorDtos;
        }

        public AuthorDto GetAuthorByName(string firstName)
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

            return authorDto;
        }

        public List<AuthorDto> GetAuthorsByRecentBook(int publishedOnAndAfter)
        {
            var authors = authorRepository.GetAuthorsByRecentBook(publishedOnAndAfter);

            var authorDtos = new List<AuthorDto>();

            foreach (var author in authors)
            {
                var authorDto = new AuthorDto { AuthorName = author.FirstName + " " + author.LastName };

                authorDtos.Add(authorDto);
            }

            return authorDtos;
        }

        public AuthorDto GetAuthorById(int authorId)
        {
            var author = authorRepository.FindAnAuthorById(authorId);

            var authorDto = new AuthorDto 
            {
                AuthorName = author.FirstName + " " + author.LastName
            };

            return authorDto;
        }

        public List<AuthorWithBooksDto> GetAuthorsWithBooks()
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

            return authorDtos;
        }
        
        public bool InsertAuthor(AddAuthorDto authorDto)
        {
            if (authorDto.FirstName == null && authorDto.LastName == null)
            {
                throw new Exception("FirstName and LastName required");
            }

            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName
            };

            var result = authorRepository.InsertAuthor(author);

            return true;
        }

        public bool SetBookBasePrice(int authorId, int bookId, decimal price)
        {
            var author = authorRepository.GetAuthorByIdWithBooks(authorId);

            author.Books[bookId - 1].BasePrice = price;

            authorRepository.UpdateAuthorBook(author, bookId -1);

            return true;
        }

        public void InsertMultipleAuthors()
        {
            authorRepository.InsertMultipleAuthors();
        }

        public bool QueryAggregate(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception("LastName required for query");
            }

            var result = authorRepository.QueryAggregate(lastName);

            return true;
        }

        public void QueryFilters()
        {
            var name = "Julie";

            var filters = "L%";

            var result = authorRepository.QueryFilters(name, filters);
        }

        public bool RetrieveAndUpdateAuthor(string name, string newName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Insert name you would like to change");
            }

            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new Exception("New name is required");
            }

            var result = authorRepository.RetrieveAndUpdateAuthor(name, newName);

            return true;
        }

        public bool RetrieveAndUpdateMultipleAuthorsLastNames(string lastName, string updatedLastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception("Insert name you would like to change");
            }

            if (string.IsNullOrWhiteSpace(updatedLastName))
            {
                throw new Exception("New name is required");
            }

            var result = authorRepository.RetrieveAndUpdateMultipleAuthorsLastNames(lastName, updatedLastName);

            return true;
        }

        public bool SkipAndTakeAuthors(int groupSize)
        {
            if (groupSize <= 0)
            {
                throw new Exception("A Group size larger than 0 is needed");
            }

            var result = authorRepository.SkipAndTakeAuthors(groupSize);

            return true;
        }

        public bool SortAuthorsDecendingOrder()
        {
            var result = authorRepository.SortAuthors();

            return true;
        }
    }
}