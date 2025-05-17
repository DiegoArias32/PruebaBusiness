// Business/Implements/GenericBusiness.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.Extensions.Logging;

namespace Business.Implements
{
    public abstract class GenericBusiness<TDto, TEntity> : IGenericBusiness<TDto, TEntity>
        where TDto : class
        where TEntity : class
    {
        protected readonly IGenericData<TEntity> _repository;
        protected readonly ILogger _logger;

        public GenericBusiness(IGenericData<TEntity> repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Obtiene todos los registros
        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();
                return entities.Select(e => MapEntityToDto(e)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los registros");
                throw;
            }
        }

        // Obtiene un registro por su ID
        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                    return null;
                
                return MapEntityToDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el registro con ID {Id}", id);
                throw;
            }
        }

        // Crea un nuevo registro
        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            try
            {
                var entity = MapDtoToEntity(dto);
                var createdEntity = await _repository.CreateAsync(entity);
                return MapEntityToDto(createdEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el registro");
                throw;
            }
        }

        // Actualiza un registro existente
        public virtual async Task<bool> UpdateAsync(TDto dto)
        {
            try
            {
                var entity = MapDtoToEntity(dto);
                return await _repository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el registro");
                throw;
            }
        }

        // Elimina un registro (borrado lógico)
        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el registro con ID {Id}", id);
                throw;
            }
        }

        // Elimina permanentemente un registro
        public virtual async Task<bool> PermanentDeleteAsync(int id)
        {
            try
            {
                return await _repository.PermanentDeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar permanentemente el registro con ID {Id}", id);
                throw;
            }
        }

        // Métodos a implementar en las clases derivadas
        protected abstract TEntity MapDtoToEntity(TDto dto);
        protected abstract TDto MapEntityToDto(TEntity entity);
    }
}