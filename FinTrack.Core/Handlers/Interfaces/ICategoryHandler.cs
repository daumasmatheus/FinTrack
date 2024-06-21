using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;
using FinTrack.Core.Responses.Base;

namespace FinTrack.Core.Handlers.Interfaces
{
    public interface ICategoryHandler
    {
        Task<BaseResponse<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<BaseResponse<Category?>> UpdateAsync(UpdateCategoryRequest request);
        Task<BaseResponse<Category?>> DeleteAsync(DeleteCategoryRequest request);
        Task<PagedBaseReponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request);
        Task<BaseResponse<Category?>> GetById(GetCategoryByIdRequest request);
    }
}
