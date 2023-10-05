using AutoMapper;

namespace NewsAPICore.BLL.Utilities.AutoMapperProfiles;

public static class AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<string, string>().ConvertUsing(src => "https://hacker-news.firebaseio.com/v0/item/" + src + ".json?print=pretty");
        }
    }
}
