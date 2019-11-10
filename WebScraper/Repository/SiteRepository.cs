﻿using System.Threading.Tasks;
using WebScraper.Services;
using WebScraper.Services.Interface;

namespace WebScraper.Repository
{
    public class SiteRepository : ISiteRepository
    {
        private readonly ISEOFactory _googleSiteSEO;
        private readonly ISearchHelper _searchHelper;
        public SiteRepository(ISEOFactory googleSiteSEO, ISearchHelper searchHelper)
        {
            _googleSiteSEO = googleSiteSEO;
            _searchHelper = searchHelper;
        }

        public async Task<string[]> GetSearchResultLocationAsync(string url, string[] keywords)
        {
            var resultSections = await _googleSiteSEO.GetSiteStreamAsync();
            return _searchHelper.SearchResultLocation(resultSections, url, keywords);
        }
    }
}
