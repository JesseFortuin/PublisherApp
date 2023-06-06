namespace Publisher.Shared.Dtos
{
    public class ImportAuthorDto
    {
        public ImportAuthorDto(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        private string _firstName;

        private string _lastName;

        public string FirstName => _firstName;

        public string LastName => _lastName;
    }
}
