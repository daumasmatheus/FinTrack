namespace FinTrack.Core.Requests.Base
{
    public abstract class PagedBaseRequest : BaseRequest
    {
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
        public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
    }
}
