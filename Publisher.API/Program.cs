using Microsoft.EntityFrameworkCore;
using Publisher.Application;
using Publisher.Infrastructure;
using PublisherData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookFacade, BookFacade>();

builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IPublisherFacade, PublisherFacade>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<IArtistFacade, ArtistFacade>();

builder.Services.AddScoped<ICoverFacade, CoverFacade>();

builder.Services.AddScoped<ICoverRepository, CoverRepository>();

builder.Services.AddScoped<IArtistRepository, ArtistRepository>();

builder.Services.AddDbContext<PubContext>(DBContextOptions =>
DBContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("PubContextConnection"))
.EnableSensitiveDataLogging()
.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
