using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Users;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<List<User>> GetAll();
    Task<User?> GetById(long id);
    Task<Entities.User?> GetUserByEmail(string email);
}
