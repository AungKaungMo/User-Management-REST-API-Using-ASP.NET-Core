using UserManagementApi.DTOs;
using UserManagementApi.Models;

namespace UserManagementApi.Services;

// In-memory storage for coursework: data resets on application restart.
public sealed class InMemoryUserService : IUserService
{
    private readonly Dictionary<Guid, User> _users = new();
    private readonly object _gate = new();

    public IReadOnlyCollection<User> GetAll()
    {
        lock (_gate) return _users.Values.OrderBy(u => u.Name).ToArray();
    }

    public User? GetById(Guid id)
    {
        lock (_gate) return _users.GetValueOrDefault(id);
    }

    public User? Create(UserRequest request)
    {
        lock (_gate)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            if (_users.Values.Any(u => u.Email == email)) return null;
            var user = new User(Guid.NewGuid(), request.Name.Trim(), email, request.Age, DateTimeOffset.UtcNow);
            _users.Add(user.Id, user);
            return user;
        }
    }

    public UpdateResult Update(Guid id, UserRequest request)
    {
        lock (_gate)
        {
            if (!_users.TryGetValue(id, out var current)) return UpdateResult.NotFound;
            var email = request.Email.Trim().ToLowerInvariant();
            if (_users.Values.Any(u => u.Id != id && u.Email == email)) return UpdateResult.EmailTaken;
            _users[id] = current with { Name = request.Name.Trim(), Email = email, Age = request.Age };
            return UpdateResult.Updated;
        }
    }

    public bool Delete(Guid id)
    {
        lock (_gate) return _users.Remove(id);
    }
}
