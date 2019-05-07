namespace LigalFrontend.Models
{
    public class objetoOrdenMapa
    {
        public string coleccion { get; set; }
        public string nombreCampo { get; set; }

        public objetoOrdenMapa() { }

        public objetoOrdenMapa(string col, string nCampo)
        {
            coleccion = col;
            nombreCampo = nCampo;
        }
    }
}