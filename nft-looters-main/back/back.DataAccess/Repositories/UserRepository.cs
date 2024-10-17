using back.DataAccess.Entities;
using back.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommonUser?> GetById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UserExist(string login)
        {
            try
            {
                return await _context.Users.AnyAsync(u => u.Email == login);
            }
            catch (Exception ex)
            {
                // Логирование исключения и повторное выбрасывание
                // Логируйте исключение, чтобы получить больше информации о проблеме
                // Например, можно использовать ILogger для логирования
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return false; // Или вернуть false, в зависимости от требований
            }
        }
    }
}
