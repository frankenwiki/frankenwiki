using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace Frakenwiki.Web.Features
{
    public class DefaultModule : NancyModule
    {
        public DefaultModule()
        {
            Get["/"] = _ => View["index"];
        }
    }
}