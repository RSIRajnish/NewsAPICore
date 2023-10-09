using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NewsAPICore.BLL.Services;
using NewsAPICore.BLL.Utilities.AutoMapperProfiles;
using NewsAPICore.DTO.DTOs;
using System.Net;
using Xunit;

namespace NewsAPICore.Test.BLL.Services;

public class NewsServiceTests
{
    private readonly IMapper _mapper;
    private IOptions<NewsApiURLOptionDTO> _options;
    private readonly IMemoryCache _memoryCache;
    public NewsServiceTests()
    {
        var myProfile = new AutoMapperProfiles.AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        _mapper = new Mapper(configuration);
        var sampleOptions = new NewsApiURLOptionDTO() { NewsApiIdsUrl = "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty", NewsApiContentUrlPart1 = "https://hacker-news.firebaseio.com/v0/item/", NewsApiContentUrlPart2 = ".json?print=pretty" };
        _options = Options.Create(sampleOptions);
        _memoryCache = new Mock<IMemoryCache>().Object;
      
    }

    /// <summary>
    /// Test the Stories ids service method
    /// </summary>
    /// <returns></returns>

    [Fact]
    public async Task GetNewsStories_WhenSuccess_ReturnsListString()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("It worked!")
        };

        //Mock the httpclientfactory
        var _httpMessageHandler = new Mock<HttpMessageHandler>();
        var mockFactory = new Mock<IHttpClientFactory>();

        _httpMessageHandler.Protected()
          .Setup<Task<HttpResponseMessage>>("SendAsync",
           ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
           ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(new HttpResponseMessage
           {
               StatusCode = HttpStatusCode.OK
           });


        var httpClient = new HttpClient(_httpMessageHandler.Object);
        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var service = new NewsService(mockFactory.Object, _mapper, _options, _memoryCache);
        var result = await service.GetStories();
        // Assert
        Assert.NotEmpty(result);
    }

    /// <summary>
    /// Test the Stories item service method
    /// </summary>
    /// <returns></returns>

    [Fact]
    public async Task GetNewsStoriesItems_WhenSuccess_ReturnsListString()
    {
        int pageNo = 0;
        int startPosition = 0;
        int NoOfRecords = 0;
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("It worked!")
        };

        //Mock the httpclientfactory
        var _httpMessageHandler = new Mock<HttpMessageHandler>();
        var mockFactory = new Mock<IHttpClientFactory>();

        _httpMessageHandler.Protected()
          .Setup<Task<HttpResponseMessage>>("SendAsync",
           ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
           ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(new HttpResponseMessage
           {
               StatusCode = HttpStatusCode.OK
           });


        var httpClient = new HttpClient(_httpMessageHandler.Object);
        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var service = new NewsService(mockFactory.Object, _mapper, _options, _memoryCache);
        NewsModelList result = await service.GetStoriesItem(pageNo, startPosition, NoOfRecords);
        // Assert
        Assert.Empty(result.newsModels);
    }
}