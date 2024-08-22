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
            return await _context.Set<T>()
                                           .AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.ID == id && x.IsDelated == false , cancellationToken);
        }

        public async Task  HardDelteAsync(int id ,  CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().Where(x => x.ID == id).ExecuteDeleteAsync(cancellationToken: cancellationToken);

        }


        public async Task SoftDelteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>()
            .Where(x => x.ID == id)
            .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDelated, true), cancellationToken: cancellationToken);
        }


        public async Task<bool> IsEntityExsit(Expression<Func<T, bool>> expression , CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public Task UpdateAsync(T item, CancellationToken cancellationToken = default)
        { 
             _context.Set<T>().Update(item);
            return Task.CompletedTask;
        }
        
    }
}
