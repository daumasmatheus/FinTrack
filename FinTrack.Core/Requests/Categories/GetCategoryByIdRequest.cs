using FinTrack.Core.Requests.Base;

namespace FinTrack.Core.Requests.Categories
{
    public class GetCategoryByIdRequest : BaseRequest
    {
        public long Id { get; set; }
    }
}
