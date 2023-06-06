using Publisher.Domain.Entities;
using PublisherData;

namespace Publisher.Infrastructure
{
    public class DataLogicRepository : IDataLogicRepository
    {
        PubContext pubContext;

        public DataLogicRepository(PubContext pubContext)
        {
            this.pubContext = pubContext;
        }

        public DataLogicRepository()
        {
            pubContext = new PubContext();
        }

        public int ImportAuthors(List<Author> authors)
        {
            pubContext.Authors.AddRange(authors);

            return pubContext.SaveChanges();
        }
    }
}
