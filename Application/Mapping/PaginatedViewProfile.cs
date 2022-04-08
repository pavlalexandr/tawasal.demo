using Application.Model;
using AutoMapper;
using Domain.Entities.DummyApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class PaginatedViewProfile : Profile
    {
        public PaginatedViewProfile()
        {
            CreateMap(typeof(Response<>), typeof(PaginatedView<>));
        }
    }
}
