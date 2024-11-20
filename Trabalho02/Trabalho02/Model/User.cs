using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Trabalho02.Model
{
    public class User
    {
        [PrimaryKey]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
