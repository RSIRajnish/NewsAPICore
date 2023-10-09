using NewsAPICore.DTO.DTOs;

namespace NewsAPICore.BLL.Services.IServices;

public interface INewsService
{
    public Task<List<string>> GetStories();
    public Task<NewsModelList> GetStoriesItem(int pageNo, int startPosition,int NoOfRecords);
}
