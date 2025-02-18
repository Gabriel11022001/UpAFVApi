using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.Models
{
    public class Categoria
    {

        public int CategoriaId { get; set; }
        [ Required(ErrorMessage = "Informe o nome da categoria!") ]
        [ MinLength(3, ErrorMessage = "O nome da categoria deve ter no mínimo 3 caracteres!") ]
        [ MaxLength(150, ErrorMessage = "O nome da categoria deve ter no máximo 150 caracteres!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe o status da categoria!") ]
        public Boolean Ativo { get; set; }

    }
}
