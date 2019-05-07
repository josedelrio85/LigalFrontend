using JSV.XOne.Replica.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace LigalFrontend.Models
{
    [XOneTable]
    public partial class GEN_COORDENADASGPSMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> IDEMPRESA { get; set; }
        [XOneColumn]
        public Nullable<int> IDUSUARIO { get; set; }
        [XOneColumn]
        [Display(Name = "Latitud")]
        //[RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> LATITUDGPS { get; set; }
        [XOneColumn]
        [Display(Name = "Longitud")]
        //[RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> LONGITUDGPS { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        //[RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> RUMBOGPS { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FECHAHORAGPS { get; set; }
        [XOneColumn]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FECHAHORAPDA { get; set; }
        [XOneColumn]
        public Nullable<int> OPCIONES { get; set; }
        [XOneColumn]
        public Nullable<int> OP1 { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        [Display(Name = "Punto")]
        public string NPUNTO { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        //[RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> DISTANCIA { get; set; }
    }


    [XOneTable]
    public partial class GEN_DIARIOMATRICULAMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> IDUV { get; set; }
        [XOneColumn]
        [Display(Name = "Fecha"), Required(ErrorMessage = "Introduzca una fecha")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FECHA { get; set; }
        [XOneColumn]
        [Display(Name = "Km Iniciales"), Required(ErrorMessage = "Introduzca un Km Iniciales.")]
        [RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> KMINICIALES { get; set; }
        [XOneColumn]
        [Display(Name = "Km Finales"), Required(ErrorMessage = "Introduzca un Km Finales.")]
        [RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> KMFINALES { get; set; }
        [XOneColumn]
        [Display(Name = "Tarea")]
        public string IDTAREA { get; set; }
        [XOneColumn]
        [Display(Name = "Observación Tarea")]
        [StringLength(255)]
        public string TAREAS { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_observaMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Observación")]
        [StringLength(30), Required(ErrorMessage = "Introduzca una observación.")]
        public string OBSERVA { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_puntorecogidadiaMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Código Punto")]
        [Required(ErrorMessage = "Introduzca un Código Punto.")]
        public Nullable<int> IDPUNTO { get; set; }
        [XOneColumn]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FECHA { get; set; }
        [XOneColumn]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Introduzca un usuario.")]
        public Nullable<int> IDUSUARIO { get; set; }
        [XOneColumn]
        [Display(Name = "Programado")]
        public Nullable<int> PROGRAMADO { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        [Display(Name = "Visitado")]
        public Nullable<int> VISITADO { get; set; }
        [XOneColumn]
        [Display(Name = "Hora")]
        public string HORA { get; set; }
        [XOneColumn]
        [Display(Name = "Foto1")]
        public string FOTO1 { get; set; }
        [XOneColumn]
        [Display(Name = "Foto2")]
        public string FOTO2 { get; set; }
        [XOneColumn]
        [Display(Name = "Foto3")]
        public string FOTO3 { get; set; }
    }

    [XOneTable]
    public partial class GEN_PUNTOSRECOGIDAMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Código Punto")]
        [StringLength(11), Required(ErrorMessage = "Introduzca un Código Punto.")]
        public string CODIGOP { get; set; }
        [XOneColumn]
        [Display(Name = "Nombre Punto")]
        [StringLength(100), Required(ErrorMessage = "Introduzca un Nombre Punto.")]
        public string NOMBRE { get; set; }
        [XOneColumn]
        [Display(Name = "Longitud")]
        ////[RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> COORDX { get; set; }
        [XOneColumn]
        [Display(Name = "Latitud")]
        ////[RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> COORDY { get; set; }
        [XOneColumn]
        public Nullable<double> POSX { get; set; }
        [XOneColumn]
        public Nullable<double> POSY { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        public string DIRGPS { get; set; }
        [XOneColumn]
        [Display(Name = "Recogedor")]
        [Required(ErrorMessage = "Introduzca un Recogedor.")]
        public Nullable<int> IDUSUARIO { get; set; }
        [XOneColumn]
        public Nullable<int> IDUSUARIOR { get; set; }
        [XOneColumn]
        [Display(Name = "Descripción")]
        [StringLength(120)]
        public string DESCRIPCION { get; set; }
        [XOneColumn]
        public Nullable<double> DISTANCIA { get; set; }
    }

    [XOneTable]
    public partial class gen_recogidasRMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Cliente")]
        public Nullable<int> IDCLIENTE { get; set; }
        [XOneColumn]
        [Display(Name = "Estimadas")]
        public Nullable<int> GRESTIMADAS { get; set; }
        [XOneColumn]
        [Display(Name = "Entregadas")]
        public Nullable<int> GRADDEJA { get; set; }
        [XOneColumn]
        [Display(Name = "Recogidas")]
        public Nullable<int> GRADRECOGE { get; set; }
        [XOneColumn]
        [Display(Name = "Frascos")]
        public Nullable<int> NUMFRASCOS { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        public Nullable<int> IDRECOGIDA { get; set; }
        [XOneColumn]
        [Display(Name = "Observación")]
        public Nullable<int> IDOBSERVA { get; set; }
        [XOneColumn]
        [Display(Name = "Observaciones")]
        public string OBSERVACIONES { get; set; }
        [XOneColumn]
        [Display(Name = "Foto1")]
        public string FOTO1 { get; set; }
        [XOneColumn]
        [Display(Name = "Foto2")]
        public string FOTO2 { get; set; }
        [XOneColumn]
        [Display(Name = "Foto3")]
        public string FOTO3 { get; set; }
    }

    [XOneTable]
    public partial class GEN_RESULTADOQUINOLONASMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Producto")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un Producto.")]
        public string DESCRIPCION { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class GEN_SERIECOORDENADASMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> SERIEGAN { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FECHA { get; set; }
        [XOneColumn]
        public string COORDX { get; set; }
        [XOneColumn]
        public string COORDY { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        public string DIRGPS { get; set; }
        [XOneColumn]
        public Nullable<int> XONE { get; set; }
    }

    [XOneTable]
    public partial class GEN_TIPOVISITAMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public string TIPOVISITA { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class GEN_USUARIOSVEHICULOMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> IDUSUARIO { get; set; }
        [XOneColumn]
        public Nullable<int> IDMATRICULA { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class GEN_VEHICULOMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Matrícula")]
        [StringLength(50)]  //Required(ErrorMessage = "Introduzca una matrícula.")
        public string MATRICULA { get; set; }
        [XOneColumn]
        [Display(Name = "Modelo")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un modelo.")]
        public string MODELO { get; set; }
        [XOneColumn]
        [Display(Name = "Año compra")]
        [Required(ErrorMessage = "Introduzca un año de compra.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ANHOCOMPRA { get; set; }
        [XOneColumn]
        [Display(Name = "Tipo Combustible")]
        [StringLength(50)]
        public string TIPOCOMBUSTIBLE { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_clientesMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name ="Código Cliente")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un código de cliente.")]
        public string CODIGOC { get; set; }
        [XOneColumn]
        [Display(Name ="Nombre")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un nombre.")]
        public string NOMBREC { get; set; }
        [XOneColumn]
        [Display(Name = "Dirección")]
        [StringLength(50), Required(ErrorMessage = "Introduzca una dirección.")]
        public string DIRECCION { get; set; }
        [XOneColumn]
        [Display(Name = "Teléfono")]
        [StringLength(50)]
        public string TELEFONO { get; set; }
        [XOneColumn]
        [Display(Name = "Móvil")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un número de móvil.")]
        public string MOVIL { get; set; }
        [XOneColumn]
        [Display(Name = "Email")]
        [StringLength(255)]
        [EmailAddress(ErrorMessage = "Email incorrecto.")]
        public string EMAIL { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        [Display(Name = "Población")]
        [StringLength(50), Required(ErrorMessage = "Introduzca una población.")]
        public string POBLACION { get; set; }
        [XOneColumn]
        [Display(Name = "Provincia")]
        [StringLength(50), Required(ErrorMessage = "Introduzca una provincia.")]
        public string PROVINCIA { get; set; }
        [XOneColumn]
        [Display(Name = "Código Postal")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un código postal.")]
        public string CPOSTAL { get; set; }
    }

    [XOneTable]
    public partial class Gen_clientesenpuntoMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Introduzca un Cliente.")]
        public Nullable<int> IDCLIENTE { get; set; }
        [XOneColumn]
        [Display(Name = "Punto")]
        [Required(ErrorMessage = "Introduzca un Código Punto.")]
        public Nullable<int> IDPUNTO { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_empresaMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> IDPADRE { get; set; }
        [XOneColumn]
        public Nullable<int> IDMONEDA { get; set; }
        [XOneColumn]
        public Nullable<int> CODIGO { get; set; }
        [XOneColumn]
        public string ETIQUETA { get; set; }
        [XOneColumn]
        public string NOMBRE2 { get; set; }
        [XOneColumn]
        public string NIF { get; set; }
        [XOneColumn]
        public string TIPOVIA { get; set; }
        [XOneColumn]
        public string CALLE { get; set; }
        [XOneColumn]
        public string NUMERO { get; set; }
        [XOneColumn]
        public string ESCALERA { get; set; }
        [XOneColumn]
        public string PISO { get; set; }
        [XOneColumn]
        public string PUERTA { get; set; }
        [XOneColumn]
        public string POBLACION { get; set; }
        [XOneColumn]
        public string CP { get; set; }
        [XOneColumn]
        public string PROVINCIA { get; set; }
        [XOneColumn]
        public string TELEFONO { get; set; }
        [XOneColumn]
        public string FAX { get; set; }
        [XOneColumn]
        public string EMAIL { get; set; }
        [XOneColumn]
        public string URL { get; set; }
        [XOneColumn]
        public string CUENTACONT { get; set; }
        [XOneColumn]
        public string CUENTABANC { get; set; }
        [XOneColumn]
        public Nullable<int> RECARGO { get; set; }
        [XOneColumn]
        public Nullable<int> OPCIONES { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_estadosMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public string DESCRIPCION { get; set; }
        [XOneColumn]
        public string SIMBOLO { get; set; }
        [XOneColumn]
        public Nullable<int> PROPS { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_inspeccionesMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Fecha Hora Registro")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaHoraRegistro { get; set; }
        [XOneColumn]
        [Display(Name = "Fecha Visita")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaHoraVisita { get; set; }
        [XOneColumn]
        [Display(Name = "Tipo Inspección")]
        public string TipoInspeccion { get; set; }
        [XOneColumn]
        [Display(Name = "Situación")]
        public string Situacion { get; set; }
        [XOneColumn]
        [Display(Name = "Usuario")]
        public Nullable<int> IDUsuario { get; set; }
        [XOneColumn]
        [Display(Name = "Muestra Recogida")]
        public string IDMuestraRecogida { get; set; }
        [XOneColumn]
        [Display(Name = "Explotación")]
        public string IDExplotacion { get; set; }
        [XOneColumn]
        [Display(Name = "Muestra Origen")]
        public string IDMuestraOrigen { get; set; }
        [XOneColumn]
        [Display(Name = "Fecha Muestra Origen")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaMuestraOrigen { get; set; }
        [XOneColumn]
        [Display(Name = "Fecha Analisis Origen")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaAnalisisOrigen { get; set; }
        [XOneColumn]
        [Display(Name = "Resultado CHARM")]
        public string ResultadoCHARM { get; set; }
        [XOneColumn]
        [Display(Name = "Código Industria")]
        public string IDIndustria { get; set; }
        [XOneColumn]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
        [XOneColumn]
        public Nullable<int> Informado { get; set; }
        [XOneRowIDColumn]
        //[StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        public Nullable<int> Actualizado { get; set; }
        [XOneColumn]
        [Display(Name = "Resultado")]
        public string Resultado { get; set; }
        [XOneColumn]
        [Display(Name = "Fecha Siguiente Visita")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaSiguienteVisita { get; set; }
        [XOneColumn]
        public string Nombre { get; set; }
        [XOneColumn]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [XOneColumn]
        [Display(Name = "Población")]
        public string Poblacion { get; set; }
        [XOneColumn]
        public string Provincia { get; set; }
        [XOneColumn]
        [Display(Name = "Teléfono 1")]
        public string Telefono1 { get; set; }
        [XOneColumn]
        [Display(Name = "Teléfono 2")]
        public string Telefono2 { get; set; }
        [XOneColumn]
        [Display(Name = "Industria")]
        public string INDUSTRIAN { get; set; }
        [XOneColumn]
        public Nullable<int> IDORIGEN { get; set; }
        [XOneColumn]
        [Display(Name = "Destrucción presencia inspector")]
        public Nullable<int> Destrucion { get; set; }
        [XOneColumn]
        [Display(Name = "Nº Visita")]
        public Nullable<int> NumeroVisita { get; set; }
        [XOneColumn]
        [Display(Name = "Litros Leche")]
        public Nullable<int> LitrosLeche { get; set; }
        [XOneColumn]
        [Display(Name = "Nº Ordeñadoras")]
        public Nullable<int> NumMuxiduras { get; set; }
        [XOneColumn]
        [Display(Name = "Serie Ganadero")]
        public Nullable<int> SERIEGANADERO { get; set; }
        [XOneColumn]
        [Display(Name = "Observaciones")]
        public Nullable<int> IDOBSERVA { get; set; }
        [XOneColumn]
        [Display(Name = "Mamitis")]
        public Nullable<int> MAMITE { get; set; }
        [XOneColumn]
        public Nullable<int> MAMITESN { get; set; }
        [XOneColumn]
        [Display(Name = "De otras enfermedades")]
        public Nullable<int> OUTRAS { get; set; }
        [XOneColumn]
        public Nullable<int> OUTRASSN { get; set; }
        [XOneColumn]
        [Display(Name = "Parto reciente")]
        public Nullable<int> PARTO { get; set; }
        [XOneColumn]
        public Nullable<int> PARTOSN { get; set; }
        [XOneColumn]
        [Display(Name = "Asociación varios antimicrobianos")]
        public Nullable<int> ASOCIACION { get; set; }
        [XOneColumn]
        public Nullable<int> ASOCIACIONSN { get; set; }
        [XOneColumn]
        [Display(Name = "Empleo dosis más altas que las recomendadas")]
        public Nullable<int> EMPDOSIS { get; set; }
        [XOneColumn]
        [Display(Name = "Tratamiento intramamario, se aprovechó leche tetos NO tratados")]
        public Nullable<int> TRATAINTRAM { get; set; }
        [XOneColumn]
        [Display(Name = "Orden de ordeño incorrecta")]
        public Nullable<int> ORDEMUXI { get; set; }
        [XOneColumn]
        [Display(Name = "Ordeño por error de un animal tratado")]
        public Nullable<int> MUXIERRO { get; set; }
        [XOneColumn]
        [Display(Name = "Incorporación animal comprado y comercialización leche")]
        public Nullable<int> INCORPORACION { get; set; }
        [XOneColumn]
        [Display(Name = "No se identifican los animales tratados")]
        public Nullable<int> NOIDENTIF { get; set; }
        [XOneColumn]
        [Display(Name = "Se aplicó tratamiento animal equivocado")]
        public Nullable<int> TRATANIERR { get; set; }
        [XOneColumn]
        [Display(Name = "Error comunicación persona que trató y que la ordeñó")]
        public Nullable<int> ERRCOMUNI { get; set; }
        [XOneColumn]
        [Display(Name = "No es posible conocer la causa a partir de la información del ganadero")]
        public Nullable<int> NONCAUSA { get; set; }
        [XOneColumn]
        [Display(Name = "Algun tratamiento registrado en los últimos 15 días")]
        public Nullable<int> ALGUNTRATA { get; set; }
        [XOneColumn]
        [Display(Name = "No compró el tratamiento")]
        public Nullable<int> NONCOMPROBAT { get; set; }
        [XOneColumn]
        [Display(Name = "No administró el tratamiento")]
        public Nullable<int> NONADMINT { get; set; }
        [XOneColumn]
        [Display(Name = "No fue posible realizar la encuesta")]
        public Nullable<int> NONPPOSIBLEE { get; set; }
        [XOneColumn]
        [Display(Name = "Observaciones tratamiento")]
        public string OBSTRATA { get; set; }
        [XOneColumn]
        [Display(Name = "Vacas lactación")]
        public Nullable<int> VacasLacta { get; set; }
        [XOneColumn]
        [Display(Name = "No destrucción presencia inspector")]
        public Nullable<int> NONDESTRUCION { get; set; }
        [XOneColumn]
        public Nullable<int> MODIFICADO { get; set; }
        [XOneColumn]
        [Display(Name = "Longitud")]
        public string COORDX { get; set; }
        [XOneColumn]
        [Display(Name = "Latitud")]
        public string COORDY { get; set; }
        [XOneColumn]
        public Nullable<double> POSX { get; set; }
        [XOneColumn]
        public Nullable<double> POSY { get; set; }
        [XOneColumn]
        public Nullable<int> INCORPORACACION { get; set; }
        [XOneColumn]
        public Nullable<int> NONPOSIBLEE { get; set; }
        [XOneColumn]
        public string DIRGPS { get; set; }
        [XOneColumn]
        [Display(Name = "Resultado Quinolona")]
        public string RESULTADOQUINO { get; set; }
        [XOneColumn]
        [Display(Name = "Solicita test lento")]
        public Nullable<int> SOLICITATEST { get; set; }
        [XOneColumn]
        [Display(Name = "XOne")]
        public Nullable<int> XONE { get; set; }
        [XOneColumn]
        [Display(Name = "Foto1")]
        public string FOTO1 { get; set; }
        [XOneColumn]
        [Display(Name = "Foto2")]
        public string FOTO2 { get; set; }
        [XOneColumn]
        [Display(Name = "Foto3")]
        public string FOTO3 { get; set; }
    }

    [XOneTable]
    public partial class gen_motivosMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Descripción")]
        [StringLength(50), Required(ErrorMessage = "Introduzca una Descripción.")]
        public string DESCRIPCION { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_ObsInspMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Observación")]
        [StringLength(50), Required(ErrorMessage = "Introduzca una observación.")]
        public string OBSERVA { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_productosMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Código Producto")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un Código Producto.")]
        public string CODIGOPRO { get; set; }
        [XOneColumn]
        [Display(Name = "Producto")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un Producto.")]
        public string PRODUCTO { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_puntosenrutaMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> IDPUNTO { get; set; }
        [XOneColumn]
        public Nullable<int> IDRUTA { get; set; }
        [XOneColumn]
        public Nullable<int> NUMGRADILLAS { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_recogidasMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> IDRUTA { get; set; }
        [XOneColumn]
        public Nullable<int> IDPUNTO { get; set; }
        [XOneColumn]
        public Nullable<int> IDUSUARIO { get; set; }
        [XOneColumn]
        public Nullable<int> IDCLIENTE { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FECHA { get; set; }
        [XOneColumn]
        public string HORA { get; set; }
        [XOneColumn]
        public Nullable<int> GRADDEJA { get; set; }
        [XOneColumn]
        public string OBSERVACIONES { get; set; }
        [XOneColumn]
        public Nullable<int> GRADRECOGE { get; set; }
        [XOneColumn]
        public Nullable<int> NUMFRASCOS { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        public Nullable<int> IDSERIE { get; set; }
        [XOneColumn]
        public Nullable<int> IDMONEDA { get; set; }
        [XOneColumn]
        public string NOMCLIENTE { get; set; }
        [XOneColumn]
        public string DIRCLIENTE { get; set; }
        [XOneColumn]
        public Nullable<int> IDTIPODOC { get; set; }
        [XOneColumn]
        public Nullable<int> NUMERO { get; set; }
        [XOneColumn]
        public string NUMCOMPLETO { get; set; }
        [XOneColumn]
        public Nullable<int> IDEMPRESA { get; set; }
        [XOneColumn]
        public Nullable<int> GRESTIMADAS { get; set; }
        [XOneColumn]
        public Nullable<int> IDMOTIVO { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FPROGRAMADA { get; set; }
        [XOneColumn]
        public Nullable<int> EXECUTED { get; set; }
        [XOneColumn]
        public Nullable<int> PROGRAMADA { get; set; }
        [XOneColumn]
        public Nullable<int> IDLIGAL { get; set; }
        [XOneColumn]
        public Nullable<int> MAIL { get; set; }
    }

    [XOneTable]
    public partial class gen_resultadosMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Descripción")]
        [StringLength(50), Required(ErrorMessage = "Introduzca una Descripción.")]
        public string DESCRIPCION { get; set; }
        [XOneColumn]
        public Nullable<int> TIPO { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_tipouserMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> CODIGO { get; set; }
        [XOneColumn]
        public string ETIQUETA { get; set; }
        [XOneColumn]
        public Nullable<int> DERECHOS { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_usuariosMetadata
    {
        [Display(Name = "Usuario")]
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> IDEMPRESA { get; set; }
        [XOneColumn]
        public Nullable<int> IDTIPO { get; set; }
        [XOneColumn]
        [Display(Name = "Login")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un login.")]
        public string LOGIN { get; set; }
        [XOneColumn]
        [Display(Name = "Password")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un password.")]
        [DataType(DataType.Password)]
        public string PWD { get; set; }
        [XOneColumn]
        [Display(Name = "Nombre")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un Nombre.")]
        public string NOMBRE { get; set; }
        [XOneColumn]
        [Display(Name = "Apellido 1")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un primer apellido.")]
        public string APELLIDO1 { get; set; }
        [XOneColumn]
        [Display(Name = "Apellido 2")]
        [StringLength(50), Required(ErrorMessage = "Introduzca un segundo apellido.")]
        public string APELLIDO2 { get; set; }
        [XOneColumn]
        [Display(Name = "Derechos")]
        [Range(1, 3, ErrorMessage = "Introduzca un valor entre 1 y 3.")]
        [Required(ErrorMessage = "Introduzca un valor para derechos.")]
        public Nullable<int> DERECHOS { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        [Display(Name = "Código")]
        [Range(0, int.MaxValue, ErrorMessage = "Introduzca un número entero válido para Código.")]
        public Nullable<int> CODIGO { get; set; }
        [XOneColumn]
        [Display(Name = "Tipo de Usuario")]
        [StringLength(50)]
        public string USERTYPE { get; set; }
    }

    [XOneTable]
    public partial class gen_visitasMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DATAFICHA { get; set; }
        [XOneColumn]
        public string GANDEIRO { get; set; }
        [XOneColumn]
        public Nullable<int> SERIE { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DATAMOSTRA { get; set; }
        [XOneColumn]
        public string RESULTADO { get; set; }
        [XOneColumn]
        public Nullable<int> NUMVACAS { get; set; }
        [XOneColumn]
        public Nullable<int> LITROSLEITE { get; set; }
        [XOneColumn]
        public string RESULTADOCHARM { get; set; }
        [XOneColumn]
        public Nullable<bool> MAMITES { get; set; }
        [XOneColumn]
        public Nullable<bool> PRESCRIPCION { get; set; }
        [XOneColumn]
        public Nullable<bool> REXTRATAMENTO { get; set; }
        [XOneColumn]
        public Nullable<bool> RECEITAS { get; set; }
        [XOneColumn]
        public Nullable<bool> ALMACENAMED { get; set; }
        [XOneColumn]
        public Nullable<bool> TEMPESPMAL { get; set; }
        [XOneColumn]
        public Nullable<bool> TEMPESPINCUMP { get; set; }
        [XOneColumn]
        public Nullable<bool> TEMPESPAXUST { get; set; }
        [XOneColumn]
        public Nullable<bool> TARTASECFORA { get; set; }
        [XOneColumn]
        public Nullable<bool> OUTROSMEDIC { get; set; }
        [XOneColumn]
        public Nullable<bool> TRATAINTRAMAMA { get; set; }
        [XOneColumn]
        public Nullable<bool> ORDMUXINCORREC { get; set; }
        [XOneColumn]
        public Nullable<bool> UTILMEDINADEC { get; set; }
        [XOneColumn]
        public Nullable<bool> ANIMALNOVO { get; set; }
        [XOneColumn]
        public Nullable<bool> OUTROS { get; set; }
        [XOneColumn]
        public string OBSERVA { get; set; }
        [XOneColumn]
        public Nullable<int> ACTUALIZADO { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_visitavetMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> IDUSUARIO { get; set; }
        [XOneColumn]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FECHA { get; set; }
        [XOneColumn]
        [Display(Name = "Serie Ganadero")]
        public Nullable<int> SERIEGAN { get; set; }
        [XOneColumn]
        [Display(Name = "Longitud")]
        public string COORDX { get; set; }
        [XOneColumn]
        [Display(Name = "Latitud")]
        public string COORDY { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        //[RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> POSX { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        //[RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> POSY { get; set; }
        [XOneColumn]
        [Display(Name = "Observaciones")]
        public string OBSERVA { get; set; }
        [XOneColumn]
        [Display(Name = "Actualizado")]
        public Nullable<int> ACTUALIZADO { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        [Display(Name = "")]
        //[RegularExpression("^[0-9]*(\\,)?[0-9]+$", ErrorMessage = "Utilice a , como separador decimal.")]
        public Nullable<double> PRECISION { get; set; }
        [XOneColumn]
        public Nullable<int> IDTIPOINCIDENCIA { get; set; }
        [XOneColumn]
        [Display(Name = "Transportista / Inspector")]
        public string TRANSPINSP { get; set; }
        [XOneColumn]
        [Display(Name = "Foto1")]
        public string FOTO1 { get; set; }
        [XOneColumn]
        [Display(Name = "Foto2")]
        public string FOTO2 { get; set; }
        [XOneColumn]
        [Display(Name = "Foto3")]
        public string FOTO3 { get; set; }
    }

    [XOneTable]
    public partial class Gen_tiposDocumentoMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public string SIMBOLO { get; set; }
        [XOneColumn]
        public string DESCRIPCION { get; set; }
        [XOneColumn]
        public string REPORTE { get; set; }
        [XOneColumn]
        public string OPCIONES { get; set; }
        [XOneColumn]
        public string PLANTILLA { get; set; }
        [XOneColumn]
        public string LINEAS { get; set; }
        [XOneColumn]
        public Nullable<int> ENCABEZADO { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_trataMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> IDINSP { get; set; }
        [XOneColumn]
        [Display(Name ="Fecha")]
        public Nullable<System.DateTime> FECHAT { get; set; }
        [XOneColumn]
        public Nullable<int> IDTRATA { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_antibioticosMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Producto")]
        public string NomeProducto { get; set; }
        [XOneColumn]
        public string Aplicacion { get; set; }
        [XOneColumn]
        public Nullable<int> IDCODLAB { get; set; }
        [XOneColumn]
        public Nullable<int> CODLAB { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class GEN_TAREASDIARIOMetadata
    {
        public int ID { get; set; }
        [Display(Name ="Descripción")]
        public string DESCRIPCION { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }

    [XOneTable]
    public partial class gen_productosgrMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        public Nullable<int> IDPRODUCTO { get; set; }
        [XOneColumn]
        [Display(Name = "Cantidad")]
        public Nullable<int> CANTIDAD { get; set; }
        [XOneColumn]
        public Nullable<double> CTEMPERATURA { get; set; }
        [XOneColumn]
        [Display(Name = "Temperatura")]
        public Nullable<double> TEMPERATURA { get; set; }
        [XOneColumn]
        public Nullable<int> IDRECOGIDA { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
        [XOneColumn]
        public Nullable<int> IDRECOGIDAN { get; set; }
    }


    [XOneTable]
    public partial class gen_tipoincidenciarutaMetadata
    {
        public int ID { get; set; }
        [XOneColumn]
        [Display(Name = "Incidencia")]
        public string INCIDENCIA { get; set; }
        [XOneRowIDColumn]
        [StringLength(60, MinimumLength = 35, ErrorMessage = "Erro no ROWID, contacte co administrador.")]
        public string ROWID { get; set; }
    }
}