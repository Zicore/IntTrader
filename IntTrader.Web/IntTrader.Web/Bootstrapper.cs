using System;
using System.Globalization;
using System.Threading;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Session;
using Nancy.TinyIoc;

namespace IntTrader.Web
{
    using Nancy;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public Bootstrapper()
        {
            //WebService.Initialize();
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            StaticConfiguration.Caching.EnableRuntimeViewUpdates = true;
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Clear();
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("content"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("scripts"));
        }

        protected override IRootPathProvider RootPathProvider
        {
            get { return new RootPathProvider(); }
        }

    }
}