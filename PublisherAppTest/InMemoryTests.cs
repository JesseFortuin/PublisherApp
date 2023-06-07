using Microsoft.EntityFrameworkCore;
using Publisher.Application;
using Publisher.Domain.Entities;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;
using PublisherData;

namespace PublisherAppTest
{
    [TestClass]
    public class InMemoryTests
    {
        [TestMethod]
        public void CanInsertAuthorIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<PubContext>();

            builder.UseInMemoryDatabase(
                Guid.NewGuid().ToString());

            using (var context = new PubContext(builder.Options))
            {
                var author = new Author { FirstName = "Test", LastName = "Test" };

                context.Authors.Add(author);

                Assert.AreEqual(EntityState.Added, context.Entry(author).State);
            }
        }

        [TestMethod]
        public void InsertAuthors_Succeeds_ReturnsCorrectResultNumber()
        {
            //arrange
            var builder = new DbContextOptionsBuilder<PubContext>();

            builder.UseInMemoryDatabase(
                "InsertAuthors_Succeeds_ReturnsCorrectResultNumber");

            var authorDtoList = new List<ImportAuthorDto>
            {
                new ImportAuthorDto("a", "b"),
                new ImportAuthorDto("c", "d"),
                new ImportAuthorDto("Frank", "Sinatra")
            };

            IDataLogicRepository logicRepository = new DataLogicRepository(new PubContext(builder.Options));

            IDataLogicFacade logicFacade = new DataLogicFacade(logicRepository);

            //act
            var result = logicFacade.ImportAuthors(authorDtoList);

            //assert
            Assert.AreEqual(authorDtoList.Count, result);
        }
    }
}
