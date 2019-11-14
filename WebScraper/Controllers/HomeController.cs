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
            if(request != null && !string.IsNullOrEmpty(request.SearchUrl))
            {
                var result = await _siteRepository.GetSearchResultLocationAsync(request.SearchUrl, request.Keywords, request.SearchNumberResults);
                return Json(result);
            }
            return Json(new string[] { "0" });
        }
    }
}