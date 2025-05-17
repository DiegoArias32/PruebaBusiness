using Entity.Model;
using Entity.DTOs;  // Agregado
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRolUserBusiness : IGenericBusiness<RolUserDto, RolUser>  // Cambiado
    {
        Task<IEnumerable<RolUserDto>> GetByUserIdAsync(int userId);  // Cambiado
        Task<IEnumerable<RolUserDto>> GetByRoleIdAsync(int roleId);  // Cambiado
    }
}