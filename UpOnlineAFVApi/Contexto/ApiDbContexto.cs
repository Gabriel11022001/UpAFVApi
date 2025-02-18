using Microsoft.EntityFrameworkCore;
using UpOnlineAFVApi.Models;

namespace UpOnlineAFVApi.Contexto
{
    public class ApiDbContexto: DbContext
    {

        public DbSet<Categoria> Categorias { get; set; }

        public ApiDbContexto(DbContextOptions<ApiDbContexto> contexto): base(contexto) { }

    }
}
