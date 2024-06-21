using FinTrack.Core.Requests.Base;
using System.Reflection;
using System.Text.Json.Serialization;

namespace FinTrack.Core.Responses.Base
{
    public class PagedBaseReponse<TData> : BaseResponse<TData>
    {
        [JsonConstructor]
        public PagedBaseReponse(TData? data, int totalCount, int currentPage = 1, int pageSize = Configuration.DefaultPageSize) : base(data)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PagedBaseReponse(TData? data, int code = Configuration.DefaultStatusCode, string? message = null){}

        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
        public int TotalCount { get; set; }
    }
}
