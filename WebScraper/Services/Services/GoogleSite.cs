using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebScraper.Data;
using WebScraper.Services.Interface;

namespace WebScraper.Services
{
    public class GoogleSite : ISEOFactory
    {
        private string url = "https://www.google.com.au";
        private int numOfResults = 100;
        private string[] delimiter = { "<div><div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">", "</div></div></div></div></div></div></div></div>" };
        private string[] hrefDelimiter = { "<a href=\"/url?q=", ">" };
        private IParseHelper _parseHelper { get; set; }

        public GoogleSite(IParseHelper parseHelper)
        {
            _parseHelper = parseHelper;
        }

        public async Task<List<HtmlSection>> GetSiteStreamAsync(string keywords, int? resultsNum)
        {
            numOfResults = resultsNum ?? numOfResults;
            var searchUrl = GenerateStringUrl(numOfResults, keywords);
            var htmlSections = new List<HtmlSection>();
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            var httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(searchUrl);
            cancellationToken.Token.ThrowIfCancellationRequested();

            var response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            var theStreamReader = new StreamReader(response);
            string[] fileLines = theStreamReader.ReadToEnd().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            foreach (var section in fileLines.Skip(1).Take(numOfResults).Select((value, i) => new { i, value }))
            {
                if (!string.IsNullOrEmpty(section.value) && section.value.Contains(hrefDelimiter.First()))
                {
                    var result = new HtmlSection();
                    result.Index = section.i + 1;
                    result.Href = _parseHelper.ParseHtmlTag(section.value, hrefDelimiter);
                    htmlSections.Add(result);
                }
            }
            return htmlSections.OrderBy(o => o.Index).ToList();
        }

        private string GenerateStringUrl(int ResultsNum, string keywords)
        {
            var searchUrl = $"{url}/search?num={ResultsNum}";
            if (!string.IsNullOrEmpty(keywords))
            {
                var parsedKeywords = keywords.Replace(' ', '+');
                searchUrl = $"{searchUrl}&q={parsedKeywords}"; 
            }
            return searchUrl;
        }
    }
}
