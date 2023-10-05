using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPICore.DTO.DTOs
{
    public class Param
    {
        public int pageNo { get; set; }
        public int startPosition { get; set; }
        public bool IsNewRequest { get; set; }
        public string searchText { get; set; }
        

    }
}
