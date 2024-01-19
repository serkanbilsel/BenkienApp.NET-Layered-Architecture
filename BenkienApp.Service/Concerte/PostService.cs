using BenkienApp.Data;
using BenkienApp.Data.Concerte;
using BenkienApp.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenkienApp.Service.Concerte
{
    public class PostService : PostRepository, IPostService
    {
        public PostService(DatabaseContext context) : base(context)
        {
        }
    }
}
