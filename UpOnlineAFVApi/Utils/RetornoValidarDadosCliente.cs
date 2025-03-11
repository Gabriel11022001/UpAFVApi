namespace UpOnlineAFVApi.Utils
{
    public class RetornoValidarDadosCliente
    {

        public Boolean Ok { get; set; }
        public Dictionary<String, String> ValidacoesDados { get; set; }

        public RetornoValidarDadosCliente()
        {
            Ok = true;
            ValidacoesDados = new Dictionary<string, string>();
        }

    }
}
