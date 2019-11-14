using System;
namespace WebScraper.Models
{
    public class RequestParamsModel
    {
        public string SearchUrl { get; set; }
        public string Keywords { get; set; }
        public int? SearchNumberResults { get; set; }
    }
}
