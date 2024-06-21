using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;
using FinTrack.Core.Responses.Base;
using System.Net.Http.Json;

namespace FinTrack.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
    {
        private readonly HttpClient client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

        public async Task<BaseResponse<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var result = await client.PostAsJsonAsync("v1/categories", request);
            return await result.Content.ReadFromJsonAsync<BaseResponse<Category?>>() ?? new BaseResponse<Category?>(null, 400, "Falha ao criar nova categoria");
        }

        public async Task<BaseResponse<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            var result = await client.DeleteAsync($"v1/categories/{request.Id}");
            return await result.Content.ReadFromJsonAsync<BaseResponse<Category?>>() ?? new BaseResponse<Category?>(null, 400, "Falha ao remover a categoria");
        }        

        public async Task<BaseResponse<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            var result = await client.PutAsJsonAsync($"v1/categories/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<BaseResponse<Category?>>() ?? new BaseResponse<Category?>(null, 400, "Falha ao atualizar a categoria");
        }

        public async Task<PagedBaseReponse<List<Category>?>> GetAllAsync(GetAllCategoriesRequest request)
            => await client.GetFromJsonAsync<PagedBaseReponse<List<Category>?>>($"v1/categories") ??
                        new PagedBaseReponse<List<Category>?>(null, 400, "Falha ao obter as categorias");

        public async Task<BaseResponse<Category?>> GetById(GetCategoryByIdRequest request)
            => await client.GetFromJsonAsync<BaseResponse<Category?>>($"v1/categories/{request.Id}") ??
                    new BaseResponse<Category?>(null, 400, "Falha ao obter a categoria");
    }
}
