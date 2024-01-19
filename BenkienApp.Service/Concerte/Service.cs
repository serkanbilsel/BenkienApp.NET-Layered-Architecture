using BenkienApp.Data;
using BenkienApp.Data.Concerte;
using BenkienApp.Data.Entity;
using BenkienApp.Service.Abstract;

namespace BenkienApp.Service.Concrete
{
    public class Service<T> : Repository<T>, IService<T> where T : class, IEntity, new()
    {
        public Service(DatabaseContext context) : base(context)
        {
        }
    }
}