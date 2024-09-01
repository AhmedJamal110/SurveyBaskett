namespace SurveyBasket.API.Repository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
       
         Task<IGenericRespository<T>> Respository<T>() where T : BaseEntity;

        Task<int> Complete(); 
    
    
    }
}
