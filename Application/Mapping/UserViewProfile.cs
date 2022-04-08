using Application.Model;
using AutoMapper;
using Domain.Entities.DummyApi;

namespace Application.Mapping
{
    public class UserViewProfile : Profile
    {
        public UserViewProfile()
        {
            CreateMap<User, UserView>();
        }
    }
}
