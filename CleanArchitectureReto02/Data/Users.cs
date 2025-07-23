using CleanArchitectureReto02.Models;

namespace CleanArchitectureReto02.Data
{
    public class Users
    {
        private static readonly List<User> UserList =
        [
                new User { Id = 1, Username = "admin", Password = "Admin123", Role = "Admin" },
                new User { Id = 2, Username = "user", Password = "Contraseña123", Role = "User" },
                new User { Id = 3, Username = "guest", Password = "Invitado123", Role = "User" },
                new User { Id = 4, Username = "test", Password = "Test123", Role = "User" },
                new User { Id = 5, Username = "dev", Password = "Dev123", Role = "Admin" }
        ];
        public static List<User> GetUsers() => UserList;    
    }
}
