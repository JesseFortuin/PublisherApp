namespace Publisher.Shared.Dtos
{
    public class ApiResponseDto<T>
    {
        public bool IsSuccess { get; set; }

        public T? Value { get; set; }

        public string ErrorMessage { get; set; }

        public ApiResponseDto(T value)
        {
            IsSuccess = true;

            Value = value;
        }

        public ApiResponseDto(string errorMessage)
        {
            IsSuccess = false;

            ErrorMessage = errorMessage;
        }
    }
}
