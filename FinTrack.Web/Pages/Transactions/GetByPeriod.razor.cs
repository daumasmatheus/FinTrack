using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Core.Models;
using FinTrack.Core.Requests.Categories;
using FinTrack.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Globalization;

namespace FinTrack.Web.Pages.Transactions
{
    public partial class GetByPeriodPage : ComponentBase
    {
        public CultureInfo _pt_BR = CultureInfo.GetCultureInfo("pt-BR");

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsBusy { get; set; } = false;
        public List<Transaction> Transactions { get; set; } = [];

        [Inject]
        public ITransactionHandler TransactionHandler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IDialogService Dialog { get; set; } = null!;

        public async void OnSearchButtonClickedAsync()
        {
            IsBusy = true;
            try
            {
                GetTransactionsByPeriodRequest request = new();
                request.StartDate = StartDate;
                request.EndDate = EndDate;

                var result = await TransactionHandler.GetByPeriod(request);
                if (result.IsSuccess)
                {
                    Transactions = result.Data ?? [];
                    StateHasChanged();
                }                    
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

        public async Task OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await Dialog.ShowMessageBox("Atenção", $"Ao prosseguir a categoria {title} será removida. Deseja Continuar?", yesText: "Excluir", noText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id, title);

            StateHasChanged();
        }

        private async Task OnDeleteAsync(long id, string title)
        {
            try
            {
                await TransactionHandler.DeleteAsync(new DeleteTransactionRequest { Id = id });
                Transactions.RemoveAll(x => x.Id == id);

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
