using Microsoft.IdentityModel.Tokens;

namespace UpOnlineAFVApi.Servico
{
    public class Resposta<T>
    {

        private String _msg = "";
        public String Msg
        {
            get
            {

                return _msg;
            }
            set
            {

                if (value.Trim().IsNullOrEmpty())
                {
                    _msg = "Resposta da requisição sem mensagem!";
                }
                else
                {
                    _msg = value.Trim();
                }

            }
        }
        public Boolean Ok { get; set; }
        public T Conteudo { get; set; }

        public Resposta() { }

        public Resposta(String msg, Boolean ok, T conteudo)
        {
            _msg = msg.Trim();
            Ok = ok;
            Conteudo = conteudo;
        }

    }
}
