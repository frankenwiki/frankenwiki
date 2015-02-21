using Autofac;

namespace Frankenwiki.Example.NancyWeb.Plumbing
{
    public static class IoC
    {
        public static IContainer BoomShakalaka()
        {
            var builder = new ContainerBuilder();

            var assemblies = new[]
            {
                typeof (WebAutofacModule).Assembly,
            };

            builder.RegisterAssemblyModules(assemblies);

            return builder.Build();
        }
    }
}