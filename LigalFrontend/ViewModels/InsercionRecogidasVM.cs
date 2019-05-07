using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using System.Collections.Generic;
using LigalFrontend.ViewModels;

namespace LigalFrontend.ViewModels
{
    public class InsercionRecogidasVM
    {
        public gen_puntorecogidadia puntoRecogidaDia { get; set; }
        public GEN_PUNTOSRECOGIDA pRecogida { get; set; }
        public gen_usuarios usuario { get; set; }

        public List<GEN_PUNTOSRECOGIDA> listaPuntoRecogidaDia { get; set; }
        public List<gen_usuarios> listaUsuarios { get; set; }
        public List<RecogClientesVM> listaRecogCliente { get; set; }

        public buscadorInsercionRecogidas buscador { get; set; }

        public ImageSliderVM listaImagenes { get; set; }

        public bool BoolProgramado
        {
            get { return puntoRecogidaDia.PROGRAMADO == 1; }
            set { puntoRecogidaDia.PROGRAMADO  = value ? 1 : 0; }
        }

        public bool BoolVisitado
        {
            get { return puntoRecogidaDia.VISITADO == 1; }
            set { puntoRecogidaDia.VISITADO = value ? 1 : 0; }
        }

        public InsercionRecogidasVM()
        {
            puntoRecogidaDia = new gen_puntorecogidadia();
            pRecogida = new GEN_PUNTOSRECOGIDA();
            usuario = new gen_usuarios();

            listaPuntoRecogidaDia = new List<GEN_PUNTOSRECOGIDA>();
            listaRecogCliente = new List<RecogClientesVM>();
            listaUsuarios = new List<gen_usuarios>();
        }

    }
}