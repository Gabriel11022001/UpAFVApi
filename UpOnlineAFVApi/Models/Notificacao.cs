using System.ComponentModel.DataAnnotations;

namespace UpOnlineAFVApi.Models
{
    public class Notificacao
    {

        public int NotificacaoId { get; set; }
        [ Required(ErrorMessage = "Informe o título da notificação!") ]
        [ MaxLength(255, ErrorMessage = "O título da notificação deve possuir no máximo 255 caracteres!") ]
        [ MinLength(6, ErrorMessage = "O título da notificação deve possuir no mínimo 6 caracteres!") ]
        public String Titulo { get; set; }
        [ Required(ErrorMessage = "Informe a mensagem da notificação!") ]
        public String Mensagem { get; set; }
        [ Required(ErrorMessage = "Informe o status da notificação!") ]
        public Boolean Status { get; set; }

    }
}
