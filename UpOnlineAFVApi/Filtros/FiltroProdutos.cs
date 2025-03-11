namespace UpOnlineAFVApi.Filtros
{
    public class FiltroProdutos
    {

        public double PrecoVendaInicial { get; set; }
        public double PrecoVendaFinal { get; set; }
        private String _nomeProduto = "";
        public String NomeProduto
        {
            get
            {

                return _nomeProduto;
            }
            set
            {

                if (value == null || value == "")
                {
                    _nomeProduto = "";
                }
                else
                {
                    _nomeProduto = value;
                }

            }
        }
        public Boolean Status { get; set; } = true;
        private String _descricao = "";
        public String Descricao
        {
            get
            {

                return _descricao;
            }
            set
            {

                if (value == null || value == "")
                {
                    _descricao = "";
                }
                else
                {
                    _descricao = value;
                }

            }
        }

    }
}
