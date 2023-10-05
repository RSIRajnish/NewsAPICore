using NewsAPICore.DTO.DTOs;

namespace NewsAPICore.BLL.Services.IServices;

public interface INewsService
{
    public Task<List<string>> GetStories();
    public Task<List<NewsModel>> GetStoriesItem(int pageNo, int startPosition,string searchText);
}
