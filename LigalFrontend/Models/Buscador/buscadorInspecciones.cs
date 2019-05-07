using LigalFrontend.DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorInspecciones
    {
        [Display(Name="Usuario")]
        public string idUsuario { get; set; }
        [Display(Name="Explotación")]
        public string IDExplotacion { get; set; }
        [Display(Name="Fecha Visita Inicio")]
        public string FechaHoraVisitaI { get; set; }
        [Display(Name="Fecha Visita Fin")]
        public string FechaHoraVisitaF { get; set; }
        [Display(Name="Industria")]
        public string INDUSTRIAN { get; set; }
        [Display(Name="Resultado")]
        public string Resultado { get; set; }
        [Display(Name="Resultado CHARM")]
        public string ResultadoCHARM { get; set; }
        [Display(Name="Resultado Quinolona")]
        public string RESULTADOQUINO { get; set; }

        public List<gen_usuarios> listaUsuarios;
        public List<gen_resultados> listaResultadoCharm;
        public List<GEN_RESULTADOQUINOLONAS> listaResultadoQuino;

        public buscadorInspecciones()
        {
            listaUsuarios = new UsuariosRepo().getListaUsuarios();
            listaResultadoCharm = new GenericRepository<LigalEntities, gen_resultados>().getTodo();
            listaResultadoQuino = new GenericRepository<LigalEntities, GEN_RESULTADOQUINOLONAS>().getTodo();
        }
    }
}