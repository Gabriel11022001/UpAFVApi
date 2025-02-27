using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.Models
{
    public class Endereco
    {

        public int EnderecoId { get; set; }
        [ Required(ErrorMessage = "Informe o cep!") ]
        public String Cep { get; set; }
        [ Required(ErrorMessage = "Informe o logradouro!") ]
        public String Logradouro { get; set; }
        public String Complemento { get; set; }
        [ Required(ErrorMessage = "Informe o bairro!") ]
        public String Bairro { get; set; }
        [ Required(ErrorMessage = "Informe a cidade!") ]  
        public String Cidade { get; set; }
        [ Required(ErrorMessage = "Informe a uf!") ]
        public String Uf { get; set; }
        [ Required(ErrorMessage = "Informe o id do cliente!") ]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

    }
}
