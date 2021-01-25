using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Models
{
    public class UserViewModel
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
