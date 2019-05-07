using JSV.XOne.Replica;
using LigalFrontend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace LigalFrontend.DAL
{
    public class GenericRepository<C, T> : IGenericRepository<T>, IDisposable
        where T : class
        where C : DbContext, new()
    {
        //abstract
        private C _entities = new C();
        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(GenericRepository<C,T>));

        public void Insert(T entity)
        {
            _entities.Set<T>().Add(entity);
            AtackQueue(entity, XOneOperEnum.Insert);
        }

        public void Update(T entity)
        {
            _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            AtackQueue(entity, XOneOperEnum.Update);
        }

        public void Delete(T entity)
        {
            _entities.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            _entities.Set<T>().Remove(entity);
            AtackQueue(entity, XOneOperEnum.Delete);
        }

        public void Save()
        {
            try
            {
                _entities.SaveChanges();
            }
            catch (Exception e)
            {
                log.Fatal("Fallo salvando entidad " + _entities.GetType().ToString());
                log.Fatal("Mensaje -- " + e.Message);
                log.Fatal("InnerException -- " + e.InnerException);
                log.Fatal("StackTrace " + e.StackTrace);
            }
        }

        private void AtackQueue(T entity, XOneOperEnum operacion)
        {
            _entities.Set<master_replica_queue>().Add(XOneReplica.CreateReplicaRow<master_replica_queue>(entity, operacion));
        }

        //Para usar con entidades que no son tratadas como VM
        public virtual List<T> getTodo()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query.ToList();
        }

        public IQueryable<T> getById(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public void Dispose()
        {
            _entities.Dispose();
        }
    }
}