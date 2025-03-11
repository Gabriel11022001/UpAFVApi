using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.DTOs
{
    public class ProdutoDTO
    {

        public int ProdutoId { get; set; }
        [ Required(ErrorMessage = "Informe o nome do produto!") ]
        [ StringLength(255, MinimumLength = 3, ErrorMessage = "O nome do produto deve ter entre 3 e 255 caracteres!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe a descrição do produto!") ]
        public String Descricao { get; set; }
        [ Required(ErrorMessage = "Informe o preço de compra do produto!") ] 
        public Double PrecoCompra { get; set; }
        [ Required(ErrorMessage = "Informe o preço de venda do produto!") ]
        public Double PrecoVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        [ Required(ErrorMessage = "Informe o estoque do produto!") ]
        public int Estoque { get; set; }
        [ Required(ErrorMessage = "Informe o status do produto!") ]
        public Boolean Ativo { get; set; }
        public String UrlFotoProduto { get; set; }
        public int CategoriaId { get; set; }
        public CategoriaDTO? CategoriaDTO { get; set; }

    }
}
