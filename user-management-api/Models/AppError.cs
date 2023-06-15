namespace user_management_api.Models
{
    public class AppError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public AppError() { }
        public AppError(int statusCode, string message, string data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
