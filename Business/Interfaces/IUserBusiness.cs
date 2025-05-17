using Entity.DTOs;
using Entity.Model;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserBusiness : IGenericBusiness<UserDto, User>
    {
        Task<UserDto> GetByEmailAsync(string email);
        Task<bool> ValidateCredentialsAsync(string email, string password);
    }
}