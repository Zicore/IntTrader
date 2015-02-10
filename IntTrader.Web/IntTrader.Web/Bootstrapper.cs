using System;
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

        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            this.Conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("content/scripts"));
            CookieBasedSessions.Enable(pipelines);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Clear();
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("content/css", "/content/css"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("content/scripts", "/content/scripts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("content/img", "/content/img"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("content/fonts", "/content/fonts"));
        }

        protected override IRootPathProvider RootPathProvider
        {
            get { return new RootPathProvider(); }
        }
    }
}