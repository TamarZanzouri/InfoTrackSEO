using System;
using System.Collections.Generic;
using System.Linq;
using WebScraper.Data;
using WebScraper.Services.Interface;

namespace WebScraper.Services.Services
{
    public class SearchHelper : ISearchHelper
    {
        public string[] SearchResultLocation(List<HtmlSection> htmlSections, string url, string[] keywords)
        {
            var indexes = new List<string>();

            indexes.AddRange(htmlSections.Where(x => keywords.Any(s => x.Description.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1)).Select(s => s.Index.ToString()).ToList());
            indexes.AddRange(htmlSections.Where(x => keywords.Any(s => x.SubTitle.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1)).Select(s => s.Index.ToString()).ToList());
            indexes.AddRange(htmlSections.Where(x => keywords.Any(s => x.Title.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1)).Select(s => s.Index.ToString()).ToList());
            indexes.AddRange(htmlSections.Where(x => x.Href.Contains(url) || url.Contains(x.Href)).Select(s => s.Index.ToString()).ToList());
            return indexes.Any() ? indexes.GroupBy(g => g).Select(s => s.First()).ToArray() : new string[] { "0" };
        }
    }
}
