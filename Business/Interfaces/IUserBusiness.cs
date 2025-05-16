using Entity;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserBusiness : IBaseBusiness<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<bool> ValidateCredentialsAsync(string email, string password);
    }
}