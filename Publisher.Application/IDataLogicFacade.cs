using Publisher.Shared.Dtos;

namespace Publisher.Application
{
    public interface IDataLogicFacade
    {
        public int ImportAuthors(List<ImportAuthorDto> authorsDto);
    }
}
