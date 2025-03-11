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
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IClienteServico, ClienteServico>();
builder.Services.AddScoped<INotificacaoRepositorio, NotificacaoRepositorio>();
builder.Services.AddScoped<INotificacaoServico, NotificacaoServico>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ITokenRepositorio, TokenRepositorio>();
builder.Services.AddScoped<ILoginServico, LoginServico>();
builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();
builder.Services.AddScoped<ITokenServico, TokenServico>();
builder.Services.AddScoped<INivelAcessoRepositorio, NivelAcessoRepositorio>();
builder.Services.AddScoped<INivelAcessoServico, NivelAcessoServico>();
builder.Services.AddScoped<IPermissaoRepositorio, PermissaoRepositorio>();
builder.Services.AddScoped<IPermissaoServico, PermissaoServico>();
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<IProdutoServico, ProdutoServico>();

// configurar o cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", (builder) =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
