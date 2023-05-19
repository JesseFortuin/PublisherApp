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

        public bool AddAuthorWithBook(AddAuthorDto authorDto, AddBookDto bookDto)
        {
            if (authorDto.FirstName == null && authorDto.LastName == null && 
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

            var authorIdentifiers = new List<(string, string)>();

            //foreach (var authorDto in authorDtos)
            //{
            //    if (authorDto.FirstName == null && authorDto.LastName == null)
            //    {
            //        throw new Exception("FirstName and LastName of author or authors required");
            //    }

            //    authorDto = new Author
            //    {
            //        FirstName = authorDto.FirstName,
            //        LastName = authorDto.LastName
            //    };

            //    authors.Add(authorDto);
            //}

            //{
            //    FirstName = authorDto1.FirstName,
            //    LastName = authorDto1.LastName
            //};

            //var author2 = new Author
            //{
            //    FirstName = authorDto2.FirstName,
            //    LastName = authorDto2.LastName
            //};

            //var result = authorRepository.AddManyAuthors(author1, author2);

            return true;
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

            var author = authorRepository.FindAnAuthor(authorId);

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

            if (authorRepository.DeleteAnAuthor(authorId) == false)
            {
                throw new Exception("Author not found in Database");
            }

            return true;        
        }

        public void FindAndPaginationQuery()
        {
            throw new NotImplementedException();
        }

        public bool GetAuthors()
        {
            var result = authorRepository.GetAuthors();

            return true;
        }

        public bool GetAuthorsWithBooks()
        {
            var result = authorRepository.GetAuthorsWithBooks();

            return true;
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