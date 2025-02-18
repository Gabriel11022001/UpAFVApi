using Microsoft.EntityFrameworkCore;
using UpOnlineAFVApi.Contexto;
using UpOnlineAFVApi.Repositorio;
using UpOnlineAFVApi.Servico;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configurar conexão com o banco de dados
String stringConexaoBancoDados = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContexto>(options =>
{
    options.UseSqlServer(stringConexaoBancoDados);
});

// injeções de dependência
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<ICategoriaServico, CategoriaServico>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
