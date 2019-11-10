using System.Collections.Generic;
using WebScraper.Data;

namespace WebScraper.Services.Interface
{
    public interface ISearchHelper
    {
        string[] SearchResultLocation(List<HtmlSection> htmlSections, string url, string[] keywords);
    }
}
