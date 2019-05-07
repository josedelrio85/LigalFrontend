using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LigalFrontend.DAL
{
    public class ClientesRepo : IViewModelRepository<ClienteVM, buscadorClientes>, IDisposable
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, gen_clientes> repo;
        private IEnumerable<ClienteVM> vm;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ClientesRepo));

        public ClientesRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, gen_clientes>();
        }

        public IEnumerable<ClienteVM> consultaBase()
        {
            var sql = from cli in context.gen_clientes
                      select new ClienteVM
                      {
                          cliente = cli
                      };

            return sql;
        }

        public ClienteVM getById(int? id)
        {
            IQueryable<ClienteVM> vm = consultaBase().AsQueryable();

            return vm.Where(x => x.cliente.ID == id).SingleOrDefault();
        }

        public IEnumerable<ClienteVM> getAll()
        {
            return consultaBase().AsQueryable().ToList();
        }

        public IEnumerable<ClienteVM> getByParametro(buscadorClientes buscador)
        {
            IQueryable<ClienteVM> vmq = consultaBase().AsQueryable();

            if (!String.IsNullOrEmpty(buscador.CODIGOC))
            {
                string codigoc = Functions.Functions.CleanInput(buscador.CODIGOC);
                vmq = vmq.Where(x => x.cliente.CODIGOC.Contains(codigoc));
            }

            if (!String.IsNullOrEmpty(buscador.NOMBREC))
            {
                string nombrec = Functions.Functions.CleanInput(buscador.NOMBREC);
                vmq = vmq.Where(x => x.cliente.NOMBREC.Contains(nombrec));
            }

            if (!String.IsNullOrEmpty(buscador.POBLACION))
            {
                string pobla = Functions.Functions.CleanInput(buscador.POBLACION);
                vmq = vmq.Where(x => x.cliente.POBLACION.Contains(pobla));
            }

            IEnumerable<ClienteVM> ievm = vmq.ToList();
            return ievm;
        }

        public IEnumerable<ClienteVM> getSorted(buscadorClientes paramFiltro, objetoOrdenMapa paramOrden, int direccion)
        {
            IEnumerable<ClienteVM> resuFiltro = getByParametro(paramFiltro);

            if (paramOrden.coleccion == "cliente" && paramOrden.nombreCampo == "CODIGOC")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.cliente.CODIGOC) : resuFiltro.OrderByDescending(x => x.cliente.CODIGOC);
            }

            if (paramOrden.coleccion == "cliente" && paramOrden.nombreCampo == "NOMBREC")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.cliente.NOMBREC) : resuFiltro.OrderByDescending(x => x.cliente.NOMBREC);
            }

            if (paramOrden.coleccion == "cliente" && paramOrden.nombreCampo == "POBLACION")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.cliente.POBLACION) : resuFiltro.OrderByDescending(x => x.cliente.POBLACION);
            }

            return resuFiltro.ToList();
        }

        public void Insert(ClienteVM vm)
        {
            vm.cliente.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.cliente);
        }

        public void Update(ClienteVM vm)
        {
            repo.Update(vm.cliente);
        }

        public void Delete(ClienteVM vm)
        {
            repo.Delete(vm.cliente);
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