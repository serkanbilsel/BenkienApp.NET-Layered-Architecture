using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BenkienApp.Data.Abstract
{
    public interface IRepository<T> where T : class // IRepository interface'i dışarıdan alacağı T tipinde bir parametreyle çalışacak
                                                    // SENKRON METOTLAR         // ve where şartı ile bu T nin veri tipi bir class olmalıdır
    {
        List<T> GetAll(); // DB deki tüm kayıtları çekmemizi sağlayacak metot imzası
        List<T> GetAll(Expression<Func<T, bool>> expression); // Uygulamada verileri listelerken p=>p.IsActive vb gibi sorgulama ve filtreleme kodları kullanabilmemizi sağlar.
        T Get(Expression<Func<T, bool>> expression); // Entitityframeworkdeki FirstOrDefault a karşılık geliyor
        T Find(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();

        // ASEKNRON METOTLAR
        Task<T> FindAsync(int id);

        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync(); // lambda expression kullanarak db de filtreleme yapıp geriye 1 tane kayıt döndürür
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression); // lambda expression kullanarak db de filtreleme yapıp geriye liste döndürür
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> include = null);

        Task AddAsync(T entity);

        Task<int> SaveAsync(); // Asenkron kaydetme



    }
}
