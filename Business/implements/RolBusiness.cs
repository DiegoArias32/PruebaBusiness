// Business/Implements/RolBusiness.cs
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
    public class RolBusiness : GenericBusiness<RolDto, Rol>
    {
        public RolBusiness(RolData repository, ILogger<RolBusiness> logger)
            : base(repository, logger)
        {
        }

        protected override Rol MapDtoToEntity(RolDto dto)
        {
            return new Rol
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                CreateAt = DateTime.Now
            };
        }

        protected override RolDto MapEntityToDto(Rol entity)
        {
            return new RolDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        protected IEnumerable<RolDto> MapEntitiesToDtos(IEnumerable<Rol> entities)
        {
            return entities.Select(MapEntityToDto).ToList();
        }
        
        // Métodos específicos si los necesitas
    }
}