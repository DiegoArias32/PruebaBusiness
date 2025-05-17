// Business/Implements/UserBusiness.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{
    public class UserBusiness : GenericBusiness<UserDto, User>
    {
        private readonly UserData _userData;

        public UserBusiness(UserData userData, ILogger<UserBusiness> logger)
            : base(userData, logger)
        {
            _userData = userData;
        }

        // Implementaciones de los métodos de mapeo
        protected override User MapDtoToEntity(UserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                CreateAt = DateTime.Now,
                // Los demás campos que necesites mapear
            };
        }

        protected override UserDto MapEntityToDto(User entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Password = string.Empty // No enviamos la contraseña en los DTO por seguridad
            };
        }

        protected IEnumerable<UserDto> MapEntitiesToDtos(IEnumerable<User> entities)
        {
            return entities.Select(MapEntityToDto).ToList();
        }

       
    }
}