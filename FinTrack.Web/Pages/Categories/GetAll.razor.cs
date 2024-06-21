using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FinTrack.Web.Pages.Categories
{
    public partial class GetAllCategoriesPage : ComponentBase
    {
        public bool IsBusy { get; set; } = false;
        public List<Category> Categories { get; set; } = new();

        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;
        [Inject]
        public IDialogService Dialog { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllCategoriesRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSuccess)
                    Categories = result.Data ?? [];
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                throw;
            }
            finally 
            {
                IsBusy = false;
            }
        }

        public async void OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await Dialog.ShowMessageBox("Atenção", $"Ao prosseguir a categoria {title} será removida. Deseja Continuar?", yesText:"Excluir", noText:"Cancelar");

            if (result is true)
                await OnDeleteAsync(id, title);

            StateHasChanged();
        }

        public void OnEditButtonClicked(long id)
        {
            NavigationManager.NavigateTo($"/categorias/{id}");
        }

        public async Task OnDeleteAsync(long id, string title)
        {
            try
            {
                await Handler.DeleteAsync(new DeleteCategoryRequest { Id = id });
                Categories.RemoveAll(x => x.Id == id);

                Snackbar.Add($"Categoria {title} removida", Severity.Info);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                throw;
            }
        }
    }
}
