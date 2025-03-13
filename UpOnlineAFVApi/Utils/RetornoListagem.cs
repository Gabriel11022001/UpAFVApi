namespace UpOnlineAFVApi.Utils
{
    public class RetornoListagem<T>
    {

        private int _paginaAtual = 0;
        public int PaginaAtual
        {
            get
            {

                return _paginaAtual;
            }
            set
            {

                if (value == 0)
                {
                    _paginaAtual = 1;
                }
                else
                {
                    _paginaAtual = value;
                }

            }
        }
        public int UltimaPagina { get; set; }
        public int TotalElementos { get; set; }
        public T Elementos { get; set; }

    }
}
