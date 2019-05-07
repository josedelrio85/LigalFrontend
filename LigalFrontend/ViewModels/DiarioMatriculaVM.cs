using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.ViewModels
{
    public class DiarioMatriculaVM
    {
        public GEN_DIARIOMATRICULA diario { get; set; }
        public UsuariosVehiculoVM usuVeh { get; set; }
        public GEN_TAREASDIARIO tarea { get; set; }

        public List<GEN_VEHICULO> listaVehiculos { get; set; }
        public List<GEN_TAREASDIARIO> listaTareas { get; set; }
        public List<gen_usuarios> listaUsuarios { get; set; }

        public buscadorDiario buscador { get; set; }

        public DiarioMatriculaVM() {}

        [Display(Name = "Usuario"), Required(ErrorMessage = "Seleccione un usuario.")]
        public int map_idUsuario { get; set; }
        [Display(Name = "Matricula"), Required(ErrorMessage = "Seleccione una matrícula.")]
        public int map_idMatricula { get; set; }
    }
}