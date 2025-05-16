using Business.Interfaces;
using Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Implements
{
    public class BaseBusiness<T> : IBaseBusiness<T> where T : class
    {
        protected readonly IGenericRepository<T> _repository;

        public BaseBusiness(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}