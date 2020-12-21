using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MovieShop.Core.Models.Response
{
    public class UserLoginResponseModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public object Id { get; set; }
        public List<string> Roles { get; set; }
    }
}
