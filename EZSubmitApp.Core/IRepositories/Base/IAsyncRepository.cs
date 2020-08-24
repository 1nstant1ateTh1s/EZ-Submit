using EZSubmitApp.Core.Entities.Base;

namespace EZSubmitApp.Core.IRepositories.Base
{
    public interface IAsyncRepository<T>: IAsyncRepositoryBase<T, int> where T : IBaseEntity<int>
    {
    }
}
