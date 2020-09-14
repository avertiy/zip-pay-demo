using AutoMapper;
using TestProject.WebAPI.Data.Entities;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<CreateUserModel, User>();
            CreateMap<Account, AccountModel>();
        }
    }
}
