// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publisher.Application;
using Publisher.Infrastructure;
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

//Console.WriteLine($"Book Author is {bookDto.AuthorName}, book id is {bookDto.BookId}, and title of the book us {bookDto.Title}");

bookFacade.GetAllBooks();