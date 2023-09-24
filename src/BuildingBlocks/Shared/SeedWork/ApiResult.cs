namespace Shared.SeedWork
{
    public class ApiResult<T>
    {
        public ApiResult()
        {

        }
        public ApiResult(bool isSuccceeded, string message = null)
        {

            Message = message;
            IsSuccceeded = isSuccceeded;
        }
        public ApiResult(bool isSuccceeded, T data, string message = null)
        {
            Data = data;
            Message = message;
            IsSuccceeded = isSuccceeded;
        }

        public bool IsSuccceeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
