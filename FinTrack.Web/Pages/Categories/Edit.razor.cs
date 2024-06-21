using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinTrack.Web.Pages.Categories
{
    public partial class EditCategoryPage : ComponentBase
    {
        [Parameter]
        public long Id { get; set; }

        public bool IsBusy { get; set; } = false;
        public UpdateCategoryRequest InputModel { get; set; } = new();

        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var result = await Handler.GetById(new GetCategoryByIdRequest { Id = Id });
                if (result.IsSuccess && result.Data is not null)
                {
                    InputModel.Id = result.Data.Id;
                    InputModel.Title = result.Data.Title;
                    InputModel.Description = result.Data.Description;
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                throw;
            }
        }

        public async Task OnValidSubmitAsync()
        {
            try
            {
                var result = await Handler.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/categorias");
                }
                else
                    Snackbar.Add(result.Message, Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                throw;
            }
        }
    }
}
