using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        #region Get
        Task<IEnumerable<T>> All();
        Task<T> GetById(int id);
        #endregion

        #region Insert
        Task<bool> Add(T entity);
        #endregion

        #region Delete
        Task<bool> Delete(int id);
        #endregion

        #region Update
        Task<bool> Update(T entity);
        #endregion
    }
}
