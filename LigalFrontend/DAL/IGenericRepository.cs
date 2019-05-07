namespace LigalFrontend.DAL
{
    interface IGenericRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
