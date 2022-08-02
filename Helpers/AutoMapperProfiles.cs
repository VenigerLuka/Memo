using AutoMapper;
using MemoProject.Data;
using MemoProject.Models.Memo;

namespace MemoProject.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Memo, MemoViewModel>();
            CreateMap<MemoViewModel, Memo>();
        }
    }
}
