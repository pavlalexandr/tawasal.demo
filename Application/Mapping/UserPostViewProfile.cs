using Application.Model;
using AutoMapper;
using Domain.Entities.DummyApi;

namespace Application.Mapping
{
    public class UserPostViewProfile : Profile
    {
        public UserPostViewProfile()
        {
            CreateMap<UserPost, UserPostView>();
        }
    }
}
