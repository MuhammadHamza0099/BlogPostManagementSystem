using AutoMapper;
using BPMS.API.Data.DTOs;
using BPMS.API.Data.Entities;
using BPMS.API.Extensions;

namespace BPMS.API.Mappings
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            //CreateMap<Source, Destination>()
            CreateMap<BlogPost, BlogPostDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToSqid()));

            CreateMap<BlogPostDTO, BlogPost>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.FromSqid()));
        }
    }
}
