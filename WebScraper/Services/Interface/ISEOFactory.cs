using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebScraper.Data;

namespace WebScraper.Services
{
    public interface ISEOFactory
    {
        Task<List<HtmlSection>> GetSiteStreamAsync();
    }
}
