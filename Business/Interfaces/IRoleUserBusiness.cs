using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRoleUserBusiness : IBaseBusiness<RoleUser>
    {
        Task<IEnumerable<RoleUser>> GetByUserIdAsync(int userId);
        Task<IEnumerable<RoleUser>> GetByRoleIdAsync(int roleId);
    }
}