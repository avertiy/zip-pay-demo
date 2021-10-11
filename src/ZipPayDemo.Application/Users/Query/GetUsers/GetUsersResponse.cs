using System.Collections.Generic;
using TestProject.WebAPI.Models;

namespace ZipPayDemo.Application.Users.Query.GetUsers
{
    public class GetUsersResponse
    {
        public IList<UserModel> Users { get; set; }
    }
}
