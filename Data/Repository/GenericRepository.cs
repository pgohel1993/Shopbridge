using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Protected Members 
        protected Shopbridge_Context _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;
        #endregion

        #region Constructor
        public GenericRepository(Shopbridge_Context context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            this.dbSet = _context.Set<T>();
        }
        #endregion

        #region Implementation of Generic Repository Methods
        #region Get
        #region Get All
        public virtual async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();
        }
        #endregion

        #region GetById
        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }
        #endregion
        #endregion

        #region Insert
        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }
        #endregion

        #region Delete
        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
