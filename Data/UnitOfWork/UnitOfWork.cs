using Microsoft.Extensions.Logging;
using Shopbridge_base.Data.IConfiguration;
using Shopbridge_base.Domain.Services;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Private Members
        private readonly Shopbridge_Context _context;
        private readonly ILogger _logger;
        #endregion

        #region public properties
        public IProductService Products { get; private set; }
        #endregion

        #region Constructor
        public UnitOfWork(Shopbridge_Context context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            Products = new ProductService(_context, _logger);
        }
        #endregion

        #region Implementation of Unit of work method
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Implementation of Disposable method
        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}
