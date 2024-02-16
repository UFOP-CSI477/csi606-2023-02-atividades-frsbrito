using atividade_pratica.Context;
using atividade_pratica.Repository;
using atividade_pratica.Services;
using atividade_pratica.Entities;
using atividade_pratica.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<AtividadeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<BaseRepository<Pessoa>>();
builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<BaseRepository<Doacao>>();
builder.Services.AddScoped<DoacaoService>();
builder.Services.AddScoped<BaseRepository<LocalColeta>>();
builder.Services.AddScoped<LocalColetaService>();
builder.Services.AddScoped<BaseRepository<TipoSanguineo>>();
builder.Services.AddScoped<TipoSanguineoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(builder =>
{
    builder.SwaggerDoc("v1", new() { Title = "atividade_pratica", Version = "v1" });
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(atividade_pratica.Client._Imports).Assembly);

app.MapControllers();
app.Run();
