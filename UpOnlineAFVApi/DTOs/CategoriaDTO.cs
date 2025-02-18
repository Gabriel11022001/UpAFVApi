using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.DTOs
{
    public class CategoriaDTO
    {

        public int CategoriaId { get; set; }
        [ Required(ErrorMessage = "Informe o nome da categoria!") ]
        [ StringLength(150, MinimumLength = 3, ErrorMessage = "O nome da categoria deve ter entre 3 e 150 caracteres!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe o status da categoria!") ]
        public Boolean Status { get; set; }

    }
}
