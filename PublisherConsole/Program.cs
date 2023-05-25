// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publisher.Application;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;
using PublisherData;

IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IPublisherFacade, PublisherFacade>();

        services.AddSingleton<PubContext>();

        services.AddSingleton<IAuthorRepository, AuthorRepository>();

        services.AddSingleton<IBookFacade, BookFacade>();

        services.AddSingleton<IBookRepository, BookRepository>();
    })
    .Build();

var publisher = host.Services.GetRequiredService<IPublisherFacade>();

var bookFacade = host.Services.GetService<IBookFacade>();

//var authors = new List<AddAuthorDto>
//{
//    new AddAuthorDto { FirstName = "Andrzej", LastName = "Sapkowski" }
//};

//publisher.AddManyAuthors(authors.ToArray());

//publisher.GetAuthors();
//var bookDto = bookFacade.GetBookById(1);

//Console.WriteLine($"Book Author is {bookDto.AuthorName}, book id is {bookDto.BookId}, and title of the book is {bookDto.Title}");

//var books = new List<BookDto>
//{
//    new BookDto { AuthorName = "Andrzej Sapkowski", Title = "The Witcher: Blood of Elves", PublishDate = new DateTime (2009, 12, 4)},
//    new BookDto { Title = "The Witcher: Time of Contempt", PublishDate = new DateTime (2013, 1, 21)}
//};

//bookFacade.AddBooksToAnAuthor(books.ToArray());

//var result = bookFacade.GetAllBooks();

//foreach (var bookDto in result)
//{
//    Console.WriteLine($"Book Author: {bookDto.AuthorName}, book Id: {bookDto.BookId}, Title: {bookDto.Title}");
//}

//var author = new AddAuthorDto { FirstName = "Lynda", LastName = "Rutledge" };

//var book = new AddBookDto { PublishDate = new DateTime(2021, 2, 1), Title = "West With Giraffes" };

//publisher.AddAuthorWithBook(author, book);

//var book = new AddBookDto { PublishDate = new DateTime(2012, 1, 1), Title = "Wool" };

//publisher.AddNewBookToExistingAuthor("Howey", book);

//publisher.EagerLoadBooksWithAuthors();

var book = bookFacade.GetBookById(8);

Console.WriteLine(book);

//      publisher.GetAuthorsWithBooks();
