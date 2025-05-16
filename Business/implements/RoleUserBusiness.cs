using Business.Interfaces;
using Data.Interfaces;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Implements
{
    public class RoleUserBusiness : BaseBusiness<RoleUser>, IRoleUserBusiness
    {
        public RoleUserBusiness(IGenericRepository<RoleUser> repository) : base(repository)
        {
        }

        public override async Task<RoleUser> CreateAsync(RoleUser roleUser)
        {
            // Validations
            if (roleUser.UserId <= 0)
                throw new ArgumentException("ID de usuario inválido.");

            if (roleUser.RoleId <= 0)
                throw new ArgumentException("ID de rol inválido.");

            // Check if this assignment already exists
            var existingAssignments = await GetByUserIdAsync(roleUser.UserId);
            if (existingAssignments.Any(ru => ru.RoleId == roleUser.RoleId))
                throw new ArgumentException("El usuario ya tiene asignado este rol.");

            return await base.CreateAsync(roleUser);
        }

        public override async Task<RoleUser> UpdateAsync(RoleUser roleUser)
        {
            // Validations
            if (roleUser.Id <= 0)
                throw new ArgumentException("ID de asignación inválido.");

            if (roleUser.UserId <= 0)
                throw new ArgumentException("ID de usuario inválido.");

            if (roleUser.RoleId <= 0)
                throw new ArgumentException("ID de rol inválido.");

            return await base.UpdateAsync(roleUser);
        }

        public async Task<IEnumerable<RoleUser>> GetByUserIdAsync(int userId)
        {
            var allRoleUsers = await _repository.GetAllAsync();
            return allRoleUsers.Where(ru => ru.UserId == userId);
        }

        public async Task<IEnumerable<RoleUser>> GetByRoleIdAsync(int roleId)
        {
            var allRoleUsers = await _repository.GetAllAsync();
            return allRoleUsers.Where(ru => ru.RoleId == roleId);
        }
    }
}