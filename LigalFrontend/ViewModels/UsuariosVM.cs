using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class UsuariosVM
    {
        public gen_usuarios usuario { get; set; }
        public gen_tipouser tipoUsuario { get; set; }
        public gen_empresa empresa { get; set; }

        public List<gen_tipouser> listaTipoUsuario { get; set; }

        public buscadorUsuarios buscador { get; set; }

        public UsuariosVM() {
            usuario = new gen_usuarios();
        }
    }
}