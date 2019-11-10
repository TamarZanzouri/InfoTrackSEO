using System;
using System.Web.Mvc;
using WebScraper.Repository;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISiteRepository _siteRepository;

        public HomeController(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> GetResultsAsync(RequestParamsModel request)
        {
            if(request != null && !string.IsNullOrEmpty(request.url) && !string.IsNullOrEmpty(request.keywords))
            {
                var parsedKeywords = Array.ConvertAll(request.keywords.Split(','), p => p.Trim());//request.keywords.Split(',').ToArray();
                var result = await _siteRepository.GetSearchResultLocationAsync(request.url, parsedKeywords);
                return Json(result);
            }
            return Json(new string[] { "0" });
        }
    }
}