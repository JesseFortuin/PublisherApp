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
    })
    .Build();

var publisher = host.Services.GetRequiredService<IPublisherFacade>();

publisher.InsertMultipleAuthors();