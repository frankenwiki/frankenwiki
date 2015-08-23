using System.Reflection;
using Autofac;
using Frankenwiki.Host.Nancy.Features;

namespace Frankenwiki.Example.Lucene.Plumbing
{
    public class IoC
    {
        public static Assembly[] Assemblies =
       {
                typeof (WebAutofacModule).Assembly,
                typeof(IndexModule).Assembly
            };

        public static IContainer BoomShakalaka()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assemblies);

            return builder.Build();
        }
    }
}