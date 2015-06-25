using Nancy;

namespace Frankenwiki.Host.Nancy.Features
{
    public class CategoriesModule : NancyModule
    {
        public CategoriesModule(IFrankenstore store)
        {
            Get["/categories", true] = async (o, token) =>
            {
                var categories = await store.GetAllCategoriesAsync();

                return View["categories", categories];
            };
        }
    }
}