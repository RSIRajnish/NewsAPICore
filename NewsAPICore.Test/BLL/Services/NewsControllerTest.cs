using Microsoft.AspNetCore.Mvc;
using Moq;
using NewsAPICore.API.Controllers;
using NewsAPICore.BLL.Services.IServices;
using NewsAPICore.DTO.DTOs;
using System.Reflection;
using Xunit;

namespace NewsAPICore.Test.BLL.Services
{
    public class NewsControllerTest
    {
        private readonly NewsController _newsController;
        private readonly Mock<INewsService> _newsService;

        public NewsControllerTest()
        {
            _newsService = new Mock<INewsService>();
            _newsController = new NewsController(_newsService.Object);
        }

        /// <summary>
        /// Test the Stories item controller method
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task GetStoriesItemTest_WhenSuccess_ReturnsItemList()
        {
            //Arrange

            _newsService.Setup(p => p.GetStoriesItem(0,0,1)).ReturnsAsync(NewsMockData.GetItemMockData());
            NewsModelList newsModelList = new NewsModelList();

            //Act

            var Mockresult = await _newsController.GetStoriesItem(0, 0,1);

            //Assert
            var okObjectResult = Mockresult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            if (okObjectResult != null && okObjectResult.Value != null)
            {
                newsModelList = (NewsModelList)okObjectResult.Value;
            }

            Assert.NotNull(newsModelList);
            Assert.True(newsModelList.newsModels[0].title.Equals("News Title"));
            Assert.True(newsModelList.newsModels[0].url.Equals("https://hacker-news.firebaseio.com"));

        }
    }
}
