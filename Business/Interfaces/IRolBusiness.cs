using Entity.Model;
using Entity.DTOs;  // Agregado

namespace Business.Interfaces
{
    public interface IRolBusiness : IGenericBusiness<RolDto, Rol>  // Cambiado
    {
        // Métodos específicos adicionales si fueran necesarios
    }
}