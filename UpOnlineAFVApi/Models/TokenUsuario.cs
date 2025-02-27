namespace UpOnlineAFVApi.Models
{
    public class TokenUsuario
    {

        public int TokenUsuarioId { get; set; }
        public String Token { get; set; }
        public DateTime DataRegistro { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
