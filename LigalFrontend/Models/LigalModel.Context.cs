﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LigalFrontend.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LigalEntities : DbContext
    {
        public LigalEntities()
            : base("name=LigalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<gen_actionlogs> gen_actionlogs { get; set; }
        public virtual DbSet<gen_antibioticos> gen_antibioticos { get; set; }
        public virtual DbSet<gen_bolsas> gen_bolsas { get; set; }
        public virtual DbSet<gen_bolsasrecogida> gen_bolsasrecogida { get; set; }
        public virtual DbSet<gen_clientes> gen_clientes { get; set; }
        public virtual DbSet<Gen_clientesenpunto> Gen_clientesenpunto { get; set; }
        public virtual DbSet<gen_conectargps> gen_conectargps { get; set; }
        public virtual DbSet<GEN_CONSOLAREPLICA> GEN_CONSOLAREPLICA { get; set; }
        public virtual DbSet<GEN_CONSOLAVBS> GEN_CONSOLAVBS { get; set; }
        public virtual DbSet<GEN_CONTROLTEMPERATURA> GEN_CONTROLTEMPERATURA { get; set; }
        public virtual DbSet<GEN_COORDENADASGPS> GEN_COORDENADASGPS { get; set; }
        public virtual DbSet<Gen_Correos> Gen_Correos { get; set; }
        public virtual DbSet<gen_detvisita> gen_detvisita { get; set; }
        public virtual DbSet<GEN_DIARIOMATRICULA> GEN_DIARIOMATRICULA { get; set; }
        public virtual DbSet<gen_Distancia> gen_Distancia { get; set; }
        public virtual DbSet<gen_empresa> gen_empresa { get; set; }
        public virtual DbSet<gen_estados> gen_estados { get; set; }
        public virtual DbSet<gen_globalsearch> gen_globalsearch { get; set; }
        public virtual DbSet<gen_gradillas> gen_gradillas { get; set; }
        public virtual DbSet<gen_inicioruta> gen_inicioruta { get; set; }
        public virtual DbSet<gen_inspecciones> gen_inspecciones { get; set; }
        public virtual DbSet<gen_laboratorios> gen_laboratorios { get; set; }
        public virtual DbSet<gen_monedas> gen_monedas { get; set; }
        public virtual DbSet<gen_motivos> gen_motivos { get; set; }
        public virtual DbSet<gen_observa> gen_observa { get; set; }
        public virtual DbSet<gen_ObsInsp> gen_ObsInsp { get; set; }
        public virtual DbSet<gen_perifericos> gen_perifericos { get; set; }
        public virtual DbSet<gen_productos> gen_productos { get; set; }
        public virtual DbSet<gen_productosgr> gen_productosgr { get; set; }
        public virtual DbSet<gen_programacion> gen_programacion { get; set; }
        public virtual DbSet<gen_puntorecogidadia> gen_puntorecogidadia { get; set; }
        public virtual DbSet<gen_puntosenruta> gen_puntosenruta { get; set; }
        public virtual DbSet<GEN_PUNTOSRECOGIDA> GEN_PUNTOSRECOGIDA { get; set; }
        public virtual DbSet<gen_recogidas> gen_recogidas { get; set; }
        public virtual DbSet<gen_recogidasR> gen_recogidasR { get; set; }
        public virtual DbSet<GEN_RESULTADOQUINOLONAS> GEN_RESULTADOQUINOLONAS { get; set; }
        public virtual DbSet<gen_resultados> gen_resultados { get; set; }
        public virtual DbSet<gen_rutasrecogida> gen_rutasrecogida { get; set; }
        public virtual DbSet<GEN_SERIECOORDENADAS> GEN_SERIECOORDENADAS { get; set; }
        public virtual DbSet<gen_series> gen_series { get; set; }
        public virtual DbSet<GEN_TAREASDIARIO> GEN_TAREASDIARIO { get; set; }
        public virtual DbSet<Gen_tiposDocumento> Gen_tiposDocumento { get; set; }
        public virtual DbSet<gen_tipouser> gen_tipouser { get; set; }
        public virtual DbSet<GEN_TIPOVISITA> GEN_TIPOVISITA { get; set; }
        public virtual DbSet<gen_trata> gen_trata { get; set; }
        public virtual DbSet<gen_usuarios> gen_usuarios { get; set; }
        public virtual DbSet<GEN_USUARIOSVEHICULO> GEN_USUARIOSVEHICULO { get; set; }
        public virtual DbSet<GEN_VEHICULO> GEN_VEHICULO { get; set; }
        public virtual DbSet<gen_visitas> gen_visitas { get; set; }
        public virtual DbSet<gen_visitavet> gen_visitavet { get; set; }
        public virtual DbSet<master_replica_queue> master_replica_queue { get; set; }
        public virtual DbSet<rl> rl { get; set; }
        public virtual DbSet<GEN_TIPOINCIDENCIARUTA> GEN_TIPOINCIDENCIARUTA { get; set; }
    }
}
