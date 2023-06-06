using Microsoft.EntityFrameworkCore;
using Publisher.Domain.Entities;
using PublisherData;

namespace PublisherAppTest
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void CanInsertAuthorIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<PubContext>();

            builder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubTestData");

            using (var context = new PubContext(builder.Options))
            {
                context.Database.EnsureDeleted();

                context.Database.EnsureCreated();

                var author = new Author { FirstName = "Test", LastName = "Test" };

                context.Authors.Add(author);

                //Debug.WriteLine($"Before save: {author.AuthorId}");

                context.SaveChanges();

                //Debug.WriteLine($"After save: {author.AuthorId}");

                Assert.AreNotEqual(0, author.AuthorId);
            }
        }
    }
}