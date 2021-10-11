using AutoMapper;
using TestProject.WebAPI.Models;
using ZipPayDemo.Domain.Entities;

namespace ZipPayDemo.Application
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
