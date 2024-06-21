using FinTrack.Api.Data;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;
using FinTrack.Core.Responses.Base;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Api.Handlers
{
    public class CategoryHandler(AppDbContext dbContext) : ICategoryHandler
    {
        public async Task<BaseResponse<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            Category category = new()
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId
            };

            try
            {
                await dbContext.Categories.AddAsync(category);
                await dbContext.SaveChangesAsync();

                return new BaseResponse<Category?>(category, message: "Categoria criada com sucesso");
            }
            catch (Exception)
            {
                return new BaseResponse<Category?>(category, 404, "Não foi possivel criar a categoria");
                throw;
            }
        }

        public async Task<BaseResponse<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                Category? category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new BaseResponse<Category?>(null, 404, "Categoria não encontrada");

                dbContext.Categories.Remove(category);
                await dbContext.SaveChangesAsync();

                return new BaseResponse<Category?>(category, message: "Categoria removida com sucesso");
            }
            catch (Exception)
            {
                return new BaseResponse<Category?>(null, 404, "Não foi possivel remover a categoria");
                throw;
            }
        }

        public async Task<PagedBaseReponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var query = dbContext.Categories.AsNoTracking().Where(x => x.UserId == request.UserId)
                                                               .OrderBy(x => x.Title);

                var categories = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                            .Take(request.PageSize)
                                            .ToListAsync();

                var count = await query.CountAsync();

                return new PagedBaseReponse<List<Category>>(categories, count, currentPage: request.PageNumber, pageSize: request.PageSize);

            }
            catch (Exception)
            {
                return new PagedBaseReponse<List<Category>>(null, 404, "Falha ao obter as categorias");
                throw;
            }
        }

        public async Task<BaseResponse<Category?>> GetById(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

                return category is null ?
                    new BaseResponse<Category?>(category, 404, "Categoria não encontrada") : new BaseResponse<Category?>(category);
            }
            catch (Exception)
            {
                return new BaseResponse<Category?>(null, 404, "Falha ao obter a categoria");
                throw;
            }
        }

        public async Task<BaseResponse<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                Category? category = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new BaseResponse<Category?>(category, 201, "Categoria não encontrada");

                category.Title = request.Title;
                category.Description = request.Description; 
                
                dbContext.Categories.Update(category);
                await dbContext.SaveChangesAsync();

                return new BaseResponse<Category?>(category, message: "Categoria atualizada com sucesso");
            }
            catch (Exception)
            {
                return new BaseResponse<Category?>(null, 404, "Não foi possivel atualizar a categoria");
                throw;
            }
        }
    }
}
