using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public class UserService : IUserService
{
    private int _nextId = 1;
    private readonly Dictionary<int, User> _users = [];
    private readonly HashSet<string> _emails = [];

    public IEnumerable<User> GetAll() => _users.Values.ToList();

    public User? GetById(int id)
    {
        _users.TryGetValue(id, out var user);
        return user;
    }

    public User Add(User user)
    {
        Normalize(user);
        ValidateUniqueEmail(user);

        user.Id = _nextId++;
        _users[user.Id] = user;
        _emails.Add(user.Email);

        return user;
    }

    public bool Update(int id, User updated)
    {
        if (!_users.ContainsKey(id))
            return false;

        Normalize(updated);
        ValidateUniqueEmail(updated, id);

        var oldEmail = _users[id].Email;

        // Only update the email index if the email actually changed
        if (!oldEmail.Equals(updated.Email, StringComparison.OrdinalIgnoreCase))
        {
            _emails.Remove(oldEmail);
            _emails.Add(updated.Email);
        }

        updated.Id = id;
        _users[id] = updated;

        return true;
    }

    public bool Delete(int id)
    {
        if (_users.TryGetValue(id, out var user))
        {
            _emails.Remove(user.Email);
            return _users.Remove(id);
        }

        return false;
    }

    private void Normalize(User user)
    {
        user.FirstName = user.FirstName.Trim();
        user.LastName = user.LastName.Trim();
        user.Email = user.Email.Trim().ToLowerInvariant();
    }

    private void ValidateUniqueEmail(User user, int? currentUserId = null)
    {
        if (_emails.Contains(user.Email) &&
            _users.Values.Any(u => u.Email == user.Email && u.Id != currentUserId))
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }
    }
}