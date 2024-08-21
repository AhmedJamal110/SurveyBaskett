using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SurveyBasket.API.Repository
{
    public class GenericRespository<T> : IGenericRespository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContect _context;

        public GenericRespository( ApplicationDbContect context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T item , CancellationToken cancellationToken = default)
        {
           await _context.Set<T>().AddAsync(item, cancellationToken);
            return item;
        }

        public  IQueryable<T> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _context.Set<T>().Where(x => x.IsDelated == false);
        }

        public async Task<T?> GetByIdAsync(int id , CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task HardDelteAsync(T item , int id , CancellationToken cancellationToken = default)
        {
             await _context.Set<T>().Where(x => x.ID == id).ExecuteDeleteAsync();
        }


        public Task SoftDelteAsync(T item , CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> IsEntityExsit(Expression<Func<T, bool>> expression , CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public Task UpdateAsync(T item , CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
