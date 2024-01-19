using BenkienApp.Data.Abstract;
using BenkienApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BenkienApp.Data.Concerte
{
    public class Repository<T> : IRepository<T> where T : class, IEntity,  new() //Repository classına gönderilecek T nin şartları: T(brand, category,product vb) bir class olmalı, IEntity den implemente almalı ve new lenebilir olmalı
    {
        internal DatabaseContext _context; // boş bir database context oluşturduk
        internal DbSet<T> _dbSet; // boş bir dbset tanımladık. REpository e gönderilecek T classını parametre verdik

        public Repository(DatabaseContext context) // S.O.L.I.D prensipleri
        {
            _context = context; // _context i burada doldurduk, aşağıda kullanabilmek için(doldurmadan kullanmak istersek null reference hatası olur)
            _dbSet = context.Set<T>(); // Repository e gönderilecek T classı için context üzerindeki db sete göre kendini ayarla
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            var query = _dbSet.AsNoTracking().Where(expression);

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>> orderBy)
        {
            var query = _dbSet.AsNoTracking().Where(expression);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }


        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Find(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public List<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList(); // eğer sadece listeleme yapacaksak yani liste üzerinde
            // kayıt güncelleme gibi bir işlem yapmayacaksak entity framework deki AsNoTracking() yöntemi ile 
            // listeyi daha performanslı çekebiliriz.
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking().Where(expression).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        //public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        //{
        //    return await _dbSet.AsNoTracking().Where(expression).ToListAsync();
        //}

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);

        }

        public int Save()
        {
            return _context.SaveChanges(); // entity framework de savcechanges (ekle, güncelle, sil vb işlemleri db ye işleyen metot) metodu direk context üzerinden çalışır, dbset in böyle bir metodu olmadığı için _context.SaveChanges() diyerek database contextimiz üzerindeki tüm işlemleri(ekleme, güncelleme silme vb) veritabanına yansıtmamıısağlan bu metodu 1 kere çağırmamız gerekli yoksa işlemler db ye işlenmez!!
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
