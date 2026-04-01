using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public class UserService : IUserService
{
    private readonly Dictionary<int, User> _users = [];
    private int _nextId = 1;

    public IEnumerable<User> GetAll() => _users.Values;

    public User? GetById(int id)
    {
        _users.TryGetValue(id, out var user);
        return user;
    }

    public User Add(User user)
    {
        user.Id = _nextId++;
        _users[user.Id] = user;
        return user;
    }

    public bool Update(int id, User updated)
    {
        if (!_users.ContainsKey(id))
            return false;

        updated.Id = id; // ensure ID consistency
        _users[id] = updated;
        return true;
    }

    public bool Delete(int id)
    {
        return _users.Remove(id);
    }
}
