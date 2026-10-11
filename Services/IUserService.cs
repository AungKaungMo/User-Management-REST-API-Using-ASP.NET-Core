using UserManagementApi.DTOs;
using UserManagementApi.Models;

namespace UserManagementApi.Services;

public interface IUserService
{
    IReadOnlyCollection<User> GetAll();
    User? GetById(Guid id);
    User? Create(UserRequest request);
    UpdateResult Update(Guid id, UserRequest request);
    bool Delete(Guid id);
}

public enum UpdateResult { Updated, NotFound, EmailTaken }
