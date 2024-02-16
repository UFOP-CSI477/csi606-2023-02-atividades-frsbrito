using Caderno_de_Notas_NoSQL;
using Caderno_de_Notas_NoSQL.Context;
using Caderno_de_Notas_NoSQL.Repository;
using Caderno_de_Notas_NoSQL.Components;
using Caderno_de_Notas_NoSQL.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddLogging();
builder.Services.AddSingleton<IMongoDBContext, MongoDBContext>();
builder.Services.AddTransient<INotasRepository, NotasRepository>();
builder.Services.AddTransient<INotasAPIController, NotasAPIController>();

builder.Services.AddScoped<HttpClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var logger = app.Services.GetRequiredService<ILoggerFactory>()
    .CreateLogger<Program>();

logger.LogInformation("Iniciando log da aplicação.");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
