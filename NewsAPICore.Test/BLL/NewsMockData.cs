using NewsAPICore.DTO.DTOs;

namespace NewsAPICore.Test.BLL
{
    public class NewsMockData
    {
        public static NewsModelList GetItemMockData()
        {
            NewsModelList newsModelList = new NewsModelList();
            newsModelList.newsModels = new List<NewsModel>();
            
            NewsModel model = new NewsModel();
            model.title = "News Title";
            model.url = "https://hacker-news.firebaseio.com";

            newsModelList.newsModels.Add(model);
            newsModelList.RecordCount = newsModelList.newsModels.Count;
            return newsModelList;
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