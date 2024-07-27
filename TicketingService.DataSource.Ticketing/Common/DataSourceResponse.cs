namespace TicketingService.DataSource.Ticketing.Common
{
    public class DataSourceResponse<T>
    {
        public T? Data { get; set; }
        public int Count { get; set; }
        public bool success { get; set; }

        public static DataSourceResponse<T> Create((T? data, bool success) data, (int data, bool success) count)
        {
            return new DataSourceResponse<T>()
            {
                Data = data.data,
                Count = count.data,
                success = data.success && count.success
            };
        }
    }
}
