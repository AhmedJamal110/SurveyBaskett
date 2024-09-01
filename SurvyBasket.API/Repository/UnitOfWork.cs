
using System.Collections;

namespace SurveyBasket.API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContect _context;
        private readonly ILogger<UnitOfWork> _logger;
        private Hashtable _repositoryTable;

        public UnitOfWork(ApplicationDbContect context , ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            _repositoryTable = [];
        }

        public Task<IGenericRespository<T>> Respository<T>() where T : BaseEntity
        {
            var type =  typeof(T).Name;
            if (!_repositoryTable.ContainsKey(type))
            {
                var repo = new GenericRespository<T>(_context);
                _repositoryTable.Add(type, repo);
            }

            return Task.FromResult(_repositoryTable[type] as IGenericRespository<T>);
        }

        public async Task<int> Complete()
        {
            try
            {
               return  await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"error happend while save chaings{ex.Message}");
                return -1;
            }
        }


        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();

     
    }
}
