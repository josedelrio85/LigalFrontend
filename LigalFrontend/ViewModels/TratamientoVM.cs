using LigalFrontend.Models;
using System.Collections.Generic;

namespace LigalFrontend.ViewModels
{
    public class TratamientoVM
    {
        public gen_trata tratamiento { get; set; }
        public gen_antibioticos antibioticos { get; set; }

        public List<gen_antibioticos> listaAntibioticos { get; set; }

        public TratamientoVM()
        {
            tratamiento = new gen_trata();
            antibioticos = new gen_antibioticos();
        }
    }
}