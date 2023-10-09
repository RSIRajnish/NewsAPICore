using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPICore.DTO.DTOs
{
    public class NewsModel
    {
        public string title { get; set; }
        public string url { get; set; }

    }

    public class NewsModelList
    {
        public IList <NewsModel> newsModels { get; set; }
        public int RecordCount  { get; set; }

    }
}
