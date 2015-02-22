using Nancy;

namespace Frankenwiki.Example.NancyWeb.Features
{
    public class CategoryModule : NancyModule
    {
        public CategoryModule(
            IFrankenstore store)
        {
            Get["/category/{slug*}", true] = async (o, token) =>
            {
                var slug = (string) o.slug;
                var index = await store.GetIndexForCategory(slug);

                return View["category", index];
            };
        }
    }
}