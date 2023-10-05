using NewsAPICore.DTO.DTOs;

namespace NewsAPICore.Test.BLL
{
    public class NewsMockData
    {
        public static List<NewsModel> GetItemMockData()
        {
            List<NewsModel> newsModels = new List<NewsModel>();
            NewsModel model = new NewsModel();
            model.title = "News Title";
            model.url = "https://hacker-news.firebaseio.com";
            newsModels.Add(model);
            return newsModels;
        }

        public static List<string> GetStoriesMockData()
        {
            Random r = new Random();
            List<string> StoriesId = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                StoriesId.Add(r.Next(9000000, 9999999).ToString());
            }
            return StoriesId;
        }
    }
}