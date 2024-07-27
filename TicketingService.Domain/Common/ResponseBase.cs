namespace TicketingService.Domain.Common
{
    public class ResponseBase<T>
    {
        public T? Data { get; set; }
        public ResponseStatus Status { get; set; }
        public int Count { get; set; }
    }
    public enum ResponseStatus
    {
        Success, Error
    }
}
