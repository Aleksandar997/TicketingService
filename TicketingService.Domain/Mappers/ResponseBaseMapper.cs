using Framework.DataSource;
using TicketingService.DataSource.Ticketing.Common;
using TicketingService.Domain.Common;

namespace TicketingService.Domain.Mappers
{
    public static class ResponseBaseMapper
    {
        public static ResponseBase<Tout> ToResponseBase<Tin, Tout>(this DataSourceResponse<Tin> dataSourceResponse, Func<Tin, Tout> mapper)
        {
            if (dataSourceResponse.Data != null)
            {
                return new ResponseBase<Tout>()
                {
                    Data = mapper(dataSourceResponse.Data)
                };
            }
            return new ResponseBase<Tout>();
        }

        public static async Task<ResponseBase<Tout>> ToResponseBase<Tin, Tout>(this Task<DataSourceResponse<Tin>> dataSourceResponse, Func<Tin, Tout> mapper)
        {
            var response = await dataSourceResponse;
            if (response.success && response.Data != null)
            {
                return new ResponseBase<Tout>()
                {
                    Data = mapper(response.Data),
                    Status = ResponseStatus.Success,
                    Count = response.Count
                };
            }
            return new ResponseBase<Tout>();
        }
    }
}
