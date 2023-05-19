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
    })
    .Build();

var publisher = host.Services.GetRequiredService<IPublisherFacade>();

//var authors = new List<AddAuthorDto>
//{
//    new AddAuthorDto { FirstName = "Andrzej", LastName = "Sapkowski" }
//};

//publisher.AddManyAuthors(authors.ToArray());

publisher.GetAuthorsWithBooks();