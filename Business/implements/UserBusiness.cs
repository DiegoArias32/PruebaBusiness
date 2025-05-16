using Business.Interfaces;
using Data.Interfaces;
using Entity;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Business.Implements
{
    public class UserBusiness : BaseBusiness<User>, IUserBusiness
    {
        public UserBusiness(IGenericRepository<User> repository) : base(repository)
        {
        }

        public override async Task<User> CreateAsync(User user)
        {
            // Validations
            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentException("El nombre del usuario es requerido.");

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("El email del usuario es requerido.");

            if (string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("La contraseña del usuario es requerida.");

            // Hash password
            user.Password = HashPassword(user.Password);
            user.Active = true;
            user.CreateAt = DateTime.Now;

            return await base.CreateAsync(user);
        }

        public override async Task<User> UpdateAsync(User user)
        {
            // Validations
            if (user.Id <= 0)
                throw new ArgumentException("ID de usuario inválido.");

            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentException("El nombre del usuario es requerido.");

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("El email del usuario es requerido.");

            // Get existing user to preserve password if not changing
            var existingUser = await _repository.GetByIdAsync(user.Id);
            if (existingUser == null)
                throw new ArgumentException($"No se encontró un usuario con ID {user.Id}.");

            // Only update password if provided
            if (!string.IsNullOrEmpty(user.Password))
                user.Password = HashPassword(user.Password);
            else
                user.Password = existingUser.Password;

            return await base.UpdateAsync(user);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                throw new ArgumentException($"No se encontró un usuario con ID {id}.");

            // Logical delete
            user.Active = false;
            user.DeletAt = DateTime.Now;
            await _repository.UpdateAsync(user);
            
            return true;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var users = await _repository.GetAllAsync();
            return users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);
            if (user == null || !user.Active)
                return false;

            string hashedPassword = HashPassword(password);
            return user.Password == hashedPassword;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}