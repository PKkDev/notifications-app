namespace NotificationsApp.Domain.DTO.Error
{
    public class HttpResponseException
    {
        public string Message { get; set; }

        public HttpResponseException(string message)
        {
            Message = message;
        }
    }
}
