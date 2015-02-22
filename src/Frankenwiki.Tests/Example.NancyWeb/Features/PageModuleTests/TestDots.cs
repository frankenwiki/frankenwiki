using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using NSubstitute;
using NSubstitute.Routing.Handlers;
using Shouldly;
using Xunit;

namespace Frankenwiki.Tests.Example.NancyWeb.Features.PageModuleTests
{
    public class TestDots
    {
        [Fact]
        public void TestWithoutDots()
        {
            var module = GetModule();
            var browser = GetBrowser(module);

            var result = browser.Get("/echo/one-two-three");

            result.Body.AsString().ShouldBe("one-two-three");
        }

        [Fact]
        public void TestWithSomeDots()
        {
            var module = GetModule();
            var browser = GetBrowser(module);

            var result = browser.Get("/echo/four.jpg");

            result.Body.AsString().ShouldBe("four.jpg");
        }

        private static Browser GetBrowser(ConfigurableNancyModule module)
        {
            var browser = new Browser(with => with.Module(module));
            return browser;
        }

        private static ConfigurableNancyModule GetModule()
        {
            var module = new ConfigurableNancyModule(with => with.Get("/echo/{slug*}", (o, nancyModule) => o.slug));
            return module;
        }
    }

    public class TestDots2
    {
        [Fact]
        public void TestWithoutDots()
        {
            var module = new ConfigurableNancyModule(with => with.Get("/echo/{slug*}", (o, nancyModule) => o.slug));
            var browser = new Browser(with => with.Module(module));

            Assert.Equal(browser.Get("/echo/foo").Body.AsString(), "foo");
            Assert.Equal(browser.Get("/echo/foo/bar").Body.AsString(), "foo/bar");
            Assert.Equal(browser.Get("/echo/foo.bar/gaz").Body.AsString(), "foo.bar/gaz");
            Assert.Equal(browser.Get("/echo/foo.bar").Body.AsString(), "foo.bar");
        }
    }

}
