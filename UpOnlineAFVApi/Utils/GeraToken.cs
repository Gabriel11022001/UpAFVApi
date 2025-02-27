using System.Security.Cryptography;
using System.Text;

namespace UpOnlineAFVApi.Utils
{
    public static class GeraToken
    {        

        // gerar token do usuário logado
        public static String GerarToken(String nomeUsuarioLogado)
        {
            DateTime dataAtual = new DateTime();
            String token = "";

            string tokenCriptografar = $"{ nomeUsuarioLogado }{ dataAtual.TimeOfDay }";

            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(tokenCriptografar);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            token = sb.ToString();

            return token;
        }

    }
}
