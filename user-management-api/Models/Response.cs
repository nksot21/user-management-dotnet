namespace user_management_api.Models
{
    public class Response
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }

        public Response() { }

        public Response(int status, string message, Object data) {
            this.Status = status;
            this.Message = message; 
            this.Data = data;
        }
    }
}
