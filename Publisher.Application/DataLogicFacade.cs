using Publisher.Domain.Entities;
using Publisher.Infrastructure;
using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public class DataLogicFacade : IDataLogicFacade
    {
        private readonly IDataLogicRepository logicRepository;

        public DataLogicFacade(IDataLogicRepository logicRepository)
        {
            this.logicRepository = logicRepository;
        }

        public int ImportAuthors(List<ImportAuthorDto> authorsDto)
        {
            var authorList = new List<Author>();

            foreach (var authorDto in authorsDto)
            {
                var author = new Author
                { 
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName
                };

                authorList.Add(author);
            }

            return logicRepository.ImportAuthors(authorList);
        }
    }
}
