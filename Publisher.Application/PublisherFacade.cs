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

        public void AddAuthor(AddAuthorDto authorDto)
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
        }

        public void AddAuthorWithBook(AddAuthorDto authorDto, AddBookDto bookDto)
        {
            if (authorDto.FirstName == null && authorDto.LastName == null || 
                bookDto == null)
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
        }

        public void AddCoupleOfAuthors(AddAuthorDto authorDto1, AddAuthorDto authorDto2)
        {
            if (authorDto1.FirstName == null && authorDto1.LastName == null &&
                authorDto2.FirstName == null && authorDto2.LastName == null)
            {
                throw new Exception("FirstName and LastName of both authors required");
            }

            var author1 = new Author
            {
                FirstName = authorDto1.FirstName,
                LastName = authorDto1.LastName
            };

            var author2 = new Author
            {
                FirstName = authorDto2.FirstName,
                LastName = authorDto2.LastName
            };

            var result = authorRepository.AddSomeMoreAuthors(author1, author2);
        }

        public void FindAndPaginationQuery()
        {
            throw new NotImplementedException();
        }

        public void GetAuthors()
        {
            var result = authorRepository.GetAuthors();
        }

        public void GetAuthorsWithBooks()
        {
            var result = authorRepository.GetAuthorsWithBooks();
        }

        public void InsertAuthor(AddAuthorDto authorDto)
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
        }

        public void QueryAggregate(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception("LastName required for query");
            }

            var result = authorRepository.QueryAggregate(lastName);
        }

        public void QueryFilters()
        {
            var name = "Julie";

            var filters = "L%";

            var result = authorRepository.QueryFilters(name, filters);
        }

        public void RetrieveAndUpdateAuthor(string name, string newName)
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
        }

        public void SkipAndTakeAuthors(int groupSize)
        {
            if (groupSize <= 0)
            {
                throw new Exception("A Group size larger than 0 is needed");
            }

            var result = authorRepository.SkipAndTakeAuthors(groupSize);
        }

        public void SortAuthorsDecendingOrder()
        {
            var result = authorRepository.SortAuthors();
        }
    }
}