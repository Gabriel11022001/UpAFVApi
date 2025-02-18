using UpOnlineAFVApi.Contexto;

namespace UpOnlineAFVApi.Repositorio
{
    public abstract class RepositorioBase
    {

        protected readonly ApiDbContexto Contexto;

        public RepositorioBase(ApiDbContexto contexto)
        {
            Contexto = contexto;
        }

    }
}
