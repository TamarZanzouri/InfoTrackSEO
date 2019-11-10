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
        private string url = "https://www.google.com.au/search?num=100&q=online+title+search";
        //private string fileDelimiter = "<div id=\"bottomads\"></div>";
        private string[] delimiter = { "<div><div class=\"ZINbbc xpd O9g5cc uUPGi\"><div class=\"kCrYT\">", "</div></div></div></div></div></div></div></div>" };
        private string[] hrefDelimiter = { "<a href=\"/url?q=", ">" };
        private string[] titleDelimiter = { "<div class=\"BNeawe vvjwJb AP7Wnd\">", "</div>" };
        private string[] subTitleDelimiter = { "<div class=\"BNeawe UPmit AP7Wnd\">", "</div></a>" };
        private string[] descriptionDelimiter = { "<div class=\"BNeawe s3v9rd AP7Wnd\"><div><div><div class=\"BNeawe s3v9rd AP7Wnd\">", "</div></div></div></div></div></div></div></div>" };
        private IParseHelper _parseHelper { get; set; }

        public GoogleSite(IParseHelper parseHelper)
        {
            _parseHelper = parseHelper;
        }

        public async Task<List<HtmlSection>> GetSiteStreamAsync()
        {
            var htmlSections = new List<HtmlSection>();
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            var httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(url);
            cancellationToken.Token.ThrowIfCancellationRequested();

            var response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            var theStreamReader = new StreamReader(response);
            string[] fileLines = theStreamReader.ReadToEnd().Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            foreach (var section in fileLines.Skip(1).Take(100).Select((value, i) => new { i, value }))
            {
                if (!string.IsNullOrEmpty(section.value) && section.value.Contains(hrefDelimiter.First()))
                {
                    var result = new HtmlSection();
                    result.Index = section.i + 1;
                    result.Href = _parseHelper.ParseHtmlTag(section.value, hrefDelimiter);
                    result.Title = _parseHelper.ParseHtmlTag(section.value, titleDelimiter);
                    result.SubTitle = _parseHelper.ParseHtmlTag(section.value, subTitleDelimiter);
                    result.Description = _parseHelper.ParseHtmlTag(section.value, descriptionDelimiter);
                    htmlSections.Add(result);
                }
            }
            return htmlSections.OrderBy(o => o.Index).ToList();
        }
    }
}
