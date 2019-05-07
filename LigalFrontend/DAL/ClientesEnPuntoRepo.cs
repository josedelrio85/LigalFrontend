using LigalFrontend.Models;
using LigalFrontend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;
using LigalFrontend.Models.Buscador;

namespace LigalFrontend.DAL
{
    public class ClientesEnPuntoRepo : IViewModelRepository<ClientesEnPuntoVM, buscadorClientesenPunto>
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, Gen_clientesenpunto> repo;
        private IEnumerable<ClientesEnPuntoVM> vm;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ClientesEnPuntoRepo));

        public ClientesEnPuntoRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, Gen_clientesenpunto>();
        }

        public IEnumerable<ClientesEnPuntoVM> consultaBase()
        {
            vm = from cp in context.Gen_clientesenpunto
                 join cli in context.gen_clientes on cp.IDCLIENTE equals cli.ID into JoinedCliCli
                 join pr in context.GEN_PUNTOSRECOGIDA on cp.IDPUNTO equals pr.ID into JoinedPunCli
                 from clicli in JoinedCliCli.DefaultIfEmpty()
                 from puncli in JoinedPunCli.DefaultIfEmpty()
                 select new ClientesEnPuntoVM
                 {
                     clientesEnPunto = cp,
                     cliente = clicli,
                     puntosRecogida = puncli
                 };
            vm.First().buscador = new buscadorClientesenPunto();
            return vm;
        }

        public IEnumerable<ClientesEnPuntoVM> consultaBaseAlt()
        {
            vm = from cp in context.Gen_clientesenpunto
                 join cli in context.gen_clientes on cp.IDCLIENTE equals cli.ID
                 join pr in context.GEN_PUNTOSRECOGIDA on cp.IDPUNTO equals pr.ID
                 select new ClientesEnPuntoVM
                 {
                     clientesEnPunto = cp,
                     cliente = cli,
                     puntosRecogida = pr
                 };

            return vm;
        }

        public List<ClientesEnPuntoVM> getAll()
        {
            IQueryable<ClientesEnPuntoVM> vmq = consultaBaseAlt().AsQueryable();
            vmq = vmq.OrderBy(x => x.clientesEnPunto.IDCLIENTE);
            return vmq.ToList();
        }

        public ClientesEnPuntoVM getById(int? id)
        {
            IQueryable<ClientesEnPuntoVM> vmq = consultaBaseAlt().AsQueryable();
            ClientesEnPuntoVM pr = vmq.Where(x => x.clientesEnPunto.ID == id).SingleOrDefault();
            return pr;
        }

        public IEnumerable<ClientesEnPuntoVM> getByParametro(buscadorClientesenPunto buscador)
        {
            IQueryable<ClientesEnPuntoVM> vmq = consultaBaseAlt().AsQueryable();

            if (!String.IsNullOrEmpty(buscador.CODIGOP))
            {
                string codigo = Functions.Functions.CleanInput(buscador.CODIGOP.ToString());
                vmq = vmq.Where(x => x.puntosRecogida.CODIGOP == codigo);
            }

            if (!String.IsNullOrEmpty(buscador.NOMBREC))
            {
                string nombre = Functions.Functions.CleanInput(buscador.NOMBREC.ToString());
                vmq = vmq.Where(x => x.cliente.NOMBREC.Contains(nombre));
            }

            IEnumerable<ClientesEnPuntoVM> ievm = vmq.ToList();
            return ievm;
        }

        public IEnumerable<ClientesEnPuntoVM> getSorted(buscadorClientesenPunto paramFiltro, objetoOrdenMapa paramOrden, int direccion)
        {
            IEnumerable<ClientesEnPuntoVM> resuFiltro = getByParametro(paramFiltro);

            if (paramOrden.coleccion == "puntosRecogida" && paramOrden.nombreCampo == "CODIGOP")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.puntosRecogida.CODIGOP) : resuFiltro.OrderByDescending(x => x.puntosRecogida.CODIGOP);
            }

            if (paramOrden.coleccion == "cliente" && paramOrden.nombreCampo == "NOMBREC")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.cliente.NOMBREC) : resuFiltro.OrderByDescending(x => x.cliente.NOMBREC);
            }

            return resuFiltro.ToList();
        }

        public void Insert(ClientesEnPuntoVM vm)
        {
            vm.clientesEnPunto.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.clientesEnPunto);
        }

        public void Update(ClientesEnPuntoVM vm)
        {
            repo.Update(vm.clientesEnPunto);
        }

        public void Delete(ClientesEnPuntoVM vm)
        {
            repo.Delete(vm.clientesEnPunto);
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