using System.Threading.Tasks;

namespace WebScraper.Repository
{
    public interface ISiteRepository
    {
        Task<string[]> GetSearchResultLocationAsync(string url, string[] keywords);
    }
}
