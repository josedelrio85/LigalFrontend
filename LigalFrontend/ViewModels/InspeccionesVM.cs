using LigalFrontend.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.ViewModels
{
    public class InspeccionesVM
    {
        public gen_inspecciones inspeccion { get; set; }
        public gen_ObsInsp observacionInsp { get; set; }

        public InspeccionesVM()
        {
            inspeccion = new gen_inspecciones();
            observacionInsp = new gen_ObsInsp();
        }

        public List<TratamientoVM> listaTratamientos { get; set; }

        public List<gen_resultados> listaResultados { get; set; }
        public List<GEN_RESULTADOQUINOLONAS> listaResultadosQuinolonas { get; set; }
        public List<gen_ObsInsp> listaObservaciones { get; set; }
        public List<gen_usuarios> listaUsuarios { get; set; }

        [Display(Name = "Resultado Quinolona")]
        public int map_IdResultadoQuino { get; set; }
        [Display(Name = "Resultado CHARM")]
        public int map_IdResultadoCharm { get; set; }

        public bool BoolXONE
        {
            get { return inspeccion.XONE == 1; }
            set { inspeccion.XONE = value ? 1 : 0; }
        }

        public bool BoolDestrucion
        {
            get { return inspeccion.Destrucion == 1; }
            set { inspeccion.Destrucion = value ? 1 : 0; }
        }

        public bool BoolMAMITE
        {
            get { return inspeccion.MAMITE == 1; }
            set { inspeccion.MAMITE = value ? 1 : 0; }
        }

        public bool BoolMAMITESN
        {
            get { return inspeccion.MAMITESN == 1; }
            set { inspeccion.MAMITESN = value ? 1 : 0; }
        }

        public bool BoolOUTRAS
        {
            get { return inspeccion.OUTRAS == 1; }
            set { inspeccion.OUTRAS = value ? 1 : 0; }
        }

        public bool BoolPARTO
        {
            get { return inspeccion.PARTO == 1; }
            set { inspeccion.PARTO = value ? 1 : 0; }
        }

        public bool BoolASOCIACION
        {
            get { return inspeccion.ASOCIACION == 1; }
            set { inspeccion.ASOCIACION = value ? 1 : 0; }
        }

        public bool BoolEMPDOSIS
        {
            get { return inspeccion.EMPDOSIS == 1; }
            set { inspeccion.EMPDOSIS = value ? 1 : 0; }
        }

        public bool BoolTRATAINTRAM
        {
            get { return inspeccion.TRATAINTRAM == 1; }
            set { inspeccion.TRATAINTRAM = value ? 1 : 0; }
        }

        public bool BoolORDEMUXI
        {
            get { return inspeccion.ORDEMUXI == 1; }
            set { inspeccion.ORDEMUXI = value ? 1 : 0; }
        }

        public bool BoolMUXIERRO
        {
            get { return inspeccion.MUXIERRO == 1; }
            set { inspeccion.MUXIERRO = value ? 1 : 0; }
        }

        public bool BoolINCORPORACION
        {
            get { return inspeccion.INCORPORACION == 1; }
            set { inspeccion.INCORPORACION = value ? 1 : 0; }
        }

        public bool BoolNOIDENTIF
        {
            get { return inspeccion.NOIDENTIF == 1; }
            set { inspeccion.NOIDENTIF = value ? 1 : 0; }
        }

        public bool BoolTRATANIERR
        {
            get { return inspeccion.TRATANIERR == 1; }
            set { inspeccion.TRATANIERR = value ? 1 : 0; }
        }

        public bool BoolERRCOMUNI
        {
            get { return inspeccion.ERRCOMUNI == 1; }
            set { inspeccion.ERRCOMUNI = value ? 1 : 0; }
        }

        public bool BoolNONCAUSA
        {
            get { return inspeccion.NONCAUSA == 1; }
            set { inspeccion.ERRCOMUNI = value ? 1 : 0; }
        }

        public bool BoolALGUNTRATA
        {
            get { return inspeccion.ALGUNTRATA == 1; }
            set { inspeccion.ALGUNTRATA = value ? 1 : 0; }
        }

        public bool BoolNONCOMPROBAT
        {
            get { return inspeccion.NONCOMPROBAT == 1; }
            set { inspeccion.NONCOMPROBAT = value ? 1 : 0; }
        }

        public bool BoolNONADMINT
        {
            get { return inspeccion.NONADMINT == 1; }
            set { inspeccion.NONADMINT = value ? 1 : 0; }
        }

        public bool BoolNONPOSIBLEE
        {
            get { return inspeccion.NONPOSIBLEE == 1; }
            set { inspeccion.NONPOSIBLEE = value ? 1 : 0; }
        }

        public bool BoolNONDESTRUCION
        {
            get { return inspeccion.NONDESTRUCION == 1; }
            set { inspeccion.NONDESTRUCION = value ? 1 : 0; }
        }

        
        [Display(Name = "Bloqueado")]
        public bool map_SITUA1 { get; set; }
        [Display(Name = "Liberado")]
        public bool map_SITUA2 { get; set; }
        [Display(Name = "En espera")]
        public bool map_SITUA3 { get; set; }
        [Display(Name = "Pend. Xunta")]
        public bool map_SITUA4 { get; set; }

        [Display(Name = "Si")]
        public bool map_MAMITES { get; set; }
        [Display(Name = "No")]
        public bool map_MAMITEN { get; set; }

        [Display(Name = "Si")]
        public bool map_OUTRASS { get; set; }
        [Display(Name = "No")]
        public bool map_OUTRASN { get; set; }

        [Display(Name = "Si")]
        public bool map_PARTOS { get; set; }
        [Display(Name = "No")]
        public bool map_PARTON { get; set; }

        [Display(Name = "Si")]
        public bool map_ASOCIACIONS { get; set; }
        [Display(Name = "No")]
        public bool map_ASOCIACIONN { get; set; }

        [Display(Name = "Si")]
        public bool map_ALGUNTRATAS { get; set; }
        [Display(Name = "No")]
        public bool map_ALGUNTRATAN { get; set; }


        public ImageSliderVM listaImagenes { get; set; }
    }
}