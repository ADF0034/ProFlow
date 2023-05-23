namespace ProFlow.Models
{
    public class ResponseMessage
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ResponseMessage(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
