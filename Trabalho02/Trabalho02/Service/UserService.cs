using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho02.Model;

namespace Trabalho02.Service
{
    public class UserService
    {
        private List<User> Users { get; set; } = new();

        public UserService()
        {
            Users.Add(new User { Username = "admin", Password = "admin" });
        }

        public bool AuthenticateUser(string username, string password)
        {
            return Users.Any(user => user.Username == username && user.Password == password);
        }

        public void RegisterUser(User user)
        {
            Users.Add(user);
        }
    }
}
