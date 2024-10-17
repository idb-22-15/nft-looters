using back.DataAccess.Entities;

namespace back.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<CommonUser?> GetById(Guid id);
        Task<bool> UserExist(string login);
    }
}