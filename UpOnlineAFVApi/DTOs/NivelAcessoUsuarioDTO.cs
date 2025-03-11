using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.DTOs
{
    public class NivelAcessoUsuarioDTO
    {

        public int NivelAcessoId { get; set; }
        [ Required(ErrorMessage = "Informe o nome do nível de acesso!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe o status do nível de acesso!") ]  
        public Boolean Ativo { get; set; }
        public List<PermissaoNivelAcessoUsuarioDTO> PermissaoNivelAcessoUsuarioDTOS { get; set; }

        public NivelAcessoUsuarioDTO()
        {
            PermissaoNivelAcessoUsuarioDTOS = new List<PermissaoNivelAcessoUsuarioDTO>();
        }

    }
}
