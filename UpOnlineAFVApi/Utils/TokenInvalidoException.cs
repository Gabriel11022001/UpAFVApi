namespace UpOnlineAFVApi.Utils
{
    public class TokenInvalidoException: Exception
    {

        public TokenInvalidoException(): base() { }

        public TokenInvalidoException(String mensagem): base(mensagem) { }

    }
}
