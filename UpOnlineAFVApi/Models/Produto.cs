using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.Models
{
    public class Produto
    {

        public int ProdutoId { get; set; }
        [ Required(ErrorMessage = "Informe o nome do produto!") ]
        [ MaxLength(255, ErrorMessage = "O nome do produto deve ter no máximo 255 caracteres!") ]
        [ MinLength(3, ErrorMessage = "O nome do produto deve possuir no mínimo 3 caracteres!") ]  
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe a descrição do produto!") ]
        public String Descricao { get; set; }
        [ Required(ErrorMessage = "Informe o preço de compra!") ]
        public Double PrecoCompra { get; set; }
        [ Required(ErrorMessage = "Informe o preço de venda!") ]
        public Double PrecoVenda { get; set; }
        [ Required(ErrorMessage = "Informe o status do produto!") ]
        public Boolean Ativo { get; set; }
        [ Required(ErrorMessage = "Informe a quantidade de unidades em estoque do produto!") ]
        public int Estoque { get; set; }
        public String UrlFotoProduto { get; set; }
        [ Required(ErrorMessage = "Informe a data de cadastro do produto!") ]
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

    }
}
