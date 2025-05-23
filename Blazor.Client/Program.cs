using Blazor.Client;
using Blazor.Client.Interfaces;
using Blazor.Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Cargar configuración desde appsettings.json
using var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await http.GetFromJsonAsync<Dictionary<string, string>>("appsettings.json");

string baseUrl = config["BaseUrl"];
Console.WriteLine($"Base URL: {baseUrl}");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });

builder.Services.AddScoped<IPersonService, PersonaService>();

builder.Services.AddSweetAlert2();
await builder.Build().RunAsync();
