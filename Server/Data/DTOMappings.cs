using AutoMapper;
using Shared.Models;

namespace Server.Data
{
    // Internal sealed = Can't be inherit
    internal sealed class DTOMappings : Profile
    {
        public DTOMappings()
        {
            CreateMap<Post, PostDTO>().ReverseMap();
        }
    }
}
