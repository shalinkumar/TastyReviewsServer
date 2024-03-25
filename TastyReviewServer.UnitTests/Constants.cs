using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyReviewServer.UnitTests
{
    public static class Constants
    {
        public static class Users
        {
            public static class Admin
            {
                public const string FirstName = "Shalin";
                public const string LastName = "Kumar";
                public const string UserName = "AdminShalin12";
                public const string Email = "Shalin.admin@live.com";
            }

            public static class NonAdmin
            {
                public const string FirstName = "Shalin";
                public const string LastName = "Kumar";
                public const string UserName = "Shalin@10881231";
                public const string Email = "Shalin.doe@live.com";                
            }

            public static class Passwords
            {
                public const string Default = "123qwe";
            }
        }
    }
}
