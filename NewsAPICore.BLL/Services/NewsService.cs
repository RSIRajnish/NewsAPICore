using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NewsAPICore.BLL.Services.IServices;
using NewsAPICore.DTO.DTOs;
using System.Reflection;
using System.Text.Json;

namespace NewsAPICore.BLL.Services;

public class NewsService : INewsService
{
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<NewsApiURLOptionDTO> _options;
    private readonly IMemoryCache _memoryCache;

    public NewsService(IHttpClientFactory httpClientFactory, IMapper mapper, IOptions<NewsApiURLOptionDTO> options, IMemoryCache memoryCache)
    {
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
        _options = options;
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// Get the stories Id list
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetStories()
    {
        List<string> finalResult;

        using (HttpClient httpClient = _httpClientFactory.CreateClient())
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(_options.Value.NewsApiIdsUrl),
                Method = HttpMethod.Get,
            };
            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            Stream stream = httpResponseMessage.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string response = reader.ReadToEnd();
            finalResult = response.Replace('[', ' ').Replace(']', ' ').Replace(", ", ",").Trim().Split(',').ToList();
        }
        return finalResult;
    }
           
    /// <summary>
    /// Get news item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public NewsModel GetNewsContent(string id)
    {
        string uri = $"{_options.Value.NewsApiContentUrlPart1}{id}{_options.Value.NewsApiContentUrlPart2}";
        using (HttpClient httpClient = _httpClientFactory.CreateClient())
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri(uri),
                Method = HttpMethod.Get,
            };
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);
            Stream stream = httpResponseMessage.Content.ReadAsStream();
            StreamReader reader = new StreamReader(stream);
            string response = reader.ReadToEnd();
            NewsModel? newsModel = JsonSerializer.Deserialize<NewsModel>(response);
            return newsModel;

        }
    }

    /// <summary>
    /// get stories item 
    /// </summary>
    /// <param name="pageNo"></param>
    /// <param name="startPosition"></param>
    /// <param name="IsNewRequest"></param>
    /// <returns></returns>

    public async Task<NewsModelList> GetStoriesItem(int pageNo, int startPosition, int noOfRecords)
    {
        List<string> finalResult = new List<string>();
        NewsModelList newsModelList = new NewsModelList();
        newsModelList.newsModels = new List<NewsModel>();
       
        int endPosition = 10;
        int currentStartPosition = 0;
        if (pageNo > 0)
        {
            pageNo = pageNo - 1;
        }
        finalResult = await GetStories();

        finalResult = finalResult.Take(noOfRecords).ToList();
        newsModelList.RecordCount = finalResult.Count;
        currentStartPosition = (((pageNo + pageNo) * 100) + startPosition);
       
        if (finalResult != null)
        {
            string[]? Result = finalResult.Skip((currentStartPosition)).Take(endPosition).ToArray();

            foreach (var id in Result)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    if (!_memoryCache.TryGetValue(id, out NewsModel model))
                    {
                        model = GetNewsContent(id);
                        _memoryCache.Set(id, model, TimeSpan.FromDays(1));
                    }
                    newsModelList.newsModels.Add(model);
                }

            }
        }
        return newsModelList;
    }

}