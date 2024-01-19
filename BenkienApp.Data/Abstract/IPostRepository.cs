using BenkienApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BenkienApp.Data.Abstract
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetPostByIncludeAsync(int id);

        Task<List<Post>> GetAllPostsByIncludeAsync();

        Task<List<Post>> GetSomePostsByIncludeAsync(Expression<Func<Post, bool>> expression);
    }
}
