using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;
using FinTrack.Core.Requests.Transactions;
using FinTrack.Web.Handlers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Globalization;

namespace FinTrack.Web.Pages.Transactions
{
    public partial class EditTransactionPage : ComponentBase
    {
        public CultureInfo _pt_BR = CultureInfo.GetCultureInfo("pt-BR");

        [Parameter]
        public long Id { get; set; }

        public bool IsBusy { get; set; } = false;
        public UpdateTransactionRequest InputModel { get; set; } = new();

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
            try
            {
                var result = await TransactionHandler.GetById(new GetTransactionByIdRequest { Id = Id });
                if (result.IsSuccess && result.Data is not null)
                {
                    InputModel.Id = result.Data.Id;
                    InputModel.Title = result.Data.Title;
                    InputModel.Amount = result.Data.Amount;
                    InputModel.TransactionType = result.Data.TransactionType;
                    InputModel.CategoryId = result.Data.CategoryId;
                    InputModel.PaidOrReceivedAt = result.Data.PaidOrReceivedAt;
                }

                await GetCategories();
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                throw;
            }
        }

        private async Task GetCategories()
        {
            var result = await CategoryHandler.GetAllAsync(new GetAllCategoriesRequest());
            if (result.IsSuccess && result.Data is not null)
                Categories = result.Data;
        }

        public async Task OnValidSubmitAsync()
        {
            try
            {
                var result = await TransactionHandler.UpdateAsync(InputModel);
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
        }
    }
}
