using FinTrack.Core.Requests.Base;

namespace FinTrack.Core.Requests.Categories
{
    public class DeleteCategoryRequest : BaseRequest
    {
        public long Id { get; set; }
    }
}
