using LigalFrontend.DAL;
using LigalFrontend.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models.Buscador
{
    public class buscadorDiario
    {
        [Display(Name = "Conductor")]
        public string idUsuario { get; set; }
        [Display(Name = "Matricula")]
        public string idMatricula { get; set; }
        [Display(Name = "Fecha Inicio")]
        public string FechaHoraVisitaI { get; set; }
        [Display(Name = "Fecha Fin")]
        public string FechaHoraVisitaF { get; set; }

        public List<gen_usuarios> listaUsuarios;
        public List<GEN_VEHICULO> listaVehiculo;

        public buscadorDiario()
        {
            listaUsuarios = new UsuariosRepo().getListaUsuarios();
            listaVehiculo = new GenericRepository<LigalEntities, GEN_VEHICULO>().getTodo();
        }
    }
}