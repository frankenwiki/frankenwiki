using System.Linq;
using Frankenwiki.Example.NancyWeb.Models;
using Nancy;

namespace Frankenwiki.Example.NancyWeb.Features
{
    public class SearchModule : NancyModule
    {
        public SearchModule(IFrankensearch search)
        {
            Get["/search", true] = async (o, context) =>
            {
                if (Request.Query["phrase"] == null)
                {
                    return View["search"];
                }
                var phrase = (string)Request.Query["phrase"];
                var results = await search.SearchAsync(phrase);

                return View["search-results", new SearchResultsModel(results.Select(x => new SearchResultModel(x)), phrase)];
            };
        }
    }
}