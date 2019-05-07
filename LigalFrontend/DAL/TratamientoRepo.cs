using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LigalFrontend.DAL
{
    public class TratamientoRepo : IViewModelRepository<TratamientoVM, ClaseDummy>
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, gen_trata> repo;
        private IEnumerable<TratamientoVM> vm;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(TratamientoRepo));

        public TratamientoRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, gen_trata>();
        }

        public IEnumerable<TratamientoVM> consultaBase()
        {
            vm = from tr in context.gen_trata
                 join anti in context.gen_antibioticos on tr.IDTRATA equals anti.ID into JoinTraAnti
                from anti in JoinTraAnti.DefaultIfEmpty()
                select new TratamientoVM
                {
                    tratamiento = tr,
                    antibioticos = anti
                };

            return vm;
        }

        public List<TratamientoVM> getAll()
        {
            IQueryable<TratamientoVM> vmq = consultaBase().AsQueryable();
            return vmq.ToList();

        }

        public TratamientoVM getById(int? id)
        {
            IQueryable<TratamientoVM> vmq = consultaBase().AsQueryable();
            TratamientoVM pr = vmq.Where(x => x.tratamiento.ID == id).SingleOrDefault();
            return pr;
        }

        public List<TratamientoVM> getByIdInspeccion(int idInsp)
        {
            IQueryable<TratamientoVM> vmq = consultaBase().AsQueryable();
            List<TratamientoVM> lista = vmq.Where(x => x.tratamiento.IDINSP == idInsp).ToList();

            return lista;
        }

        public IEnumerable<TratamientoVM> getByParametro(ClaseDummy param)
        {
            throw new NotImplementedException();
        }

        public void Insert(TratamientoVM vm)
        {
            vm.tratamiento.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.tratamiento);
        }

        public void Update(TratamientoVM vm)
        {
            repo.Update(vm.tratamiento);
        }

        public void Delete(TratamientoVM vm)
        {
            repo.Delete(vm.tratamiento);
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