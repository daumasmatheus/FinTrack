using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FinTrack.Web;
using MudBlazor.Services;
using FinTrack.Core;
using FinTrack.Core.Handlers.Interfaces;
using FinTrack.Web.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddHttpClient(WebConfiguration.HttpClientName, options =>
{
    options.BaseAddress = new Uri(Configuration.BackEndUrl);
});

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

await builder.Build().RunAsync();
