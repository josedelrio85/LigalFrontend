using System.Collections.Generic;

namespace LigalFrontend.DAL
{
    interface IViewModelRepository<T,C> where T : class
                                        where C : class

    {
        IEnumerable<T> consultaBase();

        T getById(int? id);
        
        IEnumerable<T> getByParametro(C buscador);
    }
}
