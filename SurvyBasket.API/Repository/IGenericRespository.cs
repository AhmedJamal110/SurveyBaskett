using System.Linq.Expressions;

namespace SurveyBasket.API.Repository
{
    public interface IGenericRespository<T> where T : BaseEntity
    {
        IQueryable<T> GetAllAsync(CancellationToken cancellationToken = default );
        Task<T?> GetByIdAsync(int id , CancellationToken cancellationToken = default);
        Task<bool> IsEntityExsit(Expression<Func<T, bool>> expression , CancellationToken cancellationToken = default);
        Task<T> AddAsync(T item , CancellationToken cancellationToken = default);
        Task UpdateAsync(T item , CancellationToken cancellationToken = default);
        Task HardDelteAsync( int id , CancellationToken cancellationToken = default);
        Task SoftDelteAsync(  int id , CancellationToken cancellationToken = default);


    }
}
