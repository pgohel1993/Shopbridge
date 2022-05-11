using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class ProductService : GenericRepository<Product>, IProductService
    {
        //private readonly ILogger<ProductService> _logger;
        #region Constructor
        public ProductService(Shopbridge_Context context, ILogger logger) : base(context, logger)
        {

        }
        #endregion

        #region Implementation of Generic Repository and Product service methods
        #region Get 
        public override async Task<IEnumerable<Product>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(ProductService));
                return new List<Product>();
            }
        }

        #endregion

        #region Update
        public override async Task<bool> Update(Product entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
                if (existingUser == null)
                {
                    return await Add(entity);
                }

                existingUser.Name = entity.Name;
                existingUser.Description = entity.Description;
                existingUser.Price = entity.Price;
                existingUser.Complated = entity.Complated;
                existingUser.IsActive = entity.IsActive;
                return true;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} Update method error", typeof(ProductService));
                return false;
            }
        }
        #endregion

        #region Delete
        public override async Task<bool> Delete(int id)
        {

            try
            {
                var exist = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (exist != null)
                {
                    dbSet.Remove(exist);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "{Repo} Delete method error", typeof(ProductService));
                return false;
            }
        }
        #endregion
        #endregion

    }
}
