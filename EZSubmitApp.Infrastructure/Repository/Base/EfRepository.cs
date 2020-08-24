using EZSubmitApp.Core.Entities.Base;
using EZSubmitApp.Core.IRepositories.Base;
using EZSubmitApp.Infrastructure.Data;

namespace EZSubmitApp.Infrastructure.Repository.Base
{
    public class EfRepository<T> : EfRepositoryBase<T, int>, IAsyncRepository<T> 
        where T : class, IBaseEntity<int>
    {
        public EfRepository(EZSubmitDbContext dbContext)
            :base(dbContext)
        {
        }
    }
}
