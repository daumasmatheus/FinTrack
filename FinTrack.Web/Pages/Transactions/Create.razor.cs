using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;
using FinTrack.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Globalization;

namespace FinTrack.Web.Pages.Transactions
{
    public partial class CreateTransactionPage : ComponentBase
    {
        public CultureInfo _pt_BR = CultureInfo.GetCultureInfo("pt-BR");

        public bool IsBusy { get; set; } = false;
        public CreateTransactionRequest InputModel { get; set; } = new();
        public List<Category> Categories { get; set; } = [];

        [Inject]
        public ITransactionHandler TransactionHandler { get; set; } = null!;
        [Inject]
        public ICategoryHandler CategoryHandler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await GetCategories();            
        }

        private async Task GetCategories()
        {
            var result = await CategoryHandler.GetAllAsync(new GetAllCategoriesRequest());
            if (result.IsSuccess && result.Data is not null)
                Categories = result.Data;
        }

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await TransactionHandler.CreateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/transacoes");
                }
                else
                    Snackbar.Add(result.Message, Severity.Error);
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
    }
}
