namespace UpOnlineAFVApi.DTOs
{
    public class TokenDTO
    {

        public String Token { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime DataExpiracao { get; set; }
        public UsuarioDTO UsuarioDTO { get; set; }

    }
}
