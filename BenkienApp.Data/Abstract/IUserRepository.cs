using BenkienApp.Data.Entity;

namespace BenkienApp.Data.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByIncludeAsync(int userId);
        Task<List<User>> GetAllUserByIncludeAsync();
    }
}
