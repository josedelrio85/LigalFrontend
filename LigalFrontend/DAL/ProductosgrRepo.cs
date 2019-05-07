using LigalFrontend.Models;
using LigalFrontend.Models.Buscador;
using LigalFrontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LigalFrontend.DAL
{
    public class ProductosgrRepo : IViewModelRepository<ProductosgrVM, buscadorProductosgr>, IDisposable
    {
        private LigalEntities context;
        private GenericRepository<LigalEntities, gen_productosgr> repo;
        private IEnumerable<ProductosgrVM> vm;

        public ProductosgrRepo()
        {
            context = new LigalEntities();
            repo = new GenericRepository<LigalEntities, gen_productosgr>();
        }

        public IEnumerable<ProductosgrVM> consultaBase()
        {
            var sql = from pgr in context.gen_productosgr
                      join prod in context.gen_productos on pgr.IDPRODUCTO equals prod.ID into JoinedPgrProd
                      from pgrprod in JoinedPgrProd.DefaultIfEmpty()
                      select new ProductosgrVM
                      {
                          productogr = pgr,
                          producto = pgrprod
                      };

            return sql;
        }

        public ProductosgrVM getById(int? id)
        {
            IQueryable<ProductosgrVM> vm = consultaBase().AsQueryable();

            return vm.Where(x => x.productogr.ID == id).SingleOrDefault();
        }

        public IEnumerable<ProductosgrVM> getAll()
        {
            return consultaBase().AsQueryable().ToList();
        }

        public List<ProductosgrVM> getByIdRecogida(int? id)
        {
            IQueryable<ProductosgrVM> vm = consultaBase().AsQueryable();

            return vm.Where(x => x.productogr.IDRECOGIDAN == id).ToList();
        }

        public IEnumerable<ProductosgrVM> getSorted(buscadorProductosgr paramFiltro, objetoOrdenMapa paramOrden, int direccion)
        {
            IEnumerable<ProductosgrVM> resuFiltro = getByParametro(paramFiltro);

            if (paramOrden.coleccion == "producto" && paramOrden.nombreCampo == "PRODUCTO")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.producto.PRODUCTO) : resuFiltro.OrderByDescending(x => x.producto.PRODUCTO);
            }

            if (paramOrden.coleccion == "productogr" && paramOrden.nombreCampo == "CANTIDAD")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.productogr.CANTIDAD) : resuFiltro.OrderByDescending(x => x.productogr.CANTIDAD);
            }

            if (paramOrden.coleccion == "productogr" && paramOrden.nombreCampo == "TEMPERATURA")
            {
                resuFiltro = (direccion == 1) ? resuFiltro.OrderBy(x => x.productogr.TEMPERATURA) : resuFiltro.OrderByDescending(x => x.productogr.TEMPERATURA);
            }

            return resuFiltro.ToList();
        }

        public void Insert(ProductosgrVM vm)
        {
            vm.productogr.ROWID = Guid.NewGuid().ToString();
            repo.Insert(vm.productogr);
        }

        public void Update(ProductosgrVM vm)
        {
            repo.Update(vm.productogr);
        }

        public void Delete(ProductosgrVM vm)
        {
            repo.Delete(vm.productogr);
        }

        public void Save()
        {
            repo.Save();
        }

        public void Dispose()
        {
            repo.Dispose();
        }


        public IEnumerable<ProductosgrVM> getByParametro(buscadorProductosgr buscador)
        {
            throw new NotImplementedException();
        }
    }
}