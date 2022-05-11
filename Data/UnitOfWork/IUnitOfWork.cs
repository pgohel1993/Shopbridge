using Shopbridge_base.Domain.Services.Interfaces;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.IConfiguration
{
    public interface IUnitOfWork
    {
        public IProductService Products { get; }

        Task CompleteAsync();
    }
}
