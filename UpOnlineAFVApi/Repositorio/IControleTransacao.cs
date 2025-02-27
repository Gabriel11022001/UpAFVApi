namespace UpOnlineAFVApi.Repositorio
{
    public interface IControleTransacao
    {

        Task IniciarTransacao();

        Task CommitarTransacao();

        Task RollbackTransacao();

    }
}
