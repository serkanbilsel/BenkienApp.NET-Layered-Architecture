using BenkienApp.Data.Abstract;
using BenkienApp.Data.Entity;

namespace BenkienApp.Service.Abstract
{
    public interface IService<T> : IRepository<T> where T : class, IEntity, new()
    {

    }
}
