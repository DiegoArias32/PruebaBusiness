// Business/IGenericBusiness.cs
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public interface IGenericBusiness<TDto, TEntity> 
        where TDto : class
        where TEntity : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
        Task<TDto> CreateAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> PermanentDeleteAsync(int id);
    }
}