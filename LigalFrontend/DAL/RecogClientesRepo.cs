using LigalFrontend.Models;
using LigalFrontend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;
using LigalFrontend.Models.Buscador;

namespace LigalFrontend.DAL
{
    public class RecogClientesRepo : IViewModelRepository<RecogClientesVM, ClaseDummy>
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, gen_recogidasR> repo;
        private IEnumerable<RecogClientesVM> vm;

        public RecogClientesRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, gen_recogidasR>();
        }

        public IEnumerable<RecogClientesVM> consultaBase()
        {
            vm = from rr in context.gen_recogidasR
                 join cli in context.gen_clientes on rr.IDCLIENTE equals cli.ID
                 join obs in context.gen_observa on rr.IDOBSERVA equals obs.ID into JoinedObsRR
                 from obs in JoinedObsRR.DefaultIfEmpty()
                 select new RecogClientesVM
                 {
                     recogidasR = rr,
                     cliente = cli,
                     observa = obs
                 };

            return vm;
        }

        public RecogClientesVM getById(int? id)
        {
            IQueryable<RecogClientesVM> vmq = consultaBase().AsQueryable();
            RecogClientesVM pr = vmq.Where(x => x.recogidasR.ID == id).SingleOrDefault();
            return pr;
        }

        public List<RecogClientesVM> getByIdRecogida(int idrecogida)
        {
            IQueryable<RecogClientesVM> vmq = consultaBase().AsQueryable();
            List<RecogClientesVM> lista = vmq.Where(x => x.recogidasR.IDRECOGIDA == idrecogida).ToList();

            return lista;
        }

        public IEnumerable<RecogClientesVM> getByParametro(ClaseDummy param)
        {
            throw new NotImplementedException();
        }

        public void Insert(RecogClientesVM vm)
        {
            vm.recogidasR.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.recogidasR);
        }

        public void Update(RecogClientesVM vm)
        {
            repo.Update(vm.recogidasR);
        }

        public void Delete(RecogClientesVM vm)
        {
            repo.Delete(vm.recogidasR);
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