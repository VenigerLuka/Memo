using AutoMapper;
using MemoProject.Data;
using MemoProject.Models.Memos;

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
