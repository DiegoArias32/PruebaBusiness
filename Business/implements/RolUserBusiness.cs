// Business/Implements/RolUserBusiness.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{
    public class RolUserBusiness : GenericBusiness<RolUserDto, RolUser>
    {
        private readonly RolUserData _rolUserData;

        public RolUserBusiness(RolUserData rolUserData, ILogger<RolUserBusiness> logger)
            : base(rolUserData, logger)
        {
            _rolUserData = rolUserData ?? throw new ArgumentNullException(nameof(rolUserData));
        }

        protected override RolUser MapDtoToEntity(RolUserDto dto)
        {
            return new RolUser
            {
                Id = dto.Id,
                UserId = dto.UserId,
                RolId = dto.RolId,
                CreateAt = DateTime.Now
            };
        }

        protected override RolUserDto MapEntityToDto(RolUser entity)
        {
            return new RolUserDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                RolId = entity.RolId
            };
        }

        protected IEnumerable<RolUserDto> MapEntitiesToDtos(IEnumerable<RolUser> entities)
        {
            return entities.Select(MapEntityToDto).ToList();
        }

    }
}