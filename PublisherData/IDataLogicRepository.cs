using Publisher.Domain.Entities;

namespace Publisher.Infrastructure
{
    public interface IDataLogicRepository
    {
        public int ImportAuthors(List<Author> authors);
    }
}
