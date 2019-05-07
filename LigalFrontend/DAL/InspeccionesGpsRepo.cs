using LigalFrontend.Models;
using LigalFrontend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;
using LigalFrontend.Models.Buscador;

namespace LigalFrontend.DAL
{
    public class InspeccionesGpsRepo : IViewModelRepository<InspeccionesGpsVM, buscadorInspeccionesGps>
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, gen_inspecciones> repo;
        private IEnumerable<InspeccionesGpsVM> vm;

        public InspeccionesGpsRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, gen_inspecciones>();
        }

        public IEnumerable<InspeccionesGpsVM> consultaBase()
        {
            vm = from ins in context.gen_inspecciones
                 join usu in context.gen_usuarios on ins.IDUsuario equals usu.ID
                 select new InspeccionesGpsVM
                 {
                     inspeccion = ins,
                     usuario = usu,
                     buscador = new Models.Buscador.buscadorInspeccionesGps()

                 };

            return vm;
        }

        public InspeccionesGpsVM getById(int? id)
        {
            IQueryable<InspeccionesGpsVM> vmq = consultaBase().AsQueryable();
            InspeccionesGpsVM pr = vmq.Where(x => x.inspeccion.ID == id).SingleOrDefault();
            return pr;
        }

        public IEnumerable<InspeccionesGpsVM> getInspeccionesQuinolonas()
        {
            IQueryable<InspeccionesGpsVM> vmq = consultaBase().AsQueryable();
            //t1.TipoInspeccion=1 AND RESULTADOQUINO IS NOT NULL
            IEnumerable<InspeccionesGpsVM> pr = vmq.Where(x => x.inspeccion.TipoInspeccion == "1")
                                .Where(x => !x.inspeccion.RESULTADOQUINO.Equals(string.Empty))
                                .ToList();

            return pr;
        }

        public IEnumerable<InspeccionesGpsVM> getByParametro(buscadorInspeccionesGps param)
        {
            IQueryable<InspeccionesGpsVM> vmq = consultaBase().AsQueryable();

            vmq = aplicaParametros(vmq, param);

            IEnumerable<InspeccionesGpsVM> ievm = vmq.OrderBy(x => x.inspeccion.FechaHoraVisita).ToList();
            return vmq.ToList();
        }

        public IEnumerable<InspeccionesGpsVM> getByParametroQuino(buscadorInspeccionesGps param)
        {
            IQueryable<InspeccionesGpsVM> vmq = consultaBase().AsQueryable();

            vmq = vmq.Where(x => x.inspeccion.TipoInspeccion == "1")
                .Where(x => !x.inspeccion.RESULTADOQUINO.Equals(string.Empty) && x.inspeccion.RESULTADOQUINO != null);

            vmq = aplicaParametros(vmq, param);

            IEnumerable<InspeccionesGpsVM> ievm = vmq.OrderBy(x => x.inspeccion.FechaHoraVisita).ToList();
            return ievm;
        }

        protected IQueryable<InspeccionesGpsVM> aplicaParametros(IQueryable<InspeccionesGpsVM> vmq, buscadorInspeccionesGps param)
        {
            if (param.idUsuario != null)
            {
                int idUsuario = Int32.Parse(param.idUsuario);
                vmq = vmq.Where(x => x.inspeccion.IDUsuario == idUsuario);
            }

            if (param.TipoInspeccion != null)
            {
                string resu = Functions.Functions.CleanInput(param.TipoInspeccion.ToString());
                vmq = vmq.Where(x => x.inspeccion.TipoInspeccion.Contains(resu));
            }

            if (param.FechaHoraVisitaI != null)
            {
                System.DateTime dIni = Functions.Functions.textoToFecha(param.FechaHoraVisitaI);
                vmq = vmq.Where(x => x.inspeccion.FechaHoraVisita > dIni);
            }

            if (param.FechaHoraVisitaF != null)
            {
                System.DateTime dFin = Functions.Functions.textoToFecha(param.FechaHoraVisitaF);
                vmq = vmq.Where(x => x.inspeccion.FechaHoraVisita <= dFin);
            }

            if (param.Industria != null)
            {
                string[] separatingChars = { "," };
                List<string> ids = new List<string>();

                if (param.Industria.Contains(","))
                {
                    ids = param.Industria.Split(separatingChars, System.StringSplitOptions.None).ToList();
                }
                else
                {
                    ids.Add(param.Industria);
                }

                //Librería LINQKit
                var predicate = LinqKit.PredicateBuilder.New<InspeccionesGpsVM>();  
                foreach (string id in ids)
                {
                    predicate = predicate.Or(x => x.inspeccion.IDIndustria == id);
                }

                vmq = vmq.Where(predicate);
            }

            return vmq;
        }


        public IEnumerable<InspeccionesGpsVM> getSorted(buscadorInspeccionesGps paramFiltro, objetoOrdenMapa paramOrden, int direccion, bool quino = false)
        {
            IEnumerable<InspeccionesGpsVM> resuFiltro = (!quino) ? getByParametro(paramFiltro) : getByParametroQuino(paramFiltro); 

            if (paramOrden.coleccion == "usuario" && paramOrden.nombreCampo == "NOMBRE")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.usuario.NOMBRE) : resuFiltro.OrderByDescending(x => x.usuario.NOMBRE);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "Nombre")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.Nombre) : resuFiltro.OrderByDescending(x => x.inspeccion.Nombre);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "INDUSTRIAN")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.INDUSTRIAN) : resuFiltro.OrderByDescending(x => x.inspeccion.INDUSTRIAN);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "FechaHoraVisita")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.FechaHoraVisita) : resuFiltro.OrderByDescending(x => x.inspeccion.FechaHoraVisita);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "SERIEGANADERO")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.SERIEGANADERO) : resuFiltro.OrderByDescending(x => x.inspeccion.SERIEGANADERO);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "Poblacion")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.Poblacion) : resuFiltro.OrderByDescending(x => x.inspeccion.Poblacion);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "RESULTADOQUINO")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.RESULTADOQUINO) : resuFiltro.OrderByDescending(x => x.inspeccion.RESULTADOQUINO);
            }

            if (paramOrden.coleccion == "inspeccion" && paramOrden.nombreCampo == "ResultadoCHARM")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.inspeccion.ResultadoCHARM) : resuFiltro.OrderByDescending(x => x.inspeccion.ResultadoCHARM);
            }
            
            return resuFiltro.ToList();
        }

        public void Insert(InspeccionesGpsVM vm)
        {
            vm.inspeccion.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.inspeccion);
        }

        public void Update(InspeccionesGpsVM vm)
        {
            repo.Update(vm.inspeccion);
        }

        public void Delete(InspeccionesGpsVM vm)
        {
            repo.Delete(vm.inspeccion);
        }

        public void Save()
        {
            repo.Save();
        }

        public void Dispose()
        {
            repo.Dispose();
        }

    }
}