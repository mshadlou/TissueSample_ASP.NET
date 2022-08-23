using TissueSample2.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TissueSample2.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<ICollectionService, CollectionServiceManager>();
builder.Services.AddScoped<ISampleService, SampleServiceManager>();
builder.Services.AddScoped<IModal, ModalManager>();
builder.Services.AddScoped<IDashboardManger, DashboardManager>();

builder.Services.AddScoped(sp => 
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
);

await builder.Build().RunAsync();
