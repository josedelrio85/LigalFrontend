using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LigalFrontend.DAL
{
    public class DiarioRepo : IViewModelRepository<DiarioMatriculaVM, buscadorDiario>, IDisposable
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, GEN_DIARIOMATRICULA> repo;
        private IEnumerable<DiarioMatriculaVM> vm;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DiarioRepo));
        private int maxRecords = 500;

        public DiarioRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, GEN_DIARIOMATRICULA>();
        }

        public IEnumerable<DiarioMatriculaVM> consultaBase()
        {
            var sql = from dia in context.GEN_DIARIOMATRICULA
                      join usuv in context.GEN_USUARIOSVEHICULO on dia.IDUV equals usuv.ID
                      join user in context.gen_usuarios on usuv.IDUSUARIO equals user.ID
                      join veh in context.GEN_VEHICULO on usuv.IDMATRICULA equals veh.ID
                      join tar in context.GEN_TAREASDIARIO on dia.IDTAREA equals tar.ID.ToString() into JoinedDiaTar
                      from tar in JoinedDiaTar.DefaultIfEmpty()
                      select new DiarioMatriculaVM
                      {
                          diario = dia,
                          usuVeh = new UsuariosVehiculoVM()
                          {
                              usuVehiculo = usuv,
                              usuario = user,
                              vehiculo = veh
                          },
                          tarea = tar
                      };

            return sql;
        }

        public List<DiarioMatriculaVM> getAll()
        {
            IQueryable<DiarioMatriculaVM> vmq = consultaBase().AsQueryable();
            return vmq.OrderByDescending(x => x.diario.FECHA).Take(maxRecords).ToList();
        }

        public DiarioMatriculaVM getById(int? id)
        {
            IQueryable<DiarioMatriculaVM> vmq = consultaBase().AsQueryable();
            DiarioMatriculaVM pr = vmq.Where(x => x.diario.ID == id).SingleOrDefault();
            return pr;
        }

        public GEN_DIARIOMATRICULA getByIdUV(int? iduv)
        {
            //Se obtiene el objeto con valor mayor para kmfinales, para utilizar las propiedades IDUV y KMFINALES
            IQueryable <DiarioMatriculaVM> vmq = consultaBase().AsQueryable();
            vmq = vmq.Where(x => x.diario.IDUV == iduv);
            vmq = vmq.OrderByDescending(x => x.diario.KMFINALES);
            var a = vmq.FirstOrDefault();

            return (a == null) ? null : a.diario;
        }

        public IEnumerable<DiarioMatriculaVM> getByParametro(buscadorDiario param)
        {
            IQueryable<DiarioMatriculaVM> vmq = consultaBase().AsQueryable();

            if (!String.IsNullOrEmpty(param.idUsuario))
            {
                int idUsuario = Int32.Parse(param.idUsuario);
                vmq = vmq.Where(x => x.usuVeh.usuario.ID == idUsuario);
            }

            if (!String.IsNullOrEmpty(param.idMatricula))
            {
                int idMatricula = Int32.Parse(param.idMatricula);
                vmq = vmq.Where(x => x.usuVeh.usuVehiculo.IDMATRICULA == idMatricula);
            }

            if (!String.IsNullOrEmpty(param.FechaHoraVisitaI))
            {
                System.DateTime dIni = Functions.Functions.textoToFecha(param.FechaHoraVisitaI);
                vmq = vmq.Where(x => x.diario.FECHA >= dIni);
            }

            if (!String.IsNullOrEmpty(param.FechaHoraVisitaF))
            {
                System.DateTime dFin = Functions.Functions.textoToFecha(param.FechaHoraVisitaF);
                vmq = vmq.Where(x => x.diario.FECHA < dFin);
            }

            IEnumerable<DiarioMatriculaVM> ievm = vmq.OrderByDescending(x => x.diario.FECHA).Take(maxRecords).ToList();
            return ievm;
        }

        public IEnumerable<DiarioMatriculaVM> getSorted(buscadorDiario paramFiltro, objetoOrdenMapa paramOrden, int direccion)
        {
            IEnumerable<DiarioMatriculaVM> resuFiltro = getByParametro(paramFiltro);

            if (paramOrden.coleccion == "usuVeh" && paramOrden.nombreCampo == "usuario")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.usuVeh.usuario.NOMBRE) : resuFiltro.OrderByDescending(x => x.usuVeh.usuario.NOMBRE);
            }

            if (paramOrden.coleccion == "usuVeh" && paramOrden.nombreCampo == "vehiculo")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.usuVeh.vehiculo.MATRICULA) : resuFiltro.OrderByDescending(x => x.usuVeh.vehiculo.MATRICULA);
            }

            if (paramOrden.coleccion == "diario" && paramOrden.nombreCampo == "FECHA")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.diario.FECHA) : resuFiltro.OrderByDescending(x => x.diario.FECHA);
            }

            if (paramOrden.coleccion == "tarea" && paramOrden.nombreCampo == "DESCRIPCION")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.tarea.DESCRIPCION) : resuFiltro.OrderByDescending(x => x.tarea.DESCRIPCION);
            }

            return resuFiltro.ToList();
        }

        public void Insert(DiarioMatriculaVM vm)
        {
            vm.diario.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.diario);
        }

        public void Update(DiarioMatriculaVM vm)
        {
            repo.Update(vm.diario);
        }

        public void Delete(DiarioMatriculaVM vm)
        {
            repo.Delete(vm.diario);
        }

        public void Save()
        {
            try
            {
                repo.Save();
            }
            catch (Exception e)
            {
                log.Fatal("Fallo salvando entidad " + repo.GetType().ToString());
            }
        }

        public void Dispose()
        {
            repo.Dispose();
        }
    }
}